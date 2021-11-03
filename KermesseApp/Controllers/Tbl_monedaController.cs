using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public String ListarSimbolo(FormCollection fc)
        {
            int id = Int32.Parse(fc["moneda"]);
            var tblmoneda = db.tbl_moneda.Where(x => x.id_moneda == id).First();
            var signo = tblmoneda.signo;

            return signo;
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

        public ActionResult VerMoneda(int id)
        {
            var tblmoneda = db.tbl_moneda.Where(x => x.id_moneda == id).First();

            return View(tblmoneda);
        }

        public ActionResult EditMoneda(int id)
        {
            tbl_moneda tbm = db.tbl_moneda.Find(id);

            if(tbm == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(tbm);
            }
        }

        [HttpPost]
        public ActionResult UpdateMoneda(tbl_moneda m)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    m.estado = 2;
                    db.Entry(m).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Tbl_moneda");
            }
            catch
            {
                return View();
            }

        }

    }
}