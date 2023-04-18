using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroDirector : MonoBehaviour
{

    [SerializeField] public GameObject text;
    [SerializeField] public GameObject gameCoordinator;
    [SerializeField] public static float TextFlashInterval = 0.750f;
    private float gpCountdown = 2;
    private float timeToFlashText = TextFlashInterval;
    bool enterPressed = false;
    bool saveCreationFailed = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(timeToFlashText < 0 && enterPressed){
            text.SetActive(!text.activeSelf);
            timeToFlashText = TextFlashInterval;
        }else{
            timeToFlashText -= Time.deltaTime;
        }

        if (!enterPressed  && Input.GetKeyDown(KeyCode.Return))
        {
            enterPressed = true;
            Debug.Log("Starting....");
            bool saveExists = gameCoordinator.GetComponent<GameCoordinator>().SaveGameExists();
            Debug.Log(saveExists);
            if (!saveExists)
            {
                text.GetComponent<TMPro.TextMeshProUGUI>().text = "SAVE NOT FOUND... CREATING";
                gameCoordinator.GetComponent<GameCoordinator>().CreateDatabases();
                saveCreationFailed =  !gameCoordinator.GetComponent<GameCoordinator>().SaveGameExists()


            }

        }

        if (saveCreated){

            gpCountdown -= Time.deltaTime;

            if(gpCountdown < 0)
            {
                text.GetComponent<TMPro.TextMeshProUGUI>().text = ">save created........ soul captured";

            }

        }

        if (saveCreationFailed)
        {
            text.GetComponent<TMPro.TextMeshProUGUI>().text = "ERROR! FAILED TO CREATE/LOAD SAVE";

        }



    }
}
