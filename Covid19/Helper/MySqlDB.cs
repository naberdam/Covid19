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
            /*MySqlConnection conn = new MySqlConnection(this.ConnectionString);
            MySqlCommand cmd;
            string s0;
            string location_secure_file_priv = "";
            try
            {
                conn.Open();
                List<object[]> list = new List<object[]>();
                s0 = "show variables like 'secure_file_priv';";
                cmd = new MySqlCommand(s0, conn);
                using (var readerMySql = cmd.ExecuteReader())
                {
                    while (readerMySql.Read())
                    {
                        object[] lineInformationFromSQL = new object[readerMySql.FieldCount];
                        readerMySql.GetValues(lineInformationFromSQL);
                        list.Add(lineInformationFromSQL);
                    }
                }
                conn.Close();
                location_secure_file_priv = (list[0][1].ToString());
                location_secure_file_priv = location_secure_file_priv.Replace('\\', '/');
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                conn.Open();
                s0 = "CREATE DATABASE IF NOT EXISTS `hello`;";
                cmd = new MySqlCommand(s0, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                conn.Open();
                s0 = "CREATE TABLE IF NOT EXISTS hello.discounts(city VARCHAR(90) NOT NULL, latitude DOUBLE DEFAULT 32.33, lng DOUBLE DEFAULT 32.33, country VARCHAR(45) NOT NULL, id int NOT NULL, PRIMARY KEY (id)); " +
                    "LOAD DATA INFILE 'C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/worldcities.csv' INTO TABLE hello.discounts FIELDS TERMINATED BY ',' ENCLOSED BY '\"' LINES TERMINATED BY '\n' IGNORE 1 ROWS (@city, @dummy, @lat, @lng, @country, @dummy, @dummy, @dummy, @dummy, @dummy, @id) set city=@city,latitude=@lat,lng=@lng,country=@country,id=@id;"; 
                cmd = new MySqlCommand(s0, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }*/
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
