using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Uses a combination of Bullet.cs and EnemyBullet.cs script
public class BossBullet : Bullet
{
    //Enemy Bullets are slower than the player's
    public float speed2 = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
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
