//clase controladora de la vista Users.cshtml
//definimos una clase JS usando prototype


function UserViewController() {
    this.ViewName = "Users";
    //nombre del controlador que consume en el API del backend
    this.API_ControllerName = "Usuario";
    //metodo constructor
    this.InitView = function () {
        this.loadTable();

        //asociar evento al click crear
     

        $("#btnUpdate").click(function () {
            var vc = new UserViewController();
            vc.Update();
        })

        $("#btnDelete").click(function () {
            var vc = new UserViewController();
            vc.Delete();
        })

    }
    //metodo de carga de la tabla
    this.loadTable = function () {
        var ca = new ControlActions();
        var endPoint = this.API_ControllerName + "/RetrieveAll"
        var urlService = ca.GetUrlApiService(endPoint);

        var columns = [];
        columns[0] = { 'data': 'cedula' };
        columns[1] = { 'data': 'nombre' };
        columns[2] = { 'data': 'apellido' };
        columns[3] = { 'data': 'correo' };
        columns[4] = { 'data': 'telefono' };
        columns[5] = { 'data': 'estado' };
        columns[6] = { 'data': 'rol' };
      
    

        //convertir tabla plana en una tablas en una que se vea mejor
        //$ para llamar a jquery
        $('#tblUsers').DataTable({

            "ajax": {
                url: urlService, "dataSrc": ""
            },
            "columns": columns

        });

        //asignar evento de mapeo de dto seleccionado con el form
        $("#tblUsers tbody").on("click", "tr", function () {
            var row = $(this).closest('tr');

            var userDTO = $("#tblUsers").DataTable().row(row).data();


            //extraer el DTO en el for,
            $("#txtCedula").val(userDTO.cedula);
            $("#txtName").val(userDTO.nombre);
            $("#txtLastName").val(userDTO.apellido);
            $("#txtTelefono").val(userDTO.telefono);
            $("#txtEmail").val(userDTO.correo);
            $("#txtEstado").val(userDTO.estado);
            $("#txtRol").val(userDTO.rol);

            




        })

    }

    this.Create = function () {
        var userDTO = {};

        //VALORES DEFAULT
        userDTO.id = 0;
        userDTO.created = "2026-03-05T20:30:00";
        userDTO.updated = "2026-03-05T20:30:00";

        //valores que capturamos y asignamos a la variable con jquery
        userDTO.cedula = $("#txtCedula").val();
        userDTO.lastName = $("#txtLastName").val();
        userDTO.email = $("#txtEmail").val();
        userDTO.status = $("#txtStatus").val();
        userDTO.birthDate = $("#txtBirthDate").val();
        userDTO.password = $("#txtPwd").val();

        //enviar al api
        var ca = new ControlActions();
        var urlEndPoint = this.API_ControllerName + "/Create";
        ca.PostToAPI(urlEndPoint, userDTO, function () {
            //recargar tabla
            $("#tblUsers").DataTable().ajax.reload();
        })

    }


    this.Update = function () {
        var userDTO = {};

      

        

        //VALORES DEFAULT
        userDTO.IdUsuario = -1;
        userDTO.Created = "2026-03-05T20:30:00";
        userDTO.Updated = "2026-03-05T20:30:00";
        
        userDTO.Contrasena = "X";
        userDTO.ConfirmarContrasena = "X";


       
       
        //valores que capturamos y asignamos a la variable con jquery
        userDTO.cedula = $("#txtCedula").val();
        userDTO.nombre = $("#txtName").val();
        userDTO.apellido = $("#txtLastName").val();
        userDTO.correo = $("#txtEmail").val();
        userDTO.estado = $("#txtEstado").val();
        userDTO.rol = $("#txtRol").val();
        userDTO.Telefono = $("#txtTelefono").val();
        //enviar al api
        var ca = new ControlActions();
        var urlEndPoint = this.API_ControllerName + "/Update";

        ca.PutToAPI(urlEndPoint, userDTO, function () {
            //recargar tabla
            $("#tblUsers").DataTable().ajax.reload();
        })

    }

    this.Delete = function () {
        var userDTO = {};

        //VALORES DEFAULT
        userDTO.IdUsuario = -1;
        userDTO.Created = "2026-03-05T20:30:00";
        userDTO.Updated = "2026-03-05T20:30:00";
        
        userDTO.Contrasena = "X";
        userDTO.ConfirmarContrasena = "X";


          


        //valores que capturamos y asignamos a la variable con jquery
        userDTO.Cedula = $("#txtCedula").val();
        userDTO.Nombre = $("#txtName").val();
        userDTO.Apellido = $("#txtLastName").val();
        userDTO.Correo = $("#txtEmail").val();
        userDTO.Estado = $("#txtEstado").val();
        userDTO.Rol = parseInt($("#txtRol").val());
        userDTO.Telefono = $("#txtTelefono").val();
        //enviar al api
        var ca = new ControlActions();
        var urlEndPoint = this.API_ControllerName + "/Delete";
        ca.DeleteToAPI(urlEndPoint, userDTO, function () {
            //recargar tabla
            $("#tblUsers").DataTable().ajax.reload();
        })

    }
}

//instancia y render del controlador

$(document).ready(function () {
    var vc = new UserViewController();
    vc.InitView();
}) 