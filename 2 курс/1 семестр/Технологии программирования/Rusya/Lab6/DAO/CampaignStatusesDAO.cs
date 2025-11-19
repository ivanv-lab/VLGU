using Lab6.Models;
using Npgsql;

namespace Lab6.DAO;

public class CampaignStatusesDAO:DAO
{
    public CampaignStatuses getStatus(long statusId)
    {
        CampaignStatuses status = new CampaignStatuses();
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                String commandString = String.Format("SELECT * from campaign_statuses " +
                                                     "WHERE id='{0}'", statusId);
                using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            status.id = reader.GetFieldValue<int>(0);
                            status.name = reader.GetFieldValue<string>(1);
                        }

                        reader.Close();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return status;
    }

    public List<CampaignStatuses> getStatuses()
    {
        List<CampaignStatuses> statusesList = new List<CampaignStatuses>();
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string commandString = "SELECT * from campaign_statuses";

                using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            statusesList.Add(
                                new CampaignStatuses(
                                    reader.GetFieldValue<int>(0),
                                    reader.GetFieldValue<string>(1)
                                ));
                        }

                        reader.Close();
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return statusesList;
    }
}