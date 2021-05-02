using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelotMClient.Worker
{
    public class AdminGroupDao : BaseWorker
    {
       public AdminGroupDao() : base() 
        {
        }

        public AdminGroupDao (NotifyDBfinishedHandler handler)
            : base(handler)
        {
        }

        public void getAdminGroups()
        {
            this.type = Type.GetType("CelotMClient.Model.AdminGroup");
            this.queryMode = QueryMode.SelectRows;
            string commandText = 
                @"SELECT 
                    *
                  FROM admin_group
                  ORDER BY AdminGroupNo ASC
                ";
            this.command.CommandText = commandText;
            this.Query();
        }
    }
}
