using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    class Nota_Materia
    {
        public int? Nota_Materia_id { get; set; }
        public int Nota_id { get; set; }
        public int Materia_id { get; set; }


        public bool Grabar(out string error)
        {
            error = "";
            if (this.Nota_Materia_id.HasValue)
            {
                return true;
            }
            else
            {
                if (validar(out error))
                {
                    int result = Datos.Notas_Materias.Insert_Nota_Materia(
                        this.Nota_id,
                        this.Materia_id
                    ) ;
                    return result == 1;
                }
                else
                {
                    return false;
                }

            }
        }

        public static List<Nota_Materia> ObtenerTodas()
        {
            List<Nota_Materia> Nota_Materia = new List<Nota_Materia>();
            SqlDataReader dr = Datos.Notas_Materias.GetALL_Notas();
            while (dr.Read())
            {
                Nota_Materia not_Mat = new Nota_Materia();
                not_Mat.Nota_Materia_id = Convert.ToInt32(dr["Nota_Materia_id"]);
                not_Mat.Nota_id = Convert.ToInt32(dr["Nota_id"]);
                not_Mat.Materia_id = Convert.ToInt32(dr["Materia_id"]);

                Nota_Materia.Add(not_Mat);
            }

            return Nota_Materia;
        }

        public static Nota_Materia Obtener(int id)
        {
            Nota_Materia not_Mat = new Nota_Materia();

            SqlDataReader dr = Datos.Notas_Materias.Get_Nota_Materia(id);
            while (dr.Read())
            {
                not_Mat.Nota_Materia_id = Convert.ToInt32(dr["Nota_Materia_id"]);
                not_Mat.Nota_id = Convert.ToInt32(dr["Nota_id"]);
                not_Mat.Materia_id = Convert.ToInt32(dr["Materia_id"]);

            }

            return not_Mat;
        }

        public bool Existe(int nota_id, int materia_id)
        {
            int resultado = 0;

            int result = Convert.ToInt32(Datos.Notas_Materias.Validate_Nota_Materia(nota_id, materia_id));

            if (resultado == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Eliminar(int id)
        {
            int resultado = 0;

            resultado = Convert.ToInt32(Datos.Notas_Materias.Delete_Nota_Materia(id));

            if (resultado == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool validar(out string error)
        {
            error = "";

            if (string.IsNullOrEmpty(Convert.ToString(this.Nota_id)))
                error += "el ID de la Nota se encuentra vacio" + Environment.NewLine;

            if (string.IsNullOrEmpty(Convert.ToString(this.Materia_id)))
                error += "la ID de la Materia se encuentra vacio" + Environment.NewLine;

            if (string.IsNullOrEmpty(error))
                return true;
            else
                return false;

        }









    }
}
