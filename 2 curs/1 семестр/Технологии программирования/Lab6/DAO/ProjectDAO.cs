using Lab6.Models;
using Npgsql;
using NpgsqlTypes;

namespace Lab6.DAO;

public class ProjectDAO : DAO
{
    public List<Projects> getProjectsByClientId(long clintId)
    {
        List<Projects> list = new List<Projects>();
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                String commandString = String.Format("SELECT * from advertising_projects " +
                                                     "WHERE client_id='{0}'", clintId);
                using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Projects(
                                reader.GetFieldValue<long>(0),
                                reader.GetFieldValue<string>(1),
                                reader.GetFieldValue<string>(2),
                                reader.GetFieldValue<DateOnly>(3),
                                reader.GetFieldValue<DateOnly>(4),
                                reader.GetFieldValue<decimal>(5),
                                new ProjectStatusDAO().getStatus(reader.GetFieldValue<int>(6))
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

    public List<Projects> getAllProjects()
    {
        List<Projects> projects = new List<Projects>();

        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                String commandString = "SELECT * from advertising_projects";
                using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            projects.Add(
                                new Projects(
                                    reader.GetFieldValue<long>(0),
                                    reader.GetFieldValue<string>(1),
                                    reader.GetFieldValue<string>(2),
                                    reader.GetFieldValue<DateOnly>(3),
                                    reader.GetFieldValue<DateOnly>(4),
                                    reader.GetFieldValue<decimal>(5),
                                    new ProjectStatusDAO().getStatus(reader.GetFieldValue<int>(6))
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

        return projects;
    }

    public Projects getProject(long id)
    {
        Projects project = new Projects();
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                String commandString = String.Format("SELECT * from advertising_projects " +
                                                     "WHERE id='{0}'", id);

                using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            project.id = reader.GetFieldValue<long>(0);
                            project.name = reader.GetFieldValue<string>(1);
                            project.description = reader.GetFieldValue<string>(2);
                            project.startDate = reader.GetFieldValue<DateOnly>(3);
                            project.endDate = reader.GetFieldValue<DateOnly>(4);
                            project.budget = reader.GetFieldValue<decimal>(5);
                            project.status =
                                new ProjectStatusDAO().getStatus(reader.GetFieldValue<int>(6));
                            project.client = new ClientDAO().getClient(reader.GetFieldValue<long>(7));
                        }

                        reader.Close();
                    }
                }

                if (project.id > 0)
                {
                    project.tasks = new TasksDAO().getTasksByProjectId(project.id);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return project;
    }

    public Projects createProject(Projects contract)
    {
        Projects project = new Projects(
            contract.name, contract.description, contract.startDate,
            contract.endDate, contract.budget, contract.status, contract.client);
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string commandString =
                    String.Format(
                        "INSERT INTO advertising_projects(name, description, start_date, end_date, budget, status_id, client_id) " +
                        "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                        project.name,
                        project.description,
                        project.startDate.ToString("yyyy-MM-dd"),
                        project.endDate.ToString("yyyy-MM-dd"),
                        project.budget.ToString().Replace(',', '.'),
                        project.status.id,
                        project.client.id);

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

        return project;
    }

    public Projects updateProject(long id, Projects project)
    {
        Projects updatedProject = new Projects();
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                String commandString = string.Format("UPDATE advertising_projects " +
                                                     "SET name='{0}'," +
                                                     "description='{1}'," +
                                                     "start_date='{2}'," +
                                                     "end_date='{3}'," +
                                                     "budget='{4}'," +
                                                     "status_id='{5}'," +
                                                     "client_id='{6}' " +
                                                     "WHERE id='{7}'",
                    project.name, project.description, project.startDate.ToString("yyyy-MM-dd"),
                    project.endDate.ToString("yyyy-MM-dd"),
                    project.budget.ToString().Replace(',', '.'), project.status.id, project.client.id, id);

                using (NpgsqlCommand command = new NpgsqlCommand(commandString, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            updatedProject = getProject(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return updatedProject;
    }

    public void deleteProject(long id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                String commandString = String.Format
                    ("DELETE from advertising_projects WHERE id='{0}'", id);
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