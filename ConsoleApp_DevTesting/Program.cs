
using DataAccess.CRUD;
using Entities_DTOs;

public class Program {

    public static void Main(string[] args)
    {

        Console.WriteLine("Hello, World!");


        UsuarioCrudFactory uc = new UsuarioCrudFactory();
        Usuario oUsuario = new Usuario();
        /*
        TEST CREACION USUARIO
        oUsuario.Cedula = "123";
        oUsuario.Nombre = "testnombre";
            oUsuario.Apellido = "testapellido";
            oUsuario.Correo = "test@correo.com";
            oUsuario.Contrasena = "123";
        oUsuario.Telefono = "123123123";
        uc.Registrar(oUsuario);
        */

        //TEST LOGIN
        oUsuario.Correo = "jchavesl@ucenfotec.ac.cr";
        oUsuario.Contrasena = "$2a$11$Q95Z8DPoB2TBeuSRKMwFAeze4KQnD.8pxC2xQfhi0lDcKAkPWAeFC";

      var resultado =    uc.ValidarUsuario(oUsuario);




        if (resultado != null)
        {
            Console.WriteLine($"IdUsuario: {resultado.IdUsuario}");
            Console.WriteLine($"Rol: {resultado.Rol}");
        }
        else
        {
            Console.WriteLine("Usuario no encontrado o credenciales incorrectas");
        }


    }

    


}
