using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
namespace KKSysDatabase
{
    [Serializable]
    //Class for testing serialize
    internal class Test :ISerializable
    {
        public String text;

        public int testo;

        public Test(int testo, String text)
        {
            this.text = text;
            this.testo = testo;
        }

        public Test(SerializationInfo info, StreamingContext conext)
        {
            this.text = (String)info.GetValue("text", typeof (String));
            this.testo = (int)info.GetValue("testo", typeof(int));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("testo", testo);
            info.AddValue("text", text);
        }


       
    }

    class DatabaseConnector
    {
        private System.Data.SQLite.SQLiteConnection connection;

        private String pathToDb;

        private String databaseName = "Test.db";

        public DatabaseConnector()
        {
           
                Test test = new Test(5, "text");
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, test);
                byte[] testbyte = ms.ToArray();
                test = null;

                ms = new System.IO.MemoryStream(testbyte);
                bf = new BinaryFormatter();
                test = (Test)bf.Deserialize(ms);
               

                if (!(test.text == "text" && test.testo == 5))
                {
                    throw new Exception("Funktioniert nicht");
                }
                System.IO.Stream stream = System.IO.File.Open("Serialisierung.txt", System.IO.FileMode.Create);
                stream.Write(testbyte,0,testbyte.Length);


                connection = new SQLiteConnection();
                connection.ConnectionString = "Data Source="+databaseName;
                connection.Open();

                //Init database -> auskapseln
                System.IO.StreamReader tmpRe = new System.IO.StreamReader("InitializeDatabase.sql");
                String tmp;
                String cmd;
                while((tmp = tmpRe.ReadLine()) != null)
                {
                    cmd = tmp;
                    SQLiteCommand command = new SQLiteCommand(connection);
                    command.CommandText = cmd;
                    command.ExecuteNonQuery();
                }

                
                SQLiteCommand com = new SQLiteCommand(connection);


                string SQL = "INSERT INTO TESTTABLE (dat) VALUES (?)";

                com.CommandText = SQL;
                
            com.CreateParameter();

            SQLiteParameter data = new SQLiteParameter();
            com.Parameters.Add(data);
            data.Value = testbyte;
            com.ExecuteNonQuery();

            com.CommandText = "SELECT * FROM TESTTABLE";

                SQLiteDataReader sqlr = com.ExecuteReader();
            sqlr.Read();

           byte[] testbbyte = (byte[])sqlr[1];
            bf = new BinaryFormatter();
            ms = new System.IO.MemoryStream(testbbyte);
            Test second = (Test)bf.Deserialize(ms);
            if (second.text == test.text)
            {
                throw new Exception("Jo passt");
            }
            else
            {
                throw new Exception("Nein geht net");
            }


            sqlr.Close();
            sqlr.Dispose();
                com.CommandText = "DROP TABLE TESTTABLE";
                com.ExecuteNonQuery();
         
      
        }
        //This trhows exception
        public void insertIntoDataBase()
        {

        }

        public void updateEntry()
        {

        }

        public void selectFromDatabase()
        {

        }

        

    }


}
