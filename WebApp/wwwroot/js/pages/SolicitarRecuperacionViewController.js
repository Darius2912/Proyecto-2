function SolicitarRecuperacionViewController() {
    this.ViewName = "SolicitarRecuperacion";
    this.API_ControllerName = "Usuario";

    this.InitView = function () {
        var self = this; // guarda el contexto
        $("#btnSolicitar").click(function () {
            self.SolicitarRecuperacion();
        });
    };

    this.SolicitarRecuperacion = function () {
        var correo = $("#txtCorreo").val();
       
        if (!correo) {
            Swal.fire({
                icon: 'warning',
                title: 'Campo requerido',
                text: 'Por favor ingresa tu correo electrónico.'
            });
            return;
        }

        var dto = { Correo: correo };

        var ca = new ControlActions();
        var urlEndPoint = this.API_ControllerName + "/SolicitarRecuperacion";

        ca.PostToAPI(urlEndPoint, dto, function () {
            Swal.fire({
                icon: 'success',
                title: '¡Correo enviado!',
                text: 'Revisa tu bandeja de entrada y sigue el enlace.',
                confirmButtonText: 'Aceptar'
            }).then(function () {
                window.location.href = "/Login";
            });
        });
    };
}

$(document).ready(function () {
    var vc = new SolicitarRecuperacionViewController();
    vc.InitView(); // registra el click correctamente
});