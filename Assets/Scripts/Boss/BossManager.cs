using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    public GameObject Boss;
    public GameObject player;
    public GameObject endPanel;
    public GameObject restartPanel;
    public HealthController bar;



    // Start is called before the first frame update
    void Start()
    {
        bar = GameObject.FindFirstObjectByType<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Boss.GetComponent<EnemyHealth>().health <= 0)
        {
            StartCoroutine(end());
        }else if (bar.health == 0) { 
        
            player.SetActive(false);
            restartPanel.SetActive(true);
            StartCoroutine(restart());
            
        }


        
    }

    private IEnumerator restart()
    {
        yield return new WaitForSeconds(5);

       
        SceneManager.LoadScene("BossScene");

    }

    private IEnumerator end()
    {
        yield return new WaitForSeconds(3);

        endPanel.SetActive(true);
        player.GetComponent<PlayerMovement>().enabled = false ;
        

    }
}
