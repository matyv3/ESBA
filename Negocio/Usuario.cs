    using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Usuario
    {
        #region Propiedades
        public int? user_id { get; set; }
        [Required(ErrorMessage = "El DNI es requerido")]
        public string document { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string name { get; set; }
        [Required(ErrorMessage = "El apellido es requerido")]
        public string surname { get; set; }
        public string sexo { get; set; }
        [Required(ErrorMessage = "La dirección es requerida")]
        public string Address { get; set; }
        [Required(ErrorMessage = "El teléfono es requerido")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "El email es requerido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
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
