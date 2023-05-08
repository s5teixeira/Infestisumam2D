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
    private void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity, transform);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Player has entered the room start spawning enemies
            enabled = true;
        }
    }
}
