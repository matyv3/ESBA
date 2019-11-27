using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Negocio
{
    public class Usuario_Materia
    {



        public int? Usuario_Materia_id { get; set; }
        public int user_id { get; set; }
        public int materia_id { get; set; }
        public int? Estado_Materia_id { get; set; }

        public int? Nota_Valor { get; set; }

        public bool Grabar(out string error)
        {
            error = "";
            if (this.Usuario_Materia_id.HasValue)
            {


                int result = Convert.ToInt32(Datos.Usuarios_Materias.Update_Usuario_Materia(this.Usuario_Materia_id.Value, this.user_id, this.materia_id, this.Estado_Materia_id,this.Nota_Valor));

                return true;
            }
            else
            {
                if (validar(out error))
                {
                    int result = Convert.ToInt32(Datos.Usuarios_Materias.Insert_Usuario_Materia(
                        this.user_id,
                        this.materia_id, 
                        this.Estado_Materia_id,
                        this.Nota_Valor
                    ));
                    return result == 1;
                }
                else
                {
                    return false;
                }

            }
        }

        public static List<Usuario_Materia> ObtenerTodas()
        {
            List<Usuario_Materia> Usuario_Materia = new List<Usuario_Materia>();
            SqlDataReader dr = Datos.Usuarios_Materias.GetALL_Usuario_Materia();
            while (dr.Read())
            {
                Usuario_Materia User_Mat = new Usuario_Materia();
                User_Mat.Usuario_Materia_id = Convert.ToInt32(dr["Usuario_Materia_id"]);
                User_Mat.user_id = Convert.ToInt32(dr["user_id"]);
                User_Mat.materia_id = Convert.ToInt32(dr["materia_id"]);
                User_Mat.Estado_Materia_id = Convert.ToInt32(dr["Estado_Materia_id"]);
                User_Mat.Nota_Valor = Convert.ToInt32(dr["Nota_Valor"]);
                

                Usuario_Materia.Add(User_Mat);
            }

            return Usuario_Materia;
        }

        public static Usuario_Materia Obtener(int id)
        {
            Usuario_Materia User_Mat = new Usuario_Materia();

            SqlDataReader dr = Datos.Usuarios_Materias.Get_Usuario_Materia(id);
            while (dr.Read())
            {
                User_Mat.Usuario_Materia_id = Convert.ToInt32(dr["Usuario_Materia_id"]);
                User_Mat.user_id = Convert.ToInt32(dr["user_id"]);
                User_Mat.materia_id = Convert.ToInt32(dr["materia_id"]);
                User_Mat.Estado_Materia_id = Convert.ToInt32(dr["Estado_Materia_id"]);
                User_Mat.Nota_Valor = Convert.ToInt32(dr["Nota_Valor"]);

            }

            return User_Mat;
        }

        public static Usuario_Materia Obtener_por_user_y_materia(int user_id, int materia_id)
        {
            Usuario_Materia User_Mat = new Usuario_Materia();

            SqlDataReader dr = Datos.Usuarios_Materias.Get_Usuario_Materia(user_id, materia_id);
            while (dr.Read())
            {
                User_Mat.Usuario_Materia_id = Convert.ToInt32(dr["Usuario_Materia_id"]);
                User_Mat.user_id = Convert.ToInt32(dr["user_id"]);
                User_Mat.materia_id = Convert.ToInt32(dr["materia_id"]);
                if(dr["Estado_Materia_id"] != DBNull.Value)
                {
                    User_Mat.Estado_Materia_id = Convert.ToInt32(dr["Estado_Materia_id"]);
                    User_Mat.Nota_Valor = Convert.ToInt32(dr["Nota_Valor"]);
                }

            }

            return User_Mat;
        }

        public bool Existe(int user_id, int materia_id)
        {
            int resultado = 0;

            int result = Convert.ToInt32(Datos.Usuarios_Materias.Validate_Usuario_Materia(user_id, materia_id));

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

            resultado = Convert.ToInt32(Datos.Usuarios_Materias.Delete_Usuario_Materia(id));

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

            if (string.IsNullOrEmpty(Convert.ToString(this.user_id)))
                error += "el ID de la Nota se encuentra vacio" + Environment.NewLine;

            if (string.IsNullOrEmpty(Convert.ToString(this.materia_id)))
                error += "la ID de la Materia se encuentra vacio" + Environment.NewLine;

            if (string.IsNullOrEmpty(error))
                return true;
            else
                return false;

        }














    }
}
