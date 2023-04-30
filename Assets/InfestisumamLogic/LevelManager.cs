using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine;

public abstract class LevelManager : MonoBehaviour, ILevelManager
{

    public IDbConnection dbCon;

    public void SetDatabase(IDbConnection dbCon_)
    {
        dbCon = dbCon_;
    }

    public (Path resPath, Level resLevel) ResolvePath(int pathID)
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
            // level = resolvedLevel;
            // path = resolvedPath;
            // resPath = resolvedPath;
            // resLevel = resolvedLevel;

            return (resolvedPath, resolvedLevel);
        }
        else
        {
            return (new Path(), new Level());
        }
    }

    public void LoadScene(Level inlevel)
    {
        /*
         *  We will have logic to decide which scene goes next but for now...
         * 
         */

        SceneManager.LoadScene(inlevel.levelType);
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

    public string[] GetAdjacentLevels(int x, int y, int z)
    {
        string[] levels = new string[4];

        int northPathID = GetPathIDAtCoords(x, y + 1, z);
        int eastPathID = GetPathIDAtCoords(x + 1, y, z);
        int southPathID = GetPathIDAtCoords(x, y - 1, z);
        int westPathID = GetPathIDAtCoords(x - 1, y, z);

       

        if (northPathID != -1)
        {
            var res = ResolvePath(northPathID);
            levels[0] = res.resLevel.levelType; 

        }

        if (southPathID != -1)
        {
            var res = ResolvePath(southPathID);
            levels[2] = res.resLevel.levelType;
        }
        if (eastPathID != -1)
        {
            var res = ResolvePath(eastPathID);
            levels[1] = res.resLevel.levelType;
        }

        if (westPathID != -1)
        {
            var res = ResolvePath(westPathID);
            levels[3] = res.resLevel.levelType;
        }


        return levels;
    }

    public abstract (Path resPath, Level resLevel) MakeSceneAtCoord(int x, int y, int z, string inDirection);
    public abstract Level CreateLevel();
}
