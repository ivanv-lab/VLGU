using Lab5.Models;
using Npgsql;
using NpgsqlTypes;

namespace Lab5.DAO;

public class AdvertisingCampaignDAO : DAO
{
    public List<AdvertisingCampaign> getCampaignsByClientId(long clintId)
    {
        List<AdvertisingCampaign> list = new List<AdvertisingCampaign>();
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                String commandString = String.Format("SELECT * from advertising_campaign " +
                                                     "WHERE client_id='{0}'", clintId);
                using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new AdvertisingCampaign(
                                reader.GetFieldValue<long>(0),
                                reader.GetFieldValue<string>(1),
                                reader.GetFieldValue<string>(2),
                                reader.GetFieldValue<DateOnly>(3),
                                reader.GetFieldValue<DateOnly>(4),
                                reader.GetFieldValue<decimal>(5),
                                new CampaignStatusesDAO().getStatus(reader.GetFieldValue<int>(6))
                            ));
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

        return list;
    }

    public List<AdvertisingCampaign> getCampaigns()
    {
        List<AdvertisingCampaign> campaigns = new List<AdvertisingCampaign>();

        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                String commandString = "SELECT * from advertising_campaign";
                using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            campaigns.Add(
                                new AdvertisingCampaign(
                                    reader.GetFieldValue<long>(0),
                                    reader.GetFieldValue<string>(1),
                                    reader.GetFieldValue<string>(2),
                                    reader.GetFieldValue<DateOnly>(3),
                                    reader.GetFieldValue<DateOnly>(4),
                                    reader.GetFieldValue<decimal>(5),
                                    new CampaignStatusesDAO().getStatus(reader.GetFieldValue<int>(6))
                                ));
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

        return campaigns;
    }

    public AdvertisingCampaign getCampaign(long id)
    {
        AdvertisingCampaign campaign = new AdvertisingCampaign();
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                String commandString = String.Format("SELECT * from advertising_campaign " +
                                                     "WHERE id='{0}'", id);

                using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            campaign.id = reader.GetFieldValue<long>(0);
                            campaign.name = reader.GetFieldValue<string>(1);
                            campaign.description = reader.GetFieldValue<string>(2);
                            campaign.startDate = reader.GetFieldValue<DateOnly>(3);
                            campaign.endDate = reader.GetFieldValue<DateOnly>(4);
                            campaign.budget = reader.GetFieldValue<decimal>(5);
                            campaign.status =
                                new CampaignStatusesDAO().getStatus(reader.GetFieldValue<int>(6));
                            campaign.client = new ClientDAO().getClient(reader.GetFieldValue<long>(7));
                        }

                        reader.Close();
                    }
                }

                if (campaign.id > 0)
                {
                    campaign.tasks = new TasksDAO().getTasksByCampaignId(campaign.id);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return campaign;
    }

    public AdvertisingCampaign createCampaign(AdvertisingCampaign contract)
    {
        AdvertisingCampaign campaign = new AdvertisingCampaign(
            contract.name, contract.description, contract.startDate,
            contract.endDate, contract.budget, contract.status, contract.client);
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string commandString =
                    String.Format(
                        "INSERT INTO advertising_campaign(name, description, start_date, end_date, budget, status_id, client_id) " +
                        "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                        campaign.name,
                        campaign.description,
                        campaign.startDate.ToString("yyyy-MM-dd"),
                        campaign.endDate.ToString("yyyy-MM-dd"),
                        campaign.budget.ToString().Replace(',', '.'),
                        campaign.status.id,
                        campaign.client.id);

                using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return campaign;
    }

    public AdvertisingCampaign updateCampaign(long id, AdvertisingCampaign campaign)
    {
        AdvertisingCampaign updatedCampaign = new AdvertisingCampaign();
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                String commandString = string.Format("UPDATE advertising_campaign " +
                                                     "SET name='{0}'," +
                                                     "description='{1}'," +
                                                     "start_date='{2}'," +
                                                     "end_date='{3}'," +
                                                     "budget='{4}'," +
                                                     "status_id='{5}'," +
                                                     "client_id='{6}' " +
                                                     "WHERE id='{7}'",
                    campaign.name, campaign.description, campaign.startDate.ToString("yyyy-MM-dd"),
                    campaign.endDate.ToString("yyyy-MM-dd"),
                    campaign.budget.ToString().Replace(',', '.'), campaign.status.id, campaign.client.id, id);

                using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            updatedCampaign = getCampaign(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return updatedCampaign;
    }

    public void deleteCampaign(long id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                String commandString = String.Format
                    ("DELETE from advertising_campaign WHERE id='{0}'", id);
                using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}