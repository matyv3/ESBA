using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            if (Session["user"] == null)
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
            return View();
        }
    }
}