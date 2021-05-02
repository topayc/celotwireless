using CelotMClient.Model;
using CelotMClient.Model.NMS;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelotMClient.Worker
{
    public class AdminDao : BaseWorker
    {
        
        public AdminDao() : base() {}
        public AdminDao(NotifyDBfinishedHandler handler) : base(handler){}
    
        public void GetAdminCommands()
        {
            this.type = Type.GetType("CelotMClient.Model.AdminCommand");
            this.queryMode = QueryMode.SelectRows;
            string commandText = 
                @"SELECT 
                    A.AdminNo AS AdminNo,
                    A.AdminGroupNo AS AdminGroupNo,
                    A.Id AS Id,
                    A.Password AS Password,
                    A.Name AS Name,
                    A.AdminRegDate As AdminRegDate,
                    AG.Name AS AdminGroupName
                  FROM admin AS A 
                  INNER JOIN admin_group AS AG 
                      ON A.AdminGroupNo = AG.AdminGroupNo
                  ORDER BY AdminNo ASC
                    ";
            this.command.Parameters.Clear();
            this.command.CommandText = commandText;
            this.Query();
        }

        public void GetAdminCommand(Admin admin)
        {
            this.type = Type.GetType("CelotMClient.Model.AdminCommand");
            this.queryMode = QueryMode.SelectRows;
            string commandText =
                @"SELECT 
                    A.AdminNo AS AdminNo,
                    A.AdminGroupNo AS AdminGroupNo,
                    A.Id AS Id,
                    A.Password AS Password,
                    A.Name AS Name,
                    A.AdminRegDate As AdminRegDate,
                    AG.Name AS AdminGroupName
                  FROM admin AS A 
                  INNER JOIN admin_group AS AG 
                      ON A.AdminGroupNo = AG.AdminGroupNo
                  where AdminNo = @AdminNo
                    ";
            this.command.CommandText = commandText;
            this.command.Parameters.Clear();
            this.command.Parameters.Add("@AdmiNo", MySqlDbType.Int32);
            this.command.Parameters["@AdminGroupNo"].Value = admin.AdminNo;
            this.Query();
        }

        public void ModifyAdmin(Admin admin)
        {
            this.queryMode = QueryMode.Update;
            this.command.Parameters.Clear();
            string commandText =
                @"UPDATE admin 
                    SET 
                        AdminGroupNo = @AdminGroupNo,
                        Id = @Id,
                        Password = @Password,
                        Name = @Name
                  where 
                    AdminNo = @AdminNo
                ";
            this.command.Parameters.Clear();
            this.command.CommandText = commandText;
            this.command.Parameters.Add("@AdminGroupNo", MySqlDbType.Int32);
            this.command.Parameters.Add("@Id", MySqlDbType.VarChar);
            this.command.Parameters.Add("@Password", MySqlDbType.VarChar);
            this.command.Parameters.Add("@Name", MySqlDbType.VarChar);
            this.command.Parameters.Add("@AdminNo", MySqlDbType.Int32);

            this.command.Parameters["@AdminGroupNo"].Value = admin.AdminGroupNo;
            this.command.Parameters["@Id"].Value = admin.Id;
            this.command.Parameters["@Password"].Value = admin.Password;
            this.command.Parameters["@Name"].Value = admin.Name;
            this.command.Parameters["@AdminNo"].Value = admin.AdminNo;
            this.Query();
        }
        public void CreateAdmin(Admin admin)
        {
            this.queryMode = QueryMode.Insert;
            this.command.Parameters.Clear();
            string commandText =
                @"INSERT INTO admin ( AdminGroupNo,Id,Password,Name,AdminRegDate) VALUES(@AdminGroupNo,@Id,@Password,@Name,@AdminRegDate)";
            this.command.Parameters.Clear();
            this.command.CommandText = commandText;
            this.command.Parameters.Add("@AdminGroupNo", MySqlDbType.Int32);
            this.command.Parameters.Add("@Id", MySqlDbType.VarChar);
            this.command.Parameters.Add("@Password", MySqlDbType.VarChar);
            this.command.Parameters.Add("@Name", MySqlDbType.VarChar);
            this.command.Parameters.Add("@AdminRegDate", MySqlDbType.VarChar);

            this.command.Parameters["@AdminGroupNo"].Value = admin.AdminGroupNo;
            this.command.Parameters["@Id"].Value = admin.Id;
            this.command.Parameters["@Password"].Value = admin.Password;
            this.command.Parameters["@Name"].Value = admin.Name;
            this.command.Parameters["@AdminRegDate"].Value = admin.AdminRegDate;
            this.Query();
        }

        public void DeleteAdmin(Admin admin)
        {
            this.command.Parameters.Clear();
            this.queryMode = QueryMode.Delete;
            string commandText = "delete from admin where AdminNo = @AdminNo";
            this.command.CommandText = commandText;
            this.command.Parameters.Add("@AdminNo", MySqlDbType.Int32);
            this.command.Parameters["@AdminNo"].Value = admin.AdminNo;
            this.Query();
        }
    }
}
