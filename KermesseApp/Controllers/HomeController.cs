using KermesseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KermesseApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        private KERMESSEEntities db = new KERMESSEEntities();

        public ActionResult Reportes()
        {
            ViewBag.id_comunidad = new SelectList(db.tbl_comunidad, "id_comunidad", "nombre");
            ViewBag.id_kermeses = new SelectList(db.tbl_kermesse, "id_kermesse", "nombre");
            ViewBag.id_kermesse = new SelectList(db.vw_arqueocaja, "nombre", "nombre");
            ViewBag.id_listas = new SelectList(db.tbl_listaprecio, "id_listaprecio", "nombre");
            ViewBag.id_producto = new SelectList(db.tbl_productos, "id_producto", "nombre");
            ViewBag.resumen = new SelectList(db.vw_resumen, "nombre", "nombre");

            return View();
        }
    }
}