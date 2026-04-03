<%@ page contentType="text/html;charset=UTF-8" language="java" %>

<div class="container">
    <div class="dashboard">
        <h1>Добро пожаловать в систему управления</h1>

        <div class="cards">
            <div class="card">
                <div class="card-icon">👥</div>
                <h2>Пользователи</h2>
                <p>Управление пользователями системы</p>
                <a href="${pageContext.request.contextPath}/users" class="btn btn-primary">
                    Перейти →
                </a>
            </div>
        </div>
    </div>
</div>