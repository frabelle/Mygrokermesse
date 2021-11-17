using KermesseApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            ViewBag.id_kermesse = new SelectList(db.tbl_kermesse, "id_kermesse", "nombre");
            ViewBag.id_moneda = new SelectList(db.tbl_moneda, "id_moneda", "nombre");
            ViewBag.id_denominacion = new SelectList(db.vw_cordoba, "id_denominacion", "valor");

            return View();
        }

        public ActionResult DeleteArqueoCaja(int id)
        {
            tbl_arqueocaja tc = new tbl_arqueocaja();
            tc = db.tbl_arqueocaja.Find(id);
            this.DeleteArqueoCaja(tc);

            return RedirectToAction("Vw_ArqueoCaja");
        }

        [HttpPost]
        public ActionResult DeleteArqueoCaja(tbl_arqueocaja tac)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tac.estado = 3;
                    db.Entry(tac).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Vw_ArqueoCaja");
            }
            catch
            {
                return View();
                throw;
            }
        }

        public ActionResult GuardarArqueoCaja(int kermesse, DateTime fecha_arqueo, string total, tbl_arqueocaja_det[] detalle)
        {
            ViewBag.id_kermesse = new SelectList(db.tbl_kermesse, "id_kermesse", "nombre");
            ViewBag.id_moneda = new SelectList(db.tbl_moneda, "id_moneda", "nombre");
            ViewBag.id_denominacion = new SelectList(db.vw_denominacion, "id_Denominacion", "valor");

            tbl_arqueocaja arqueo = new tbl_arqueocaja();
            arqueo.idkermesse = kermesse;
            arqueo.fecha_arqueo = fecha_arqueo;
            arqueo.gran_total = Decimal.Parse(total);
            arqueo.fecha_creacion = DateTime.Parse("2020-11-12 00:00:00.000");
            arqueo.estado = 1;
            db.tbl_arqueocaja.Add(arqueo);

            foreach (var item in detalle)
            {
                tbl_arqueocaja_det det = new tbl_arqueocaja_det();
                det.id_arqueocaja = arqueo.id_arqueocaja;
                det.id_moneda = 1;
                det.id_denominacion = item.id_denominacion;
                det.cantidad = item.cantidad;
                det.subtotal = item.subtotal;
                db.tbl_arqueocaja_det.Add(det);
            }
            db.SaveChanges();

            String result = "¡El arqueo caja ha sido guardado satisfactoriamente!";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VerArqueoCaja(int id)
        {
            var details = db.vw_arqueocajadet.Where(x => x.id_ArqueoCaja == id);

            vw_arqueocaja tac = new vw_arqueocaja();
            tac = db.vw_arqueocaja.Where(x => x.id_ArqueoCaja == id).First();

            ViewBag.kermesse = tac.nombre;
            ViewBag.fecha = tac.fechaArqueo;
            ViewBag.total = tac.granTotal;
            ViewBag.id_arqueocaja = tac.id_ArqueoCaja;

            return View(details);
        }

        public ActionResult EditArqueoCajaMaestro(int id)
        {
            tbl_arqueocaja tbc = db.tbl_arqueocaja.Find(id);
            ViewBag.idkermesse = new SelectList(db.tbl_kermesse.Where(model => model.estado != 3), "id_kermesse", "nombre");

            if (tbc == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.idkermesse = new SelectList(db.tbl_kermesse.Where(model => model.estado!=3), "id_kermesse", "nombre");
                return View(tbc);
            }
        }

        [HttpPost]
        public ActionResult UpdateArqueoCajaMaestro(tbl_arqueocaja d)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    d.estado = 2;
                    db.Entry(d).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Vw_ArqueoCaja");
            }
            catch
            {
                return View();
            }

        }

        //DETALLE DE ARQUEO CAJA

        public ActionResult Vw_ArqueoCajaDet(int id)
        {

            var details = db.vw_arqueocajadet.Where(x => x.id_ArqueoCaja == id);

            vw_arqueocaja tac = new vw_arqueocaja();
            tac = db.vw_arqueocaja.Where(x => x.id_ArqueoCaja == id).First();

            ViewBag.id_arqueocaja = tac.id_ArqueoCaja;

            return View(details);
        }

        public ActionResult InsertArqueoCajaDet(int id)
        {
            vw_arqueocaja tac = new vw_arqueocaja();
            tac = db.vw_arqueocaja.Where(x => x.id_ArqueoCaja == id).First();

            ViewBag.id_denominacion = new SelectList(db.vw_cordoba, "id_denominacion", "valor");
            ViewBag.id_arqueocaja = tac.id_ArqueoCaja;

            return View();
        }

        [HttpPost]
        public ActionResult InsertArqueoCajaDet(tbl_arqueocaja_det td)
        {

            if (ModelState.IsValid)
            {
                tbl_arqueocaja_det tbd = new tbl_arqueocaja_det();

                tbd.id_moneda = 1;
                tbd.id_denominacion = td.id_denominacion;

                tbl_denominacion tdc = new tbl_denominacion();
                tdc = db.tbl_denominacion.Where(x => x.id_denominacion == td.id_denominacion).First();

                tbd.id_arqueocaja = td.id_arqueocaja;
                tbd.subtotal = tdc.valor * td.cantidad;
                tbd.cantidad = td.cantidad;

                db.tbl_arqueocaja_det.Add(tbd);

                tbl_arqueocaja d = new tbl_arqueocaja();

                d = db.tbl_arqueocaja.Where(x => x.id_arqueocaja == td.id_arqueocaja).First();

                d.gran_total = (tbd.subtotal + d.gran_total);
                d.estado = 2;
                db.Entry(d).State = EntityState.Modified;

                db.SaveChanges();
                ModelState.Clear();
            }

            var list = db.vw_arqueocaja.ToList();
            return View("Vw_ArqueoCaja", list);
        }

        public ActionResult DeleteArqueoCajaDet(int id)
        {
            tbl_arqueocaja_det tbd = new tbl_arqueocaja_det();
            tbd = db.tbl_arqueocaja_det.Find(id);
            db.tbl_arqueocaja_det.Remove(tbd);

            tbl_arqueocaja d = new tbl_arqueocaja();

            d = db.tbl_arqueocaja.Where(x => x.id_arqueocaja == tbd.id_arqueocaja).First();

            d.gran_total = (d.gran_total - tbd.subtotal);
            d.estado = 2;
            db.Entry(d).State = EntityState.Modified;

            db.SaveChanges();

            var list = db.vw_arqueocaja.ToList();

            return View("Vw_ArqueoCaja", list);
        }

    }
}