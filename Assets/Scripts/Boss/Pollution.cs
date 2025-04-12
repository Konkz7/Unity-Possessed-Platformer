using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Pollution : MonoBehaviour
{
    [SerializeField]
    GameObject[] hazards;
    [SerializeField]
    Vector2 spawnValues;
    [SerializeField]
    int hazardCount;
    [SerializeField]
    float spawnWait;
    [SerializeField]
    float waveWait;

    public GameObject boss;
  
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWaves());
        
    }

    // Update is called once per frame
    void Update()
    {
        
        

    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            if (boss.activeInHierarchy)
            {
                for (int i = 0; i < hazardCount; i++)
                {
                    Vector2 spawnPosition = new Vector2(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y);
                    Quaternion spawnRotation = Quaternion.identity;
                    GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                    GameObject litter = Instantiate(hazard, spawnPosition, spawnRotation);
                    litter.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(0f, 100f);
                    yield return new WaitForSeconds(spawnWait);
                }
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
}
