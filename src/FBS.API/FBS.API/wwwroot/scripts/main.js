const API_BASE = window.location.origin;

const authForm = document.getElementById('auth-form');
const emailGroup = document.getElementById('email-group');
const formTitle = document.getElementById('form-title');
const mainButton = document.getElementById('main-button');
const switchModeBtn = document.getElementById('switch-mode');
const messageDiv = document.getElementById('message');

const usernameInput = document.getElementById('username');
const emailInput = document.getElementById('email');
const passwordInput = document.getElementById('password');

let isLoginMode = true;

function showMessage(text, isError = false) {
    messageDiv.textContent = text;
    messageDiv.className = isError ? 'message error' : 'message success';
    messageDiv.style.display = 'block';
    setTimeout(() => {
        messageDiv.style.display = 'none';
    }, 3000);
}

function setLoading(isLoading) {
    if (isLoading) {
        mainButton.classList.add('loading');
        mainButton.disabled = true;
        mainButton.textContent = isLoginMode ? 'Вход...' : 'Регистрация...';
    } else {
        mainButton.classList.remove('loading');
        mainButton.disabled = false;
        mainButton.textContent = isLoginMode ? 'войти' : 'создать аккаунт';
    }
}

switchModeBtn.addEventListener('click', () => {
    isLoginMode = !isLoginMode;

    if (isLoginMode) {
        formTitle.textContent = "Войти в FBS";
        mainButton.textContent = "войти";
        switchModeBtn.textContent = "зарегистрироваться";
        emailGroup.style.display = "none";
        emailInput.removeAttribute('required');
        usernameInput.placeholder = "Email";  // Для логина поле = email
    } else {
        formTitle.textContent = "Регистрация в FBS";
        mainButton.textContent = "создать аккаунт";
        switchModeBtn.textContent = "уже есть аккаунт? войти";
        emailGroup.style.display = "block";
        emailInput.setAttribute('required', 'true');
        usernameInput.placeholder = "Имя пользователя";  // Для регистрации = имя
    }

    messageDiv.style.display = 'none';
    usernameInput.value = '';
    emailInput.value = '';
    passwordInput.value = '';
});

authForm.addEventListener('submit', async (e) => {
    e.preventDefault();
    messageDiv.style.display = 'none';

    if (passwordInput.value.length < 6) {
        showMessage("Пароль должен содержать минимум 6 символов", true);
        return;
    }

    const endpoint = isLoginMode ? '/api/Auth/login' : '/api/Auth/register';
    const url = `${API_BASE}${endpoint}`;

    let payload;

    if (isLoginMode) {
        // Для логина отправляем email и пароль
        payload = {
            email: usernameInput.value,
            password: passwordInput.value
        };
    } else {
        // Для регистрации отправляем userName, email, password
        if (usernameInput.value.length < 3) {
            showMessage("Имя пользователя должно содержать минимум 3 символа", true);
            return;
        }
        if (emailInput.value && !isValidEmail(emailInput.value)) {
            showMessage("Введите корректный email", true);
            return;
        }

        payload = {
            userName: usernameInput.value,  // ВАЖНО: userName, не name
            email: emailInput.value,
            password: passwordInput.value
        };
    }

    setLoading(true);

    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            },
            body: JSON.stringify(payload)
        });

        const data = await response.json();

        if (response.ok) {
            if (isLoginMode) {
                if (data.token) {
                    localStorage.setItem('jwt', data.token);
                    showMessage("✅ Вы успешно вошли в систему!");
                    setTimeout(() => {
                        window.location.href = "/dashboard.html";
                    }, 1500);
                } else {
                    showMessage("✅ Вход выполнен успешно!");
                }
            } else {
                showMessage("✅ Регистрация прошла успешно! Теперь вы можете войти.");
                setTimeout(() => {
                    switchModeBtn.click();
                }, 2000);
            }
        } else {
            const errorMsg = data.error || data.message || "Что-то пошло не так";
            showMessage(`❌ ${errorMsg}`, true);
        }
    } catch (error) {
        console.error("Ошибка сети:", error);
        showMessage("❌ Не удалось связаться с сервером.", true);
    } finally {
        setLoading(false);
    }
});

function isValidEmail(email) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}