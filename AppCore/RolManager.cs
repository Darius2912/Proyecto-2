using DataAccess.CRUD;
using Entities_DTOs;
using System;
using System.Collections.Generic;

namespace AppCore
{
    public class RolManager : BaseManager
    {
        private readonly List<string> _rolesPermitidos = new List<string> { "Admin", "Usuario", "Propietario" };

        public void Create(Rol rol)
        {
            try
            {
                ValidateRol(rol, isNew: true);

                var rCrud = new RolCrudFactory();
                rCrud.Create(rol);
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
        }

        public void Update(Rol rol)
        {
            try
            {
                ValidateRol(rol, isNew: false);

                var rCrud = new RolCrudFactory();
                rCrud.Update(rol);
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
        }

        public void Delete(Rol rol)
        {
            try
            {
                // Validar que el rol no esté asignado a usuarios
                var urCrud = new UsuarioRolCrudFactory();
                var asignaciones = urCrud.RetrieveAll<UsuarioRol>();
                if (asignaciones.Exists(x => x.IdRol == rol.IdRol))
                    throw new Exception("No se puede eliminar el rol porque está asignado a uno o más usuarios.");

                var rCrud = new RolCrudFactory();
                rCrud.Delete(rol);
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
        }

        public List<Rol> RetrieveAll()
        {
            var list = new List<Rol>();
            try
            {
                var rCrud = new RolCrudFactory();
                list = rCrud.RetrieveAll<Rol>();
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
            return list;
        }

        public Rol RetrieveById(int id)
        {
            var rol = new Rol();
            try
            {
                var rCrud = new RolCrudFactory();
                rol = rCrud.RetrieveById<Rol>(id);
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
            return rol;
        }

        // 🔹 Validaciones
        private void ValidateRol(Rol rol, bool isNew)
        {
            if (string.IsNullOrWhiteSpace(rol.NombreRol))
                throw new Exception("El nombre del rol es obligatorio.");

            if (rol.NombreRol.Length < 3)
                throw new Exception("El nombre del rol debe tener al menos 3 caracteres.");

            if (!_rolesPermitidos.Contains(rol.NombreRol))
                throw new Exception($"El rol '{rol.NombreRol}' no está permitido en el sistema.");

            if (isNew)
            {
                var rCrud = new RolCrudFactory();
                var existingRoles = rCrud.RetrieveAll<Rol>();
                if (existingRoles.Exists(x => x.NombreRol == rol.NombreRol))
                    throw new Exception("Ya existe un rol con este nombre.");
            }
        }
    }
}
