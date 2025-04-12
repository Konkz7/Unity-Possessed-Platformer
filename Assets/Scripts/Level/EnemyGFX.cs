using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyGFX : MonoBehaviour
{

    public Animator controller;
    public float jumpDetection = 0f;
    public float jumpForce =0f;
    public GameObject enemy;
    Vector3 pos, velocity;
    




    private CircleCollider2D groundCheck;

    public GameObject attackPoint;
    public float radius;
    public LayerMask player;
    private SpecialController eye;
    private HealthController bar;

    private bool grounded = true;

    void Awake()
    {
        pos = transform.position;
    }

    private void Start()
    {
        groundCheck = GetComponent<CircleCollider2D>();
        eye = GameObject.FindFirstObjectByType<SpecialController>();
        bar = GameObject.FindFirstObjectByType<HealthController>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        velocity = (transform.position - pos) / Time.deltaTime;
        pos = transform.position;
        controller.SetFloat("Speed", velocity.magnitude);
  

    }

    private void Update()
    {
        Collider2D[] sensed = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, player);
        if (sensed.Length > 0)
        {
            controller.SetTrigger("IsAttacking");
        }
    }

    public void attack()
    {
        Collider2D[] hitbox = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, player);
        if (hitbox.Length > 0)
        {
            if (hitbox[0].gameObject.GetComponent<PlayerMovement>().canMove)
            {
                if (hitbox[0].gameObject.GetComponent<PlayerMovement>().isParrying)
                {
                    if (hitbox[0].gameObject.GetComponent<PlayerMovement>().isParryingPerfect)
                    {
                        bar.doDamage(-1, transform, 8000f, true);
                        eye.takeSoul(1);
                    }
                    else
                    {
                        bar.doDamage(0, transform, 8000f, true);
                        eye.takeSoul(1);
                    }
                }
                else
                {
                    bar.doDamage(1, enemy.transform, 8000f, true);

                }
            }

        }

    }





    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }

    

    public void die()
    {
        
        eye.takeSoul(1);
        enemy.SetActive(false);
    }
}
