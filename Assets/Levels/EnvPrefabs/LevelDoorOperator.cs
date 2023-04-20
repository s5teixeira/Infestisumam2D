using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoorOperator : MonoBehaviour
{

    public enum DoorState
    {
        DoorOpen,
        DoorClosed
    }

    [SerializeField] DoorState currentState;
    [SerializeField] List<GameObject> DoorStates = new List<GameObject>();
    private GameObject doorItself;
    private bool playerInTriggerZone = false;


    // Start is called before the first frame update
    void Start()
    {
        SetDoor();


    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerSoul")
        {
            playerInTriggerZone = true;
        }
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);

    }



    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerSoul")
        {
            playerInTriggerZone = false;
        }

    }

    public bool GetPlayerInTriggerArea()
    {
        return playerInTriggerZone;
    }

    public DoorState GetDoorState()
    {
        return currentState;
    }

    public void SetDoorState(int newState)
    {
        Debug.LogWarning(this.name + " SET DOOR STATE CALLED NEW STATE " + (DoorState)newState + " / int"+ newState);
        currentState = (DoorState)newState;
        SetDoor();
    }

    private void SetDoor()
    {

        doorItself = transform.GetChild((int)currentState).gameObject;
        doorItself.SetActive(true);

        for (int i = 0; i < DoorStates.Count; i++)
        {
            if(i != (int)currentState)
            {
                DoorStates[i].SetActive(false);

            }

        }
       

        /*
        for (int i = 0; i < transform.hierarchyCount; i++)
        {
            if (i != (int)currentState)
            {
                try
                {
                    Debug.Log("I " + transform.GetChild(i).gameObject.name);
                    transform.GetChild(i).gameObject.SetActive(false);
                    Debug.Log("====================");
                }
                catch (UnityException e)
                {
                    Debug.LogError(this.name + " caught unity exception: " + e.ToString());
                }
            }
        }*/

        Debug.LogWarning(this.name + " SetState executed:  " + currentState);

    }

}
