using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{

 
    public LayerMask player;
    public GameObject attackPoint;
    public float radius;
    private SpecialController eye;
    



    // Start is called before the first frame update
    void Start()
    {
        eye = GameObject.FindFirstObjectByType<SpecialController>();

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void attack()
    {
        
        Collider2D[] hitbox = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, player);
        if (hitbox.Length > 0)
        {
            HealthController bar = GameObject.FindFirstObjectByType<HealthController>();

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
                bar.doDamage(1, transform, 8000f,true);

            }

        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }

    public void die()
    {
        PortalGFX.recharged = true;
        Destroy(this.transform.parent.gameObject);
    }
}
