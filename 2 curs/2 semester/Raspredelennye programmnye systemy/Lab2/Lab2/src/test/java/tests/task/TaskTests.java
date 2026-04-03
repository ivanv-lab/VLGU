package tests.task;

import DAO.TaskRepository;
import DAO.UserRepository;
import DTO.TaskCreate;
import DTO.TaskUpdate;
import DTO.UserCreate;
import model.User;
import org.junit.jupiter.api.*;
import sql.SqlWorker;

import static org.junit.jupiter.api.Assertions.*;

@TestMethodOrder(MethodOrderer.OrderAnnotation.class)
public class TaskTests {

    static SqlWorker worker=new SqlWorker(
            "postgres",
            "postgres",
            "lab_db"
    );
    static UserRepository userRepository = new UserRepository(worker);
    TaskRepository taskRepository = new TaskRepository(worker);
    static User user;

    @BeforeAll
    static void setUp(){
        user=userRepository.createUser(new UserCreate(
                "user for tasks"
        ));
    }

    @Test
    @Order(1)
    void createTaskEmptyUser(){
        assertThrows(RuntimeException.class,()->taskRepository.createTask(new TaskCreate
                ("new task","new task",100)));
    }

    @Test
    @Order(2)
    void getAllTasksEmpty(){
        assertTrue(taskRepository.getAllTasks().isEmpty());
    }

    @Test
    @Order(3)
    void getTaskEmpty(){
        assertThrows(RuntimeException.class,
                ()->taskRepository.getTask(5));
    }

    @Test
    @Order(4)
    void createTask(){
        assertEquals("new task", taskRepository
                .createTask(new TaskCreate("new task",
                        "new task",user.getId()))
                .getName());
    }

    @Test
    @Order(5)
    void getTask(){
        assertEquals("new task", taskRepository
                .getTask(2).getName());
    }

    @Test
    @Order(6)
    void getAllTasks(){
        User userForTasks=userRepository.createUser(new UserCreate("new user for tasks"));

        taskRepository.createTask(new TaskCreate(
                "new task 2","new task 2",
                userForTasks.getId()
        ));
        taskRepository.createTask(new TaskCreate(
                "new task 3","new task 3",
                userForTasks.getId()
        ));

        assertEquals(2, taskRepository.getUserTasks(userForTasks.getId())
                .size());
    }

    @Test
    @Order(7)
    void updateTask(){
        assertEquals("updated task",
                taskRepository.updateTask(2,
                        new TaskUpdate("updated task",
                                "updated task"))
                        .getName());
    }

    @Test
    @Order(8)
    void getTasksByUser(){
        assertEquals(2,
                taskRepository.getUserTasks(2)
                        .size());
    }

    @Test
    @Order(9)
    void deleteTask(){
        assertTrue(taskRepository.deleteTask(3));
        assertEquals(2, taskRepository.getAllTasks().size());
    }
}
