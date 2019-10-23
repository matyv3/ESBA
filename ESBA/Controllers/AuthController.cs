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
            if (TempData["success"] != null)
            {
                ViewBag.success = TempData["success"].ToString();
            }
            return View();
        }

        public ActionResult GuardarUsuario()
        {
            Usuario user = new Usuario();
            user.name = Request.Form["name"];
            user.surname = Request.Form["surname"];
            user.Email = Request.Form["Email"];
            user.Password = Request.Form["Password"];
            user.Phone = Request.Form["Phone"];
            user.sexo = Request.Form["sexo"];
            user.Address = Request.Form["Address"];
            user.document = Request.Form["document"];
            user.Rol = "1"; // todo: 
            user.Grabar();
            TempData["success"] = "Registrado correctamente. Escriba email y contraseña para ingresar";
            return RedirectToAction("Index", "Auth");
        }
    }
}