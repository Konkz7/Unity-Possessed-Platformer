using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class BossController : MonoBehaviour
{
    public static bool cooling = false;
    public Animator animator;
    public Transform returnPoint;
    public Transform slimePoint;
    
    public GameObject beamPrefab;
    public GameObject slimePrefab;
    public AudioSource beamSound;



    public LayerMask player;

    private Transform target;
    private bool startingAttack = false;
    private bool midAttack = true;
    private int chosenMove = 0;
    private bool flipped = false;

    
  

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(Cooldown());
    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetBool("IsDying"))
        {
            Vector3 direction = (target.position - transform.position).normalized;

            if (direction.x > 0)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
                //GetComponentInChildren<SpriteRenderer>().flipX = true;
                //flipped = true;
            }
            else
            {
                gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);

                //GetComponentInChildren<SpriteRenderer>().flipX = false;
                //flipped = false;
            }


            if (cooling)
            {
                transform.position = Vector2.MoveTowards(transform.position, returnPoint.position, 10 * Time.deltaTime);
                StartCoroutine(Cooldown());
            }


            if (!midAttack)
            {
                int attackChoice = chosenMove;
                if (!startingAttack)
                {
                    attackChoice = Random.Range(0, 3);
                    chosenMove = attackChoice;
                }


                if (attackChoice == 0)
                {

                    Vector2 targety = new Vector2(transform.position.x, target.position.y - 3);
                    transform.position = Vector2.MoveTowards(transform.position, targety, 10 * Time.deltaTime);


                    if (!startingAttack)
                    {
                        StartCoroutine(beaming());
                    }

                }
                else if (attackChoice == 1)
                {
                    if (!startingAttack)
                    {
                        StartCoroutine(casting());
                    }
                }
                else if (attackChoice == 2)
                {

                    if (Vector2.Distance(transform.position, target.position) > 2f)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, target.position, 10 * Time.deltaTime);
                    }


                    if (!startingAttack)
                    {
                        StartCoroutine(cleaving());
                    }



                }

            }
        }


    }

    private IEnumerator beaming()
    {
        startingAttack = true;
        beamSound.Play();
        yield return new WaitForSeconds(3);
        startingAttack = false;
        animator.SetTrigger("IsBeaming");
        
        /*
        if (flipped) { 
            beamPrefab.transform.position = new Vector2(-beamPrefab.transform.position.x, beamPrefab.transform.position.y);
            beamPrefab.transform.localScale = new Vector3(-1.75f, 1.51f,1);
        }
        */
        Instantiate(beamPrefab,gameObject.transform);

        /*
        if (flipped)
        {
            beamPrefab.transform.position = new Vector2(-beamPrefab.transform.position.x, beamPrefab.transform.position.y);
            beamPrefab.transform.localScale = new Vector3(1.75f, 1.51f, 1);
        }
        */
    
        midAttack = true;
       
    }

    private IEnumerator casting()
    {

        startingAttack = true;
        int slimeCount = Random.Range(1, 3);
        for (int i = 0; i < slimeCount; i++)
        {
            animator.SetTrigger("IsCasting");

            /*
            if (flipped)
            {
                slimePoint.transform.position = new Vector2(-slimePoint.transform.position.x, slimePoint.transform.position.y);
                slimePrefab.transform.localScale = new Vector3(-5f, 5f, 1);
            }
            */
            Instantiate(slimePrefab);
            slimePrefab.transform.position = slimePoint.transform.position;

            /*
            if (flipped)
            {
                slimePoint.transform.position = new Vector2(-slimePoint.transform.position.x, slimePoint.transform.position.y);
                slimePrefab.transform.localScale = new Vector3(5f, 5f, 1);
            }
            */
            yield return new WaitForSeconds(1);

        }
        startingAttack = false;


        

    }

    private IEnumerator cleaving()
    {

        startingAttack = true;
        yield return new WaitForSeconds(1.5f);
        startingAttack = false;
        animator.SetTrigger("IsCleaving");
        
        
      
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(3f);
        midAttack = false;
        cooling = false;

    }

    







}
