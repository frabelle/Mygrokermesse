using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KermesseApp.Models;

namespace KermesseApp.Controllers
{
    public class Tbl_DenominacionController : Controller
    {
        private KERMESSEEntities db = new KERMESSEEntities();
        // GET: Tbl_Denominaciones
        public ActionResult Tbl_Denominacion()
        {
            return View(db.tbl_denominacion.ToList());
        }

        public ActionResult Vw_Denominacion()
        {
            return View(db.vw_denominacion.ToList());
        }

        public ActionResult InsertDenominacion()
        {
            ViewBag.id_moneda = new SelectList(db.tbl_moneda, "id_moneda", "nombre");
            
            return View();
        }

        [HttpPost]
        public ActionResult InsertDenominacion(tbl_denominacion td)
        {
            if (ModelState.IsValid)
            {
                tbl_denominacion tbd = new tbl_denominacion();
                tbd.id_moneda = td.id_moneda;
                tbd.valor = td.valor;
                tbd.valor_letras = td.valor_letras;
                tbd.estado = 1;

                db.tbl_denominacion.Add(tbd);
                db.SaveChanges();
                ModelState.Clear();
            }

            ViewBag.id_moneda = new SelectList(db.tbl_moneda.Where(model => model.estado!=3), "id_moneda", "nombre");

            var list = db.vw_denominacion.ToList();
            
            return View("Vw_Denominacion", list);
        }

        //public ActionResult DeleteDenominacion(int id)
        //{
        //    tbl_denominacion tbd = new tbl_denominacion();
        //    tbd = db.tbl_denominacion.Find(id);
        //    db.tbl_denominacion.Remove(tbd);
        //    db.SaveChanges();

        //    var list = db.vw_denominacion.ToList();

        //    return View("Vw_Denominacion", list);
        //}

        public ActionResult DeleteDenominacion(int id)
        {
            tbl_denominacion tc = new tbl_denominacion();
            tc = db.tbl_denominacion.Find(id);
            this.DeleteDenominacion(tc);

            return RedirectToAction("Vw_Denominacion");
        }

        [HttpPost]
        public ActionResult DeleteDenominacion(tbl_denominacion td)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    td.estado = 3;
                    db.Entry(td).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Vw_Denominacion");
            }
            catch
            {
                return View();
                throw;
            }
        }

        public ActionResult EditDenominacion(int id)
        {
            tbl_denominacion tbd = db.tbl_denominacion.Find(id);
            ViewBag.id_moneda = new SelectList(db.tbl_moneda.Where(model => model.estado!=3), "id_moneda", "nombre");

            if (tbd == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.id_moneda = new SelectList(db.tbl_moneda, "id_moneda", "nombre");
                return View(tbd);
            }
        }

        [HttpPost]
        public ActionResult UpdateDenominacion(tbl_denominacion d)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    d.estado = 2;
                    db.Entry(d).State = EntityState.Modified;
                    db.SaveChanges();
                }

                ViewBag.id_moneda = new SelectList(db.tbl_moneda.Where(model => model.estado != 3), "id_moneda", "nombre");
                return RedirectToAction("Vw_Denominacion");
            }
            catch
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult FilterDenominacion(String cadena)
        {
            if (String.IsNullOrEmpty(cadena))
            {
                var list = db.vw_denominacion.ToList();
                return View("Vw_Denominacion", list);
            }
            else
            {
                var listFiltrada = db.vw_denominacion.Where(x => x.nombre.Contains(cadena) || x.valor_letras.Contains(cadena) || x.equivalente.Contains(cadena));
                return View("Vw_Denominacion", listFiltrada);
            }
        }

    }
}