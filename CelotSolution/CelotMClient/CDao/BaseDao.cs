using CelotMClient.CustomForm;
using CelotMClient.Manager;
using CelotMClient.NMSStructure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelotMClient.CDao
{
    public delegate void NotifyDataBaseFinishedHandler(object Sender, DataBaseFinishedEventArgs e);
    public class DataBaseFinishedEventArgs
    {
        public bool Succeed { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
        public MySqlConnection Connection { get; set; }
    }

    public enum QueryType
    {
        Select, Insert, update, Delete
    }
    public class BaseDao
    {
        protected BackgroundWorker worker;
        public event NotifyDataBaseFinishedHandler NotifyDataBaseFinished;
        protected object result;
        protected Type type;

        protected AlertDialog alertDialog;
        protected MySqlConnection con;
        protected MySqlCommand command;
        protected MySqlDataReader reader;

        protected QueryType queryType;
        protected bool enableObjectAutoBind;
        protected int queryCode;
        protected bool async;

        protected bool   succeed = true;
        protected String message = "";

        protected bool showProgress = false;

        public Type Type
        {
            get { return this.Type; }
            set { this.type = value; }
        }

        public int QueryCode
        {
            get { return this.queryCode; }
            set { this.queryCode = value; }
        }

        public bool EnableObjectAutoBind
        {
            get { return this.enableObjectAutoBind; }
            set { this.enableObjectAutoBind = value; }
        }

        public bool Async
        {
            get { return this.async; }
            set { this.async = value; }
        }

        public BaseDao()
        {
            this.enableObjectAutoBind = true;
            this.async = true;

            this.worker = new BackgroundWorker();
            this.worker.WorkerReportsProgress = true;
            this.worker.WorkerSupportsCancellation = true;
            this.worker.ProgressChanged += new ProgressChangedEventHandler(db_ProgressChanged);
            this.worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(db_RunWorkerCompleted);

            this.alertDialog = new AlertDialog();
            this.con = DatabaseManager.Instance().GetConnection();
            this.command = new MySqlCommand();
            this.command.Connection = this.con;
        }

        public BaseDao(NotifyDataBaseFinishedHandler handler) : this()
        {
            this.NotifyDataBaseFinished += handler;
        }

        private void db_ProgressChanged(object sender, ProgressChangedEventArgs e) { }

        private void db_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.CloseProgressDialog();
           
            if (EnableObjectAutoBind)
            {
                if (this.reader != null)
                    this.reader.Close();
                this.con.Close();
            }
            
            if (NotifyDataBaseFinished != null)
            {
                DataBaseFinishedEventArgs args = new DataBaseFinishedEventArgs();
                args.Result = this.result;
                args.Succeed = this.succeed;
                args.Message = this.message;
                this.NotifyDataBaseFinished(this, args);
            }
        }

        public void End()
        {
            //autobinding 을 하지 않을 경우, 이 메서드를 호출해서 리더와 커넥션을 닫아주어야 한다.
            if (!EnableObjectAutoBind)
            {
                this.reader.Close();
                this.con.Close();
            }
        }

        
        protected void Query()
        {
            this.showProgressDialog();
            this.worker.RunWorkerAsync();
        }

        public void RawQuery(string query)
        {
            this.command.CommandText = query;
            this.Query();
        }

        private CircularProgressForm progressForm; 

        public void showProgressDialog()
        {
            if (!this.showProgress) return;
            if (progressForm != null)
            {
                progressForm.Dispose();
                progressForm = null;
            }

            progressForm = new CircularProgressForm();
            MainForm mainForm = CelotApplication.Instance().MainForm;
            progressForm.Location = new Point(
                (mainForm.Right - mainForm.Left / 2) - (progressForm.Width/2),
                (mainForm.Bottom - mainForm.Top / 2) - (progressForm.Height/ 2));
            progressForm.Show();
        }

        public void CloseProgressDialog()
        {
            if (!this.showProgress) return;
            if (progressForm != null)
            {
                progressForm.Close();
                progressForm.Dispose();
            }
        }
    }
}
