﻿using KermesseApp.Models;
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

        public ActionResult InsertArqueoCajaTest()
        {
            ViewBag.id_kermesse = new SelectList(db.tbl_kermesse, "id_kermesse", "nombre");
            ViewBag.id_moneda = new SelectList(db.tbl_moneda, "id_moneda", "nombre");
            ViewBag.id_denominacion = new SelectList(db.vw_denominacion, "id_Denominacion", "valor");

            return View();
        }

        //[HttpPost]
        //public ActionResult InsertArqueoCaja(tbl_arqueocaja tac, tbl_arqueocaja_det tacd)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        tbl_arqueocaja ac = new tbl_arqueocaja();
        //        //                ac.idkermesse = tac.idkermesse;
        //        ac.idkermesse = 1;
        //        ac.gran_total = tac.gran_total;
        //        ac.fecha_arqueo = tac.fecha_arqueo;
        //        ac.fecha_creacion = DateTime.Now;
        //        ac.estado = 1;

        //        db.tbl_arqueocaja.Add(ac);
        //        db.SaveChanges();
        //        ModelState.Clear();

        //        tbl_arqueocaja_det acd = new tbl_arqueocaja_det();
        //        acd.id_moneda = 1;
        //        acd.id_denominacion = 1;
        //        acd.subtotal = 480;
        //        acd.id_arqueocaja = ac.id_arqueocaja;

        //        db.tbl_arqueocaja_det.Add(acd);
        //        db.SaveChanges();
        //        ModelState.Clear();

        //    }

        //    ViewBag.id_kermesse = new SelectList(db.tbl_kermesse, "id_kermesse", "nombre");
        //    ViewBag.id_moneda = new SelectList(db.tbl_moneda, "id_moneda", "nombre");
        //    ViewBag.id_denominacion = new SelectList(db.vw_denominacion, "id_Denominacion", "valor");

        //    var list = db.vw_arqueocaja.ToList();

        //    return View("Vw_ArqueoCaja", list);
        //}

        public ActionResult InsertArqueoCaja(int kermesse, DateTime fecha_arqueo, string total, tbl_arqueocaja_det[] detalle)
        {
            ViewBag.id_kermesse = new SelectList(db.tbl_kermesse, "id_kermesse", "nombre");
            ViewBag.id_moneda = new SelectList(db.tbl_moneda, "id_moneda", "nombre");
            ViewBag.id_denominacion = new SelectList(db.vw_denominacion, "id_Denominacion", "valor");

            string result = "Error! Order Is Not Complete!";
            tbl_arqueocaja arqueo = new tbl_arqueocaja();
            arqueo.idkermesse = kermesse;
            arqueo.fecha_arqueo = fecha_arqueo;
            arqueo.gran_total = Decimal.Parse(total);
            arqueo.fecha_creacion = DateTime.Now;
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
            result = "Success! Order Is Complete!";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}