package model.sql;

import org.postgresql.ds.PGConnectionPoolDataSource;

import javax.sql.ConnectionPoolDataSource;
import javax.sql.PooledConnection;
import java.sql.Connection;

import java.sql.SQLException;

public class SqlWorker {
    private final ConnectionPoolDataSource dataSource;

    public SqlWorker(String user, String password,
                     String dbName, int port){
        PGConnectionPoolDataSource source = new PGConnectionPoolDataSource();
        source.setServerNames(new String[]{"localhost"});
        source.setDatabaseName(dbName);
        source.setUser(user);
        source.setPassword(password);
        source.setPortNumbers(new int[]{port});

        dataSource = source;
    }

    public Connection getConnection(){
        try {
            PooledConnection pooledConnection = dataSource.getPooledConnection();
            return pooledConnection.getConnection();
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }
}
