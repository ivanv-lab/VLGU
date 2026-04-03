package tests.user;

import DAO.UserRepository;
import DTO.UserCreate;
import DTO.UserUpdate;
import org.junit.jupiter.api.MethodOrderer;
import org.junit.jupiter.api.Order;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.TestMethodOrder;
import sql.SqlWorker;

import static org.junit.jupiter.api.Assertions.*;

@TestMethodOrder(MethodOrderer.OrderAnnotation.class)
public class UserTests {
    UserRepository repository = new UserRepository(
            new SqlWorker(
                    "postgres",
                    "postgres",
                    "lab_db"
            ));

    @Test
    @Order(1)
    void getEmptyUsers(){
        assertTrue(repository.getAllUsers().isEmpty());
    }

    @Test
    @Order(2)
    void getEmptyUser(){
        assertThrows(RuntimeException.class, ()->repository.getUser(1));
    }

    @Test
    @Order(3)
    void createUser(){
        assertEquals("new user", repository.createUser(new UserCreate("new user"))
                .getName());
    }

    @Test
    @Order(4)
    void getUser(){
        assertEquals("new user", repository.getUser(1)
                .getName());
    }

    @Test
    @Order(5)
    void updateUser(){
        assertEquals("updated user", repository.updateUser(1, new UserUpdate("updated user"))
                .getName());
    }

    @Test
    @Order(6)
    void getAllUsers(){
        assertEquals("updated user",repository.getAllUsers()
                .get(0).getName());
    }

    @Test
    @Order(7)
    void deleteUser(){
        assertTrue(repository.deleteUser(1));
        assertTrue(repository.getAllUsers().isEmpty());
    }
}
