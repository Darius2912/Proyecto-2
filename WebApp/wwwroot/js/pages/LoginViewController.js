

document.addEventListener("DOMContentLoaded", () => {
   
    window.switchToRegister = () => {
        document.querySelector('.login-form').style.display = 'none';
        document.querySelector('.register-form').style.display = 'flex';
    };

    window.switchToLogin = () => {
        document.querySelector('.register-form').style.display = 'none';
        document.querySelector('.login-form').style.display = 'flex';
    };

    const forgotLink = document.querySelector('.forgot-password a');
    if (forgotLink) {
        forgotLink.addEventListener('click', function (e) {
            e.preventDefault();
            alert('Funcionalidad de recuperación de contraseña en desarrollo');
        });
    }

    const loginForm = document.getElementById("loginForm");
    if (loginForm) {
        loginForm.addEventListener("submit", async (e) => {
            e.preventDefault();

            const correo = document.getElementById("correo").value;
            const contrasena = document.getElementById("contrasena").value;

            try {
                // 🔹 URL de tu API para validar credenciales
                const API_URL = "https://localhost:7178/api/usuario/login";

                const response = await fetch(API_URL, {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({ Correo: correo, Contrasena: contrasena })
                });

                if (!response.ok) {
                    throw new Error("Credenciales inválidas");
                }

                 this.URL_API = "";

            } catch (error) {
                alert("❌ Error en login: " + error.message);
            }
        });
    }

    const registerForm = document.getElementById("registerFormElement");
    if (registerForm) {
        registerForm.addEventListener("submit", (e) => {
            e.preventDefault();
            alert("Registro en desarrollo");
        });
    }
});
