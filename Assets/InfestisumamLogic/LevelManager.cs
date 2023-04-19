using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public List<string> GameLevels = new List<string>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextScene()
    {
        /*
         *  We will have logic to decide which scene goes next but for now...
         * 
         */

        SceneManager.LoadScene("EasyLevel");
    }
}
