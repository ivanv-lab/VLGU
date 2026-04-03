package repository;

import DTO.UserSet;
import model.User;
import sql.SqlWorker;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

public class UserRepository {
    private final SqlWorker worker;

    public UserRepository(SqlWorker worker) {
        this.worker = worker;
    }

    public List<User> getAllUsers() {
        List<User> users = new ArrayList<>();
        try (PreparedStatement statement = worker.getConnection()
                .prepareStatement("SELECT * FROM USERS")) {

            ResultSet usersRS = statement.executeQuery();

            while (usersRS.next()) {
                users.add(new User(usersRS.getLong(1),
                        usersRS.getString(2),
                        usersRS.getTimestamp(3)));
            }

            usersRS.close();
        } catch (SQLException e) {
            throw new RuntimeException("Не удалось получить Пользователей", e);
        }

        return users;
    }

    public User getUser(long id) {
        User user = null;
        try (PreparedStatement statement = worker.getConnection()
                .prepareStatement("SELECT * FROM USERS " +
                        "WHERE id = ?")) {

            statement.setLong(1, id);
            ResultSet usersRS = statement.executeQuery();

            if (usersRS.next())
                user = new User(usersRS.getLong(1),
                        usersRS.getString(2),
                        usersRS.getTimestamp(3));

            usersRS.close();
        } catch (SQLException e) {
            throw new RuntimeException("Не удалось получить Пользователя с id = " + id, e);
        }

        if (user == null)
            throw new RuntimeException("Не удалось получить Пользователя с id = " + id);

        return user;
    }

    public User createUser(UserSet user) {
        try (PreparedStatement statement = worker.getConnection()
                .prepareStatement("INSERT INTO users (name, created_date) " +
                        "VALUES (?, ?)", Statement.RETURN_GENERATED_KEYS)) {

            statement.setString(1, user.getName());
            statement.setTimestamp(2, user.getCreatedDate());

            int result = statement.executeUpdate();
            if (result == 0) throw new RuntimeException("Не удалось добавить Пользователя");

            long newUserId = 0;
            try (ResultSet resultSet = statement.getGeneratedKeys()) {
                if (resultSet.next())
                    newUserId = resultSet.getLong(1);
            }

            return getUser(newUserId);
        } catch (SQLException e) {
            throw new RuntimeException("Не удалось создать Пользователя", e);
        }
    }

    public User updateUser(long id, UserSet user) {
        try (PreparedStatement statement = worker.getConnection()
                .prepareStatement("UPDATE users " +
                        "SET name = ?," +
                        "created_date = ? " +
                        "where id = ?")) {

            if(getUser(id)==null)
                throw new RuntimeException("Пользвоателя с id = "+id+" не существует");

            statement.setString(1, user.getName());
            statement.setTimestamp(2, user.getCreatedDate());
            statement.setLong(3, id);

            int result = statement.executeUpdate();
            if (result == 0) throw new RuntimeException("Не удалось добавить Пользователя");

            return getUser(id);
        } catch (SQLException e) {
            throw new RuntimeException("Не удалось обновить Пользователя c id = " + id, e);
        }
    }

    public boolean deleteUser(long id) {
        try (PreparedStatement statement = worker.getConnection()
                .prepareStatement("DELETE from users " +
                        "WHERE id = ?")) {

            if(getUser(id)==null)
                throw new RuntimeException("Пользвоателя с id = "+id+" не существует");

            statement.setLong(1, id);

            int result = statement.executeUpdate();
            return result > 0;
        } catch (SQLException e) {
            throw new RuntimeException("Не удалось обновить Пользователя с id = " + id, e);
        }
    }
}
