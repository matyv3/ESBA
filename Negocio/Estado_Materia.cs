using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    class Estado_Materia
    {

        public int? Estado_Materia_id { get; set; }
        public string Descripcion { get; set; }



        public bool Grabar(out string error)
        {
            error = "";
            if (this.Estado_Materia_id.HasValue)
            {

                int result = Convert.ToInt32(Datos.Estado_Materias.Update_Estado_Materia(this.Estado_Materia_id.Value, this.Descripcion));


                return true;
            }
            else
            {
                if (validar(out error))
                {
                    int result = Convert.ToInt32(Datos.Estado_Materias.Insert_Estado_Materia(
                        this.Descripcion
                    ));
                    return result == 1;
                }
                else
                {
                    return false;
                }

            }
        }

        public static List<Estado_Materia> ObtenerTodas()
        {
            List<Estado_Materia> Estado_Materia = new List<Estado_Materia>();
            SqlDataReader dr = Datos.Estado_Materias.GetALL_Estado_Materia();
            while (dr.Read())
            {
                Estado_Materia Est_mat = new Estado_Materia();
                Est_mat.Estado_Materia_id = Convert.ToInt32(dr["Estado_Materia_id"]);
                Est_mat.Descripcion = dr["Descripcion"].ToString();

                Estado_Materia.Add(Est_mat);
            }

            return Estado_Materia;
        }

        public static Estado_Materia Obtener(int id)
        {
            Estado_Materia Est_Mat = new Estado_Materia();

            SqlDataReader dr = Datos.Estado_Materias.Get_Estado_Materia(id);
            while (dr.Read())
            {
                Est_Mat.Estado_Materia_id = Convert.ToInt32(dr["Estado_Materia_id"]);
                Est_Mat.Descripcion = dr["Descripcion"].ToString();

            }

            return Est_Mat;
        }

        public bool Existe(string Descripcion)
        {
            int resultado = 0;

            int result = Datos.Estado_Materias.Validate_Estado_Materia(Descripcion);

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

            resultado = Convert.ToInt32(Datos.Estado_Materias.Delete_Estado_Materia(id));

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

            if (string.IsNullOrEmpty(this.Descripcion))
                error += "la descripcion se encuentra vacio" + Environment.NewLine;

            if (string.IsNullOrEmpty(error))
                return true;
            else
                return false;

        }



    }
}
