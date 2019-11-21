using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Querys
    {


        public Querys() { }

        public static SqlDataReader Query_For_UserID(int user_id)
        {
            SqlConnection cn = new SqlConnection("server= . ; database = ESBA_WEB ; integrated security = true");
            try
            {

                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_Query_User_id", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user_id", user_id);
                return cmd.ExecuteReader();



            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
                cn.Close();
            }

        }

        public static SqlDataReader Query_For_MateriaID(int Materia_id)
        {
            SqlConnection cn = new SqlConnection("server= . ; database = ESBA_WEB ; integrated security = true");
            try
            {

                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_Query_User_id", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Materia_id", Materia_id);
                return cmd.ExecuteReader();



            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
                cn.Close();
            }

        }


    }
}
