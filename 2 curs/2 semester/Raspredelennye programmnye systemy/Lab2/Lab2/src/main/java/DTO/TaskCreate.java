package DTO;

import java.sql.Timestamp;

public class TaskCreate {
    private String name;
    private String text;
    private Timestamp createdDate;
    private final long userId;

    public TaskCreate(String name, String text, long userId) {
        this.name = name;
        this.text = text;
        this.createdDate = new Timestamp(System.currentTimeMillis());
        this.userId = userId;
    }

    public String getName() {
        return name;
    }

    public String getText() {
        return text;
    }

    public Timestamp getCreatedDate() {
        return createdDate;
    }

    public long getUserId() {
        return userId;
    }
}
