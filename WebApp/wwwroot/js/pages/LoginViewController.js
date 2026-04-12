document.addEventListener("DOMContentLoaded", () => {

    // const API_URL = "https://localhost:7106/api";
    const API_URL = "https://ecommerce-w-apehakegexd0bedr.eastus-01.azurewebsites.net/api/";

    const loginForm = document.getElementById("loginForm");
    if (loginForm) {
        loginForm.addEventListener("submit", async (e) => {
            e.preventDefault();

            const usuario = {
                Correo: document.getElementById("correo").value,
                Contrasena: document.getElementById("contrasena").value
            };

            try {
                const response = await fetch(`${API_URL}/Usuario/Login`, {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify(usuario)
                });

                if (response.ok) {
                    const data = await response.json();
                    localStorage.setItem("usuario", JSON.stringify(data));

                    // Redirigir según rol
                    if (data.rol === 1)
                        window.location.href = "/Home/Index";
                    else
                        window.location.href = "/Home/Privacy";
                } else {
                    document.getElementById("mensaje").innerText = "Usuario no encontrado";
                }
            } catch (error) {
                document.getElementById("mensaje").innerText = "Error al conectar con el servidor";
            }
        });
    }
});