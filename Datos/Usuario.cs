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
    public class Usuario
    {
        

        public static int Insert_User(string document, string name, string surname, string sexo, string address, string phone, string user_mail, string user_password, string rol_id)
        {
            try
            {
                int id = 0;

                SqlConnection cn = new SqlConnection("server=. ; database = ESBA_WEB ; integrated security = true");

                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_Insert_User", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@document", document);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@surname", surname);
                    cmd.Parameters.AddWithValue("@sexo", sexo);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@user_mail", user_mail);
                    cmd.Parameters.AddWithValue("@user_password", user_password);
                    cmd.Parameters.AddWithValue("@rol_id", rol_id);
                    cmd.Parameters.Add("@mensaje", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@user_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        id = Convert.ToInt32(cmd.Parameters["@user_id"]);
                    }

                cn.Close();
                return id;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }


        public static string Update_User(int user_id, string document, string name, string surname, string sexo, string address, string phone, string user_mail, string user_password, string rol_id)
        {
            try
            {
                string msj="";

                SqlConnection cn = new SqlConnection("server=. ; database = ESBA_WEB ; integrated security = true");

                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_Update_User", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user_id", user_id);
                cmd.Parameters.AddWithValue("@document", document);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@surname", surname);
                cmd.Parameters.AddWithValue("@sexo", sexo);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@user_mail", user_mail);
                cmd.Parameters.AddWithValue("@user_password", user_password);
                cmd.Parameters.AddWithValue("@rol_id", rol_id);
                cmd.Parameters.Add("@mensaje", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;


                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    msj = Convert.ToString(cmd.Parameters["@mensaje"].Value.ToString());
                }

                cn.Close();
                return msj;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public static string Delete_User(int user_id)
        {
            try
            {
                string msj = "";

                SqlConnection cn = new SqlConnection("server=. ; database = ESBA_WEB ; integrated security = true");

                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_Insert_User", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@int user_id", user_id);
                cmd.Parameters.Add("@mensaje", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;


                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    msj = Convert.ToString(cmd.Parameters["@mensaje"].Value.ToString());
                }

                cn.Close();
                return msj;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}
