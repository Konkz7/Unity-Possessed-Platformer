using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform target;
    public float speed;
    Rigidbody2D rb;

    Vector2 moveDirection;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();  
     
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180;
            rb.rotation = angle;
            moveDirection = direction;

            if (direction.x > 0)
            {
                
                GetComponentInChildren<SpriteRenderer>().flipY = true;

            }
            else
            {
              
                GetComponentInChildren<SpriteRenderer>().flipY = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if(target)
        {
            speed += 0.02f;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;    
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
