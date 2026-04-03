using Lab6.Models;
using Npgsql;

namespace Lab6.DAO;

public class ProjectStatusDAO : DAO
{
    public ProjectStatuses getStatus(long statusId)
    {
        ProjectStatuses status = new ProjectStatuses();
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                String commandString = String.Format("SELECT * from project_statuses " +
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

    public List<ProjectStatuses> getStatuses()
    {
        List<ProjectStatuses> statusesList = new List<ProjectStatuses>();
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string commandString = "SELECT * from project_statuses";

                using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            statusesList.Add(
                                new ProjectStatuses(
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