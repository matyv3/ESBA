﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio
{
    public class Materia
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public int CantModulos { get; set; }


        public bool Grabar(out string error)
        {
            error = "";
            if (this.Id.HasValue)
            {
                return true;
            }
            else
            {
                if(validar(out error))
                {
                    int result = Datos.Materias.Insert_Materia(
                        this.Nombre,
                        this.CantModulos
                    );
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
            Usuario Materia = new Usuario();

            SqlDataReader dr = Datos.Usuarios.Get_User(id);
            while (dr.Read())
            {
                Materia.Id = Convert.ToInt32(dr["user_id"]);
                Materia.Nombre = dr["document"].ToString();
                Materia.CantModulos = dr["name"].ToString();
                usuario.surname = dr["surname"].ToString();
                usuario.sexo = dr["sexo"].ToString();
                usuario.Address = dr["address"].ToString();
                usuario.Phone = dr["phone"].ToString();
                usuario.Email = dr["mail"].ToString();
                usuario.Password = dr["password"].ToString();
                usuario.Rol = dr["descripcion"].ToString();
                usuario.rol_id = Convert.ToInt32(dr["rol_id"]);
            }

            return Materia;
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
