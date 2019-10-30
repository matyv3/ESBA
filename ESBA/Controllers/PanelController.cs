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
            return RedirectToAction("Index", "Auth");
        }
    }
}