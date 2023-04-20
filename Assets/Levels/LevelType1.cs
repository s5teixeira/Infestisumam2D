using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperTiled2Unity;
using System;
using TMPro;



public class LevelType1 : MonoBehaviour
{

    [SerializeField] public GameObject LevelMesh;
    [SerializeField] public Camera camera;
    [Header("WARNING! I DONT KNOW HOW TO EXTRACT WIDTH AND HEIGHT FROM SUPERTILED2UNITY, PLEASE ENTER THE VALUES MANUALLY!")]

    [SerializeField] float mapWidth;
    [SerializeField] float mapHeight;

    [SerializeField] LevelDoorOperator northernDoor;
    [SerializeField] LevelDoorOperator eastDoor;
    [SerializeField] LevelDoorOperator southDoor;
    [SerializeField] LevelDoorOperator westDoor;
    [SerializeField] GameCoordinator gameCoordinator;


    [SerializeField] TextMeshPro DebugOutput;

    private Level thisLevel;
    private Path thisPath;
    private bool isTransitioning = false;

    // Start is called before the first frame update
    void Start()
    {

        Debug.LogError(" I AM STARTING !!! ");
        camera.transform.position = new Vector3(0, 0, -4);

        gameCoordinator.GetComponent<GameCoordinator>().LoadLevelData();
        thisLevel = gameCoordinator.GetComponent<GameCoordinator>().GetCurrentLevel();
        thisPath = gameCoordinator.GetComponent<GameCoordinator>().GetCurrentPath();
        Debug.Log(thisLevel.eastDoorState);
        northernDoor.SetDoorState(thisLevel.northDoorState);
        eastDoor.SetDoorState(thisLevel.eastDoorState);
        southDoor.SetDoorState(thisLevel.southDoorState);
        westDoor.SetDoorState(thisLevel.westDoorState);

        Debug.Log("THIS LEVEL ID IS " + thisLevel.levelID);

        DebugOutput.SetText(String.Format("LVLID: {0}/XYZ: {1}|{2}|{3}/", thisLevel.levelID, thisPath.locX, thisPath.locY, thisPath.locZ));

    }

    // Update is called once per frame
    void Update()
    {
        if(northernDoor.GetComponent<LevelDoorOperator>().GetPlayerInTriggerArea() && northernDoor.GetComponent<LevelDoorOperator>().GetDoorState() == LevelDoorOperator.DoorState.DoorOpen)
        {
            Debug.Log("Northern door triggered");
            
            gameCoordinator.GetComponent<GameCoordinator>().ProcessTransition("north");
        }
        Debug.Log(southDoor.GetComponent<LevelDoorOperator>().GetPlayerInTriggerArea() + " / " + southDoor.GetComponent<LevelDoorOperator>().GetDoorState() + " " + (southDoor.GetComponent<LevelDoorOperator>().GetPlayerInTriggerArea() && southDoor.GetComponent<LevelDoorOperator>().GetDoorState() == LevelDoorOperator.DoorState.DoorOpen)); 
        if (eastDoor.GetComponent<LevelDoorOperator>().GetPlayerInTriggerArea() && eastDoor.GetComponent<LevelDoorOperator>().GetDoorState() == LevelDoorOperator.DoorState.DoorOpen)
        {
            Debug.Log("eastDoor door triggered");
            gameCoordinator.GetComponent<GameCoordinator>().ProcessTransition("east");

        }

        if (southDoor.GetComponent<LevelDoorOperator>().GetPlayerInTriggerArea() && southDoor.GetComponent<LevelDoorOperator>().GetDoorState() == LevelDoorOperator.DoorState.DoorOpen)
        {
            Debug.Log("southDoor door triggered");
            gameCoordinator.GetComponent<GameCoordinator>().ProcessTransition("south");

        }

        if (westDoor.GetComponent<LevelDoorOperator>().GetPlayerInTriggerArea() && westDoor.GetComponent<LevelDoorOperator>().GetDoorState() == LevelDoorOperator.DoorState.DoorOpen)
        {
            Debug.Log("westDoor door triggered");
            gameCoordinator.GetComponent<GameCoordinator>().ProcessTransition("west");

        }
    }


}
