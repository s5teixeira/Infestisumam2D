using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using System.Data;
using System;

public struct Path
{
    public int pathID { get; set; }
    public int levelID { get; set; }
    public int locX { get; set; }
    public int locY { get; set; }
    public int locZ { get; set; }

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
    Level level;
    Path path;

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
        string getPathAndLevel = "SELECT * FROM Path INNER JOIN Level ON Level.level_id = Path.level_id WHERE Path.path_id = " + pathID + " ORDER BY Path.path_id DESC LIMIT 1";
        Debug.Log(getPathAndLevel);
        dbcmd.CommandText = getPathAndLevel;
        Level resolvedLevel = new Level();
        Path resolvedPath = new Path();

        using (IDataReader dataReader = dbcmd.ExecuteReader())
        {
            while (dataReader.Read())
            {
                isResolved = true;
                //Debug.Log(dataReader.GetInt32(0));
                resolvedPath.pathID = dataReader.GetInt32(0);
                resolvedPath.levelID = dataReader.GetInt32(1);
                resolvedPath.locX = dataReader.GetInt32(2);
                resolvedPath.locY = dataReader.GetInt32(3);
                resolvedPath.locZ = dataReader.GetInt32(4);

                resolvedLevel.levelID = dataReader.GetInt32(5);
                resolvedLevel.levelType = dataReader.GetString(6);
                resolvedLevel.northDoorState = dataReader.GetInt32(7);
                resolvedLevel.eastDoorState = dataReader.GetInt32(8);
                resolvedLevel.southDoorState = dataReader.GetInt32(9);
                resolvedLevel.westDoorState = dataReader.GetInt32(10);
            }


        }

        Debug.LogError("RESOLVED PAHT  " + resolvedPath.pathID);

        if (isResolved)
        {
            level = resolvedLevel;
            path = resolvedPath;
        }

        return isResolved;        
    }


    public bool MakeSceneAtCoord(int x, int y, int z, string sceneName)
    {
        IDbCommand dbcmd = dbCon.CreateCommand();
        Level newLevel = CreateLevel(sceneName);
        string addLevel = String.Format("INSERT INTO Level(level_type, north_door_state, east_door_state, south_door_state, west_door_state) VALUES('{0}', {1}, {2}, {3}, {4}) RETURNING level_id", 
                                        newLevel.levelType, newLevel.northDoorState, newLevel.eastDoorState, newLevel.southDoorState, newLevel.westDoorState);
        dbcmd.CommandText = addLevel;
        int levelID = -1;
        using (IDataReader dataReader = dbcmd.ExecuteReader())
        {
            while (dataReader.Read())
            {
                levelID = dataReader.GetInt32(0);
            }


        }

        Debug.Log("LEVEL ID " + levelID);
        if (levelID == -1)
        {
            return false;
        }

        level = newLevel;
        level.levelID = levelID;
        string addPath = String.Format("INSERT INTO Path(level_id, xloc, yloc, zloc) VALUES({0}, {1}, {2}, {3}) RETURNING path_id", level.levelID, x, y, z);
        dbcmd.CommandText = addPath;
        int pathID = -1;
        using (IDataReader dataReader = dbcmd.ExecuteReader())
        {
            if (dataReader.Read())
            {
                pathID = dataReader.GetInt32(0);
            }


        }
        Debug.Log("Path ID: " + pathID);
        if (pathID == -1)
        {
            return false;
        }
        path.pathID = level.levelID;
        path.locX = x;
        path.locY = y;
        path.locZ = z;
        return true;
    }

    public int GetPathIDAtCoords(int x, int y, int z)
    {
        // Returns -1 if no lavel exists there
        bool roomExists = false;
        IDbCommand dbcmd = dbCon.CreateCommand();

        string getPath = String.Format("SELECT * FROM Path WHERE xloc = {0} AND yloc = {1} AND zloc = {2}", x, y, z);
        dbcmd.CommandText = getPath;
        int pathID = -1;
        using (IDataReader dataReader = dbcmd.ExecuteReader())
        {
            if (dataReader.Read())
            {
                bool pathExists = true;
                pathID = dataReader.GetInt32(0);
            }


        }
        Debug.LogError(" ID AT COORD  " + pathID);
        return pathID;
    }


    private Level CreateLevel(string sceneName)
        {
            Level lvl = new Level { };
            lvl.levelType = sceneName;
            lvl = InstallDoorsOnLevel(lvl);


        return lvl;
        }

    private Level InstallDoorsOnLevel(Level lvl)
    {
        int doorsInstalled = 0;
        int[] doorStates = {1, 1, 1, 1}; // 0 is door closed; 1 is open
        for(int i=0; i < 4; i++)
        {
            bool shouldThisDoorLock = false;
            if (doorsInstalled == 0)
            {
                shouldThisDoorLock = (UnityEngine.Random.Range(1.0f, 2.0f) < 2.0f);

            }
            else if(doorsInstalled > 0 && doorsInstalled < 3)
            {
                shouldThisDoorLock = (UnityEngine.Random.Range(1.0f, 3.0f)  < 2.0f);

            }else if (doorsInstalled > 2 && doorsInstalled <=4)
            {
                shouldThisDoorLock = (UnityEngine.Random.Range(1.0f, 7.0f) < 3.0f);
            }

            if (shouldThisDoorLock)
            {
                doorStates[i] = 0;
                doorsInstalled++;
            }

        }

        if(doorsInstalled < 2)
        {
            // No door were installed then select a random door
            doorStates[Convert.ToInt32(UnityEngine.Random.Range(0.0f, 3.0f))] = 0;

        }

        lvl.northDoorState = doorStates[0];
        lvl.eastDoorState = doorStates[1];
        lvl.southDoorState = doorStates[2];
        lvl.westDoorState = doorStates[3];

        return lvl;
    }



    // Getters
    public Level GetLevel()
    {
        return level;
    }

    public Path GetPath()
    {
        return path;
    }

    public void SetDoorStatus(string direction, int status)
    {
        switch (direction)
        {
            case "north":
                level.northDoorState = status;

                break;

            case "east":
                level.northDoorState = status;
                break;

            case "south":
                level.southDoorState = status;
                break;

            case "west":
                level.westDoorState = status;
                break;

        }

        IDbCommand dbcmd = dbCon.CreateCommand();

        string getLastCharacter = String.Format("UPDATE Level SET {0}_door_state = {1} WHERE level_id = {2}", direction, status, level.levelID);

        dbcmd.CommandText = getLastCharacter;
        bool characterExists = false;
        dbcmd.ExecuteNonQuery();
    }


    public void LoadScene()
    {
        /*
         *  We will have logic to decide which scene goes next but for now...
         * 
         */

        SceneManager.LoadScene(level.levelType);
    }



}

