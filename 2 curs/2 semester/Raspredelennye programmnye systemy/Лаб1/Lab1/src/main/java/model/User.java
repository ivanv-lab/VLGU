package model;

import java.sql.Timestamp;

public class User {
    private long id;
    private String name;
    private Timestamp createdDate;

    public User(long id, String name, Timestamp createdDate) {
        this.id = id;
        this.name = name;
        this.createdDate=createdDate;
    }

    public long getId() {
        return id;
    }

    public String getName() {
        return name;
    }

    public Timestamp getCreatedDate(){
        return createdDate;
    }

    @Override
    public String toString(){
        return "User {id: %s, name: %s, created date: %s}"
                .formatted(id, name, createdDate);
    }
}
