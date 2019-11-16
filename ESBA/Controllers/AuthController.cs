using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESBA.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        
        /// <summary>
        /// Esto se ejecuta antes que cualquier metodo
        /// sirve para validar si tiene permisos
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Session["user_id"] != null)
            {
                filterContext.Result = RedirectToAction("Index", "Panel");
                return;
            }
            base.OnActionExecuted(filterContext);
        }
        public ActionResult Index()
        {
            if (TempData["error"] != null)
            {
                ViewBag.error = TempData["error"].ToString();
            }
            if (TempData["success"] != null)
            {
                ViewBag.success = TempData["success"].ToString();
            }
            return View();
        }

        public ActionResult Login()
        {
            string email = Request.Form["email"];
            string password = Request.Form["password"];
            int user_id = Usuario.Validar(email, password);
            if (user_id != 0 && user_id != -1)
            {
                // guardar usuario en sesion
                Usuario user = Usuario.Obtener(user_id);
                Session["user_id"] = user.user_id;
                Session["Rol"] = user.Rol;
                return RedirectToAction("Index","Panel");
            }
            else
            {
                TempData["error"] = "Datos incorrectos";
                return RedirectToAction("Index","Auth");
            }
        }

        public ActionResult Register()
        {
            if (TempData["error"] != null)
            {
                ViewBag.error = TempData["error"].ToString();
            }
            if (TempData["success"] != null)
            {
                ViewBag.success = TempData["success"].ToString();
            }
            return View();
        }

        public ActionResult GuardarUsuario(Usuario usuario)
        {
            usuario.rol_id = 1; // Alumno por default
             
            if (!usuario.Existe())
            {
                if (usuario.Grabar(out string error))
                {
                    TempData["success"] = "Ingrese usuario y contraseña para loguearse";
                    return RedirectToAction("Index", "Auth");
                }
                else
                {
                    TempData["error"] = "Error al grabar: " + error;
                    return RedirectToAction("Index", "Auth");
                }
            }
            else
            {
                TempData["error"] = "Email ya registrado";
                return RedirectToAction("Register", "Auth");
            }
        }
    }
}