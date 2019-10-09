using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Usuarios
    {
        #region Propiedades
        public int? Id { get; set; }
        public string Nombre{ get; set; }
        public string Apellido { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string DNI { get; set; }
        public string Email{ get; set; }
        public string Password{ get; set; }
        #endregion

        public bool Validar(string email, string contrasena)
        {
            // valida contra la capa de datos y devuelve true o false si existe

            return true;
        }


    }
}
