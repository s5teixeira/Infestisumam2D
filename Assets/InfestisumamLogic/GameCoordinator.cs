using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class GameCoordinator : MonoBehaviour
{

    [SerializeField]public static string DatabaseName = ".infestisumam.save";
    IDbConnection dbcon;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("URI=file:" + Application.persistentDataPath + "/" + DatabaseName);
        dbcon = new SqliteConnection("URI=file:" + Application.persistentDataPath + "/" + DatabaseName);
        dbcon.Open();
    }


    public bool SaveGameExists()
    {
       
        IDataReader reader;

        IDbCommand dbcmd = dbcon.CreateCommand();
        string q_createTable = "SELECT name FROM sqlite_master WHERE type='table' AND name='Character' AND name='Path'";
        int tablesExpected = 2;

        dbcmd.CommandText = q_createTable;
        reader = dbcmd.ExecuteReader();

        int tableCount = 0;
        while (reader.Read())
        {
            tableCount++;
            //ReadSingleRow((IDataRecord)reader);
        }

        Debug.Log(tableCount);

        // clean up
        reader.Dispose();
        dbcmd.Dispose();

        return tableCount > 0;
    }


    public void CreateDatabases()
    {

    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
