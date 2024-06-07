using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ServiceTechno
{
    internal class Class1
    {
        public static string DS = "DESKTOP-FJMBK7D\\DOKA", IC = "technoservice";

        public static string Users_ID = "null", Password = "null", App_Name = "Сервис";

        public static string ConnectionString = string.Format("Data Source = {0}; Initial Catalog = {1}; Integrated Security = true;", DS, IC, "; Persist Security Info = true; User ID = sa; Password = 123");

        public SqlConnection connection = new SqlConnection(ConnectionString);

        private SqlCommand command = new SqlCommand();

        public DataTable resultTable = new DataTable();

        public SqlDependency dependency = new SqlDependency();

        public enum act { select, manipulation };
        public int ExecuteQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }
        public bool loginuser(string login, string password)
        {
            string selectquery = "select count(*) as usercount from dbo.[Client] where Login=@Login and Password=@Password";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(selectquery, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int usercount = reader.GetInt32(reader.GetOrdinal("usercount"));
                            return usercount > 0;

                        }
                        return false;
                    }

                }
            }
        }



        public bool loginAdmin(string login, string password)
        {
            string selectquery = "select count(*) as usercount from dbo.[Sotrudnik] where Login=@Login and Password=@Password";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(selectquery, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int usercount = reader.GetInt32(reader.GetOrdinal("usercount"));
                            return usercount > 0;

                        }
                        return false;
                    }

                }
            }
        }

        public DataTable GetZayavka(string Buyer_Login)
        {
            DataTable datatable = new DataTable();
            string selectquery = $"select [dbo].[Zayavka], Number, Date, Problem, Device_ID, Type_ID, Client_ID, Status_ID from [dbo].[ZakazUslug]";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(selectquery, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(datatable);
                    }

                }
            }
            return datatable;
        }

        public void sqlExecute(string quety, act act)
        {
            command.Connection = connection;

            command.CommandText = quety;

            command.Notification = null;

            switch (act)
            {
                case act.select:

                    dependency.AddCommandDependency(command);

                    SqlDependency.Start(connection.ConnectionString);

                    connection.Open();

                    resultTable.Load(command.ExecuteReader());////////////////

                    connection.Close();

                    break;

                case act.manipulation:

                    connection.Open();

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }


                    connection.Close();

                    break;
            }
        }
    }
}
