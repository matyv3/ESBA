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
            return RedirectToAction("Materias","Panel");
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
            if (id != null)
            {
                Materia materia = Materia.Obtener(Convert.ToInt32(id));
                return View("Materia", materia);
            }
            else
            {
                Usuario user = Usuario.Obtener(Convert.ToInt32(Session["user_id"]));
                List<Materia> materias = Materia.ObtenerTodas();
                ViewBag.rol = user.Rol;
                return View(materias);
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
            if(user.Rol == "alumno")
            {
                return RedirectToAction("NotFound","Error");
            }

            // si es profesor hay que traer los alumnos suyos
            // si es administrativo todos
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

        public ActionResult Historial() {
            return View();
        }
    }
}