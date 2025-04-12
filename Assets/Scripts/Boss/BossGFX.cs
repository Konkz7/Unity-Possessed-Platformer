using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BossGFX : MonoBehaviour
{

    public GameObject attackPoint;
    public float radius;
    public LayerMask player;
    private HealthController bar;

    // Start is called before the first frame update
    void Start()
    {
        bar = GameObject.FindFirstObjectByType<HealthController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cleave()
    {
        Collider2D[] hitbox = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, player);
        if (hitbox.Length > 0)
        {
           
            if (hitbox[0].gameObject.GetComponent<PlayerMovement>().isParrying)
            {
                bar.doDamage(-1, gameObject.transform.parent.transform, 8000f, true);
                    
            }
            else
            {
                bar.doDamage(1, gameObject.transform.parent.transform, 8000f, true);

            }
            
        }
    }
    public void die()
    {
        Destroy(GameObject.FindFirstObjectByType<Pollution>());
        gameObject.transform.parent.gameObject.SetActive(false);    
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
}
