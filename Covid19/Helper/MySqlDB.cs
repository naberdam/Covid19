using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Helper
{
    public class MySqlDB
    {
        private string ConnectionString { get; set; }

        public MySqlDB(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<object[]> GetSqlListWithoutParameters(string command)
        {
            List<object[]> list = new List<object[]>();
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand sqlCommand = new MySqlCommand(command, connection);
                using (var readerMySql = sqlCommand.ExecuteReader())
                {
                    while (readerMySql.Read())
                    {
                        object[] lineInformationFromSQL = new object[readerMySql.FieldCount];
                        readerMySql.GetValues(lineInformationFromSQL);
                        list.Add(lineInformationFromSQL);
                    }
                }
            }
            return list;
        }
    }
}
