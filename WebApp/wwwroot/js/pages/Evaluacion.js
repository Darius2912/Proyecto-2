function PagosViewController() {

    this.ViewName = "Pagos";
    this.API_ControllerName = "Pago";

    this.InitView = function () {
        this.LoadTablePendientes();
    }

    this.LoadTablePendientes = function () {

        var ca = new ControlActions();
        var endPoint = this.API_ControllerName + "/RetrieveAllPlanesPendientes";
        var urlService = ca.GetUrlApiService(endPoint);

        var columns = [];

        columns[0] = { 'data': 'idPropiedad' };
        columns[1] = {
            'data': 'fechaCalculo',
            'render': function (data) {
                return new Date(data).toLocaleDateString();
            }
        };
        columns[2] = {
            'data': 'precioBaseHectarea',
            'render': function (data) {
                return "₡" + data.toLocaleString();
            }
        };
        columns[3] = {
            'data': 'porcentajeBosque',
            'render': function (data) {
                return data + "₡" ;
            }
        };
        columns[4] = {
            'data': 'porcentajeTerreno',
            'render': function (data) {
                return data + "₡" ;
            }
        };
        columns[5] = {
            'data': 'porcentajeHidrico',
            'render': function (data) {
                return data + "₡";
            }
        };
        columns[6] = {
            'data': 'estado',
            'render': function (data) {
                if (data === "Pendiente") {
                    return `<span class="badge bg-warning text-dark">${data}</span>`;
                } else if (data === "Aprobada") {
                    return `<span class="badge bg-success">${data}</span>`;
                } else {
                    return `<span class="badge bg-danger">${data}</span>`;
                }
            }
        };
        columns[7] = {
            'data': 'totalPago',
            'render': function (data) {
                return "<strong>₡" + data.toLocaleString() + "</strong>";
            }
        };

        //  BOTÓN APROBAR
        columns[8] = {
            'data': null,
            'render': function (data, type, row) {

                if (row.estado === "Pendiente") {
                    return `<button class="btn btn-sm btn-success btnAprobar">Aprobar</button>`;
                } else {
                    return `<button class="btn btn-sm btn-success" disabled>Aprobado</button>`;
                }
            }
        };

        $('#tablaCalculos').DataTable({
            "ajax": {
                url: urlService,
                dataSrc: ""
            },
            "columns": columns
            ,

            destroy: true,
            responsive: true,
            autoWidth: false,


            dom: 'rt<"bottom"lp><"clear">'

        });

        // EVENTO CLICK 
        $('#tablaCalculos tbody').on('click', '.btnAprobar', function () {

            var row = $(this).closest('tr');
            var data = $("#tablaCalculos").DataTable().row(row).data();

            var vc = new PagosViewController();
            vc.Aprobar(data.idPropiedad);
        });
    }

    // aprobar
    this.Aprobar = function (idPropiedad) {

        var ca = new ControlActions();
        var endPoint = this.API_ControllerName + "/AprobarPlan";

        Swal.fire({
            title: '¿Aprobar plan de pago?',
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'Si, aprobar'
        }).then((result) => {
            if (result.isConfirmed) {

               
                var data = {
                    idUsuario: 1,
                    created: new Date().toISOString(),
                    updated: new Date().toISOString(),
                    idPropiedad: idPropiedad,
                    idTipoBosque: 1,
                    precioBaseHectarea: 1,
                    porcentajeBosque: 1,
                    totalPago: 1,
                    fechaCalculo: new Date().toISOString(),
                    estado: "Aprobado",
                    idTipoPendiente: 1,
                    porcentajeTerreno: 1,
                    porcentajeHidrico: 1,
                    idConfiguracionParametros: 3
                };

                ca.PostToAPI(endPoint, data, function () {
                    Swal.fire('Aprobado', 'El pago fue aprobado correctamente', 'success');

                    $('#tablaCalculos').DataTable().ajax.reload();
                });
            }
        });
    };
      }

$(document).ready(function () {
    var vc = new PagosViewController();
    vc.InitView();
});