using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public Animator animator;

    private float speed = 7;

    public int health = 100;

    public GameObject deathEffect;

    //public PlayerMovementTest player;

    //Sounds
    SoundManager soundManager;

    //Awake() is only used for audio as of now
    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    //This line of code was originally meant for enemies to hurt a player if it was detected that they touched the player
    //This caused issues so instead it was changed so that the player script will detect if they touched enemies, and
    //hurt itself when they touched an enemy

    /*Used to hurt player
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.TakeDamage(100);
        }
    } */

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pitfall")
        {
            TakeDamage(300);
        }
    } 

    //When an enemy dies
    public void Die()
    {
        soundManager.PlaySFX(soundManager.enemyDeathSFX);

        Instantiate(deathEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = (transform.right * speed);
        //animator = GetComponent<Animator>();
        //sr = GetComponent<SpriteRenderer>();
        //rb = GetComponent<Rigidbody2D>();
    }

    //This is used to let enemies pass through each other
    public void OnEnable()
    {
        //Adds enemies to a list of objects they can ignore collisions
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject Enemy in enemies)
        {
            Physics2D.IgnoreCollision(Enemy.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = (transform.right * speed);
        //animator.SetBool("Running", true);
        //rb.velocity = new Vector2(speed, rb.velocity.y);
    }
}
