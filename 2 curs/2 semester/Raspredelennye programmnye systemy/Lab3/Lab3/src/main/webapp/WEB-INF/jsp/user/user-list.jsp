<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ page import="java.util.List" %>
<%@ page import="model.entity.User" %>
<% List<User> users = (List<User>) request.getAttribute("users"); %>

<div class="container">
    <h1>👥 Управление пользователями</h1>

    <div class="actions">
        <a href="${pageContext.request.contextPath}/users/new" class="btn btn-primary">
            ➕ Добавить пользователя
        </a>
    </div>

    <table class="data-table">
        <thead>
            <tr>
                <th>ID</th>
                <th>Имя</th>
                <th>Дата создания</th>
                <th>Дата обновления</th>
                <th>Действия</th>
            </tr>
        </thead>
        <tbody>
            <%
                if (users != null && !users.isEmpty()) {
                     for (User user : users) { %>
                         <tr>
                               <td>
                                      <span class="badge badge-id">#<%= user.getId() %></span>
                               </td>
                               <td><strong><%= user.getName() %></strong></td>
                               <td><%= user.getCreatedDate() != null ? user.getCreatedDate() : "—" %></td>
                               <td><%= user.getUpdatedDate() != null ? user.getUpdatedDate() : "—" %></td>
                         </tr>
                                <%
                                        }
                                    } else {
                                %>
                                    <tr>
                                        <td colspan="4" class="no-data">
                                            📭 Нет данных о пользователях
                                        </td>
                                    </tr>
                                <%
                                    }
                                %>
        </tbody>
    </table>
</div>