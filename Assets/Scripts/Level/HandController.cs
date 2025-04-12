using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{

    private bool apexReached = false;
    private bool soundPlayed = false;
    private bool hasSmashed = false;
    private BoxCollider2D hitbox;
    private Vector3 destSmashed;
    private Vector3 initRise;

    public float speed;
    public float distanceMoved;
    public Animator animator;
    public SpecialController eye;

    public AudioSource air;
    public AudioSource fall;

    private Color temp;
    // Start is called before the first frame update
    void Start()
    {
        
        temp = GetComponent<SpriteRenderer>().color;
        hitbox = GetComponent<BoxCollider2D>();  
    }

    // Update is called once per frame
    void Update()
    {


        if (eye.soulTally >= 2)
        {
            if (Input.GetMouseButtonDown(0) && hasSmashed == false)
            {
                hasSmashed = true;
                initRise = transform.position + new Vector3(0f, distanceMoved, 0f);
                destSmashed = transform.position + new Vector3(0f, -distanceMoved, 0f);
            }

            if (hasSmashed)
            {
                gameObject.GetComponent<MouseFollow>().enabled = false;

                if (apexReached == false)
                {
                    if (!soundPlayed)
                    {
                        air.Play();
                        soundPlayed = true;
                    }
                    transform.position = Vector2.MoveTowards(transform.position, initRise, speed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, initRise) < 0.02f)
                    {
                        apexReached = true;
                        Debug.Log("Apex");
                    }
                }
                else
                {

                    if (Vector2.Distance(transform.position, destSmashed) < 0.02f)
                    {
                        animator.SetBool("IsFist", false);
                        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
                        hasSmashed = false;
                        apexReached = false;
                        hitbox.enabled = false;
                        gameObject.GetComponent<MouseFollow>().enabled = true;
                        gameObject.GetComponent<SpriteRenderer>().color = temp;
                        eye.useSoul();

                    }
                    else
                    {
                        if (soundPlayed)
                        {
                            fall.Play();
                            soundPlayed = false;
                        }
                        animator.SetBool("IsFist", true);
                        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10;
                        hitbox.enabled = true;
                        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                        transform.position = Vector2.MoveTowards(transform.position, destSmashed, speed * Time.deltaTime * 6);
                    }
                }

            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) 
        {
            collision.GetComponent<EnemyHealth>().health -= 5;
        }
    }

   



}
