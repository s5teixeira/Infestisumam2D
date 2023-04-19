using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using System.Data;

public struct Path
{
    public int pathID {get; set;)

}

public struct Level
{
    public int levelID { get; set; }
    public string levelType { get; set; }
    public int northDoorState { get; set; }
    public int eastDoorState { get; set; }
    public int southDoorState { get; set; }
    public int westDoorState { get; set; }

}


public class LevelManager : MonoBehaviour
{

    public List<string> GameLevels = new List<string>();
    IDbConnection dbCon;

    public void SetDatabase(IDbConnection dbCon_)
    {
        dbCon = dbCon_;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ResolvePath(int pathID)
    {
        // Resolves level from path node
        bool isResolved = false;

        IDbCommand dbcmd = dbCon.CreateCommand();
        string getLastCharacter = "SELECT * FROM Path INNER JOIN Level ON Level.level_id = Path.level_id DESC LIMIT 1";

        dbcmd.CommandText = getLastCharacter;
        bool characterExists = false;
        using (IDataReader dataReader = dbcmd.ExecuteReader())
        {
                if (dataReader.Read())
                {
                    isResolved = true;
                    Debug.Log(dataReader.GetInt32(0));
                }

        }
        return isResolved;        
    }

    public bool MakeSceneAtCoord(int x, int y, int z, string sceneName)
    {
        IDbCommand dbcmd = dbCon.CreateCommand();
        string getLastCharacter = "INSERT INTO Path()";

        dbcmd.CommandText = getLastCharacter;


    }

    private Level CreateLevel(string sceneName)
        {
            Level lvl = new Level;
            lvl.levelType = sceneName;
            lvl = 



        }

    private Level InstallDoorsOnLevel(Level lvl)
        {

        }

    public void LoadScene()
    {
        /*
         *  We will have logic to decide which scene goes next but for now...
         * 
         */

        SceneManager.LoadScene("EasyLevel");
    }
}
