using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject player;
    public Transform[] checkpoints;
    public GameObject elevator;



    public GameObject shopPanel;
    public GameObject[] Items;
    public GameObject Item;
    public GameObject buyButton;
    public GameObject equipButton;
    public GameObject charm;
    public Text coinDisplay;
    public int coinCounter = 0;

  


    private Transform trueCheckpoint;
    private int currentItem = 0;
    List<int> inventory = new List<int>();


    private HandController hand;


    // Start is called before the first frame update
    void Start()
    {
        trueCheckpoint = checkpoints[0];
        Item = Items[currentItem];
        hand = GameObject.FindFirstObjectByType<HandController>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthController bar = GameObject.FindFirstObjectByType<HealthController>();

        if (bar.health <= 0)
        {
            elevator.GetComponent<SpikeElevator>().Reset();
            player.transform.position = trueCheckpoint.transform.position;
            bar.health = bar.maxHealth;
            bar.updateHealth();
        }

        foreach (Transform t in checkpoints) {

            if (Vector2.Distance(player.transform.position, t.position) < 2f) {
            
                   trueCheckpoint.position = t.position;
            }

        }

        if(Input.GetKeyDown(KeyCode.Y))
        {
            if (shopPanel.activeInHierarchy)
            {
                closeShop();
                hand.enabled = true;
            }
            else
            {
                openShop();
                hand.enabled = false;


            }
        }
        if (coinCounter >= currentItem + 1 && !inventory.Contains(currentItem))
        {
            buyButton.SetActive(true);
        }
        else
        {
            buyButton.SetActive(false);
        }


        if (!inventory.Contains(currentItem))
        {
            equipButton.SetActive(false);
        }
        else
        {
            equipButton.SetActive(true);

        }
    }

    public void incrementCoin()
    {
        coinCounter++;
        coinDisplay.text = "x" + coinCounter;
    }

    public void openShop()
    {
        shopPanel.SetActive(true);
        
    }

    public void closeShop()
    {
        shopPanel.SetActive(false);
    }

    public void nextItem()
    {
        Items[currentItem].SetActive(false);
        currentItem++;
        if (currentItem > Items.Length - 1)
        {
            currentItem = 0;
        }
        Item = Items[currentItem];
        Items[currentItem].SetActive(true);

    }

    public void prevItem()
    {
        Items[currentItem].SetActive(false);
        currentItem--;
        if (currentItem < 0)
        {
            currentItem = Items.Length - 1;
        }
        Item = Items[currentItem];
        Items[currentItem].SetActive(true);


    }

    public void buy()
    {
       
        coinCounter -= currentItem + 1;
        coinDisplay.text = "x" + coinCounter;
        inventory.Add(currentItem);
        
    }

    public void equip()
    {
        charm.GetComponent<SpriteRenderer>().sprite = Items[currentItem].GetComponentInChildren<Image>().sprite;
    }
}
