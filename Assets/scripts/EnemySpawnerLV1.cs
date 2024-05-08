using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UIElements;

//This spawner is specific to level 1, which spawns less enemies
public class EnemySpawnerLV1 : MonoBehaviour
{
    public Transform spawner1;
    public Transform spawner2;
    public Transform spawner3;
    public Transform spawner4;
    public Transform spawner5;
    public Transform spawner6;

    private int spawnNumber = 1;

    public GameObject runnerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //Comment/uncomment the next line in order to turn off/on the spawner
        //Change the second value to delay timer
        InvokeRepeating("SpawnEnemy", 10f, 1f);
    }

    void SpawnEnemy()
    {
        spawnNumber = Random.Range(0, 6);

        if(spawnNumber == 0)
        {
            Spawn(spawner1, runnerPrefab);
        }

        if (spawnNumber == 1)
        {
            Spawn(spawner2, runnerPrefab);
        }

        if (spawnNumber == 2)
        {
            Spawn(spawner3, runnerPrefab);
        }

        if (spawnNumber == 4)
        {
            Spawn(spawner5, runnerPrefab);
        }

        if (spawnNumber == 5)
        {
            Spawn(spawner6, runnerPrefab);
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
