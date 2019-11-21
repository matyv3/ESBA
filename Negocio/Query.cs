using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Negocio
{
    class Query
    {

        public int User_id;

        public string Rol_descripcion;

        public string Usuario_Nombre;

        public string Usuario_Apellido;

        public int Materia_id;

        public string Materia_nombre;

        public int Nota_valor;

        public string Estado_Materia_Descripcion;



        public static List<Query> ObtenerQuery_For_UserID(int User_id)
        {
            List<Query> Querys = new List<Query>();


            SqlDataReader dr = Datos.Querys.Query_For_UserID(User_id);
            while (dr.Read())
            {
                Query Query = new Query();

                Query.User_id = Convert.ToInt32(dr["User_id"]);
                Query.Rol_descripcion = dr["Rol_Descripcion"].ToString();
                Query.Usuario_Nombre = dr["User_Nombre"].ToString();
                Query.Usuario_Apellido = dr["User_Apellido"].ToString();
                Query.Materia_id = Convert.ToInt32(dr["Materia_id"]);
                Query.Materia_nombre = dr["Materia_Nombre"].ToString();
                Query.Nota_valor = Convert.ToInt32(dr["Nota_Valor"]);
                Query.Estado_Materia_Descripcion = dr["Estado_Materia"].ToString();


                Querys.Add(Query);
            }

            return Querys;
        }




        public static List<Query> ObtenerQuery_For_MateriaID(int Materia_id)
        {
            List<Query> Querys = new List<Query>();


            SqlDataReader dr = Datos.Querys.Query_For_MateriaID(Materia_id);
            while (dr.Read())
            {
                Query Query = new Query();

                Query.User_id = Convert.ToInt32(dr["User_id"]);
                Query.Rol_descripcion = dr["Rol_Descripcion"].ToString();
                Query.Usuario_Nombre = dr["User_Nombre"].ToString();
                Query.Usuario_Apellido = dr["User_Apellido"].ToString();
                Query.Materia_id = Convert.ToInt32(dr["Materia_id"]);
                Query.Materia_nombre = dr["Materia_Nombre"].ToString();
                Query.Nota_valor = Convert.ToInt32(dr["Nota_Valor"]);
                Query.Estado_Materia_Descripcion = dr["Estado_Materia"].ToString();


                Querys.Add(Query);
            }

            return Querys;
        }



    }
}
