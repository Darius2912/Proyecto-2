function PropertyViewCotroller() {
    this.ViewName = "Pagos";
    this.API_ControllerName = "Propiedad";

    this.InitView = function () {
        this.CargarPropiedades();

        const select = document.getElementById("NombrePropiedad");

        select.addEventListener("change", (e) => {
            const id = e.target.value;
            this.CargarDetallePago(id);
        });
    }

    this.CargarPropiedades = async function () {

        const sesion = await this.obtenerSesion();
        const idu = sesion ? sesion.idUsuario : 0;

        const select = document.getElementById("NombrePropiedad");
        const PropiedadTitulo = document.getElementById("PropiedadTitulo");

        var ca = new ControlActions();

        var endPoint = this.API_ControllerName + "/usuarioApproved/" + idu;
        var urlService = ca.GetUrlApiService(endPoint);

        try {
            const response = await fetch(urlService);
            const lista = await response.json();

            select.innerHTML = "";

            lista.forEach(item => {
                const option = document.createElement('option');
                option.value = item.id;
                option.textContent = item.nombreFinca;

                if (item.estado != "Rechazada") {

                    select.appendChild(option);
                }

                
                
            });

            // 🔥 IMPORTANTE: cargar automáticamente el primero
            if (lista.length > 0) {
                this.CargarDetallePago(lista[0].id);
            }

        } catch (error) {
            console.error('error al cargar select', error);
        }
    }

    this.CargarDetallePago = async function (idPropiedad) {


        const select = document.getElementById("NombrePropiedad");
        const nombre = select.options[select.selectedIndex].text;

        document.getElementById("PropiedadTitulo").innerText = nombre;


        var ca = new ControlActions();
        var endPoint = "Pago/RetrievePlanPagoById?Id=" + idPropiedad;
        var urlService = ca.GetUrlApiService(endPoint);

        try {
            const response = await fetch(urlService);
            const data = await response.json();

            console.log("detalle pago:", data);

            document.getElementById("precioBase").innerText = data.precioBaseHectarea;
            document.getElementById("porcentajeBosque").innerText = data.porcentajeBosque;
            document.getElementById("porcentajeHidrico").innerText = data.porcentajeHidrico;
            document.getElementById("porcentajeTerreno").innerText = data.porcentajeTerreno;
            document.getElementById("totalPago").innerText = data.totalPago;
            document.getElementById("estadoPago").innerText = data.estado;

        } catch (error) {
            console.error("error al cargar detalle", error);
        }
    }

    this.obtenerSesion = async function () {
        try {
            const response = await fetch('/Acceso/ObtenerSesion');

            if (!response.ok) {
                throw new Error('Error al obtener sesión');
            }

            return await response.json();

        } catch (error) {
            console.error('Error:', error);
            return null;
        }
    }
}







$(document).ready(function () {
    var vc = new PropertyViewCotroller();
    vc.InitView();

})