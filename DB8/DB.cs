using System.Data.SqlClient;
using System.Data;

namespace DB8
{
    class DB
    {
        private static DB _instance;
        private static SqlConnection _connection;
        private static SqlDataAdapter _servAdater, _rankAdapter, _branAdapter;

        public DataSet DataSet { get; private set; }

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
            }

            return _instance;
        }
        public DataSet SelectAllTables()
        {
            DataSet = new DataSet();

            DataTable t1 = new DataTable("Servicemans");
            DataTable t2 = new DataTable("Ranks");
            DataTable t3 = new DataTable("Branches");

            _servAdater = new SqlDataAdapter("SELECT * FROM Servicemans", _connection);
            _rankAdapter = new SqlDataAdapter("SELECT * FROM Ranks", _connection);
            _branAdapter = new SqlDataAdapter("SELECT * FROM Branches", _connection);

            _connection.Open();
            _servAdater.Fill(t1);
            _rankAdapter.Fill(t2);
            _branAdapter.Fill(t3);
            _connection.Close();

            DataSet.Tables.Add(t1);
            DataSet.Tables.Add(t2);
            DataSet.Tables.Add(t3);

            return DataSet;
        }
        public void Update()
        {
            SqlCommandBuilder builder1 = new SqlCommandBuilder(_servAdater);
            SqlCommandBuilder builder2 = new SqlCommandBuilder(_rankAdapter);
            SqlCommandBuilder builder3 = new SqlCommandBuilder(_branAdapter);

            _servAdater.AcceptChangesDuringFill = true;
            _rankAdapter.AcceptChangesDuringFill = true;
            _branAdapter.AcceptChangesDuringFill = true;

            _connection.Open();
            _servAdater.Update(DataSet, "Servicemans");
            _rankAdapter.Update(DataSet, "Ranks");
            _branAdapter.Update(DataSet, "Branches");
            _connection.Close();
        }

    }
}
