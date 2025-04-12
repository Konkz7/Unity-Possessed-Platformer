using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] private int Dmgdone;

    [SerializeField] private float DmgForce;

    [SerializeField] private HealthController healthController;

    private bool isStillTouching = false;
   


    private void Start()
    {
        if (healthController == null) { 
            healthController = GameObject.FindFirstObjectByType<HealthController>();
        }
    }
    private void Update()
    {
        if (isStillTouching)
        {
            healthController.doDamage(Dmgdone, transform, DmgForce,false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
      
            if (other.transform.CompareTag("Player"))
            {
                healthController.doDamage(Dmgdone, transform, DmgForce, false);
                isStillTouching = true;

            }

    
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        
        if (other.transform.CompareTag("Player"))
        {
            isStillTouching = false;

        }

        
    }




}
