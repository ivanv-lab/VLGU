using Npgsql;

namespace Lab5.DAO
{
    public class DAO
    {
         private const string connectionString = 
            "Server=localhost;Port=5430;Database=lab5;User Id=lab5user;Password=lab5password;";

        protected NpgsqlConnection Connection {  get; set; }

        public void connect()
        {
            Connection = new NpgsqlConnection(connectionString);
            Connection.Open();
        }

        public void disconnect()
        {
            Connection.Close();
        }

    }
}
