


function PropertyViewCotroller() {
    this.ViewName = "Pagos";
    this.API_ControllerName = "Propiedad";
    

    this.InitView =  function () {
       
        this.CargarPropiedades();


    }



    this.CargarPropiedades = async function () {

        const sesion = await this.obtenerSesion();
        const idu = sesion ? sesion.idUsuario : 0;
       



        const select = document.getElementById("NombrePropiedad");
        var ca = new ControlActions();
        alert(idu);
        var endPoint = this.API_ControllerName + "/usuarioApproved/" + idu;
        var urlService = ca.GetUrlApiService(endPoint);

        try {
            const response = await fetch(urlService);
            const lista = await response.json();

            select.innerHTML = "";


            lista.forEach(item => {
                const option = document.createElement('option');
               
                option.textContent = item.nombreFinca;
                select.appendChild(option);
            });
        } catch (error) {
            console.error('error al cargar select', error);
        }
       

    }

    this.obtenerSesion = async function () {
        try {
            const response = await fetch('/Acceso/ObtenerSesion', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if (!response.ok) {
                throw new Error('Error al obtener el id de la sesión');
            }

            const data = await response.json();

            return data; // { idUsuario }
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