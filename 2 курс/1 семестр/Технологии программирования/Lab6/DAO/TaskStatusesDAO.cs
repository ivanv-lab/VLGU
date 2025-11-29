using Lab6.Models;
using Npgsql;

namespace Lab6.DAO;

public class TaskStatusesDAO : DAO
{
    public TaskStatuses getStatus(long statusId)
    {
        TaskStatuses status = new TaskStatuses();
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                String commandString = String.Format("SELECT * from task_statuses " +
                                                     "WHERE id='{0}'", statusId);
                using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            status.id = reader.GetFieldValue<long>(0);
                            status.name = reader.GetFieldValue<string>(1);
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

        return status;
    }

    public List<TaskStatuses> getStatuses()
    {
        List<TaskStatuses> statusesList = new List<TaskStatuses>();
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string commandString = "SELECT * from task_statuses";

                using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            statusesList.Add(
                                new TaskStatuses(
                                    reader.GetFieldValue<long>(0),
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