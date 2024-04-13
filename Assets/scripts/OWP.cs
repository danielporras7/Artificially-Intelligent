using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OWP : MonoBehaviour
{
    //This is used to let bullets pass through platform
    public void OnEnable()
    {
        //Adds enemies to a list of objects they can ignore collisions
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject Bullet in bullets)
        {
            Physics2D.IgnoreCollision(Bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
