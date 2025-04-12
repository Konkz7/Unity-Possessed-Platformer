using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public Transform gfx;
    public float speed = 200f;
    public float distance = 1f;
    public float jump_force;

    private Rigidbody2D rb;
    private bool grounded = false;
    private Animator animator;
    private bool stacked = false;





    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       animator = GetComponentInChildren<Animator>();
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(rb.position, target.position) > distance)
        {
            if (target.position.x > gameObject.transform.position.x)
            {
                rb.AddForce(new Vector2(speed * Time.deltaTime, 0));
                gfx.localScale = new Vector3(-1f, 1f, 0f);

            }
            else
            {
                rb.AddForce(new Vector2(-(speed * Time.deltaTime), 0));
                gfx.localScale = new Vector3(1f, 1f, 0f);

            }

            
            if (target.position.y > gameObject.transform.position.y + 2.5 && grounded && !stacked) 
            {
                rb.AddForce(new Vector2(0,jump_force * Time.deltaTime) , ForceMode2D.Impulse);
               
                animator.SetBool("IsJumping", true);

            }


        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            stacked = true;
            Debug.Log("stacked");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            stacked = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 3)
        {
            grounded = true;
            animator.SetBool("IsJumping", false);
            animator.SetBool("InAir", false);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            grounded = false;
            animator.SetBool("InAir", true);
        }

    }
}
