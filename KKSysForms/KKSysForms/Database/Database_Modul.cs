using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data.SQLite;

namespace KKSysForms
{
    class Database_Modul
    {
        private static SQLiteConnection Connection;

        private static Database_Modul Instance;

        private static SQLiteCommand PreparedStatement;

        private static SQLiteDataReader resultTable;

        private static String Db_name;


        public static Database_Modul getInstance()
        {
            if (Instance == null)
            {
                return new Database_Modul();
            }
            return Instance;
        }


        private Database_Modul()
        {
            Connection = new SQLiteConnection();
            Connection.ConnectionString = "Data Source=" + Db_name;
            Connection.Open();

            PreparedStatement = new SQLiteCommand(Connection);

            //Part with the config file

        }

        public void Insert(String table, String data)
        {

        }

        public void Update(String table, String where, String set) { }

        public void Delete(String table, String where) { }

        
    }
}
