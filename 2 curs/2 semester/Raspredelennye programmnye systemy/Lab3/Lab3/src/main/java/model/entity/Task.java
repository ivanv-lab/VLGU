package model.entity;

import java.sql.Timestamp;

public class Task {
    private final long id;
    private String name;
    private String text;
    private Timestamp createdDate;
    private Timestamp updatedDate;
    private final long userId;

    public Task(long id, String name, String text, Timestamp createdDate, Timestamp updatedDate, long userId) {
        this.id = id;
        this.name = name;
        this.text = text;
        this.createdDate = createdDate;
        this.updatedDate = updatedDate;
        this.userId = userId;
    }

    public String getName() {
        return name;
    }
}
