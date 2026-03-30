// 🔹 RegisterViewController.js

document.addEventListener("DOMContentLoaded", () => {
    const registerForm = document.getElementById("registerForm");

    registerForm.addEventListener("submit", async function (e) {
        e.preventDefault();

        // Capturar valores del formulario
        const cedula = document.getElementById("cedula").value;
        const nombre = document.getElementById("nombre").value;
        const apellido = document.getElementById("apellido").value;
        const correo = document.getElementById("correo").value;
        const contrasena = document.getElementById("contrasena").value;
        const ConfirmarContrasena = document.getElementById("ConfirmarContrasena").value;
        const telefono = document.getElementById("telefono").value;

        // Construir objeto JSON (sin FechaRegistro, el backend la asigna)
        const nuevoUsuario = {
            Cedula: cedula,
            Nombre: nombre,
            Apellido: apellido,
            Correo: correo,
            Contrasena: contrasena,
            ConfirmarContrasena: ConfirmarContrasena,
            Telefono: telefono,
            Estado: "Activo",
            IdRol: 1 
        };

        try {
            // 🔹 URL base de tu API (no Swagger)
            const API_URL = "https://localhost:7106/api/usuario/Create";

            const response = await fetch(API_URL, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(nuevoUsuario)
            });

            if (response.ok) {
                alert(" Registro exitoso. Ahora puedes iniciar sesión.");
                window.location.href = "Acceso/Login"; // Redirige al login
            } else {
                const errorData = await response.json();
                console.error("Error en registro:", errorData);
                alert(" Error al registrar usuario. Revisa los datos.");
            }
        } catch (error) {
            console.error("Error de conexión:", error);
            alert(" No se pudo conectar con el servidor.");
        }
    });
});
