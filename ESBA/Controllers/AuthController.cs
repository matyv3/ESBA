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
            if (Usuario.Validar(email, password))
            {
                // guardar usuario en sesion
                Session["fafa"] = "fafafa";
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

        public ActionResult GuardarUsuario(Usuario user)
        {

            return RedirectToAction("Index", "Auth");
        }
    }
}