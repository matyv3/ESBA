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
                List<Estado_Materia> estados = Estado_Materia.ObtenerTodas();
                List<SelectListItem> selectEstados = new List<SelectListItem>();
                foreach(var estado in estados)
                {
                    selectEstados.Add(new SelectListItem()
                    {
                        Text = estado.Descripcion,
                        Value = estado.Estado_Materia_id.ToString()
                    });
                }
                ViewBag.Estados = selectEstados;
                Materia materia = Materia.Obtener(Convert.ToInt32(id));
                return View("Materia", materia);
            }
            else
            {
                if(user.Rol == "alumno")
                {
                    // si es alumno mostrar todas a las que se puede anotar
                    List<Materia> materias = Materia.ObtenerDisponibles(Convert.ToInt32(user.user_id));
                    return View(materias);
                }
                else if(user.Rol == "profesor")
                {
                    // si es profe mostrar solo las que da el
                    List<Materia> materias = Materia.ObtenerPorUsuario(Convert.ToInt32(user.user_id));
                    return View(materias);
                }
                else
                {
                    // el adminstrativo ve todas
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
            ViewBag.Rol = user.Rol;
            List<Usuario> estudiantes = Usuario.ObtenerUsuarios_Por_Rol(1);
            if (TempData["error"] != null)
            {
                ViewBag.error = TempData["error"].ToString();
            }
            if (TempData["success"] != null)
            {
                ViewBag.success = TempData["success"].ToString();
            }
            return View(estudiantes);
        }

        public ActionResult CrearUsuario(string rol_id) 
        {
            Usuario admin = Usuario.Obtener(Convert.ToInt32(Session["user_id"]));
            if (admin.Rol != "administrativo")
            {
                return RedirectToAction("NotFound", "Error");
            }
            Rol rol = Rol.Obtener(Convert.ToInt32(rol_id));
            Usuario user = new Usuario();
            user.rol_id = Convert.ToInt32(rol.rol_id);
            user.Rol = rol.descripcion;
            user.Password = "12345678";
            return View(user);
        }

        [HttpPost]
        public ActionResult GrabarUsuario(Usuario usuario)
        {
            string action = usuario.Rol == "alumno" ? "Estudiantes" : "Profesores";
            if (usuario.Grabar(out string error))
            {
                TempData["success"] = "Usuario guardado, se le envió por mail la contraseña: " + usuario.Password;
                return RedirectToAction(action, "Panel");
            }
            else
            {
                TempData["error"] = "Error al grabar: " + error;
                return RedirectToAction(action, "Panel");
            }            
        }

        public ActionResult Profesores()
        {
            Usuario user = Usuario.Obtener(Convert.ToInt32(Session["user_id"]));
            if (user.Rol != "administrativo")
            {
                return RedirectToAction("NotFound", "Error");
            }
            ViewBag.Rol = user.Rol;
            List<Usuario> profesores = Usuario.ObtenerUsuarios_Por_Rol(2);
            if (TempData["error"] != null)
            {
                ViewBag.error = TempData["error"].ToString();
            }
            if (TempData["success"] != null)
            {
                ViewBag.success = TempData["success"].ToString();
            }
            return View(profesores);
        }

        public ActionResult Historial()
        {
            List<Materia> materias = Materia.ObtenerPorUsuario(Convert.ToInt32(Session["user_id"]));
            return View(materias);
        }

        [HttpPost]
        public JsonResult Nota(int materia_id, int user_id, int nota, int? estado_materia)
        {
            Usuario_Materia um = Usuario_Materia.Obtener_por_user_y_materia(user_id, materia_id);
            um.Nota_Valor = nota;
            if (estado_materia.HasValue)
            {
                um.Estado_Materia_id = Convert.ToInt32(estado_materia);
            }
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