
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAware : MonoBehaviour
{

    public float proximity;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(player.transform.position,gameObject.transform.position);
        if (distance > proximity)
        {
            gameObject.GetComponent<EnemyAI>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<EnemyAI>().enabled = true;
        }
    }
}
