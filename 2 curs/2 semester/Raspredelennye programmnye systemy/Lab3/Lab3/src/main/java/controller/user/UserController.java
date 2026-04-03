package controller.user;

import model.DAO.UserRepository;
import model.DTO.UserCreate;
import model.sql.SqlWorker;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

@WebServlet("/users/*")
public class UserController extends HttpServlet {
    private UserRepository repository;

    @Override
    public void init(){
        repository=new UserRepository(
                new SqlWorker("postgres",
                        "postgres",
                        "lab_db",
                        5430));
    }

    protected void doGet(HttpServletRequest request,
                         HttpServletResponse response)
            throws ServletException, IOException {
        String pathInfo=request.getPathInfo();

        if(pathInfo==null || pathInfo.equals("/")) {
            request.setAttribute("users", repository.getAllUsers());
            getServletContext()
                    .getRequestDispatcher("/WEB-INF/jsp/user/user-list.jsp")
                    .forward(request, response);
        } else if(pathInfo.contains("/new")){
            request
                    .getRequestDispatcher("/WEB-INF/jsp/user/user-form.jsp")
                    .forward(request, response);
        }
    }

    protected void doPost(HttpServletRequest request,
                          HttpServletResponse response)
        throws ServletException, IOException {
        String pathInfo=request.getPathInfo();

        if(pathInfo.equals("/insert")){
            String name = request.getParameter("name");
            repository.createUser(new UserCreate(name));
            response.sendRedirect(request.getContextPath()+"/users");
        }
    }
}