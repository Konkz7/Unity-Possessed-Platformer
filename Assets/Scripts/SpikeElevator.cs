using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeElevator : MonoBehaviour
{
    private bool startMoving = false;
    public Transform T;
    public float speed;
    public Transform initial;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (startMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, T.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        startMoving = true;
    }

    public void Reset()
    {
        startMoving = false;
        transform.position = initial.position;
        Debug.Log("HEEY");
    }
}
