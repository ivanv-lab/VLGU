package DTO;

import java.sql.Timestamp;

public class UserCreate {
    private String name;
    private Timestamp createdDate;

    public UserCreate(String name) {
        this.name = name;
        this.createdDate = new Timestamp(System.currentTimeMillis());
    }

    public String getName() {
        return name;
    }

    public Timestamp getCreatedDate() {
        return createdDate;
    }
}
