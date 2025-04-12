using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charmFollow : MonoBehaviour
{
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
       
        target = GameObject.Find("CharmPoint").transform;
        transform.position = Vector2.MoveTowards(transform.position, target.position, 8 * Time.deltaTime);
    }
}
