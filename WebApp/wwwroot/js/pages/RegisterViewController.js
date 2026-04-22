
function RegisterViewController() {
    this.ViewName = "Register";
    this.API_ControllerName = "Usuario";


    this.InitView = function () {
        $("#btnSubmit").click(function () {
            var rc = new RegisterViewController();
            rc.Create();
        })

    }
    //metodos
    this.Create = function () {
        // Capturar valores del formulario
        const cedula = document.getElementById("cedula").value;
        const nombre = document.getElementById("nombre").value;
        const apellido = document.getElementById("apellido").value;
        const correo = document.getElementById("correo").value;
        const contrasena = document.getElementById("contrasena").value;
        const ConfirmarContrasena = document.getElementById("ConfirmarContrasena").value;
        const telefono = document.getElementById("telefono").value;

        // limpiar errores antes
        document.querySelectorAll("small").forEach(x => x.innerText = "");
     

        let errores = false;
        // expresiones
        let expresionesReEmail = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
      
        const soloLetras = /^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$/;
        const soloNumeros = /^[0-9]+$/;

        // CEDULA
        if (cedula === "") {
            document.getElementById("error-cedula").innerText = "La cédula es obligatoria*";
            errores = true;
        }

        else if (!soloNumeros.test(cedula)) {
            document.getElementById("error-cedula").innerText = "La cedula solo puede contener numeros*";
            errores = true;
        }





        // NOMBRE
        if (nombre === "") {
            document.getElementById("error-nombre").innerText = "El nombre es obligatorio*";
            errores = true;
        } else if (!soloLetras.test(nombre)) {
            document.getElementById("error-nombre").innerText = "El nombre solo debe contener letras*";
            errores = true;
        }

        // APELLIDO
        if (apellido === "") {
            document.getElementById("error-apellido").innerText = "El apellido es obligatorio*";
            errores = true;
        } else if (!soloLetras.test(apellido)) {
            document.getElementById("error-apellido").innerText = "El apellido solo debe contener letras*";
            errores = true;
        }

        // CORREO
        if (!expresionesReEmail.test(correo)) {
            document.getElementById("error-correo").innerText = "Correo inválido*";
            errores = true;
        }

        // CONTRASEÑA
        if (contrasena.length < 6) {
            document.getElementById("error-contrasena").innerText = "Mínimo 6 caracteres*";
            errores = true;
        }

        // CONFIRMAR CONTRASEÑA
        if (contrasena !== ConfirmarContrasena) {
            document.getElementById("error-confirmar").innerText = "Las contraseñas no coinciden*";
            errores = true;
        }

        // TELÉFONO
        if (telefono === "") {
            document.getElementById("error-telefono").innerText = "El teléfono es obligatorio*";
            errores = true;
        } else if (!soloNumeros.test(telefono)) {
            document.getElementById("error-telefono").innerText = "El teléfono solo debe contener números*";
            errores = true;
        }



        if (errores) return;

     
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

        //Enviar al api
        var ca = new ControlActions();
        var urlEndPoint = this.API_ControllerName + "/Create";
        ca.PostToAPI(urlEndPoint, nuevoUsuario, function () {

            Swal.fire({
                title: "Éxito",
                text: "Guardado correctamente",
                icon: "success"
            }).then(() => {
                window.location.href = "/Acceso/Login";
            });

        })

    }


}



$(document).ready(function () {
    var vc = new RegisterViewController();
    vc.InitView();
}) 