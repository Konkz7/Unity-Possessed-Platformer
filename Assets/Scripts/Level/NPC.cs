using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{

    private NPC instance;

    
    public GameObject transObject;
    public float transitionTime = 1f;
    [SerializeField] private GameObject panel;
    [SerializeField] private Text dialogueText;
    [SerializeField] private string name;
    [SerializeField] private Transform npc_Location;
    [SerializeField] private Sprite avatar;
    [SerializeField] private string[] dialouge;


    private int index;
    private bool isAlive = false;
    private GameObject hand;

    [SerializeField] private GameObject virtualCamera;
    



    PlayerMovement playerMovement;
    [SerializeField] private GameObject contButton;
    [SerializeField] private float wordspeed;
    [SerializeField] private bool playerIsClose;
    [SerializeField] private bool limit;

 
    [SerializeField]
    Transform point;
    [SerializeField]
    float speed;
    
  

    void Start()
    {
        
      
        playerMovement = PlayerMovement.instance;
        dialogueText.text = "";
        hand = GameObject.Find("hand");
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {


        if(playerIsClose && !limit)
        {
            panel.transform.Find("Image").gameObject.GetComponent<Image>().sprite = avatar;
            panel.transform.Find("Name").gameObject.GetComponent<Text>().text = name;
            virtualCamera = GameObject.Find("Fcamera");
            virtualCamera.gameObject.GetComponent<CinemachineVirtualCamera>().Follow = npc_Location;
            hand.GetComponent<HandController>().enabled = false;

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

        foreach(char letter in dialouge[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordspeed);
        }
    }

    IEnumerator Moving()
    {
        while (Vector2.Distance(npc_Location.position, point.position) > 0.02f)
        {
           yield return npc_Location.position = Vector2.MoveTowards(npc_Location.position, point.position, speed * Time.deltaTime);
        }

    }

    IEnumerator sceneTransition()
    {
 
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("BossScene");

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

                if (instance.CompareTag("transition"))
                {
                    transObject.SetActive(true);
                    StartCoroutine(sceneTransition());  
                }

                zeroText();
                playerMovement.resume();
                virtualCamera.gameObject.GetComponent<CinemachineVirtualCamera>().Follow = GameObject.Find("player").transform;
                StartCoroutine(Moving());
                gameObject.transform.GetComponent<BoxCollider2D>().enabled = false;
                hand.GetComponent<HandController>().enabled = true;
                isAlive = false;

               



            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") )
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
