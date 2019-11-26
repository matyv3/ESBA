using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Rol
    {
        public int? rol_id { get; set; }
        public String descripcion { get; set; }

        public bool Grabar(out string error)
        {
            error = "";
            if (this.rol_id.HasValue)
            {
                int result = Convert.ToInt32(Datos.Roles.Update_Roles(this.rol_id.Value, this.descripcion));
                return true;
            }
            else
            {
                if (validar(out error))
                {
                    int result = Convert.ToInt32( Datos.Roles.Insert_Roles(
                        this.descripcion
                    ));
                    return result == 1;
                }
                else
                {
                    return false;
                }

            }
        }

        public static List<Rol> ObtenerTodas()
        {
            List<Rol> Roles = new List<Rol>();
            SqlDataReader dr = Datos.Roles.GetALL_Roles();
            while (dr.Read())
            {
                Rol rol = new Rol();
                rol.rol_id = Convert.ToInt32(dr["rol_id"]);
                rol.descripcion = Convert.ToString(dr["descripcion"]);
                Roles.Add(rol);
            }

            return Roles;
        }


        public static Rol Obtener(int id)
        {
            Rol rol = new Rol();

            SqlDataReader dr = Datos.Roles.Get_Roles(id);
            while (dr.Read())
            {
                rol.rol_id = Convert.ToInt32(dr["rol_id"]);
                rol.descripcion = Convert.ToString(dr["descripcion"]);
            }
            return rol;
        }


        private bool validar(out string error)
        {
            error = "";

            if (string.IsNullOrEmpty(descripcion))
                error += "la descripcion se encuentra vacia" + Environment.NewLine;

            if (string.IsNullOrEmpty(error))
                return true;
            else
                return false;
        }

        public bool Existe(String descripcion)
        {
            int resultado = 0;

            int result = Convert.ToInt32(Datos.Roles.Validate_Roles(descripcion));

            if (resultado == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool Eliminar(int rol_id)
        {
            int resultado = 0;

            resultado = Convert.ToInt32(Datos.Roles.Delete_Roles(rol_id));

            if (resultado == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
