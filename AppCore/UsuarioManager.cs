using BCrypt.Net; // 🔹 Importar BCrypt.Net-Next

using DataAccess.CRUD;
using Entities_DTOs;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AppCore
{
    public class UsuarioManager : BaseManager
    {
       

       

        public (bool registrado, string mensaje) Create(Usuario u)
        {
            try
            {
                if (u.Contrasena == u.ConfirmarContrasena)
                {
                    u.Contrasena = ConvertirSha256(u.Contrasena);
                    UsuarioCrudFactory uc = new UsuarioCrudFactory();
                    var resultado = uc.Registrar(u);

                   

                    return resultado;
                }
                else
                {
                    return (false, "Contraseñas no coinciden");
                }
            }
            catch (Exception ex)
            {
                ManegerException(ex);
                return (false, ex.Message);
            }
        }


        public Usuario Login(Usuario u)
        {
            try
            {
                u.Contrasena = ConvertirSha256(u.Contrasena);
                UsuarioCrudFactory uc = new UsuarioCrudFactory();
                var usuario = uc.ValidarUsuario(u);
                return usuario;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Login: {ex.Message}");
                ManegerException(ex);
                return null;
            }
        }

        public void Update(Usuario u)
        {
            try
            {
                

                var uCrud = new UsuarioCrudFactory();
                uCrud.Update(u);
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
        }

        public void Delete( Usuario u)
        {
            try
            {
                
                var uCrud = new UsuarioCrudFactory();
                uCrud.Delete(u);
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
        }

        public List<Usuario> RetrieveAll()
        {
            var list = new List<Usuario>();
            try
            {
                var uCrud = new UsuarioCrudFactory();
                list = uCrud.RetrieveAll<Usuario>();
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
            return list;
        }

       

    
        private void ValidateUsuario(Usuario u, bool isNew)
        {
            if (string.IsNullOrWhiteSpace(u.Cedula))
                throw new Exception("La cédula es obligatoria.");

            if (string.IsNullOrWhiteSpace(u.Nombre))
                throw new Exception("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(u.Correo))
                throw new Exception("El correo es obligatorio.");

            if (!IsValidEmail(u.Correo))
                throw new Exception("El formato del correo no es válido.");

            if (string.IsNullOrWhiteSpace(u.Contrasena))
                throw new Exception("La contraseña es obligatoria.");

            if (!IsStrongPassword(u.Contrasena))
                throw new Exception("La contraseña debe tener al menos 8 caracteres, incluir mayúsculas, minúsculas y números.");

            if (isNew)
            {
                var uCrud = new UsuarioCrudFactory();
                var existingUsers = uCrud.RetrieveAll<Usuario>();
                if (existingUsers.Exists(x => x.Correo == u.Correo))
                    throw new Exception("Ya existe un usuario registrado con este correo.");
            }

         /*   if (u.Estado != "Activo" && u.Estado != "Inactivo")
                throw new Exception("El estado del usuario debe ser 'Activo' o 'Inactivo'.");

            if (u.FechaRegistro > DateTime.Now)
                throw new Exception("La fecha de registro no puede ser futura."); */
        }

        private bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }

        private bool IsStrongPassword(string password)
        {
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$");
            return regex.IsMatch(password);
        }

        public static string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));
                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }
    }
}

