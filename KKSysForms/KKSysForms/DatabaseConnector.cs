using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.IO;
//Webserver database (SQL Server 2000 +)
//using System.Data.SqlClient;
//Connection driver for legacy Databases (possible unused) 
//using System.Data.Odbc;
//Connection to SQL 6.5 or higher
//using System.Data.OleDb;

using System.Data.SQLite;

//using Npgsql;

//We gonna use PostgreSQL because its free -> need extern reference!
//Postgre für WEbserveranbindung
//Was mir nicht gefällt: Installation der Datenbank nötig..... SQLite?
//Geht klar, Licensierung darf aber nicht auf SQLite erfolgen - sprich copyright is nicht anmeldbar solange
//SQLite mit lizensiert wird
//Teste IF not exists anweisung
namespace KKSysDatabase
{
   
    class DatabaseConnector
    {
        public static List<KKSysForms_CardModel.Card> AddToDatabase = new List<KKSysForms_CardModel.Card>();

        private static List<byte[]> readyToAdd;

        private static List<List<byte[]>> history;

       
        //The one and only instance
        private static DatabaseConnector instance;

        private SQLiteConnection connection;

        private static SQLiteCommand command;

        private static BinaryFormatter bf;

        private static MemoryStream ms = null;

        private static SQLiteDataReader resultTable;

        //Sollten wir auf Dateisystemebene verstecken
        private const String databaseName = "KKSys.db";
        //maybe not needed
        private static String pathToDb;

        public DatabaseConnector getInstance()
        {
            if (instance == null)
            {
               
                instance = new DatabaseConnector();
                
            }
            return instance;
        }

        private DatabaseConnector()
        {

            //Create Connection
            connection = new SQLiteConnection();
            connection.ConnectionString = "Data Source=" + databaseName;
            connection.Open();

            //Init Command
            command = new SQLiteCommand(connection);
            initDatabase();

            //if no exception, initialize all objects;
            bf = new BinaryFormatter();
     


            //Benoetigt fuer die Serialisierung
            // System.IO.MemoryStream ms = new System.IO.MemoryStream();
            // BinaryFormatter bf = new BinaryFormatter();
            //  bf.Serialize(ms, test);
            // byte[] testbyte = ms.ToArray();
               

              
            
            //Creates PArameter-List for command - requires atleast one (?) inside of sql statement  
            //com.CreateParameter();
            //Required to store datatypes inside of database (like byte[])
            // SQLiteParameter data = new SQLiteParameter();
            //com.Parameters.Add(data);
            //data.Value = testbyte;
            //Used for Executes without Result table (insert, update, delete)
            //  com.ExecuteNonQuery();

            // com.CommandText = "SELECT * FROM TESTTABLE";
            //Requires for Resulttable
            //    SQLiteDataReader sqlr = com.ExecuteReader();
            //sqlr.Read();
            //Blob datatype can be easy casted to byte[]
            // byte[] testbbyte = (byte[])sqlr[1];
           
            // Test second = (Test)bf.Deserialize(ms);
            

            

            //Give Ressource free, else there can not execute anything in sql
            // sqlr.Close();
            // sqlr.Dispose();
            //  com.CommandText = "INSERT INTO TESTWITHSTRING VALUES ('HUEN')";
            //  com.ExecuteNonQuery();
            //  com.CommandText = "INSERT INTO TESTWITHSTRING VALUES ('DU HUEN')";
            
            // com.ExecuteNonQuery();


            // com.CommandText = "SELECT * FROM TESTWITHSTRING WHERE substr(dude,1,2) = 'HU'";
            // sqlr = com.ExecuteReader();
            // sqlr.Read();
  
        }


        private void initDatabase()
        {

            System.IO.StreamReader tmpRe; 
            String tmp;
            try
            {
                tmpRe = new System.IO.StreamReader("InitializeDatabase.sql");
                while ((tmp = tmpRe.ReadLine()) != null)
                {
                    command.CommandText = tmp;
                    command.ExecuteNonQuery();
                }

            }
            catch (System.IO.IOException e)
            {
                throw new Exception("Error occured by File" + e);
            }
            catch (SQLiteException e)
            {
                throw new Exception("Error in SQL-Code - Report Devs!" + e);
            }

           
        }
        //Requires FilterOption
        public void InsertIntoDatabaseCards()
        {
            if (!(readyToAdd.Count == 0))
            {
                history.Add(readyToAdd);
            }
            readyToAdd.Clear();
            

        }


        //How?
        public void InsertIntoDataBase()
        {

        }
        //How
        public void UpdateEntry()
        {

        }
        //Filter option
        public void SelectFromDatabase()
        {

        }

        

    }


}
