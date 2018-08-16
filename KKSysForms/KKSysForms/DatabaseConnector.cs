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

        public static DatabaseConnector getInstance()
        {
            if (instance == null)
            {
               
                instance = new DatabaseConnector();
                
            }
            return instance;
        }

        //TODO
        //This method should load all EventLabel from database and should load all weekly and incomming events
        //Was ist, wenn die Datenbank leer ist? vorher anfragen ob eine Tabelle mit Settings existiert! TODO
        //Weniger Variablen waeren schön
        //TODO: NonRepeat and Replace
        public List<KKSysForms_Event.EventLabel> InitialCallEventLabel()
        {
            List<KKSysForms_Event.EventLabel> tmp = new List<KKSysForms_Event.EventLabel>();
            List<int> idOfEventAtPlace = new List<int>();
            command.CommandText = "SELECT * FROM EventLabel";

            resultTable = command.ExecuteReader();
            String read = "";
            while (resultTable.Read())
            {
                idOfEventAtPlace.Add(int.Parse(resultTable[0].ToString()));
                KKSysForms_Event.EventLabel eventLabel = new KKSysForms_Event.EventLabel(resultTable[1].ToString());
                tmp.Add(eventLabel);
            }
            resultTable.Close();

            if (tmp.Count != idOfEventAtPlace.Count)
            {
                throw new Exception("Das ist jetzt aber bloed");
            }
            int[] id = idOfEventAtPlace.ToArray();
            KKSysForms_Event.EventLabel[] el = tmp.ToArray();
            tmp = null;
            tmp = new List<KKSysForms_Event.EventLabel>();
            idOfEventAtPlace = null;

            byte[] serialized;
            //Inaktive Events beachten! TODO
            //TODO: Maybe try to reduce this to one loop except for 3 Loops for all kinds of Events
            for (int i = 0; i < id.Length; i++)
            {

                command.CommandText = "SELECT serialized FROM RepeatEvents WHERE LabelID =" + id[i] + ";";
                resultTable = command.ExecuteReader();
                while (resultTable.Read())
                {
                    serialized = (byte[])resultTable[0];
                    ms = new MemoryStream(serialized);
                    KKSysForms_Event.RepeatEvent tempEvent = (KKSysForms_Event.RepeatEvent)bf.Deserialize(ms);
                    el[i].addEvent(tempEvent);
                    ms.Close();
                    

                }
                tmp.Add(el[i]);
                resultTable.Close();

            }
            ms.Close();
            ms.Dispose();
            return tmp;
        }

        //TODO
        //This Method executes a Filter
        public List<KKSysForms_Event.EventLabel> FilterCallEvent(KKSysForms_Filter.Filter filter)
        {
            return null;
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

            //Testdaten einfuegen
            command.CommandText = "INSERT INTO EventLabel (nameOf) VALUES ('Stochastik');";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO EventLabel (nameOf) VALUES ('Stochastik 2');";
            command.ExecuteNonQuery();

            KKSysForms_Event.RepeatEvent testEvent = new KKSysForms_Event.RepeatEvent("Vorlesung", new KKSysForms_Event.TimeStamp(10, 30), new KKSysForms_Event.TimeStamp(12, 0), DayOfWeek.Monday, "", "");
            command.CommandText = "INSERT INTO RepeatEvents (LabelID, nameOf,serialized,DayCode) VALUES (1,?,?,?);";
            command.CreateParameter();
            SQLiteParameter sQLiteParameter = new SQLiteParameter();
            SQLiteParameter sQLiteParamete = new SQLiteParameter();
            SQLiteParameter sQLiteParamet = new SQLiteParameter();
            command.Parameters.Add(sQLiteParameter);
            command.Parameters.Add(sQLiteParamete);
            command.Parameters.Add(sQLiteParamet);
            sQLiteParameter.Value = testEvent.Name;
            ms = new MemoryStream();
            bf.Serialize(ms, testEvent);
            byte[] ser = ms.ToArray();
            sQLiteParamete.Value = ser;
            sQLiteParamet.Value = testEvent.dayCode;
            command.ExecuteNonQuery();

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

    }


}
