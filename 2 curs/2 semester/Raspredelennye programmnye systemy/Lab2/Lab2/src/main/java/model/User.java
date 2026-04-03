package model;

import java.sql.Timestamp;

public class User {
    private final long id;
    private String name;
    private final Timestamp createdDate;
    private final Timestamp updatedDate;

    public User(long id, String name, Timestamp createdDate,
                Timestamp updatedDate) {
        this.id = id;
        this.name = name;
        this.createdDate=createdDate;
        this.updatedDate=updatedDate;
    }

    public long getId() {
        return id;
    }

    public String getName() {
        return name;
    }

    @Override
    public String toString(){
        return "User {id: %s, name: %s, created date: %s}"
                .formatted(id, name, createdDate);
    }
}
