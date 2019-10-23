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
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "El teléfono es requerido")]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "El email es requerido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password{ get; set; }
        public string Rol { get; set; }
        #endregion

        public static int Validar(string email, string contrasena)
        {
            string encriptada = Encriptar(contrasena);
            try
            {
                return Datos.Usuarios.Validate_User(email, encriptada);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static Usuario Obtener(int id)
        {
            Usuario usuario = new Usuario();

            SqlDataReader dr = Datos.Usuarios.Get_User(id);
            while (dr.Read())
            {
                usuario.user_id = Convert.ToInt32 (dr["user_id"]);
                usuario.document = dr["document"].ToString();
                usuario.name = dr["name"].ToString();
                usuario.surname = dr["surname"].ToString();
                usuario.sexo = dr["sexo"].ToString();
                usuario.Address = dr["address"].ToString();
                usuario.Phone = dr["phone"].ToString();
                usuario.Email = dr["mail"].ToString();
                usuario.Password = dr["password"].ToString();
                usuario.Rol = dr["descripcion"].ToString();
            }

            return usuario;
        }

        public bool Grabar()
        {
            // validar datos
            // validar que ese usuario no exista ya en la base
            if (user_id.HasValue) {
                // actualizar                 
            } 
            else
            {
                // crear
                Datos.Usuarios.Insert_User(
                    this.document,
                    this.name,
                    this.surname,
                    this.sexo,
                    this.Address,
                    this.Phone,
                    this.Email,
                    Encriptar(this.Password),
                    this.Rol
                );
                
            }
            return true;
        }

        public bool Existe()
        {
            int result = Datos.Usuarios.Validate_User(this.Email, "");
            return result != 0;
        }

        public bool Eliminar(int idUsuario)
        {
            return true;
        }

        private static string Encriptar(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private static string Desencriptar(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
