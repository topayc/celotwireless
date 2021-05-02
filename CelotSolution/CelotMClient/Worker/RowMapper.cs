using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CelotMClient.Worker
{
    public class RowMapper
    {
        public object SetInstance(MySqlDataReader reader,Type type)
        {
            object obj = Activator.CreateInstance(type);
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
               // Debug.WriteLine(String.Format("Property Name {0} - Property Value {1}: ", property.Name, reader[property.Name]));
                if ( !DBNull.Value.Equals(reader[property.Name]))
                {
                    property.SetValue(obj, Convert.ChangeType(reader[property.Name], property.PropertyType));
                }
            }
            return obj;
        }
    }
}
