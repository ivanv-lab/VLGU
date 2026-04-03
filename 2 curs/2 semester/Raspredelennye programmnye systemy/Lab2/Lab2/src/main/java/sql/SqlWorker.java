package sql;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

public class SqlWorker {
    private static final String CONNECTION_STRING="jdbc:postgresql://localhost:5430/%s";
    private final Connection connection;

    public SqlWorker(String user, String password, String db){
        try {
            connection = DriverManager
                    .getConnection(CONNECTION_STRING.formatted(db),
                            user,
                            password);
        } catch (SQLException e) {
            throw new RuntimeException("Не удалось подключиться к БД",e);
        }
    }

    public Connection getConnection(){
        return connection;
    }
}
