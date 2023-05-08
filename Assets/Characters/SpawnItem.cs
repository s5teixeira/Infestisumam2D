using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject itemPrefab;  // The item prefab to spawn

    private void Start()
    {
        Spawn();  
    }

}