using System;

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
    class DatabaseConnector
    {
        private System.Data.SQLite.SQLiteConnection connection;

        private String pathToDb;

        private String databaseName = "Test.db";

        public DatabaseConnector()
        {
            try
            {
                connection = new SQLiteConnection();
                connection.ConnectionString = "Data Source="+databaseName;
                connection.Open();
            }
            catch (Exception e)
            {
                connection.Dispose();
                throw e;
            }
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
