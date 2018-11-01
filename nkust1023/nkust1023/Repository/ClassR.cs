using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using nkust1023.Models;

namespace nkust1023.Repository
{
    class ClassR
    {
        public string ConnectionString
        {
            get
            {
                return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mm930\source\repos\nkust1023\nkust1023\nkust1023\data\Database1.mdf;Integrated Security=True";
            }
        }

        public List<Class1> SelectAll(string name)
        {
            var result = new List<Class1>();
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var command = new SqlCommand("", connection);
            command.CommandText = string.Format(@"
            Select id,服務分類, 資料集名稱, 主要欄位說明
            From OpenData");
            if (!string.IsNullOrEmpty(name))
                command.CommandText =
                    $"{command.CommandText} Where 資料集名稱 like N'{name}%'";
            //command.CommandText + "Where 服務分類 = N'" + name + "'";
            //command.Parameters.Add(new SqlParameter("p1", name));
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var item = new Class1();
                item.id = reader.GetInt32(0);
                item.服務分類 = reader.GetString(1);
                item.資料集名稱 = reader.GetString(2);
                item.主要欄位說明 = reader.GetString(3);
                result.Add(item);

            }

            connection.Close();
            return result;
        }
        public void Insert(Class1 item)
        {
            var newItem = item;
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var command = new SqlCommand("", connection);
            command.CommandText = string.Format(@"
            INSERT INTO OpenData(服務分類, 資料集名稱, 主要欄位說明)
            VALUES              (N'{0}', N'{1}', N'{2}')
            ", newItem.服務分類, newItem.資料集名稱, newItem.主要欄位說明);

            command.ExecuteNonQuery();

            connection.Close();
        }
        public object Update(object item)
        {
            var updateItem = item as nkust1023.Models.Class1;
            var connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            connection.Open();


            //var command = new System.Data.SqlClient.SqlCommand("", connection);
            //command.CommandText = string.Format(@"
            //UPDATE [OpenData]
            //   SET [資料集名稱] = N'{0}'
            //      ,[服務分類] = N'{1}'
            //      ,[資料集描述] = N'{2}'
            //      ,[DisplaySqe] = N'{3}'
            //WHERE ID=N'{4}'
            //        ", updateItem.服務分類, updateItem.資料集名稱, updateItem.資料集描述, updateItem.DisplaySqe, updateItem.ID);

            //command.ExecuteNonQuery();

            connection.Close();
            return item;
        }

        public void Delete(string ID)
        {
            var connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            connection.Open();

            var command = new System.Data.SqlClient.SqlCommand("", connection);
            command.CommandText = string.Format(@"
            DELETE FROM [OpenData]
            WHERE ID=N'{0}'
                    ", ID);

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}