using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGFX : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public Animator animator;
    public GameObject portal;
    public AudioSource audio;
    public static bool recharged = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (recharged)
        {
            if (Vector2.Distance(gameObject.transform.position, player.position) > 15 && Vector2.Distance(gameObject.transform.position, player.position) < 30)
            {
                audio.Play();
                animator.SetTrigger("IsPortaling");
                GameObject tmp = Instantiate<GameObject>(portal);
                tmp.transform.position = player.position;

                recharged = false;
                
            }
        }
    }

}
