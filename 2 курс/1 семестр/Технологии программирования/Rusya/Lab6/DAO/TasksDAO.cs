using Lab6.Models;
using Npgsql;

namespace Lab6.DAO;

public class TasksDAO:DAO
{
    public List<Tasks> getTasksByCampaignId(long campaignId)
    {
        List<Tasks> tasksList = new List<Tasks>();
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string commandString = string.Format("SELECT * from tasks " +
                                                     "WHERE campaign_id='{0}'", campaignId);
                using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasksList.Add(new Tasks(
                                reader.GetFieldValue<long>(0),
                                reader.GetFieldValue<string>(1),
                                reader.GetFieldValue<string>(2),
                                reader.GetFieldValue<DateOnly>(3),
                                new TaskStatusesDAO().getStatus(reader.GetFieldValue<int>(4)),
                                reader.GetFieldValue<string>(5)
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

        return tasksList;
    }

    public List<Tasks> getTasks()
    {
        List<Tasks> tasksList = new List<Tasks>();
        try
        {
            using (NpgsqlConnection connection=new NpgsqlConnection(connectionString))
            {
                connection.Open();
                String commandString = "SELECT * from tasks";
                using (NpgsqlCommand command=new NpgsqlCommand(commandString,connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasksList.Add(new Tasks(
                                reader.GetFieldValue<long>(0),
                                reader.GetFieldValue<string>(1),
                                reader.GetFieldValue<string>(2),
                                reader.GetFieldValue<DateOnly>(3),
                                new TaskStatusesDAO().getStatus(reader.GetFieldValue<int>(4)),
                                reader.GetFieldValue<string>(5),
                                new AdvertisingCampaignDAO().getCampaign(reader.GetFieldValue<long>(6))
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

        return tasksList;
    }

    public Tasks getTask(long id)
    {
        Tasks task = new Tasks();
        try
        {
            using (NpgsqlConnection connection=new NpgsqlConnection(connectionString))
            {
                connection.Open();
                String commandString = String.Format("SELECT * from tasks " +
                                                     "WHERE id='{0}'", id);
                using (NpgsqlCommand command=new NpgsqlCommand(commandString,connection))
                {
                    using (NpgsqlDataReader reader=command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            task.id = reader.GetFieldValue<long>(0);
                            task.title = reader.GetFieldValue<string>(1);
                            task.description = reader.GetFieldValue<string>(2);
                            task.deadline = reader.GetFieldValue<DateOnly>(3);
                            task.status = new TaskStatusesDAO().getStatus(reader.GetFieldValue<int>(4));
                            task.assignedTo = reader.GetFieldValue<string>(5);
                            task.campaign = new AdvertisingCampaignDAO().getCampaign(reader.GetFieldValue<long>(6));
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

        return task;
    }

    public Tasks createTask(Tasks contract)
    {
        Tasks task = new Tasks(contract.title, contract.description,
            contract.deadline, contract.status, contract.assignedTo,
            contract.campaign);
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                String commandString = String.Format(
                    "INSERT INTO tasks (title, description, deadline, status_id, assigned_to, campaign_id) " +
                    "VALUES('{0}','{1}','{2}','{3}','{4}','{5}')",
                    task.title, task.description, task.deadline.ToString("yyyy-MM-dd"), task.status.id,
                    task.assignedTo, task.campaign.id);

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

        return task;
    }

    public Tasks updateTask(long id, Tasks task)
    {
        Tasks updatedTask = new Tasks();
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                String commandString = String.Format("UPDATE tasks " +
                                                     "SET title='{0}'," +
                                                     "description='{1}'," +
                                                     "deadline='{2}'," +
                                                     "status_id='{3}'," +
                                                     "assigned_to='{4}'," +
                                                     "campaign_id='{5}' " +
                                                     "WHERE id='{6}'",
                    task.title, task.description, task.deadline.ToString("yyyy-MM-dd"),
                    task.status.id, task.assignedTo, task.campaign.id, id);
                using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            updatedTask = getTask(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return updatedTask;
    }

    public void deleteTask(long id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                String commandString = String.Format("DELETE FROM tasks WHERE id='{0}'", id);
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