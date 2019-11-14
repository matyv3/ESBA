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
    class Notas
    {
        public Notas() { }


        /// <summary>
        /// Insert de Materia
        /// </summary>
        /// <param name="materia_name"></param>
        /// <param name="cant_modulos"></param>
        /// <returns> Devuelve ID de Nota insertada o 0 en caso de error </returns>
        public static int Insert_Notas(int nota_valor)
        {
            try
            {
                int id = 0;

                SqlConnection cn = new SqlConnection("server= . ; database = ESBA_WEB ; integrated security = true");

                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_Insert_Notas", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nota_valor", nota_valor);


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
        public static int Update_Notas(int Nota_id, int nota_valor)
        {
            try
            {
                int resultado = 0;

                SqlConnection cn = new SqlConnection("server= .; database = ESBA_WEB ; integrated security = true");

                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_Update_Notas", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nota_id", Nota_id);
                cmd.Parameters.AddWithValue("@nota_valor", nota_valor);


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
        public static int Delete_Notas(int Nota_id)
        {
            try
            {
                int resultado = 0;

                SqlConnection cn = new SqlConnection("server= . ; database = ESBA_WEB ; integrated security = true");

                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_Delete_Notas", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nota_id", Nota_id);
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
        public static SqlDataReader Get_Notas(int Nota_id)
        {
            SqlConnection cn = new SqlConnection("server= . ; database = ESBA_WEB ; integrated security = true");
            try
            {

                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_Get_Notas", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nota_id", Nota_id);
                return cmd.ExecuteReader();



            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
                cn.Close();
            }

        }

        public static int Validate_Notas(int nota_valor)
        {
            try
            {
                int resultado = 0;

                SqlConnection cn = new SqlConnection("server= . ; database = ESBA_WEB ; integrated security = true");

                cn.Open();

                SqlCommand cmd = new SqlCommand("sp_Validate_Notas", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nota_valor", nota_valor);
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




        public static SqlDataReader GetALL_Notas()
        {
            SqlConnection cn = new SqlConnection("server= . ; database = ESBA_WEB ; integrated security = true");
            try
            {

                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_GetAll_Notas", cn);
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
