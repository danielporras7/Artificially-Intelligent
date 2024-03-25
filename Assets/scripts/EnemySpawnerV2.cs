using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerV2 : MonoBehaviour
{
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn)
        {
            yield return wait;


            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
    }
}
