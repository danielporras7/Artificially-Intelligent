using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss : MonoBehaviour
{
    //Holds bullet and player
    public GameObject bulletPrefab;
    public GameObject bossBulletPrefab;
    public GameObject player;
    public GameObject enemyShooter;

    //Holds the points where the bullet can spawn from
    public Transform firePointLeft;
    public Transform firePointMid;
    public Transform firePointRight;
    //Extra points for rapid fire
    public Transform ml;
    public Transform mr;

    //Holds points where the Boss can move to
    public Transform bossLeftPosition;
    public Transform bossMainPosition;
    public Transform bossRightPosition;

    public Transform currentPosition;
    public Transform spawnPosition;

    //Used to determine how fast the enemy shoots
    private float shootTimer;
    private float moveTimer;
    private float enemySpawnTimer;
    private float laserTimer;

    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public Animator animator;

    private float speed = 7;

    public int health = 4000;

    //Boolean used for the boss's initial movement downwards
    private bool once;
    //Position int holds current point: 0 = Left Position, 1 = Middle Position, 2 = Right Position
    private int position;
    //Holds a random number
    private int randomNum = 0;

    public GameObject deathEffect;

    //These 2 objects are used to notify the camera that the boss has died
    GameObject camera1;
    BossCamera camera2;

    //Sounds
    SoundManager soundManager;

    //Awake() is only used for audio as of now
    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }

    public void OnEnable()
    {
        //Adds enemies to a list of objects they can ignore collisions
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject Enemy in enemies)
        {
            Physics2D.IgnoreCollision(Enemy.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        once = false;
        position = -1;

        health = 4000;

        player = GameObject.FindGameObjectWithTag("Player");
        //facingRight = true;
        //currentPoint = firePointMid;

        //Used for when the boss dies
        camera1 = GameObject.Find("Main Camera");
        camera2 = camera1.GetComponent<BossCamera>();
    }

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
        //Notifies the camera that the boss has died
        camera2.BossDeath();

        soundManager.PlaySFX(soundManager.enemyDeathSFX);

        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Instantiate(deathEffect, firePointLeft.position, Quaternion.identity);
        Instantiate(deathEffect, firePointRight.position, Quaternion.identity);

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Gets Player Location, this is used to changed enemy sprite and to flip the enemy bullet position
        //Also to determine where the bullet should spawn from
        Vector3 playerDirection = player.transform.position - transform.position;

        //This makes the boss descend onto the stage
        if (!once)
        {
            rb.velocity = new Vector2(0, -speed);

            if(transform.position.y <= 9.8f)
            {
                rb.velocity = new Vector2(0, 0);
                once = false;
            }
        }

        //Move to left position
        if (position == 0)
        {
            rb.velocity = new Vector2(-speed, 0);

            if (transform.position.x <= 600f)
            {
                rb.velocity = new Vector2(0, 0);
                position = -1;
            }
        }

        //Move to middle position
        if (position == 1)
        {
            //If Boss is to the left
            if (transform.position.x < 612.9f)
            {
                rb.velocity = new Vector2(speed, 0);

                if (transform.position.x >= 612.9f)
                {
                    rb.velocity = new Vector2(0, 0);
                    position = -1;
                }
            }

            //If Boss is to the right
            else if(transform.position.x > 612.9f)
            {
                rb.velocity = new Vector2(-speed, 0);

                if (transform.position.x <= 612.9f)
                {
                    rb.velocity = new Vector2(0, 0);
                    position = -1;
                }
            }    
        }

        //Move to the right
        if (position == 2)
        {
            rb.velocity = new Vector2(speed, 0);

            if (transform.position.x >= 623.77f)
            {
                rb.velocity = new Vector2(0, 0);
                position = -1;
            }
        }




        //Enemy starts shooting once it is within a certain distance of the player (Once the camera reaches the enemy basically)
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < 30)
        {
            shootTimer += Time.deltaTime;
            moveTimer += Time.deltaTime;
            enemySpawnTimer += Time.deltaTime;
            laserTimer += Time.deltaTime;

            if (shootTimer > 1.25)
            {
                shootTimer = 0;
                Shoot();
            }

            if (moveTimer > 4)
            {
                moveTimer = 0;
                MoveBoss();
            }

            if (enemySpawnTimer > 8)
            {
                enemySpawnTimer = 0;
                SpawnEnemy();
            }

            if (laserTimer > 13)
            {
                laserTimer = 0;
                StartCoroutine(RapidFire());
            }

        }

    }

    //Used to shoot the player
    void Shoot()
    {
        //audioSrc.PlayOneShot(shootingSound); Not sure if enemy shots should be silent or not, probably should be
        Instantiate(bulletPrefab, firePointMid.position, firePointMid.rotation);
        Instantiate(bulletPrefab, firePointLeft.position, firePointLeft.rotation);
        Instantiate(bulletPrefab, firePointRight.position, firePointRight.rotation);

    }

    void SpawnEnemy()
    {
        Instantiate(enemyShooter, firePointLeft.position, firePointLeft.rotation);
        Instantiate(enemyShooter, firePointRight.position, firePointRight.rotation);
    }

    void MoveBoss()
    {
        randomNum = Random.Range(0, 3);

        position = randomNum;
    }
    
    IEnumerator RapidFire()
    {
        for(int i = 0; i <= 5; i++)
        {
            Instantiate(bossBulletPrefab, ml.position, ml.rotation);
            Instantiate(bossBulletPrefab, firePointMid.position, firePointMid.rotation);
            Instantiate(bossBulletPrefab, mr.position, mr.rotation);

            yield return new WaitForSeconds(.1f);
        }

    }
}
