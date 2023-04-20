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
    bool saveExists = false;
    bool initGameCalled = false;

    Level thisLevel;


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if(timeToFlashText < 0 && !enterPressed){
            text.SetActive(!text.activeSelf);
            timeToFlashText = TextFlashInterval;
        }else{
            timeToFlashText -= Time.deltaTime;
        }

        if (!enterPressed  && Input.GetKeyDown(KeyCode.Return))
        {
            enterPressed = true;
            Debug.Log("Starting....");
            saveExists = gameCoordinator.GetComponent<GameCoordinator>().SaveGameExists();
            Debug.Log(saveExists);
        }

        if (enterPressed)
        {
            if (!saveExists)
            {
                text.GetComponent<TMPro.TextMeshProUGUI>().text = "SAVE NOT FOUND... CREATING";
                gameCoordinator.GetComponent<GameCoordinator>().CreateDatabase();
                saveCreationFailed = !gameCoordinator.GetComponent<GameCoordinator>().SaveGameExists();
                saveExists = !saveCreationFailed;
            }
            else
            {
                text.GetComponent<TMPro.TextMeshProUGUI>().text = ">BOOTING<";
                if (!initGameCalled)
                {
                    initGameCalled = true;
                    Debug.Log("Started");
                    text.GetComponent<TMPro.TextMeshProUGUI>().text = "> starting up";
                    gameCoordinator.GetComponent<GameCoordinator>().InitiateGame();
                }
            }

            if (saveCreationFailed)
            {
                text.GetComponent<TMPro.TextMeshProUGUI>().text = "ERROR! FAILED TO CREATE SAVE FILE";

            }


        }






    }
}
