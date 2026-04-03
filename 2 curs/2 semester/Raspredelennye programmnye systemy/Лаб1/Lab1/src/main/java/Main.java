import DTO.UserSet;
import model.User;
import repository.UserRepository;
import sql.SqlWorker;

import java.util.List;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertTrue;

public class Main {
    public static void main(String[] args) {
        SqlWorker worker=new SqlWorker("postgres","postgres",
                "lab_db");

        UserRepository repository=new UserRepository(worker);

        System.out.println("Все пользователи (пусто):");
        List<User> users=repository.getAllUsers();
        assertTrue(users.isEmpty());

        User user1 = repository.createUser(new UserSet("user1"));
        User user2 = repository.createUser(new UserSet("user2"));
        User user3 = repository.createUser(new UserSet("user3"));

        System.out.println("Созданные пользователи:");
        System.out.println(user1.toString());
        System.out.println(user2.toString());
        System.out.println(user3.toString());

        User gettedUser = repository.getUser(user1.getId());
        assertEquals(user1.getId(), gettedUser.getId());

        System.out.println("Все пользователи:");
        users=repository.getAllUsers();
        for(User user:users)
            System.out.println(user.toString());

        User updatedUser = repository.updateUser(user2.getId(),new UserSet("updated name"));
        assertEquals("updated name", updatedUser.getName());
        System.out.println("Обновленный пользователь: "+ updatedUser);

        assertTrue(repository.deleteUser(user1.getId()));
        assertTrue(repository.deleteUser(user2.getId()));

        System.out.println("Удалили пользователей 1 и 2:");
        users=repository.getAllUsers();
        for(User user:users)
            System.out.println(user.toString());
    }
}
