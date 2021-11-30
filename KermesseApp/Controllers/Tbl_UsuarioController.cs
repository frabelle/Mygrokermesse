using KermesseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KermesseApp.Controllers
{
    public class Tbl_UsuarioController : Controller
    {
        // GET: Tbl_Usuario

        private KERMESSEEntities db = new KERMESSEEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (String.IsNullOrEmpty(username) && String.IsNullOrEmpty(password))
            {
                return View("ViewLogin");
            }
            else
            {
                var objeto = db.tbl_usuario.Where(x => x.usuario.Equals(username) && x.pwd.Equals(password)).FirstOrDefault();
                if (objeto != null)
                {
                    Session["usuario"] = objeto;
                    return Redirect("~/Home/Index");
                    //return RedirectToRoute("Default", new { controller = "Home", action = "Index" });
                    //return this.RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "¡Los datos de accesos son incorrectos, por favor intente nuevamente.";
                    return View("ViewLogin");
                }
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Remove("usuario");
            return Redirect("~/tbl_usuario/ViewLogin");
            //return View("ViewLogin");
        }
    }

}
