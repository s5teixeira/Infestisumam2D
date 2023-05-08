using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject itemPrefab;  // The item prefab to spawn

    private void Start()
    {
        Spawn();  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Spawn the item when a trigger is entered
        if (collision.CompareTag("Player"))
        {
            Spawn();
        }
    }
    private void Spawn()
    {
        Instantiate(itemPrefab, transform.position, Quaternion.identity);  // Instantiate the item prefab at the spawn point
    }

}