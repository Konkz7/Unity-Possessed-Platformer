using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallThrough : MonoBehaviour
{

    private Collider2D _collider;
    private bool playerOnPLatform;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerOnPLatform && Input.GetAxisRaw("Vertical") < 0)
        {
            _collider.enabled = false;
            StartCoroutine(EnableCollider());
        }
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        _collider.enabled = true;
    }

    private void SetPlayerOnPlatform(Collision2D other, bool value)
    {
        if (other.transform.CompareTag("Player"))
        {

            playerOnPLatform = value;

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        SetPlayerOnPlatform((Collision2D)other, true);

    }


    private void OnCollisionExit2D(Collision2D other)
    {

        SetPlayerOnPlatform((Collision2D)other, true);

    }
}


