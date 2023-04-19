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
        string characterCreate = "CREATE TABLE \"Character\" (\r\n\t\"character_id\"\tINTEGER,\r\n\t\"alive\"\tINTEGER NOT NULL DEFAULT 0,\r\n\t\"location_id\"\tINTEGER DEFAULT -1,\r\n\tPRIMARY KEY(\"character_id\" AUTOINCREMENT)\r\n)";
        string pathCreate = "CREATE TABLE \"Path\" (\r\n\t\"path_id\"\tINTEGER,\r\n\t\"level_id\"\tINTEGER NOT NULL,\r\n\t\"last_room\"\tINTEGER NOT NULL DEFAULT -1,\r\n\t\"entered_from\"\tTEXT,\r\n\tPRIMARY KEY(\"path_id\" AUTOINCREMENT)\r\n)";
        string levelCreate = "CREATE TABLE \"Level\" (\r\n\t\"level_id\"\tINTEGER,\r\n\t\"unity_gameobject\"\tTEXT NOT NULL,\r\n\tPRIMARY KEY(\"level_id\" AUTOINCREMENT)\r\n)";
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





    // Update is called once per frame
    void Update()
    {
        
    }
}
