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
    public class Usuarios
    {
        /* Pruebas en Aplicacion de consola 
         
            Console.WriteLine("Su Id de usuario es: " + User.Insert_User("44444", "lucas", "barreiro", "M", "jorge 12345", "1234-5678", "asdasd@gmail.com", "312123asdads", 2));

            Console.ReadKey();

            Console.WriteLine(User.Delete_User(2));

            Console.ReadKey();

            Console.WriteLine(User.Update_User(1, "44444", "enfermera", "rafaela", "M", "jorge 12345", "1234-5678","lorenzo@gmail.com", "312123asdads", 2));

            Console.ReadKey();

        */


        public Usuarios() { }

        
        public static int Insert_User(string document, string name, string surname, string sexo, string address, string phone, string user_mail, string user_password, string rol_id)
        {
            try
            {
                string msj = "";
                int id = 0;

                SqlConnection cn = new SqlConnection("server= . ; database = ESBA_WEB ; integrated security = true");

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

                cmd.ExecuteNonQuery();

                id = Convert.ToInt32(cmd.Parameters["@user_id"].Value.ToString());
                msj = Convert.ToString(cmd.Parameters["@mensaje"].Value.ToString());
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
                string msj = "";

                SqlConnection cn = new SqlConnection("server= .; database = ESBA_WEB ; integrated security = true");

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
                cmd.Parameters.AddWithValue("@mail", user_mail);
                cmd.Parameters.AddWithValue("@password", user_password);
                cmd.Parameters.AddWithValue("@rol_id", rol_id);
                cmd.Parameters.Add("@mensaje", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                msj = Convert.ToString(cmd.Parameters["@mensaje"].Value.ToString());


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

                SqlConnection cn = new SqlConnection("server= . ; database = ESBA_WEB ; integrated security = true");

                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_Delete_User", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user_id", user_id);
                cmd.Parameters.Add("@mensaje", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;


                cmd.ExecuteNonQuery();

                msj = Convert.ToString(cmd.Parameters["@mensaje"].Value.ToString());


                cn.Close();
                return msj;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }

        public static SqlDataReader Get_User(int user_id)
        {
            SqlConnection cn = new SqlConnection("server= . ; database = ESBA_WEB ; integrated security = true");
            try
            {
        
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_GetUser", cn);
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
        public static int Validate_User(string user_mail, string user_password)
        {
            try
            {


                SqlConnection cn = new SqlConnection("server= . ; database = ESBA_WEB ; integrated security = true");

                cn.Open();

                SqlCommand cmd = new SqlCommand("sp_Validate_User", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user_mail", user_mail);
                cmd.Parameters.AddWithValue("@user_password", user_password);
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@user_id"].Value.ToString());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static SqlDataReader GetALL_Users()
        {
            SqlConnection cn = new SqlConnection("server= . ; database = ESBA_WEB ; integrated security = true");
            try
            {

                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_GetAll_Users", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                return cmd.ExecuteReader();



            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
                cn.Close();
            }

        }

        public static SqlDataReader GetALL_User_For_Rol(int rol_id)
        {
            SqlConnection cn = new SqlConnection("server= . ; database = ESBA_WEB ; integrated security = true");
            try
            {

                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_Get_ALLUser_Rol", cn);
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


    }
}
