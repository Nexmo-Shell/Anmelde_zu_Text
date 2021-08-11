using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Anmelde_zu_Text
{
    class Datenbankzugriff
    {

        private Datenbankzugriff() { }

        private static Datenbankzugriff _dbzugriff;


        public static Datenbankzugriff GetDBZugriff()
        {
            if (_dbzugriff == null)
            {
                _dbzugriff = new Datenbankzugriff();
            }
            return _dbzugriff;
        }
        public MySqlConnection connect()
        {
            string server =" 192.168.178.53";
            string database = "DATENBANKNAME";
            string uid = "BENUTZER";
            string password = "PASSWORD";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            //SqlConnectionStringBuilder bulder = new SqlConnectionStringBuilder();
            MySqlConnection connection = new MySqlConnection(connectionString);
            return connection;

        }



    }
}

