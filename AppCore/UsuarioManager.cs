using DataAccess.CRUD;
using Entities_DTOs;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BCrypt.Net; // 🔹 Importar BCrypt.Net-Next

namespace AppCore
{
    public class UsuarioManager : BaseManager
    {
        private readonly CorreoManager _correoManager;

        public UsuarioManager(CorreoManager correoManager)
        {
            _correoManager = correoManager;
        }

        public void Create(Usuario u)
        {
            try
            {
                ValidateUsuario(u, isNew: true);

                // Encriptar contraseña
                u.Contrasena = BCrypt.Net.BCrypt.HashPassword(u.Contrasena);

                var uCrud = new UsuarioCrudFactory();
                int idUsuario = uCrud.CreateAndReturnId(u); // 🔹 crea usuario y obtiene IdUsuario

                var rolCrud = new UsuarioRolCrudFactory();
                rolCrud.AssignRole(idUsuario, 1); // 🔹 asigna rol por defecto (Usuario = 1)

                _correoManager.SendWelcomeEmail(u);
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
        }


        public void Update(Usuario u)
        {
            try
            {
                ValidateUsuario(u, isNew: false);

                // 🔹 Si viene una nueva contraseña, la volvemos a hashear
                if (!string.IsNullOrWhiteSpace(u.Contrasena))
                {
                    u.Contrasena = BCrypt.Net.BCrypt.HashPassword(u.Contrasena);
                }

                var uCrud = new UsuarioCrudFactory();
                uCrud.Update(u);
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
        }

        public void Delete(Usuario u)
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

        public Usuario RetrieveById(int id)
        {
            var usuario = new Usuario();
            try
            {
                var uCrud = new UsuarioCrudFactory();
                usuario = uCrud.RetrieveById<Usuario>(id);
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
            return usuario;
        }

        // 🔹 Nuevo método de Login con verificación de hash
        public Usuario Login(string correo, string contrasena)
        {
            try
            {
                var uCrud = new UsuarioCrudFactory();
                var usuario = uCrud.RetrieveByCorreo(correo);

                if (usuario == null)
                    throw new Exception("Usuario no encontrado.");

                // Verificar contraseña contra el hash almacenado
                if (!BCrypt.Net.BCrypt.Verify(contrasena, usuario.Contrasena))
                    throw new Exception("Credenciales inválidas.");

                // Cargar roles
                var rolCrud = new UsuarioRolCrudFactory();
                usuario.Roles = rolCrud.ObtenerRolesPorUsuario(usuario.IdUsuario);

                return usuario;
            }
            catch (Exception ex)
            {
                ManegerException(ex);
                return null;
            }
        }

        // 🔹 Validaciones completas
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

            if (u.Estado != "Activo" && u.Estado != "Inactivo")
                throw new Exception("El estado del usuario debe ser 'Activo' o 'Inactivo'.");

            if (u.FechaRegistro > DateTime.Now)
                throw new Exception("La fecha de registro no puede ser futura.");
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

        
    }
}

