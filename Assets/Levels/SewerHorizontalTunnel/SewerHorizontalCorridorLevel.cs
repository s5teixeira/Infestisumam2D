using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewerHorizontalCorridorLevel : MonoBehaviour
{
    [SerializeField] LevelDoorOperator westernDoor;
    [SerializeField] LevelDoorOperator easternDoor;
    [SerializeField] GameObject gameCoordinator;


    // Start is called before the first frame update
    void Start()
    {
        westernDoor.AttachOnPlayerInArea(OnWesternDoorEntered);
        easternDoor.AttachOnPlayerInArea(OnEasternDoorEntered);
    }
    
    private void OnEasternDoorEntered()
    {
        Debug.Log("Player at the eastern area");

        // gameCoordinator.GetComponent<GameCoordinator>().ProcessTransition("east");
        gameCoordinator.GetComponent<GameCoordinator>().Transition("east");
    }

    private void OnWesternDoorEntered()
    {
        Debug.Log("Player at the western area");
        //gameCoordinator.GetComponent<GameCoordinator>().ProcessTransition("west");
        gameCoordinator.GetComponent<GameCoordinator>().Transition("west");


    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
