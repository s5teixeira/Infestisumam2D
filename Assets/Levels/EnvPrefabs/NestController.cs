using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestController : MonoBehaviour
{

    [SerializeField] float SpawnTimeout = 5.0f;
    [SerializeField] Pawn SpawnEnemy;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SpawnTimeout > 0)
        {
            SpawnTimeout -= Time.deltaTime;
        }
        else
        {
            SpawnTimeout = 5.0f;
            Instantiate(SpawnEnemy);
        }
    }
}
