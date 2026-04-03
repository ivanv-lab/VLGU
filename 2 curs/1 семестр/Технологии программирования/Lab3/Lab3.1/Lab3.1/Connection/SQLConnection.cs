using Npgsql;

namespace Lab3._1.Connection
{
    public class SQLConnection
    {
        private string connectionString = @"Initial Catalog=lab31;" +
            @"Data Source=localhost:5430;" +
            @"User ID=lab31user;" +
            @"Password=lab31password";

        NpgsqlConnection connection;
        private void createConnection()
        {
            try
            {
                connection = new NpgsqlConnection(connectionString);
                connection.Open();
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine("Ошибка при установлении соединения: " + e.Message);
                Console.ReadKey();
                return;
            }
        }
    }
}
