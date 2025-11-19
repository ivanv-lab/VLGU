using Lab5.Models;
using Npgsql;

namespace Lab5.DAO
{
    public class RecordsDAO:DAO
    {
        public List<Records> GetAllRecords()
        {
            connect();
            List<Records> records = new List<Records>();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(
                    "SELECT * from GuestBook", Connection);
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) {
                    records.Add(
                        new Records(
                            Convert.ToInt32(reader[0]),
                            Conver
                            )
                        );
                }
            }
        }
    }
}
