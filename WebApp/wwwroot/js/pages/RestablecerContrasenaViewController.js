function RestablecerContrasenaViewController() {
    this.ViewName = "RestablecerContrasena";
    this.API_ControllerName = "Usuario";

    this.InitView = function () {
        $("#btnRestablecer").click(function () {
            var vc = new RestablecerContrasenaViewController();
            vc.Restablecer();
        });
    };

    this.GetTokenFromUrl = function () {
        var params = new URLSearchParams(window.location.search);
        return params.get("token");
    };

    this.Restablecer = function () {
        var nueva = $("#txtNuevaContrasena").val();
        var confirmar = $("#txtConfirmarContrasena").val();
        var token = this.GetTokenFromUrl();

        if (!token) {
            Swal.fire({
                icon: 'error',
                title: 'Enlace inválido',
                text: 'El enlace de recuperación no es válido o ya expiró.'
            });
            return;
        }

        if (!nueva || !confirmar) {
            Swal.fire({
                icon: 'warning',
                title: 'Campos incompletos',
                text: 'Por favor completa ambos campos.'
            });
            return;
        }

        if (nueva !== confirmar) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Las contraseñas no coinciden.'
            });
            return;
        }

        var dto = {};
        dto.Token = token;
        dto.Contrasena = nueva;
        dto.ConfirmarContrasena = confirmar;

        var ca = new ControlActions();
        var urlEndPoint = this.API_ControllerName + "/RestablecerContrasena";

        ca.PostToAPI(urlEndPoint, dto, function () {
            Swal.fire({
                icon: 'success',
                title: 'Listo!',
                text: 'Tu contraseña fue actualizada correctamente.',
                confirmButtonText: 'Ir al login'
            }).then(function () {
                window.location.href = "/Login";
            });
        });
    };
}

$(document).ready(function () {
    var vc = new RestablecerContrasenaViewController();
    vc.InitView();
});