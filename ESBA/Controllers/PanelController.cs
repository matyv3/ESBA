using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;

namespace ESBA.Controllers
{
    public class PanelController : Controller
    {
        // GET: Panel

        /// <summary>
        /// Esto se ejecuta antes que cualquier metodo
        /// sirve para validar si tiene permisos
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Session["user_id"] == null)
            {
                filterContext.Result = RedirectToAction("Index", "Auth");
                return;
            }
            base.OnActionExecuted(filterContext);
        }


        public ActionResult Index()
        {
            return RedirectToAction("Materias", "Panel");
        }

        public ActionResult Perfil()
        {
            Usuario user = Usuario.Obtener(Convert.ToInt32(Session["user_id"]));
            if (TempData["error"] != null)
            {
                ViewBag.error = TempData["error"].ToString();
            }
            if (TempData["success"] != null)
            {
                ViewBag.success = TempData["success"].ToString();
            }
            return View(user);
        }

        public ActionResult ActualizarUsuario(Usuario user)
        {
            if (user.Grabar(out string error))
            {
                TempData["success"] = "Perfil actualizado";
                return RedirectToAction("Perfil", "Panel");
            }
            else
            {
                TempData["error"] = "Error al grabar: " + error;
                return RedirectToAction("Perfil", "Panel");
            }
        }

        public ActionResult Logout()
        {
            Session["user_id"] = null;
            Session["Rol"] = null;
            return RedirectToAction("Index", "Auth");
        }

        public ActionResult Materias(int? id)
        {
            Usuario user = Usuario.Obtener(Convert.ToInt32(Session["user_id"]));
            ViewBag.rol = user.Rol;
            if (id != null)
            {
                //List<Estado_Materia> estados = Estado_Materia.ObtenerTodas();

                Materia materia = Materia.Obtener(Convert.ToInt32(id));
                return View("Materia", materia);
            }
            else
            {
                // si es alumno mostrar todas a las que se puede anotar
                if(user.Rol == "alumno")
                {
                    List<Materia> materias = Materia.ObtenerDisponibles(Convert.ToInt32(user.user_id));
                    return View(materias);
                }
                else
                {
                    // si es profe mostrar solo las que da el
                    List<Materia> materias = Materia.ObtenerTodas();
                    return View(materias);
                }

            }

        }

        public ActionResult CrearMateria()
        {
            return View();
        }

        public ActionResult GrabarMateria(Materia materia)
        {
            string error = "";
            if (materia.Grabar(out error))
            {
                TempData["success"] = "Materia guardada";
                return RedirectToAction("Materias", "Panel");
            }
            else
            {
                TempData["error"] = "Error al grabar: " + error;
                return RedirectToAction("Materias", "Panel");
            }
        }

        public ActionResult Estudiantes()
        {
            Usuario user = Usuario.Obtener(Convert.ToInt32(Session["user_id"]));
            if (user.Rol != "administrativo")
            {
                return RedirectToAction("NotFound", "Error");
            }
            List<Usuario> estudiantes = Usuario.ObtenerUsuarios_Por_Rol(1);
            return View(estudiantes);
        }

        public ActionResult Profesores()
        {
            Usuario user = Usuario.Obtener(Convert.ToInt32(Session["user_id"]));
            if (user.Rol != "administrativo")
            {
                return RedirectToAction("NotFound", "Error");
            }
            List<Usuario> profesores = Usuario.ObtenerUsuarios_Por_Rol(2);
            return View(profesores);
        }

        public ActionResult Historial()
        {
            List<Materia> materias = Materia.ObtenerPorAlumno(Convert.ToInt32(Session["user_id"]));
            return View(materias);
        }

        [HttpPost]
        public JsonResult Nota(int materia_id, int user_id, int nota)
        {
            Usuario_Materia um = Usuario_Materia.Obtener_por_user_y_materia(user_id, materia_id);
            um.Nota_Valor = nota;
            um.Grabar(out string error);

            if (!string.IsNullOrEmpty(error))
            {
                return Json(error);
            }
            return Json("sucess");
        }

        [HttpPost]
        public JsonResult Inscribirse(int materia_id)
        {
            Usuario_Materia um = new Usuario_Materia();
            um.materia_id = materia_id;
            um.user_id = Convert.ToInt32(Session["user_id"]);
            um.Nota_Valor = 0;
            um.Estado_Materia_id = 1;
            um.Grabar(out string error);
            if (!string.IsNullOrEmpty(error))
            {
                return Json(error);
            }
            return Json("success");
        }
    }
}