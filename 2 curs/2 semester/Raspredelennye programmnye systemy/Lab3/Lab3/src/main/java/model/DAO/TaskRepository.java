package model.DAO;

import model.DTO.TaskCreate;
import model.DTO.TaskUpdate;
import model.entity.Task;
import model.sql.SqlWorker;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

public class TaskRepository {
    private final SqlWorker worker;

    public TaskRepository(SqlWorker worker) {
        this.worker = worker;
    }

    public List<Task> getAllTasks() {
        List<Task> tasks = new ArrayList<>();
        try (PreparedStatement statement = worker.getConnection()
                .prepareStatement("SELECT * FROM TASKS")) {

            ResultSet tasksRS = statement.executeQuery();

            while (tasksRS.next())
                tasks.add(mapTask(tasksRS));

            tasksRS.close();
        } catch (SQLException e) {
            throw new RuntimeException("Не удалось получить Задачи", e);
        }

        return tasks;
    }

    public Task getTask(long id) {
        Task task = null;
        try (PreparedStatement statement = worker.getConnection()
                .prepareStatement("SELECT * from tasks " +
                        "where id = ?")) {

            statement.setLong(1, id);
            ResultSet tasksRS = statement.executeQuery();

            if (tasksRS.next())
                task = mapTask(tasksRS);

            tasksRS.close();
        } catch (SQLException e) {
            throw new RuntimeException("Не удалось получить Задачу с id = " + id, e);
        }

        if (task == null)
            throw new RuntimeException("Не удалось получить Задачу с id = " + id);

        return task;
    }

    public List<Task> getUserTasks(long userId) {
        List<Task> tasks = new ArrayList<>();
        try (PreparedStatement statement = worker.getConnection()
                .prepareStatement("SELECT * FROM TASKS " +
                        "where user_id = ?")) {

            statement.setLong(1, userId);
            ResultSet tasksRS = statement.executeQuery();

            while (tasksRS.next())
                tasks.add(mapTask(tasksRS));

            tasksRS.close();
        } catch (SQLException e) {
            throw new RuntimeException("Не удалось получить Задачи для Пользователя с id = " + userId, e);
        }

        return tasks;
    }

    public Task createTask(TaskCreate task) {
        try (PreparedStatement statement = worker.getConnection()
                .prepareStatement("INSERT INTO tasks (name, text, created_date, user_id) " +
                        "VALUES (?, ?, ?, ?)", Statement.RETURN_GENERATED_KEYS)) {

            statement.setString(1, task.getName());
            statement.setString(2, task.getText());
            statement.setTimestamp(3, task.getCreatedDate());
            statement.setLong(4, task.getUserId());

            int result = statement.executeUpdate();
            if(result==0) throw new RuntimeException("Не удалось добавить Задачу");

            long newTaskId=0;
            try(ResultSet resultSet=statement.getGeneratedKeys()){
                if(resultSet.next())
                    newTaskId=resultSet.getLong(1);
            }

            return getTask(newTaskId);
        } catch (SQLException e){
            throw new RuntimeException("Не удалось создать Задачу", e);
        }
    }

    public Task updateTask(long id, TaskUpdate task){
        try(PreparedStatement statement = worker.getConnection()
                .prepareStatement("UPDATE tasks " +
                        "SET name = ?," +
                        "text = ?," +
                        "updated_date = ? " +
                        "WHERE id = ?")){

            if(getTask(id)==null)
                throw new RuntimeException("Задачи с id = "+id+" не существует");

            statement.setString(1, task.getName());
            statement.setString(2, task.getText());
            statement.setTimestamp(3, task.getUpdatedDate());
            statement.setLong(4, id);

            int result = statement.executeUpdate();
            if (result == 0) throw new RuntimeException("Не удалось обновить Задачу с id = "+id);

            return getTask(id);
        } catch (SQLException e){
            throw new RuntimeException("Не удалось обновить Задачу с id = "+id, e);
        }
    }

    public boolean deleteTask(long id){
        try(PreparedStatement statement=worker.getConnection()
                .prepareStatement("DELETE from tasks " +
                        "WHERE id = ?")){

            if(getTask(id)==null)
                throw new RuntimeException("Задачи с id = "+id+" не существует");

            statement.setLong(1, id);

            int result = statement.executeUpdate();
            return result > 0;
        } catch (SQLException e){
            throw new RuntimeException("Не удалось удалить Задачу с id = " + id, e);
        }
    }

    private Task mapTask(ResultSet resultSet){
        try{
            return new Task(
                    resultSet.getLong("id"),
                    resultSet.getString("name"),
                    resultSet.getString("text"),
                    resultSet.getTimestamp("created_date"),
                    resultSet.getTimestamp("updated_date"),
                    resultSet.getLong("user_id")
            );
        } catch (SQLException e){
            throw new RuntimeException(e);
        }
    }
}
