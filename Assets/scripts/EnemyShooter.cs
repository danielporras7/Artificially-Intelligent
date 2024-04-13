using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyShooter : Enemy
{
    //Holds bullet and player
    public GameObject bulletPrefab;
    public GameObject player;

    //Holds the points where the bullet can spawn from
    public Transform firePoint;
    public Transform firePointUp;
    public Transform firePointDown;

    //This can hold either of the previous 3 shooting points
    public Transform currentPoint;

    //Used to flip the enemy sprite
    bool facingRight;

    //Used to determine how fast the enemy shoots
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        facingRight = true;
        currentPoint = firePoint;
    }


    // Update is called once per frame
    void Update()
    {
        //Gets Player Location, this is used to changed enemy sprite and to flip the enemy bullet position
        //Also to determine where the bullet should spawn from
        Vector3 playerDirection = player.transform.position - transform.position;

        //Enemy starts shooting once it is within a certain distance of the player (Once the camera reaches the enemy basically)
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if(distance < 30)
        {
            timer += Time.deltaTime;

            if (timer > 1.5)
            {
                timer = 0;
                Shoot(currentPoint);
            }
        }

        //Used to decide which shooting position the bullet should spawn and what enemy sprite to use
        if(playerDirection.y < -3)
        {
            currentPoint = firePointDown;
            animator.SetBool("Up", false);
            animator.SetBool("Down", true);
        }

        else if(playerDirection.y > 3)
        {
            currentPoint = firePointUp;
            animator.SetBool("Up", true);
            animator.SetBool("Down", false);
        }

        else
        {
            currentPoint = firePoint;
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
        }

        //Used to flip the enemy if the player goes past them
        if(playerDirection.x < 0 && facingRight)
        {
            Flip();
        }
        if(playerDirection.x > 0 && !facingRight)
        {
            Flip();
        }

    }

    //Used to shoot the player
    void Shoot(Transform position)
    {
        //audioSrc.PlayOneShot(shootingSound); Not sure if enemy shots should be silent or not, probably should be
        Instantiate(bulletPrefab, position.position, position.rotation);

    }

    //Flips the sprites and bullet spawn locations when the enemy turns around
    private void Flip()
    {

        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
