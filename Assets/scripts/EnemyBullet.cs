using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Uses the Bullet.cs script
public class EnemyBullet : Bullet
{
    public GameObject player1;
    
    //Enemy Bullets are slower than the player's
    public float speed2 = 10f;

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player");

        //Gets player location
        Vector3 direction = player1.transform.position - transform.position;

        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed2;
    }

    //Used to kill player and destroy bullet once it collides with something
    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovementTest player = collision.GetComponent<PlayerMovementTest>();

        if (player != null)
        {
            player.TakeDamage(100);
        }

        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {

        }

        else
        {
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //destroys the bullet after a few seconds
        Destroy(gameObject, 10);
    }
}