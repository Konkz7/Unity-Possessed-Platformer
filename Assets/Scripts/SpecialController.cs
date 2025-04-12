using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialController : MonoBehaviour
{
    public float soulTally = 0;
    
    
    [SerializeField] private Image[] eyes;
    public Sprite open;
    public Sprite close;
   

    
    // Start is called before the first frame update
    void Start()
    {
        updateTally();
    }

    // Update is called once per frame
    void Update()
    {
        if(soulTally > 6)
        {
            soulTally = 6;
        }
        
    }

    public void updateTally()
    {
        int breaker = Mathf.FloorToInt( soulTally / 2);
        for (int i = 0; i < eyes.Length; i++)
        {
            if (i < breaker)
            {
                eyes[i].sprite = open;
            }
            else
            {
                eyes[i].sprite = close;
            }
        }

    }

    public void takeSoul(int souls)
    {
        soulTally += souls;
        updateTally();
    }

    public void useSoul()
    {
        soulTally -= 2;
        updateTally();
    }

}
