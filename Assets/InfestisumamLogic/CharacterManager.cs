using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public struct Character
{
    public int characterID { get; set; }
    public bool isAlive { get; set; }
    public int pathID { get; set; }
    public string lastEntryDir { get; set; }
}

public class CharacterManager : MonoBehaviour
{
    [SerializeField] LanceVrotController protag;
    IDbConnection dbCon;
    private Character character;

    public void SetDatabase(IDbConnection dbCon_)
    {
        dbCon = dbCon_;
    }

    public void LoadLastCharacter()
    {
        IDbCommand dbcmd = dbCon.CreateCommand();
        string getLastCharacter = "SELECT * FROM Character WHERE alive IS 1 ORDER BY char_Id DESC LIMIT 1";
            
        dbcmd.CommandText = getLastCharacter;
        bool characterExists = false;
        using (IDataReader dataReader = dbcmd.ExecuteReader())
        {
            if (dataReader.Read())
            {
                characterExists = true;
                character.characterID = dataReader.GetInt32(0);
                character.isAlive = Convert.ToBoolean(dataReader.GetInt32(1));
                character.pathID = dataReader.GetInt32(2);
                character.lastEntryDir = dataReader.GetString(3);
            }

        }


        if (!characterExists) MakeCharacter();
    }

    public void UpdateLocation(int pathID, string entryDirection)
    {
        character.pathID = pathID;

        IDbCommand dbcmd = dbCon.CreateCommand();
        string getLastCharacter = String.Format("UPDATE Character SET pathID = {0}, lastEntryDirection ='{1}' WHERE char_id = {2}", pathID, entryDirection, character.characterID);

        dbcmd.CommandText = getLastCharacter;
        bool characterExists = false;
        dbcmd.ExecuteNonQuery();
    }


    public int GetLocation()
    {
        return character.pathID;
    }

    public string GetLastEnteredDirection()
    {
        return character.lastEntryDir;
    }


    private void MakeCharacter()
    {
        character = new Character();
        character.characterID = -1;
        character.isAlive = true;
        character.pathID = 0;

        string makeNewChar = ("INSERT INTO Character (alive, pathID, lastEntryDirection) VALUES (1, 0, 'center')");
        IDbCommand dbcmd = dbCon.CreateCommand();

        dbcmd.CommandText = makeNewChar;
        dbcmd.ExecuteNonQuery();
        LoadLastCharacter();

            /* RETURNING doesnt work for some reason
        using (IDataReader dataReader = dbcmd.ExecuteReader())
        {
            while (dataReader.Read())
            {
                Debug.Log(dataReader.GetString(0));

            }
        }
            */


    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
