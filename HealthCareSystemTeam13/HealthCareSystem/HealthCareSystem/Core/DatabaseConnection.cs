using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core
{
    class DatabaseConnection
    {
        private static OleDbConnection _instance = null;

        public static OleDbConnection GetConnection()
        {
            if (_instance == null || _instance.State == ConnectionState.Closed)
            {
                _instance = new OleDbConnection();

                _instance.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=../../Data/HCDb.mdb;
                Persist Security Info=False;";

                _instance.Open();
            }

            return _instance;
        }
    }
}
