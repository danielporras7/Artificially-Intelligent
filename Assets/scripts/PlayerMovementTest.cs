using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class PlayerMovementTest : MonoBehaviour
{
    //Used for changing sprites
    public Animator animator;
    SpriteRenderer sr;

    Color c;

    //Health and Death Variables
    public int maxHealth = 300;
    public int health;
    public int numOfHearts;

    [SerializeField] Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public GameObject deathEffect;

    GameObject[] enemies;
    GameObject[] bullets;

    //Movement variables -Some are used for testing
    private float horizontal;
    private float vertical;
    private float speed = 8;
    private float jumpingPower = 12;
    private float fallMultiplier = 3;
    Vector2 gravity;

    bool facingRight;
    bool grounded;
    bool aimingUpRight;
    bool aimingDownRight;
    bool aimingStraightUp;
    bool lyingDown;

    [SerializeField] private Rigidbody2D rb;
    //[SerializeField] private BoxCollider2D current;
    [SerializeField] private BoxCollider2D standing;
    [SerializeField] private BoxCollider2D crouchingDown;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    //Sounds
    SoundManager soundManager;

    //Awake() is only used for audio as of now

    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        gravity = new Vector2(0, -Physics2D.gravity.y);
        sr = GetComponent<SpriteRenderer>();

        c = sr.material.color;

        standing.enabled = true;
        crouchingDown.enabled = false;
        
        grounded = true;
        facingRight = true;
        
    } 

    // Update is called once per frame
    void Update()
    {
        //For horizontal movement
        horizontal = Input.GetAxisRaw("Horizontal");

        //For Aiming
        vertical = Input.GetAxisRaw("Vertical");

        //For movement sprites
        //Running Sprites
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        //Cancels Jumping sprite when grounded
        animator.SetBool("Jumping", !IsGrounded());

        //For Jumping
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            //For Jumping Sprites
            animator.SetBool("Jumping", true);
        }

        //This increases velocity as you're falling, so it makes jumping smoother imo
        if(rb.velocity.y < 0)
        {
            rb.velocity -= gravity * fallMultiplier * Time.deltaTime;
        }

        //Sprite change to aiming Sprite/Direction
        animator.SetBool("UpRight", IsAimingUpRight());
        animator.SetBool("DownRight", IsAimingDownRight());
        animator.SetBool("AimUp", IsAimingUp());
        animator.SetBool("Down", IsLyingDown());

        if(IsLyingDown())
        {
            crouchingDown.enabled = true;
            standing.enabled = false;
        }

        else
        {
            crouchingDown.enabled = false;
            standing.enabled = true;
        }


        //Used for switching directions
        if(horizontal < 0 && facingRight)
        {
            Flip();
        }

        //Used for switching directions
        if (horizontal > 0 && !facingRight)
        {
            Flip();
        }

        //Health
        if((health / 100) > numOfHearts)
        {
            health = numOfHearts * 100;
        }


        for (int i = 0; i < hearts.Length; i++)
        {

            if (i * 100 < health)
            {
                hearts[i].sprite = fullHeart;
            }

            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if(i < numOfHearts)
            {
                hearts[i].enabled = true;
            }

            else
            {
                hearts[i].enabled = false; 
            }
        }

    }

    //Checks of player is on the ground
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //Several Checks to see if player is aiming and it what direction.
    private bool IsAimingUpRight()
    {
        if(vertical == 1 && horizontal != 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
    private bool IsAimingDownRight()
    {
        if (vertical == -1 && horizontal != 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
    private bool IsAimingUp()
    {
        if (vertical == 1 && horizontal == 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
    private bool IsLyingDown()
    {
        if (vertical == -1 && horizontal == 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    private void FixedUpdate()
    {
        //Player movement
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }



    //Flips the sprites in case the user turns around
    private void Flip()
    {
        /* This was used before implementing shooting mechanics
        if(horizontal != 0)
        {
            //sr.flipX = horizontal < 0;

        } */

        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    //Player Death
    public void TakeDamage(int damage)
    {
        health -= damage;

        //IF health falls to 0, player dies
        if (health <= 0)
        {
            Die();
        }

        //If player doesn't die, give the player temporary invicibility
        else
        {
            StartCoroutine(IFrames());
        }
    }

    //Used to hurt player if they hit an enemy
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(100);
        }

        if (collision.gameObject.tag == "Pitfall")
        {
            TakeDamage(300);
        }
    } 

    //Kills the player
    public void Die()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = emptyHeart;
        }

        soundManager.PlayDeath();

        Instantiate(deathEffect, transform.position, Quaternion.identity);

        SceneController.instance.GameOverStart();

        Destroy(gameObject);

        //Wait a few second after dying
        //StartCoroutine(GameOverScreen());


    }

    //This gives the player temporary invicibility after taking enemy damage
    IEnumerator IFrames()
    {
        Physics2D.IgnoreLayerCollision(0, 7, true);

        c.a = 0.5f;
        sr.material.color = c;
        yield return new WaitForSeconds(2f);

        Physics2D.IgnoreLayerCollision(0, 7, false);

        c.a = 1f;
        sr.material.color = c;
    }

    //Waits for a few seconds
    IEnumerator GameOverScreen()
    {
        yield return new WaitForSeconds(3f);

        //Play game over screen
        SceneController.instance.GameOverStart();
    }
}
