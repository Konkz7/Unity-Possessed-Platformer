using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BeamScript : MonoBehaviour
{

    
    public Transform beamPoint;
    public float beamSizex;
    public float beamSizey;
    public AudioSource beamSound;

    private HealthController bar;
    
    


    public LayerMask player;
    private Vector2 rect;
    // Start is called before the first frame update
    void Start()
    {
        rect = new Vector2(beamSizex, beamSizey);
     
        bar = GameObject.FindFirstObjectByType<HealthController>();
        beamSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void beam()
    {

        Collider2D[] hitbox = Physics2D.OverlapBoxAll(beamPoint.transform.position, rect, 0,  player);
        if (hitbox.Length > 0)
        {
            Debug.Log("hit by beam");
            
            {
                bar.doDamage(1, transform, 8000f, true);

            }
           
        }

    }

    public void beamEnd()
    {
        BossController.cooling = true;
        Destroy(gameObject.transform.parent.gameObject);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(beamPoint.transform.position, rect);
    }
}
