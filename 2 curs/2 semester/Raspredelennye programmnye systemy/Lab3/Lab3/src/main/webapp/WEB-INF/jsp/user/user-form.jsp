<%@ page contentType="text/html;charset=UTF-8" language="java" %>

<div class="container">
    <h1>${user == null ? '➕ Добавление' : '✏️ Редактирование'} пользователя</h1>

    <form action="${pageContext.request.contextPath}/users/${user == null ? 'insert' : 'update'}"
          method="post" class="form">

        <c:if test="${user != null}">
            <input type="hidden" name="id" value="${user.id}">
        </c:if>

        <div class="form-group">
            <label for="name">Имя пользователя:</label>
            <input type="text" id="name" name="name"
                   value="${user.name}" required>
        </div>

        <div class="form-actions">
            <button type="submit" class="btn btn-success">Сохранить</button>
            <a href="${pageContext.request.contextPath}/users" class="btn btn-secondary">Отмена</a>
        </div>
    </form>
</div>