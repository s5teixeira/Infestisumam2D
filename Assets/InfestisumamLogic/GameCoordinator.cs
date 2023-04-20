using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;





public class GameCoordinator : MonoBehaviour
{

    [SerializeField]public static string DatabaseName = ".infestisumam.save";
    [SerializeField]public LevelManager levelManager;
    [SerializeField]public CharacterManager charManager;
    private bool isTransitioning = false;

    IDbConnection dbcon;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("URI=file:" + Application.persistentDataPath + "/" + DatabaseName);
        dbcon = new SqliteConnection("URI=file:" + Application.persistentDataPath + "/" + DatabaseName);
        dbcon.Open();

        if (dbcon != null && dbcon.State == ConnectionState.Open)
        {
            charManager.SetDatabase(dbcon);
            levelManager.SetDatabase(dbcon);
        }
        else
        {
            Debug.LogError("FAILED TO OPEN DATABASE  " + dbcon.State);
        }


    }

    public bool SaveGameExists()
    {
        IDbCommand dbcmd = dbcon.CreateCommand();
        string q_createTable = "SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name != 'sqlite_sequence';\r\n";
        int tablesExpected = 2;

        dbcmd.CommandText = q_createTable;
        Debug.Log("Listing tables");
        int tableCount = 0;
        using (IDataReader dataReader = dbcmd.ExecuteReader())
        {
            while (dataReader.Read())
            {
                tableCount = (int)dataReader.GetInt32(0);

            }
        }

        Debug.Log(tableCount);

        // clean up
        dbcmd.Dispose();

        return tableCount > 0;
    }

    public void CreateDatabase()
    {
        IDbCommand dbcmd = dbcon.CreateCommand();
        string characterCreate = "CREATE TABLE \"Character\" (\r\n\t\"char_id\"\tINTEGER NOT NULL,\r\n\t\"alive\"\tINTEGER NOT NULL DEFAULT 0,\r\n\t\"pathID\"\tINTEGER NOT NULL,\r\n\tPRIMARY KEY(\"char_id\" AUTOINCREMENT)\r\n)";
        string pathCreate = "CREATE TABLE \"Path\" (\r\n\t\"path_id\"\tINTEGER NOT NULL,\r\n\t\"level_id\"\tINTEGER NOT NULL,\r\n\t\"xloc\"\tINTEGER NOT NULL,\r\n\t\"yloc\"\tINTEGER NOT NULL,\r\n\t\"zloc\"\tINTEGER NOT NULL,\r\n\tPRIMARY KEY(\"path_id\" AUTOINCREMENT)\r\n)";
        string levelCreate = "CREATE TABLE \"Level\" (\r\n\t\"level_id\"\tINTEGER NOT NULL,\r\n\t\"level_type\"\tTEXT NOT NULL,\r\n\t\"north_door_state\"\tINTEGER NOT NULL,\r\n\t\"east_door_state\"\tINTEGER NOT NULL,\r\n\t\"south_door_state\"\tINTEGER NOT NULL,\r\n\t\"west_door_state\"\tINTEGER NOT NULL,\r\n\tPRIMARY KEY(\"level_id\" AUTOINCREMENT)\r\n)";
        List<string> tables = new List<string>();
        tables.Add(characterCreate);
        tables.Add(pathCreate);
        tables.Add(levelCreate);

        foreach(string tableName in tables)
        {
            dbcmd.CommandText = tableName;
            dbcmd.Prepare();
            dbcmd.ExecuteNonQuery(); 
        }

  
    }

    public void LoadLevelInformation()
    {
        charManager.LoadLastCharacter();
        bool pathResolved = levelManager.ResolvePath(charManager.GetLocation());
        Debug.Log(pathResolved + " / " + charManager.GetLocation() + " / ");
        if (!pathResolved)
        {
            Debug.LogError("FAILED TO RESOLVE LEVEL");
        }
        else
        {
            Debug.Log("LevelID: " + levelManager.GetLevel().levelID + " has been loaded from save");
        }
    }

    public void InitiateGame()
    {
        charManager.LoadLastCharacter();
        bool pathResolved = levelManager.ResolvePath(charManager.GetLocation());
        Debug.Log(pathResolved);
        if (pathResolved)
        {
            // Path resolved
            levelManager.LoadScene();
        }
        else{
            if(charManager.GetLocation() == 0)
            {
                levelManager.MakeSceneAtCoord(0, 0, 0, "EasyLevel");
                levelManager.LoadScene();
            }


        }

    }

    public void ProcessTransition(string direction)
    {
        if (!isTransitioning)
        {
            isTransitioning = true;
            
            int xModifier = 0;
            int yModifier = 0;
            string newRoomEntryDirection = "";

            switch (direction)
            {
                case "north":
                    yModifier = 1;
                    newRoomEntryDirection = "south";
                    break;

                case "east":
                    xModifier = 1;
                    newRoomEntryDirection = "west";

                    break;

                case "south":
                    yModifier = (-1);
                    newRoomEntryDirection = "north";

                    break;

                case "west":
                    xModifier = (-1);
                    newRoomEntryDirection = "east";
                    break;

            }


            Path currentPath = levelManager.GetPath();

            int pathIDAtCoords = levelManager.GetPathIDAtCoords(currentPath.locX + (xModifier), currentPath.locY + (yModifier), 0);

            //Debug.Log("Room to the " + direction + " exists " + pathIDAtCoords);
            Debug.Log(String.Format("New Location: PATHID {0}/{1}/{2}/{3}", pathIDAtCoords, currentPath.locX + (xModifier), currentPath.locY + (yModifier), 0));

            if (pathIDAtCoords == -1) {
                levelManager.MakeSceneAtCoord(currentPath.locX + (xModifier), currentPath.locY + (yModifier), 0, "EasyLevel");
                charManager.UpdateLocation(levelManager.GetPath().pathID);
                levelManager.SetDoorStatus(newRoomEntryDirection, 0); // So that we can always return
                levelManager.LoadScene();
            }
            else {
                levelManager.ResolvePath(pathIDAtCoords);
                charManager.UpdateLocation(pathIDAtCoords);

                levelManager.LoadScene();
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // GETTERS
    public Level GetCurrentLevel()
    {
        return levelManager.GetLevel();
    }

    // SETTERS

    public Path GetCurrentPath()
    {
        return levelManager.GetPath();
    }



}
