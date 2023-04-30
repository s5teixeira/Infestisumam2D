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

    [SerializeField] List<string> sewerString = new List<string>();


    // Implementation
    public override (Path resPath, Level resLevel) MakeSceneAtCoord(int _x, int _y, int _z)
    {
        x = _x; y = _y; z = _z;
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
        if (x == 0 && y == 0 && z == 0)
        {
            // Starting level
            newLevel.levelType = "SewerRoom";
        }
        else
        {
            string[] adjacentLevels = GetAdjacentLevels(x, y, z);

            string intermediateLevelType = "SewerCorridorHorizontal";
            string sewerRoom = "SewerRoom";
            int minRoll = 1;
            int roomChance = 4;

            int verticalConseq = 0;
            int horizontalConseq = 0;

            if (adjacentLevels[0] != null) // North
            {
                intermediateLevelType = "SewerCorridorVertical";

                if (adjacentLevels[0] == "SewerRoom")
                {
                    roomChance = 10;
                }
                else if (adjacentLevels[0] == "SewerCorridorVertical")
                {
                    int conseqNorth = CalculateConsecLevels("north", adjacentLevels[0]);
                    if (conseqNorth > 2)
                    {
                        roomChance = 2;
                    }
                    else
                    {
                        roomChance = 5;
                    }
                }

            }
            else if (adjacentLevels[1] != null)  // East
            {
                intermediateLevelType = "SewerCorridorHorizontal";

                if (adjacentLevels[1] == "SewerRoom")
                {
                    roomChance = 10;
                }
                else if (adjacentLevels[1] == "SewerCorridorHorizontal")
                {

                    int conseqNorth = CalculateConsecLevels("east", adjacentLevels[1]);

                    if (conseqNorth > 2)
                    {
                        roomChance = 2;
                    }
                    else
                    {
                        roomChance = 5;
                    }
                }
            }
            else if (adjacentLevels[2] != null) // South
            {
                intermediateLevelType = "SewerCorridorVertical";

                if (adjacentLevels[2] == "SewerRoom")
                {
                    roomChance = 10;
                }
                else if (adjacentLevels[1] == "SewerCorridorVertical")
                {
                    int conseqNorth = CalculateConsecLevels("south", adjacentLevels[2]);

                    if (conseqNorth > 2)
                    {
                        roomChance = 2;
                    }
                    else
                    {
                        roomChance = 5;
                    }
                }
            }
            else if (adjacentLevels[3] != null) // West
            {
                intermediateLevelType = "SewerCorridorHorizontal";
                if (adjacentLevels[3] == "SewerRoom")
                {
                    roomChance = 10;
                }
                else if (adjacentLevels[3] == "SewerCorridorHorizontal")
                {
                    int conseqNorth = CalculateConsecLevels("west", adjacentLevels[3]);

                    if (conseqNorth > 2)
                    {
                        roomChance = 2;
                    }
                    else
                    {
                        roomChance = 5;
                    }
                }
            }

            if(rnd.Next(1, roomChance) == 1)
            {
                newLevel.levelType = "SewerRoom";
            }
            else
            {
                newLevel.levelType = intermediateLevelType;

            }
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

        string acqLevelType = levelType;
        int count = 0; 
        
        while(acqLevelType == levelType)
        {
            int pathID = -1;
            switch (direction)
            {
                case "north":
                    pathID = GetPathIDAtCoords(x, y+1, z);
                    break;

                case "east":
                    pathID = GetPathIDAtCoords(x+1, y, z);
                    break;

                case "south":
                    pathID = GetPathIDAtCoords(x, y-1, z);
                    break;

                case "west":
                    pathID = GetPathIDAtCoords(x-1, y, z);
                    break;
            }

            if (pathID != -1)
            {
                var resolved = ResolvePath(pathID);
                acqLevelType = resolved.resLevel.levelType;
                if (resolved.resLevel.levelType == levelType) count++; 
            }
            else break; // If we hit -1 means nothing exists there
            
        }
        return count;
    }

}
