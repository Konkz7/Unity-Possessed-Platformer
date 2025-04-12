using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1 : MonoBehaviour
{

    public GameObject enemy;
    public Transform T;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy.activeInHierarchy)
        {
            transform.position = Vector2.MoveTowards(transform.position, T.position, 5 * Time.deltaTime);
        }
    }
}
