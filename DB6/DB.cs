using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB6
{
    class DB
    {
        private static DB _instance;
        private static SqlConnection _connection;

        private DB() { }

        public void Close()
        {
            _connection.Close();
        }

        public static DB GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DB();
                _connection = new SqlConnection(@"Data Source = DESKTOP-K7ULRCG; integrated security = true; database = Military_District;");
                _connection.Open();
            }

            return _instance;
        }

        public void SelectIntoGrid(string query, DataGridView grid)
        {
            SqlCommand command = new SqlCommand(query, _connection);
            SqlDataReader reader = command.ExecuteReader();

            grid.ColumnCount = reader.FieldCount;
            grid.RowCount = 2;

            for (int column = 0; column < reader.FieldCount; column++)
            {
                grid.Columns[column].HeaderText = reader.GetName(column);
            }

            int row = 0;
            while (reader.Read())
            {
                grid.RowCount++;
                for (int column = 0; column < reader.FieldCount; column++)
                {
                    grid[column, row].Value = reader[column].ToString();
                }
                row++;
            }
            grid.RowCount--;

            reader.Close();
        }

        public int RunQuery(string query)
        {
            SqlCommand command = new SqlCommand(query, _connection);
            return command.ExecuteNonQuery();
        }

        public string MaxAge()
        {
            SqlCommand command = new SqlCommand("select Servicemans.First_Name,Servicemans.Last_Name, max(Servicemans.Age) as 'Макс. воз-ст' from Servicemans group by Servicemans.First_Name, Servicemans.Last_Name,Servicemans.Pathronymic having Servicemans.First_Name='Иван'", _connection);
            SqlDataReader reader = command.ExecuteReader();

            string result = "Не удалось выполнить...";

            if (reader.Read())
            {
                result = "Макс возраст: " + reader["Макс. воз-ст"].ToString();
            }
            reader.Close();

            return result;
        }

        public string ServicemansAges()
        {
            string query = "select Servicemans.Age,count(Servicemans.Age) as 'Количество' from Servicemans group by Servicemans.Age";
            SqlCommand command = new SqlCommand(query, _connection);
            SqlDataReader reader = command.ExecuteReader();

            string result = "";

            while (reader.Read())
            {
                result += "Возраст "+reader["Age"].ToString() + ": " + reader["Количество"].ToString() + "\n";
            }
            reader.Close();

            return result;
        }

        public Serviceman GetServiceman(int id)
        {
            SqlCommand command = new SqlCommand($"SELECT * FROM Servicemans WHERE Id_Serviceman = {id}", _connection);
            SqlDataReader reader = command.ExecuteReader();
            Serviceman result = null;

            if (reader.Read())
            {
                result = new Serviceman()
                {
                    Id_Serviceman = (int)reader["Id_Serviceman"],
                    Last_Name = reader["Last_Name"].ToString(),
                    First_Name = reader["First_Name"].ToString(),
                    Pathronymic = reader["Pathronymic"].ToString(),
                    Age = (int)reader["Age"],
                    Id_rank = (int)reader["Id_rank"],
                    Branches_id_Branch = (int)reader["Branches_id_Branch"]
                };
            }

            reader.Close();
            return result;
        }

        public string GetRank(int id)
        {
            SqlCommand command = new SqlCommand($"SELECT * FROM Ranks WHERE Id_rank = {id}", _connection);
            SqlDataReader reader = command.ExecuteReader();
            string result = null;

            if (reader.Read())
            {
                result = reader["Name_of_rank"].ToString();
            }

            reader.Close();
            return result;
        }

        public string GetBranch(int id)
        {
            SqlCommand command = new SqlCommand($"SELECT * FROM Branches WHERE Id_Branch = {id}", _connection);
            SqlDataReader reader = command.ExecuteReader();
            string result = null;

            if (reader.Read())
            {
                result = reader["Branch_Name"].ToString();
            }

            reader.Close();
            return result;
        }
    }

    public class Serviceman
    {
        public string Last_Name;
        public string First_Name;
        public string Pathronymic;
        public int Age;
        public int Id_Serviceman;
        public int Id_rank;
        public int Branches_id_Branch;
    }

}
