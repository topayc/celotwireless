using CelotMClient.CustomForm;
using CelotMClient.Manager;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelotMClient.Worker
{

    public delegate void NotifyDBfinishedHandler(object Sender, DBFinishedEventArgs e);
    public class DBFinishedEventArgs
    {
        public object Result
        {
            get;
            set;
        }

        public MySqlConnection Connection { get; set; }
    }

    public enum QueryMode
    {
        SelectRows, SelectOne, Insert, Delete, Update, Schala
    }

    public class BaseWorker
    {
        protected BackgroundWorker worker;
        public event NotifyDBfinishedHandler notifyDBfinished;
        protected object result;
        protected Type type;

        protected AlertDialog alertDialog;
        protected MySqlConnection con;
        protected MySqlCommand command;
        protected QueryMode queryMode;
        protected bool enableRowBind;


        public QueryMode QueryMode
        {
            get { return this.queryMode; }
            set { this.queryMode = value; }
        }

        public Type Type
        {
            get { return this.Type; }
            set { this.type = value; }
        }
   
        public BaseWorker()
        {
            this.enableRowBind = true;
            this.worker = new BackgroundWorker();
            this.worker.WorkerReportsProgress = true;
            this.worker.WorkerSupportsCancellation = true;
            this.worker.DoWork += new DoWorkEventHandler(db_DoWork);
            this.worker.ProgressChanged += new ProgressChangedEventHandler(db_ProgressChanged);
            this.worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(db_RunWorkerCompleted);

            this.alertDialog = new AlertDialog();
            this.con = DatabaseManager.Instance().GetConnection();
            this.command = new MySqlCommand();
            this.command.Connection = this.con;
        }

        public BaseWorker(NotifyDBfinishedHandler handler)
            : this()
        {
            this.notifyDBfinished += handler;
        }


        public bool EnableRowBind
        {
            get { return this.enableRowBind; }
            set {this.enableRowBind = value;}
        }

        protected void Query()
        {
            this.showAletDialog();
            this.worker.RunWorkerAsync();
        }

        public void RawQuery(string query)
        {
            this.queryMode = QueryMode.Insert;
            this.command.CommandText = query;
            this.Query();
        }

        protected virtual void db_DoWork(object sender, DoWorkEventArgs e)
        {

            MySqlDataReader reader;
            int affectedRows = 0;
            try
            {
                switch (this.queryMode)
                {
                    case QueryMode.Schala:
                    case QueryMode.SelectOne:
                    case QueryMode.SelectRows:
                        reader = this.command.ExecuteReader();
                        if (this.EnableRowBind)
                        {
                            var listType = typeof(List<>);
                            var constructedListType = listType.MakeGenericType(this.type);
                            IList listInstance = (IList)Activator.CreateInstance(constructedListType);

                            RowMapper mapper = new RowMapper();
                            object obj = null;
                            while (reader.Read())
                            {
                                obj = mapper.SetInstance(reader, this.type);
                                listInstance.Add(obj);
                            }
                            reader.Close();
                            //con.Close();
                            this.result = listInstance;
                        }
                        else
                        {
                            this.result = reader;
                        }
                        break;
                    case QueryMode.Delete:
                    case QueryMode.Insert:
                    case QueryMode.Update:
                        affectedRows = this.command.ExecuteNonQuery();
                        //con.Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
        }

        protected virtual void db_ProgressChanged(object sender, ProgressChangedEventArgs e){ }

        protected virtual void db_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.closeAlertDialog();
            if (this.notifyDBfinished != null)
            {
                DBFinishedEventArgs args = new DBFinishedEventArgs();
                args.Result = this.result;
                args.Connection = this.con;
                this.notifyDBfinished(this, args);
            }
        }

        public void showAletDialog()
        {
            //this.alertDialog.Show();
        }

        public void closeAlertDialog()
        {
            //this.alertDialog.Close();
        }
    } 
}
