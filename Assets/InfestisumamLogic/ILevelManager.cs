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


interface ILevelManager
{
    public void SetDatabase(IDbConnection dbCon_);
    public int GetPathIDAtCoords(int x, int y, int z);
    public void LoadScene(Level inlevel);
    public (Path resPath, Level resLevel) ResolvePath(int pathID);
    public (Path resPath, Level resLevel) MakeSceneAtCoord(int x, int y, int z, string inDir);
    //public Level CreateLevel();

}
