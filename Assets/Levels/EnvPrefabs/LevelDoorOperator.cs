using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoorOperator : MonoBehaviour
{

    enum DoorState
    {
        DoorClosed,
        DoorOpen
    }

    [SerializeField] DoorState currentState;
    private GameObject doorItself;


    // Start is called before the first frame update
    void Start()
    {
        SetDoor();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetDoor()
    {
        Debug.Log("Door Childre: " + transform.hierarchyCount);
        doorItself = transform.GetChild((int)currentState).gameObject;
        doorItself.SetActive(true);
        for(int i = 0; i<transform.hierarchyCount; i++)
        {
            if(i != (int)currentState)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

}
