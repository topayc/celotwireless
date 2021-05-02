using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelotMClient.Manager
{
    public class DatabaseManager
    {
        public static DatabaseManager _databaseManager;
        public DatabaseStatus DatabaseStatus { get; set; }
        public string Message { get; set; }

        private DatabaseManager() { }
        public static DatabaseManager Instance()
        {
            if (_databaseManager == null)
            {
                _databaseManager = new DatabaseManager();
            }
            return _databaseManager;
        }

        public void Init()
        {
            this.CheckConnection();
        }

        public MySqlConnection GetConnection()
        {
            ApplicationConfig config = ApplicationConfig.Instance();
            String databaseHost = config.DatabaseHost;
            string databaseName = config.DatabaseName;
            string id = config.DatabaseId;
            string pass = config.DatabasePassword;
            string charset = config.DatabaseCharset;

            MySqlConnection con = new MySqlConnection();
            string connectionString = String.Format(
                "Data Source={0};Database={1};User Id={2};Password={3};charset={4}",
                databaseHost,
                databaseName,
                id,
                pass,
                charset);
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                this.DatabaseStatus = DatabaseStatus.Connected;
            }
            catch (MySqlException e)
            {
                this.DatabaseStatus = DatabaseStatus.NotConnected;
                this.Message = e.Message;
                con = null;
            }
            return con;
        }

        public static MySqlConnection GetConnection(string host, string id, string password)
        {
            MySqlConnection con = new MySqlConnection();
            string connectionString = String.Format(
                "Data Source={0};Database={1};User Id={2};Password={3};charset={4}",
                host,
                "celot_db",
                id,
                password,
                "utf8");

            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                return con;
            }
            catch (MySqlException e)
            {
                return con;
            }
        }
    

        public bool CheckConnection(string host, string id, string password)
        {
            MySqlConnection con = new MySqlConnection();
            string connectionString = String.Format(
                "Data Source={0};Database={1};User Id={2};Password={3};charset={4}",
                host,
                "celot_db",
                id,
                password,
                "utf8");

            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                this.DatabaseStatus = DatabaseStatus.Connected;
                return true;
            }
            catch (MySqlException e)
            {
                this.DatabaseStatus = DatabaseStatus.NotConnected;
                this.Message = e.Message;
                return false;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        public bool CheckConnection()
        {
            MySqlConnection conn = this.GetConnection();
            if (conn == null)
            {
                this.DatabaseStatus = DatabaseStatus.NotConnected;
                return false;
            }
            else
            {
                conn.Close();
                this.DatabaseStatus = DatabaseStatus.Connected;
                return true;
            }
        }

        public string GetNextStatusString()
        {
            string str = "";
            switch (this.DatabaseStatus)
            {
                case DatabaseStatus.NotConnected:
                    str = "Check";
                    break;
                case DatabaseStatus.Connected:
                    str = "Check";
                    break;
            }
            return str;
        }
    }
}
