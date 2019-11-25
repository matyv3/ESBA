﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Materia
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public int CantModulos { get; set; }

        List<Usuario> alumnos = new List<Usuario>();


        public bool Grabar(out string error)
        {
            error = "";
            if (this.Id.HasValue)
            {

                int result = Convert.ToInt32(Datos.Materias.Update_Materia(this.Id.Value, this.Nombre, this.CantModulos));

                return true;
            }
            else
            {
                if(validar(out error))
                {
                    int result = Convert.ToInt32(Datos.Materias.Insert_Materia(
                        this.Nombre,
                        this.CantModulos
                    ));
                    return result == 1;
                }
                else
                {
                    return false;
                }
                
            }
        }

        public static List<Materia> ObtenerTodas()
        {
            List<Materia> materias = new List<Materia>();
            SqlDataReader dr = Datos.Materias.GetALL_Materia();
            while (dr.Read())
            {
                Materia materia = new Materia();
                materia.Id = Convert.ToInt32(dr["Materia_id"]);
                materia.Nombre = dr["nombre"].ToString();
                materia.CantModulos = Convert.ToInt32(dr["Cant_Modulos"]);
                materias.Add(materia);
            }

            return materias;
        }


        public static Materia Obtener(int id)
        {
            Materia Materia = new Materia();

            SqlDataReader dr = Datos.Materias.Get_Materias(id);
            while (dr.Read())
            {
                Materia.Id = Convert.ToInt32(dr["Materia_id"]);
                Materia.Nombre = dr["nombre"].ToString();
                Materia.CantModulos = Convert.ToInt32(dr["Cant_Modulos"]);

            }

            if(Materia.Id != null)
            {
                SqlDataReader data = Datos.Querys.Query_For_MateriaID(Convert.ToInt32(Materia.Id));
                while (data.Read())
                {
                    Usuario alumno = new Usuario();
                    alumno.user_id = Convert.ToInt32(dr["U.User_id"]);
                    alumno.document = dr["U.document"].ToString();
                    alumno.name = dr["User_nombre"].ToString();
                    alumno.surname = dr["User_Apellido"].ToString();
                    alumno.Address = dr["U.address"].ToString();
                    alumno.Phone = dr["U.phone"].ToString();
                    alumno.Email = dr["U.mail"].ToString();
                    alumno.Rol = dr["Rol_Descripcion"].ToString();
                    Materia.alumnos.Add(alumno);
                }
            }

            return Materia;
        }

        public bool Existe(string Nombre)
        {
            int resultado = 0;

            int result = Convert.ToInt32(Datos.Materias.Validate_Materia(Nombre));

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

            resultado = Convert.ToInt32(Datos.Materias.Delete_Materia(id));

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

            if (string.IsNullOrEmpty(Nombre))
                error += "el nombre se encuentra vacio" + Environment.NewLine;

            if (CantModulos == 0)
                error += "cantidad de modulos incorrecta" + Environment.NewLine;

            if (string.IsNullOrEmpty(error))
                return true;
            else
                return false;

        }





    }
}
