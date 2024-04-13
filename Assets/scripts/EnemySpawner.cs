using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    public Transform spawner1;
    public Transform spawner2;
    public Transform spawner3;
    public Transform spawner4;
    public Transform spawner5;
    public Transform spawner6;

    private int spawnNumber = 1;
    private int secondNumber = 1;
    private int counter = 0;

    //Threshhold value holds a specific number. Once the number of enemies spawned exceeds that number,
    //the amount of enemies spawned will double.
    //154 was the value used for the prototype, this takes effect once the music gets more intense
    private int threshhold = 50;

    public GameObject runnerPrefab;
    public GameObject birdPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //Comment/uncomment the next line in order to turn off/on the spawner
        //Change the second value to delay timer
        //For the prototype the second value is 8f
        InvokeRepeating("SpawnEnemy", 17f, 1f);
    }

    void SpawnEnemy()
    {
        spawnNumber = Random.Range(0, 7);
        secondNumber = Random.Range(0, 2);

        //If spawn number is 0, spawn a bird enemy
        if(spawnNumber == 0)
        {
            if(secondNumber == 0)
            {
                Spawn(spawner1, birdPrefab);
                counter++;

                if (counter >= threshhold)
                {
                    Spawn(spawner6, runnerPrefab);
                }
            }

            else if (secondNumber == 1)
            {
                Spawn(spawner2, birdPrefab);
                counter++;

                if (counter >= threshhold)
                {
                    Spawn(spawner5, runnerPrefab);
                }
            }
        }

        //If spawn number is anything other than 0, spawn a normal enemy
        else
        {
            if (spawnNumber == 3 || spawnNumber == 1)
            {
                Spawn(spawner3, runnerPrefab);
                counter++;

                if (counter >= threshhold)
                {
                    Spawn(spawner4, runnerPrefab);
                }
            }

            else if (spawnNumber == 2 || spawnNumber == 4)
            {
                Spawn(spawner4, runnerPrefab);
                counter++;

                if (counter >= threshhold)
                {
                    Spawn(spawner3, runnerPrefab);
                }
            }

            else if (spawnNumber == 5)
            {
                Spawn(spawner5, runnerPrefab);
                counter++;

                if (counter >= threshhold)
                {
                    Spawn(spawner2, birdPrefab);
                }
            }

            else if (spawnNumber == 6)
            {
                Spawn(spawner6, runnerPrefab);
                counter++;

                if (counter >= threshhold)
                {
                    Spawn(spawner1, birdPrefab);
                }
            }
        }



    }

    void Spawn(Transform spawner, GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, spawner.position, spawner.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
