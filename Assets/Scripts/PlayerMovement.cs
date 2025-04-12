using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController controller;

    public Animator animator;   

    float h_move = 0f;
    public float speed = 0f;
    private float temp_speed;
    private float temp_jf;
    public GameObject attackPoint;
    public float radius;
    public LayerMask enemies;


    bool jump = false;
    bool crouch = false;
    private bool hasAkicked = false;
    public bool canMove = true;   
    public bool isParrying = false;
    public bool isParryingPerfect = false;

    public static PlayerMovement instance;

    public AudioSource hit;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        temp_speed = speed;
        temp_jf = controller.m_JumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        
        h_move = Input.GetAxisRaw("Horizontal") * speed;  // right and D = 1 and left and a = -1
        animator.SetFloat("Speed", Mathf.Abs(h_move));


        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            //animator.SetBool("IsJumping", true);
        } 
        
        if (Input.GetButtonDown("Jump") && String.Compare(gameObject.GetComponent<SpriteRenderer>().sprite.name, "konk_sitting") == 0)
        {
            jump = true;
            controller.m_JumpForce = temp_jf + 200f;
        }

        //animator.SetBool("IsJumping", false);
        if (controller.m_Grounded)
        {
            if (Input.GetButtonDown("Sit"))
            {
                crouch = true;
                animator.SetBool("IsCrouching", true);
                

            }
            if (Input.GetButtonUp("Sit"))
            {
                crouch = false;
                animator.SetBool("IsCrouching", false);
            }

        }
        if (canMove)
        {
            if (Input.GetButtonDown("Fire1") && animator.GetBool("IsParrying") == false)
            {

                if (!hasAkicked) { animator.SetBool("IsKicking", true); }

                if (!controller.m_Grounded)
                {
                    hasAkicked = true;
            
                }

            }

            if (Input.GetButtonDown("Fire2") && animator.GetBool("IsKicking") == false)
            {

                animator.SetBool("IsParrying", true);
                speed = 0f;

            }
        }

       

       
        
       
       
    }

    public void parryStart()
    {
        isParrying = true;
        isParryingPerfect = true;
    }

    public void parryEnd()
    {
        isParrying = false;
    }

    public void parryPerfectEnd()
    {
        isParryingPerfect = false;

    }






    public void OnLanding()
    {
        
        animator.SetBool("InAir", false);
        animator.SetBool("IsCrouching", false);
        crouch = false;
        hasAkicked = false;
        controller.m_JumpForce =temp_jf;
       

    }

    public void halt()
    {
        speed = 0f;
        canMove = false;
    }

    public void resume()
    {
        speed = temp_speed;
        canMove = true;
    }

    public void kickMover()
    {

        if (controller.m_FacingRight)
        {
            //gameObject.transform.Translate(new Vector3(0.5f,0,0));
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(5000f, 0f, 0f) * Time.fixedDeltaTime, ForceMode2D.Impulse);

        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(-5000f, 0f, 0f) * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }


    }


    public void attack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies); 
        foreach(Collider2D e in enemy) {

            hit.Play();
            e.GetComponent<EnemyHealth>().health -= 1f;
        
        }
    }

    public void kickStopper()
    {
        animator.SetBool("IsKicking", false);
    }

    
    public void parryStopper()
    {
        speed = temp_speed;
        animator.SetBool("IsParrying", false);
       
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position,radius);
    }
    private void FixedUpdate()
    {  
        
        controller.Move(h_move * Time.fixedDeltaTime, crouch, jump); // Time.fixeddeltatime make sure the movement is consistent no matter how many times the function is called
        jump = false;
    }
}
