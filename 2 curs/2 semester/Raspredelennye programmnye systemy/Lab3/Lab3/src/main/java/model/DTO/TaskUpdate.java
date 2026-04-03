package model.DTO;

import java.sql.Timestamp;

public class TaskUpdate {
    private String name;
    private String text;
    private Timestamp updatedDate;

    public TaskUpdate(String name, String text) {
        this.name = name;
        this.text = text;
        this.updatedDate = new Timestamp(System.currentTimeMillis());
    }

    public String getName() {
        return name;
    }

    public String getText() {
        return text;
    }

    public Timestamp getUpdatedDate() {
        return updatedDate;
    }
}
