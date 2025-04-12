using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;


public class HealthController : MonoBehaviour
{
    public int health;
    public int maxHealth;
    [SerializeField] private Image[] hearts;
    public GameObject player;
    private bool invincible = false;

    public AudioSource dmg;
    public AudioSource parry;


    void Start()
    {
        updateHealth();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateHealth()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].color = Color.white;
            }
            else
            {
                hearts[i].color = Color.black;
            }
        }

        if (health < 0) { 
            health = 0;
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void doDamage(int damage,Transform dealer, float knockback, bool yFactor)
    {
        Vector2 direction = (player.transform.position - dealer.position).normalized;
        Vector2 force = direction * knockback * Time.deltaTime;
        if(yFactor)
        {
            force.y = 0;
        }

        if (!invincible)
        {
            health -= damage;
            invincible = true;

            if (damage > 0)
            {
                dmg.Play();
                player.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                StartCoroutine(Phasing());
            }
            else
            {
                parry.Play();
                StartCoroutine(Chilling()); 
            }
            updateHealth();
        }
    }

    IEnumerator Phasing()
    {
        for (int i = 0; i < 20; i++)
        {
            if(i == 19)
            {
                yield return player.GetComponent<SpriteRenderer>().color = Color.clear;

            }
            if (player.GetComponent<SpriteRenderer>().color == Color.clear)
            {
                player.GetComponent<SpriteRenderer>().color = Color.white;
            }
            else
            {
                player.GetComponent<SpriteRenderer>().color = Color.clear;
            }
            yield return new WaitForSeconds(0.1f);
        }

        invincible = false;


    }

    IEnumerator Chilling()
    {
        
        yield return new WaitForSeconds(2);
        

        invincible = false;


    }
}
