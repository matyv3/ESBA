using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    class Estado_Materia
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
                if (validar(out error))
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

        public static List<Materia> ObtenerTodas()
        {
            List<Materia> materias = new List<Materia>();
            SqlDataReader dr = Datos.Materias.GetALL_Materia();
            while (dr.Read())
            {
                Materia materia = new Materia();
                materia.Id = Convert.ToInt32(dr["Materia_id"]);
                materia.Nombre = dr["nombre"].ToString();
                materia.CantModulos = Convert.ToInt32(dr["Cant_Modulos"]);
                materias.Add(materia);
            }

            return materias;
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
