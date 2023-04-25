using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Level newLevel = CreateLevel();




        return (newPath, newLevel);
    }


    public override Level CreateLevel()
    {
        Level newLevel = new Level();

        if (x == 0 && y == 0 && z == 0)
        {
            // Starting level
            newLevel.levelType = "SewerRoom";
            
        }


        return newLevel;
    }

    // Utility Methods
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
