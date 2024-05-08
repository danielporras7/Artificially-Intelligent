using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeSpawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if ((gameObject.transform.position.x - player.position.x) < 5)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        // Instantiate enemy
        Instantiate(enemy, spawnPoint.transform.position, spawnPoint.transform.rotation);
        Destroy(gameObject);
    }
}
