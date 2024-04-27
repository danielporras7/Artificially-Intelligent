using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        
    }

    //Used to kill enemies and destroy bullet once it collides with something
    void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        Boss boss = collision.GetComponent<Boss>();

        if (enemy != null)
        {
            enemy.TakeDamage(100);
        }
        
        else if(boss != null)
        {
            boss.TakeDamage(100);
        }

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //destroys the bullet after a few seconds
        Destroy(gameObject, 10);
    }

    //This is used to let bullets pass through platform
    public void OnEnable()
    {
        //Adds enemies to a list of objects they can ignore collisions
        GameObject[] owps = GameObject.FindGameObjectsWithTag("OneWayPlatform");

        foreach (GameObject OWP in owps)
        {
            Physics2D.IgnoreCollision(OWP.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
