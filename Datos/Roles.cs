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
    class Roles
    {
        public Roles() { }


        /// <summary>
        /// Insert de Materia
        /// </summary>
        /// <param name="materia_name"></param>
        /// <param name="cant_modulos"></param>
        /// <returns> Devuelve ID de materia insertada o 0 en caso de error </returns>
        public static int Insert_Roles(string descripcion)
        {
            try
            {
                int id = 0;

                SqlConnection cn = new SqlConnection("server= . ; database = ESBA_WEB ; integrated security = true");

                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_Insert_Roles", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@descripcion", descripcion);


                cmd.Parameters.Add("@mensaje", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                id = Convert.ToInt32(cmd.Parameters["@mensaje"].Value.ToString());
                cn.Close();
                return id;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Materia_id"></param>
        /// <param name="nombre"></param>
        /// <param name="cant_modulos"></param>
        /// <returns> Devuelve 1 si fue exitoso o 0 en caso de error </returns>
        public static int Update_Roles(int rol_id, string descripcion)
        {
            try
            {
                int resultado = 0;

                SqlConnection cn = new SqlConnection("server= .; database = ESBA_WEB ; integrated security = true");

                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_Update_Roles", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@rol_id", rol_id);
                cmd.Parameters.AddWithValue("@descripcion", descripcion);


                cmd.Parameters.Add("@mensaje", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                resultado = Convert.ToInt32(cmd.Parameters["@mensaje"].Value.ToString());


                cn.Close();
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="materia_id"></param>
        /// <returns> Devuelve 1 si fue exitoso o 0 en caso de error </returns>
        public static int Delete_Roles(int rol_id)
        {
            try
            {
                int resultado = 0;

                SqlConnection cn = new SqlConnection("server= . ; database = ESBA_WEB ; integrated security = true");

                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_Delete_Roles", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@rol_id", rol_id);
                cmd.Parameters.Add("@mensaje", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                resultado = Convert.ToInt32(cmd.Parameters["@mensaje"].Value.ToString());


                cn.Close();
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Materia_id"></param>
        /// <returns> Devuelve un SQL Reader con los Datos de Materia </returns>
        public static SqlDataReader Get_Roles(int rol_id)
        {
            SqlConnection cn = new SqlConnection("server= . ; database = ESBA_WEB ; integrated security = true");
            try
            {

                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_Get_Roles", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@rol_id", rol_id);
                return cmd.ExecuteReader();



            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
                cn.Close();
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="materia_name"></param>
        /// <returns>  Devuelve 1 si fue exitoso o 0 en caso de error  </returns>
        public static int Validate_Roles(string descripcion)
        {
            try
            {
                int resultado = 0;

                SqlConnection cn = new SqlConnection("server= . ; database = ESBA_WEB ; integrated security = true");

                cn.Open();

                SqlCommand cmd = new SqlCommand("sp_Validate_Roles", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@descripcion", descripcion);
                cmd.Parameters.Add("@mensaje", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                resultado = Convert.ToInt32(cmd.Parameters["@mensaje"].Value.ToString());
                cn.Close();

                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }



        public static SqlDataReader GetALL_Roles()
        {
            SqlConnection cn = new SqlConnection("server= . ; database = ESBA_WEB ; integrated security = true");
            try
            {

                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_GetAll_Roles", cn);
                cmd.CommandType = CommandType.StoredProcedure;
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
