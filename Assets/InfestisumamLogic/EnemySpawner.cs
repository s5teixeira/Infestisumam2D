using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemies;
    public float spawnFrequency;
    public Transform[] spawnPoints;

    private float timeSinceLastSpawn;

    private void Update()
    {
        if (timeSinceLastSpawn >= spawnFrequency)
        {
            SpawnEnemies();
            timeSinceLastSpawn = 0;
        }
        else
        {
            timeSinceLastSpawn += Time.deltaTime;
        }
    }
}
