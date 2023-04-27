using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoridorVerticalController : MonoBehaviour
{
    [SerializeField] public LevelDoorOperator northernDoor;
    [SerializeField] public LevelDoorOperator southernDoor;
    [SerializeField] public GameCoordinator gc;

    // Start is called before the first frame update
    void Start()
    {
        gc.LoadLevelData();
        northernDoor.AttachOnPlayerInArea(OnNorthernDoorTriggered);
        southernDoor.AttachOnPlayerInArea(OnSouthernDoorTriggered);


    }
     
    void OnNorthernDoorTriggered()
    {
        gc.Transition("north");
    }

    void OnSouthernDoorTriggered()
    {
        gc.Transition("south");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
