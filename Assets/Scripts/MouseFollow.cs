using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 0f;
    [Range (0,1)] public float Smodifier = 0f;
    private float slow = 0f ;

    private Vector2 cursorPos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = new Vector2 (cursorPos.x , cursorPos.y);



        if (Mathf.Abs(cursorPos.x - transform.position.x) < 2 && Mathf.Abs(cursorPos.y - transform.position.y) < 2)
        {
            slow = Smodifier;
        }else if (Mathf.Abs(cursorPos.x - transform.position.x) == 0 && Mathf.Abs(cursorPos.y - transform.position.y) == 0)
        {
            slow = 0;
        }
        else
        {
            slow = 1;

        }

    }
    public void FixedUpdate()
    {
        

        transform.position = Vector2.MoveTowards(transform.position, cursorPos, speed * slow * Time.deltaTime);


    }
}
