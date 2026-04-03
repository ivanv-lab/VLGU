package model.DTO;

import java.sql.Timestamp;

public class UserUpdate {
    private String name;
    private Timestamp updateDate;

    public UserUpdate(String name) {
        this.name = name;
        this.updateDate = new Timestamp(System.currentTimeMillis());
    }

    public String getName() {
        return name;
    }

    public Timestamp getUpdateDate() {
        return updateDate;
    }
}
