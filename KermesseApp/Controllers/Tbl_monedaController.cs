using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KermesseApp.Models;

namespace KermesseApp.Controllers
{
    public class Tbl_monedaController : Controller
    {
        private KERMESSEEntities db = new KERMESSEEntities();
        // GET: Tbl_moneda
        public ActionResult Tbl_moneda()
        {
            return View(db.tbl_moneda.ToList());
        }

        public ActionResult InsertMoneda()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertMoneda(tbl_moneda tm)
        {
            if (ModelState.IsValid)
            {
                tbl_moneda tbm = new tbl_moneda();
                tbm.nombre = tm.nombre;
                tbm.signo = tm.signo;
                tbm.estado = 1;

                db.tbl_moneda.Add(tbm);
                db.SaveChanges();
            }

            ModelState.Clear();

            var list = db.tbl_moneda.ToList();

            return View("Tbl_moneda", list);
        }

        public ActionResult DeleteMoneda(int id)
        {
            tbl_moneda tbm = new tbl_moneda();
            tbm = db.tbl_moneda.Find(id);
            db.tbl_moneda.Remove(tbm);
            db.SaveChanges();

            var list = db.tbl_moneda.ToList();

            return View("Tbl_moneda", list);
        }

    }
}