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
            return RedirectToAction("Index","Panel");
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