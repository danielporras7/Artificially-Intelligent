using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBird : Enemy
{
    //Holds bullet and player
    public GameObject bulletPrefab;
    public GameObject player;

    private float speed2 = 5;

    //Holds the points where the bullet can spawn from
    public Transform firePoint;

    //Used to determine how fast the enemy shoots
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb.velocity = (transform.right * speed2);
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy starts shooting once it is within a certain distance of the player (Once the camera reaches the enemy basically)
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < 20)
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {
                timer = 0;
                Shoot(firePoint);
            }
        }

        //This destroys the bird after a few seconds
        Destroy(gameObject, 20);

    }

    //Used to shoot the player
    void Shoot(Transform position)
    {
        //audioSrc.PlayOneShot(shootingSound); Not sure if enemy shots should be silent or not, probably should be
        Instantiate(bulletPrefab, position.position, position.rotation);

    }
}
