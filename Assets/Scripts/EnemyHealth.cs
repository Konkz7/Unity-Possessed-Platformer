using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float health;
    public float currentHealth;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if(health < currentHealth)
        {
            currentHealth = health;
            animator.SetTrigger("Attacked");
        }

        if(health <= 0 ) {

            animator.SetBool("IsDying", true);
            
        }


    }

    
}
