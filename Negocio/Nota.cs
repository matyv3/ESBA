using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Negocio
{
    class Nota
    {

        public int? Nota_id { get; set; }
        public int valor { get; set; }

        public bool Grabar(out string error)
        {
            error = "";
            if (this.Nota_id.HasValue)
            {
                return true;
            }
            else
            {
                if (validar(out error))
                {
                    int result = Datos.Notas.Insert_Notas(               
                        this.valor
                    );
                    return result == 1;
                }
                else
                {
                    return false;
                }

            }
        }

        public static List<Nota> ObtenerTodas()
        {
            List<Nota> Notas = new List<Nota>();
            SqlDataReader dr = Datos.Notas.GetALL_Notas();
            while (dr.Read())
            {
                Nota nota = new Nota();
                nota.Nota_id = Convert.ToInt32(dr["Nota_id"]);
                nota.valor = Convert.ToInt32(dr["valor"]);
                Notas.Add(nota);
            }

            return Notas;
        }


        public static Nota Obtener(int id)
        {
            Nota nota = new Nota();

            SqlDataReader dr = Datos.Notas.Get_Notas(id);
            while (dr.Read())
            {
                nota.Nota_id = Convert.ToInt32(dr["Nota_id"]);
                nota.valor = Convert.ToInt32(dr["valor"]);
            }
            return nota;
        }


        private bool validar(out string error)
        {
            error = "";

            if (string.IsNullOrEmpty( Convert.ToString(valor)))
                error += "el valor se encuentra vacio" + Environment.NewLine;

            if (string.IsNullOrEmpty(error))
                return true;
            else
                return false;
        }

        public bool Existe(int valor)
        {
            int resultado = 0;

            int result = Convert.ToInt32(Datos.Notas.Validate_Notas(valor));

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

            resultado = Convert.ToInt32(Datos.Notas.Delete_Notas(id));

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
