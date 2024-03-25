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

    public int spawnNumber = 1;

    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, 2f);
    }

    void SpawnEnemy()
    {
        spawnNumber = Random.Range(1, 5);

        if(spawnNumber == 1)
        {
            Spawn(spawner1);
        }

        else if (spawnNumber == 2)
        {
            Spawn(spawner2);
        }

        else if (spawnNumber == 3)
        {
            Spawn(spawner3);
        }

        else if (spawnNumber == 4)
        {
            Spawn(spawner4);
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
