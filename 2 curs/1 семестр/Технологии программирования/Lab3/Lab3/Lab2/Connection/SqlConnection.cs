using Lab2.Models;
using Npgsql;

namespace Lab2.Connection
{
    public class SqlConnection
    {
        private string connectionString = "Server=localhost;Port=5430;Database=lab3;User Id=lab3user;Password=lab3password;";

        public SqlConnection()
        {}

        public void createEntry(Good good)
        {
           NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            conn.Open();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(String.Format(
                    "INSERT INTO tb_Goods(name,description,price,count,raiting) " +
                    "VALUES ('{0}','{1}',{2},{3},{4})", good.Name, good.Description,
                    good.Price, good.Count, good.Raiting), conn);
                command.ExecuteNonQuery();
            }
            finally {  conn.Close(); }
        }

        public List<Good> readEnties()
        {
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            conn.Open();
            try
            {
                List<Good> list = new List<Good>();
                NpgsqlCommand command = new NpgsqlCommand(
                    "SELECT * from tb_Goods", conn);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Good(
                        Convert.ToInt32(reader[0]),
                        reader[1].ToString(),
                        reader[2].ToString(),
                        Convert.ToDouble(reader[3]),
                        Convert.ToInt32(reader[4]),
                        Convert.ToDouble(reader[5])));
                }
                return list;
            } finally { conn.Close(); }
        }

        public void removeEnry(int id)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            conn.Open();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(String.Format(
                    "DELETE from tb_Goods WHERE id={0}", id
                    ), conn);
                command.ExecuteNonQuery();
            }
            finally { conn.Close(); }
        }
    }
}
