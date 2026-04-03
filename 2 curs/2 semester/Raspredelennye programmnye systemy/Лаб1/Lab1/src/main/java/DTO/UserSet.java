package DTO;

import java.sql.Timestamp;

public class UserSet {
    private String name;
    private Timestamp createdDate;

    public UserSet(String name) {
        this.name = name;
        this.createdDate=new Timestamp(System.currentTimeMillis());
    }

    public String getName() {
        return name;
    }

    public Timestamp getCreatedDate() {
        return createdDate;
    }

}
