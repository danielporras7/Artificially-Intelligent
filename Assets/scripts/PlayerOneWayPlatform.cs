using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//OWP stands for One-Way Platform
//Script is used so that the player can go through OWPs; both jump to them and drop from them

public class PlayerOneWayPlatform : MonoBehaviour
{
    private GameObject currentOWP;

    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private BoxCollider2D playerCollider2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Allows the user to drop from a OWP
        if(Input.GetKeyDown(KeyCode.K))
        {
            if (currentOWP != null)
            {
                StartCoroutine(DisableCollision());
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOWP = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOWP = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentOWP.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        Physics2D.IgnoreCollision(playerCollider2, platformCollider);

        yield return new WaitForSeconds(1f);

        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
        Physics2D.IgnoreCollision(playerCollider2, platformCollider, false);
    }

}
