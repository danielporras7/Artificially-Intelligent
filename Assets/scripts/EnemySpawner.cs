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

    public int spawnNumber = 1;
    private int counter = 0;
    private int threshhold = 154;

    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 8f, 0.5f);
    }

    void SpawnEnemy()
    {
        spawnNumber = Random.Range(1, 7);

        if(spawnNumber == 1)
        {
            Spawn(spawner1);
            counter++;
            if(counter >= threshhold)
            {
                Spawn(spawner6);
            }
        }

        else if (spawnNumber == 2)
        {
            Spawn(spawner2);
            counter++;
            if (counter >= threshhold)
            {
                Spawn(spawner5);
            }
        }

        else if (spawnNumber == 3)
        {
            Spawn(spawner3);
            counter++;
            if (counter >= threshhold)
            {
                Spawn(spawner4);
            }
        }

        else if (spawnNumber == 4)
        {
            Spawn(spawner4);
            counter++;
            if (counter >= threshhold)
            {
                Spawn(spawner3);
            }
        }

        else if (spawnNumber == 5)
        {
            Spawn(spawner5);
            counter++;
            if (counter >= threshhold)
            {
                Spawn(spawner2);
            }
        }

        else if (spawnNumber == 6)
        {
            Spawn(spawner6);
            counter++;
            if (counter >= threshhold)
            {
                Spawn(spawner1);
            }
        }

    }

    void Spawn(Transform spawner)
    {
        Instantiate(enemyPrefab, spawner.position, spawner.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
