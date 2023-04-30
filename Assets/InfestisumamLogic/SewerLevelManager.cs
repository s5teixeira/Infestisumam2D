using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class SewerLevelManager : LevelManager
{
    private string nextScene = "";
    private int x = -1;
    private int y = -1;
    private int z = -1;
    private string inDirection = "";

    [SerializeField] List<string> sewerString = new List<string>();


    // Implementation
    public override (Path resPath, Level resLevel) MakeSceneAtCoord(int _x, int _y, int _z, string _inDirection = "default")
    {
        x = _x; y = _y; z = _z; inDirection = _inDirection ;
        Path newPath = new Path();
        //Level newLevel = new Level();
        Level newLevel = CreateLevel();
        Debug.Log("DDDDDDD " + newLevel.levelType);

        
        IDbCommand dbcmd = dbCon.CreateCommand();
        string createLevelCommand = String.Format("INSERT INTO Level(level_type, north_door_state, east_door_state, south_door_state, west_door_state) VALUES('{0}', 0,0,0,0) RETURNING level_id", newLevel.levelType);
        Debug.Log(createLevelCommand);
        dbcmd.CommandText = createLevelCommand;

        using (IDataReader dataReader = dbcmd.ExecuteReader())
        {
            while (dataReader.Read())
            {
                newLevel.levelID = dataReader.GetInt32(0);
            }
            dataReader.Close();

        }

        newPath.locX = x;
        newPath.locY = y;
        newPath.locZ = z;
        newLevel.levelID = newLevel.levelID;

        string createPathCommand = String.Format("INSERT INTO Path(level_id, xloc, yloc, zloc) VALUES({0}, {1},{2},{3}) RETURNING path_id", newLevel.levelID, x, y, z);
        Debug.Log(createPathCommand);
        dbcmd.CommandText = createPathCommand;

        using (IDataReader dataReader = dbcmd.ExecuteReader())
        {
            while (dataReader.Read())
            {
                newPath.pathID = dataReader.GetInt32(0);
            }
            dataReader.Close();
        }

        
        return (newPath, newLevel);
    }


    public override Level CreateLevel()
    {
        Level newLevel = new Level();
        System.Random rnd = new System.Random();
        string corridorLevelName = "SewerCorridorVertical";
        string roomName = "SewerRoom";


        if (inDirection == "default")
        {
            newLevel.levelType = roomName;
            return newLevel;
        }



        if(inDirection == "north" || inDirection == "south")
        {
            corridorLevelName = "SewerCorridorVertical";

        }
        else if(inDirection == "east" || inDirection == "west")
        {
             corridorLevelName = "SewerCorridorHorizontal";

        }

        int consecRooms = 0;

        switch (inDirection)
        {   // we want to know number of corridors in the opposite direction
            case "north":
                consecRooms = CalculateConsecLevels("south", corridorLevelName);
                //Debug.LogWarning("DDDDDDDDDDDDDDDDDDDDDD " + consecRooms);
                break;

            case "east":
                consecRooms = CalculateConsecLevels("west", corridorLevelName);

                break;

            case "south":
                consecRooms = CalculateConsecLevels("north", corridorLevelName);

                break;

            case "west":
                consecRooms = CalculateConsecLevels("east", corridorLevelName);

                break;
        }

        int maxRoomChance = 4;


        if (consecRooms < 2)
        {
            maxRoomChance = 5;

        }else if(consecRooms > 2)
        {
            maxRoomChance = 3;
        }

        if(rnd.Next(1, maxRoomChance) == 1)
        {
            newLevel.levelType = roomName;
        }
        else
        {
            newLevel.levelType = corridorLevelName;
        }



        return newLevel;
    }



    // Utility Methods
    
    private int RoomChance()
    {
        return 0;
    }
    
    
    private int CalculateConsecLevels(string direction, string levelType)
    
    {
        /*
         *  Calculates number of levels in given direction
         */
        Debug.LogWarning("=========================");
        string acqLevelType = levelType;
        int count = 0;
        int cycleCount = 1;
        
        while(acqLevelType == levelType)
        {
            int pathID = -1;

            switch (direction)
            {
                case "north":
                    pathID = GetPathIDAtCoords(x, y+cycleCount, z);
                    break;

                case "east":
                    pathID = GetPathIDAtCoords(x+ cycleCount, y, z);
                    break;
                case "south":
                    pathID = GetPathIDAtCoords(x, y- cycleCount, z);
                    break;
                case "west":
                    pathID = GetPathIDAtCoords(x- cycleCount, y, z);
                    break;


            }
            Debug.LogWarning("> PATH ID : " + pathID);
            if (pathID == -1) return count;

            var resolvedDetails = ResolvePath(pathID);

            acqLevelType = resolvedDetails.resLevel.levelType;
            Debug.LogWarning("HEHEHEHEHHEHE");
            Debug.LogError("COUNT " + cycleCount + " / " + levelType + " / " + resolvedDetails.resLevel.levelType);
            if (acqLevelType == levelType) count++;
            cycleCount++;


        }


        return count;
    }

}
