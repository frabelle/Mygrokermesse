using KermesseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KermesseApp.Controllers
{
    public class Tbl_ArqueoCajaController : Controller
    {

        private KERMESSEEntities db = new KERMESSEEntities();
        // GET: Tbl_ArqueoCaja
        public ActionResult Tbl_ArqueoCaja()
        {
            return View(db.tbl_arqueocaja.ToList());
        }

        public ActionResult Vw_ArqueoCaja()
        {
            return View(db.vw_arqueocaja.ToList());
        }

        public ActionResult InsertArqueoCaja()
        {
            ViewBag.idkermesse = new SelectList(db.tbl_kermesse, "id_kermesse", "nombre");
            return View();
        }

    }
}