using Lab6.Models;
using Npgsql;
using System.Data.SqlTypes;

namespace Lab6.DAO
{
    public class ClientDAO : DAO
    {
        public List<Client> getClients()
        {
            List<Client> clients = new List<Client>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString)) 
                {
                    connection.Open();
                    String commandString = "SELECT * from clients";
                    using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clients.Add(
                                    new Client(
                                        reader.GetFieldValue<long>(0),
                                        reader.GetFieldValue<string>(1),
                                        reader.GetFieldValue<string>(2),
                                        reader.GetFieldValue<string>(3),
                                        reader.GetFieldValue<string>(4),
                                        reader.GetFieldValue<string>(5)
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

            return clients;
        }

        public Client getClient(long id)
        {
            Client client = new Client();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    String commandString = string.Format("SELECT * from clients WHERE id='{0}'", id);
                    using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                client.id = Convert.ToInt64(reader[0]);
                                client.companyName = Convert.ToString(reader[1]);
                                client.contactPerson = Convert.ToString(reader[2]);
                                client.email = Convert.ToString(reader[3]);
                                client.phone = Convert.ToString(reader[4]);
                                client.address = Convert.ToString(reader[5]);
                            }

                            reader.Close();
                        }
                    }

                    if (client.id > 0)
                    {
                        client.campaigns = new AdvertisingCampaignDAO().getCampaignsByClientId(client.id);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return client;
        }

        public Client createClient(Client contract)
        {
            Client client = new Client(contract.companyName, contract.contactPerson,
                contract.email, contract.phone, contract.address);
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string commandString = String.Format(
                        "INSERT INTO clients (company_name, contact_person, email, phone, address) " +
                        "VALUES ('{0}','{1}','{2}','{3}','{4}')", client.companyName, client.contactPerson,
                        client.email, client.phone, client.address);

                    using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return client;
        }

        public Client updateClient(long id, Client client)
        {
            Client updatedClient = new Client();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    String commandString = String.Format("UPDATE clients " +
                                                         "SET company_name='{0}'," +
                                                         "contact_person='{1}'," +
                                                         "email='{2}'," +
                                                         "phone='{3}'," +
                                                         "address='{4}' " +
                                                         "WHERE id='{5}'", client.companyName, client.contactPerson,
                        client.email, client.phone, client.address, id);

                    using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                updatedClient = getClient(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return updatedClient;
        }

        public void deleteClient(long id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    String commandString = String.Format
                        ("DELETE from clients where id='{0}'", id);
                    using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Client getClientByCampaignId(long campaignId)
        {
            Client client = new Client();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    String commandString = string.Format("SELECT * from clients cl " +
                                                         "join advertising_campaign ad " +
                                                         "on cl.id=ad.client_id " +
                                                         "WHERE ad.id='{0}'", campaignId);

                    using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                client.id = Convert.ToInt64(reader[0]);
                                client.companyName = Convert.ToString(reader[1]);
                                client.contactPerson = Convert.ToString(reader[2]);
                                client.email = Convert.ToString(reader[3]);
                                client.phone = Convert.ToString(reader[4]);
                                client.address = Convert.ToString(reader[5]);
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

            return client;
        }
    }
}