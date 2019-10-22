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
        public ActionResult Index()
        {
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
                Session["user"] = user;
                return RedirectToAction("Index","Panel");
            }
            else
            {
                return RedirectToAction("Index","Auth");
            }
        }

        public ActionResult Register()
        {
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
            return RedirectToAction("Index", "Auth");
        }
    }
}