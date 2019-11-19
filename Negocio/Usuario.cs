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
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password{ get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string PasswordConfirm { get; set; }
        public string Rol { get; set; }
        public int rol_id { get; set; }
        #endregion

        public static int Validar(string email, string contrasena)
        {
            string encriptada = Seguridad.Encriptar(contrasena);
            try
            {
                return Datos.Usuarios.Validate_User(email, encriptada);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /*public static Usuario Obtener(int id)
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
                usuario.rol_id = Convert.ToInt32(dr["rol_id"]);
            }

            return usuario;
        }*/

        public static Usuario ObtenerUsuarios(int id)
        {
            List<Usuario> usuarios = List<Usuario>;
            Usuario usuario = new Usuario();

            SqlDataReader dr = Datos.Usuarios.Get_User(id);
            while (dr.Read())
            {
                usuario.user_id = Convert.ToInt32(dr["user_id"]);
                usuario.document = dr["document"].ToString();
                usuario.name = dr["name"].ToString();
                usuario.surname = dr["surname"].ToString();
                usuario.sexo = dr["sexo"].ToString();
                usuario.Address = dr["address"].ToString();
                usuario.Phone = dr["phone"].ToString();
                usuario.Email = dr["mail"].ToString();
                usuario.Password = dr["password"].ToString();
                usuario.Rol = dr["descripcion"].ToString();
                usuario.rol_id = Convert.ToInt32(dr["rol_id"]);
            }

            return usuario;
        }

        public bool Grabar( out string error)
        {

            error = "";
            // validar datos
            // validar que ese usuario no exista ya en la base
            if (user_id.HasValue) {
                // actualizar   
                Datos.Usuarios.Update_User(
                    this.user_id.Value,
                    this.document,
                    this.name,
                    this.surname,
                    this.sexo,
                    this.Address,
                    this.Phone,
                    this.Email,
                    Seguridad.Encriptar(this.Password),
                    this.rol_id.ToString()
                    );
                return true;
            } 
            else
            {
                // crear
                if (validar( out error))
                { 
                    Datos.Usuarios.Insert_User(
                                this.document,
                                this.name,
                                this.surname,
                                this.sexo,
                                this.Address,
                                this.Phone,
                                this.Email,
                                Seguridad.Encriptar(this.Password),
                                this.rol_id.ToString()
                            );
                    return true;
                }
                else
                {

                    return false;
                }
                
            }
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

        private bool validar( out string error)
        {
            error = "";

            if (string.IsNullOrEmpty(Email))
                error += "el email se encuentra vacio" + Environment.NewLine;

            if (string.IsNullOrEmpty(Password))
                error += "el pass se encuentra vacio" + Environment.NewLine;

            if (string.IsNullOrEmpty(error))
                return true;
            else
                return false;
                    
        }
    }
}
