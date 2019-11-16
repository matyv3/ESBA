using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio
{
    public class Materia
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public int CantModulos { get; set; }


        public bool Grabar(out string error)
        {
            error = "";
            if (this.Id.HasValue)
            {
                return true;
            }
            else
            {
                if(validar(out error))
                {
                    int result = Datos.Materias.Insert_Materia(
                        this.Nombre,
                        this.CantModulos
                    );
                    return result == 1;
                }
                else
                {
                    return false;
                }
                
            }
        }

        private bool validar(out string error)
        {
            error = "";

            if (string.IsNullOrEmpty(Nombre))
                error += "el nombre se encuentra vacio" + Environment.NewLine;

            if (CantModulos == 0)
                error += "cantidad de modulos incorrecta" + Environment.NewLine;

            if (string.IsNullOrEmpty(error))
                return true;
            else
                return false;

        }
    }
}
