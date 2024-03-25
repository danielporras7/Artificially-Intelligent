using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SpriteRenderer sr;
    public Rigidbody2D rb;

    private float speed = 4;


    public int health = 100;

    public GameObject deathEffect;

    public PlayerMovementTest player;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    //Used to hurt player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.TakeDamage(100);
        }
    }

    //When an enemy dies
    public void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = (transform.right * speed);
        //sr = GetComponent<SpriteRenderer>();
        //rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = new Vector2(speed, rb.velocity.y);
    }
}
