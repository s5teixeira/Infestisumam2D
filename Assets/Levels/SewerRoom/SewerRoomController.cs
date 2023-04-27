using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewerRoomController : MonoBehaviour
{
    [SerializeField] LevelDoorOperator northDoor;
    [SerializeField] LevelDoorOperator eastDoor;
    [SerializeField] LevelDoorOperator southDoor;
    [SerializeField] LevelDoorOperator westDoor;
    [SerializeField] GameCoordinator gc;


    // Start is called before the first frame update
    void Start()
    {
        northDoor.AttachOnPlayerInArea(OnPlayerAtNorthDoor);
        eastDoor.AttachOnPlayerInArea(OnPlayerAtEastDoor);
        southDoor.AttachOnPlayerInArea(OnPlayerAtSouthDoor);
        westDoor.AttachOnPlayerInArea(OnPlayerAtWestDoor);
        gc.LoadLevelData();
    }


    private void OnPlayerAtNorthDoor()
    {
        gc.Transition("north");
    }

    private void OnPlayerAtEastDoor()
    {
        gc.Transition("east");

    }

    private void OnPlayerAtSouthDoor()
    {
        gc.Transition("south");

    }

    private void OnPlayerAtWestDoor()
    {
        gc.Transition("west");

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
