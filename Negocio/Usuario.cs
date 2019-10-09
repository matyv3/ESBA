using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Usuario
    {
        #region Propiedades
        public int? user_id { get; set; }
        public string document { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string sexo { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password{ get; set; }
        public string Rol { get; set; }
        #endregion

        public bool Validar(string email, string contrasena)
        {
            // valida contra la capa de datos y devuelve true o false si existe

            return true;
        }

        public static Usuario Obtener(int id)
        {
            Usuario usuario = new Usuario();

            SqlDataReader dr = Datos.Usuarios.Get_User(id);
            while (dr.Read())
            {
                usuario.user_id = Convert.ToInt32 (dr["id"]);
                usuario.document = dr["document"].ToString();
                usuario.name = dr["name"].ToString();
                usuario.surname = dr["surname"].ToString();
                usuario.sexo = dr["sexo"].ToString();
                usuario.Address = dr["address"].ToString();
                usuario.Phone = dr["phone"].ToString();
                usuario.Email = dr["mail"].ToString();
                usuario.Password = dr["password"].ToString();
                usuario.Rol = dr["description"].ToString();

            }

            return usuario;
        }
    }
}
