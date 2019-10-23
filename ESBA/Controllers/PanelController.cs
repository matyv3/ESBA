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
            return View();
        }

        public ActionResult Perfil()
        {
            Usuario user = Usuario.Obtener(Convert.ToInt32(Session["user_id"]));

            return View(user);
        }

        public ActionResult ActualizarUsuario()
        {
            return RedirectToAction("Index", "Panel");
        }

        public ActionResult Logout()
        {
            Session["user_id"] = null;
            return RedirectToAction("Index", "Auth");
        }
    }
}