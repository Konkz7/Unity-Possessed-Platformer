using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossIntro : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject Boss;
    [SerializeField] private Text dialogueText;
    [SerializeField] private string name;
    

    [SerializeField] private Sprite avatar;
    [SerializeField] private string[] dialouge;


    private int index;
    private bool isAlive = false;


    PlayerMovement playerMovement;
    [SerializeField] private GameObject contButton;
    [SerializeField] private float wordspeed;
    [SerializeField] private bool playerIsClose;
    [SerializeField] private bool limit;


    [SerializeField]
    Transform entrancePoint;
    [SerializeField]
    float speed;

    private GameObject hand;
    private Transform bossLocation;




    void Start()
    {


        playerMovement = PlayerMovement.instance;
        dialogueText.text = "";
        hand = GameObject.Find("hand");
        bossLocation = Boss.transform;
       
    }
    // Update is called once per frame
    void Update()
    {


        if (playerIsClose && !limit)
        {
            panel.transform.Find("Image").gameObject.GetComponent<Image>().sprite = avatar;
            panel.transform.Find("Name").gameObject.GetComponent<Text>().text = name;
       
          

            if (panel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {

                panel.SetActive(true);
                StartCoroutine(Typing());
            }
            limit = true;
        }

        if (index < dialouge.Length)
        {

            if (dialogueText.text == dialouge[index])
            {
                contButton.SetActive(true);
            }
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        panel.SetActive(false);
    }

    IEnumerator Typing()
    {

        foreach (char letter in dialouge[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordspeed);
        }
    }

    IEnumerator Moving()
    {
        while (Vector2.Distance(bossLocation.position, entrancePoint.position) > 0.02f)
        {
            yield return bossLocation.position = Vector2.MoveTowards(bossLocation.position, entrancePoint.position, speed * Time.deltaTime);
        }

    }

    public void NextLine()
    {
        if (isAlive)
        {
            contButton.SetActive(false);
            if (index < dialouge.Length - 1)
            {
                index++;
                dialogueText.text = "";
                StartCoroutine(Typing());
            }
            else
            {

                zeroText();
                playerMovement.resume();
                
                StartCoroutine(Moving());
                gameObject.transform.GetComponent<BoxCollider2D>().enabled = false;
                hand.GetComponent<MouseFollow>().enabled = true ;   
                Boss.SetActive(true);
                isAlive = false;



            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            playerMovement.halt();
            isAlive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
            limit = false;
        }
    }
}
