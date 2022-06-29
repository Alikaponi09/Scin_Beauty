using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Scin_Beauty.Class
{
    class DataBase
    {
        readonly static SqlConnection sqlConnection = new SqlConnection(@"Data Source=192.168.221.12;Initial Catalog=asas;User ID=user10;Password=10");

        public DataTable Datas(string query)
        {
            DataTable dataTable = new DataTable();
            sqlConnection.Open();
            SqlCommand sqlCommands = sqlConnection.CreateCommand();
            sqlCommands.CommandText = query;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommands);
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }

        public bool Edit(string query)
        {
            sqlConnection.Open();
            SqlCommand sqlCommands = sqlConnection.CreateCommand();
            sqlCommands.CommandText = query;
            bool result = sqlCommands.ExecuteNonQuery() > 0;
            sqlConnection.Close();
            return result;
        }
    }
}
