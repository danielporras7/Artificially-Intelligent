using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public Transform firePointUr;
    public Transform firePointDr;
    public Transform firePointUp;
    public Transform firePointDown;

    public AudioSource audioSrc;
    public AudioClip shootingSound;

    private float horizontal;
    private float vertical;

    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Aiming Variables
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");


        if (Input.GetButtonDown("Fire1"))
        {
            //Shooting commands assuming facing right, but will be inverted if player is facing left so it's fine
            //Shooting Straight
            if(vertical == 0)
            {
                Shoot(firePoint);
            }

            //Shooting Upright
            else if (vertical == 1 && horizontal != 0)
            {
                Shoot(firePointUr);
            }

            //Shooting Downright
            else if (vertical == -1 && horizontal != 0)
            {
                Shoot(firePointDr);
            }

            //Shooting Up
            else if (vertical == 1 && horizontal == 0)
            {
                Shoot(firePointUp);
            }

            //Shooting LyingDown
            else if(vertical == -1 && horizontal == 0)
            {
                Shoot(firePointDown);
            }
        }


    }

    void Shoot(Transform position)
    {
        audioSrc.PlayOneShot(shootingSound);
        Instantiate(bulletPrefab, position.position, position.rotation);
        
        //Alternate code that works the same?
        /*
        var b = Instantiate(bulletPrefab, position.position, position.rotation).GetComponent<Rigidbody2D>();
        b.AddForce(position.right * 20f, ForceMode2D.Impulse);
        Destroy(b.gameObject, 10); */
    }
}
