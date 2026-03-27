using DataAccess.CRUD;
using Entities_DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppCore
{
    public class UsuarioRolManager : BaseManager
    {
        public void AsignarRol(UsuarioRol usuarioRol)
        {
            try
            {
                ValidateAsignacion(usuarioRol);

                var urCrud = new UsuarioRolCrudFactory();
                urCrud.Create(usuarioRol);
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
        }

        public void QuitarRol(UsuarioRol usuarioRol)
        {
            try
            {
                // Validar que la relación exista
                var urCrud = new UsuarioRolCrudFactory();
                var asignaciones = urCrud.RetrieveAll<UsuarioRol>();
                if (!asignaciones.Exists(x => x.IdUsuario == usuarioRol.IdUsuario && x.IdRol == usuarioRol.IdRol))
                    throw new Exception("La relación usuario-rol no existe.");

                urCrud.Delete(usuarioRol);
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
        }

        public List<UsuarioRol> RetrieveAll()
        {
            var list = new List<UsuarioRol>();
            try
            {
                var urCrud = new UsuarioRolCrudFactory();
                list = urCrud.RetrieveAll<UsuarioRol>();
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
            return list;
        }

        public UsuarioRol RetrieveById(int id)
        {
            var usuarioRol = new UsuarioRol();
            try
            {
                var urCrud = new UsuarioRolCrudFactory();
                usuarioRol = urCrud.RetrieveById<UsuarioRol>(id);
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
            return usuarioRol;
        }

        public List<Rol> ObtenerRolesPorUsuario(int idUsuario)
        {
            try
            {
                var urCrud = new UsuarioRolCrudFactory();
                var roles = urCrud.ObtenerRolesPorUsuario(idUsuario);

                // Si no tiene roles, devolver lista vacía
                return roles ?? new List<Rol>();
            }
            catch (Exception ex)
            {
                ManegerException(ex);
                return new List<Rol>();
            }
        }

        // 🔹 Validaciones
        private void ValidateAsignacion(UsuarioRol usuarioRol)
        {
            var uCrud = new UsuarioCrudFactory();
            var rCrud = new RolCrudFactory();
            var urCrud = new UsuarioRolCrudFactory();

            // Validar existencia de usuario
            var usuario = uCrud.RetrieveById<Usuario>(usuarioRol.IdUsuario);
            if (usuario == null)
                throw new Exception("El usuario no existe.");

            // Validar existencia de rol
            var rol = rCrud.RetrieveById<Rol>(usuarioRol.IdRol);
            if (rol == null)
                throw new Exception("El rol no existe.");

            // Validar duplicado
            var asignaciones = urCrud.RetrieveAll<UsuarioRol>();
            if (asignaciones.Exists(x => x.IdUsuario == usuarioRol.IdUsuario && x.IdRol == usuarioRol.IdRol))
                throw new Exception("El usuario ya tiene este rol asignado.");
        }
    }
}
