using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovementTest : MonoBehaviour
{
    //Used for changing sprites
    public Animator animator;
    SpriteRenderer sr;

    //Death Variables
    public int maxHealth = 100;
    public int health;

    public GameObject deathEffect;

    //Movement variables
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
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    // Start is called before the first frame update
     void Start()
    {
        health = maxHealth;
        gravity = new Vector2(0, -Physics2D.gravity.y);
        sr = GetComponent<SpriteRenderer>();
        
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

        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

}
