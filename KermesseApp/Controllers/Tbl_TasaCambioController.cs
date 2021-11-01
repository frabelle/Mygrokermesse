using KermesseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KermesseApp.Controllers
{
    public class Tbl_TasaCambioController : Controller
    {
        private KERMESSEEntities db = new KERMESSEEntities();
        // GET: Tbl_TasaCambio
        public ActionResult Tbl_TasaCambio()
        {
            return View(db.tbl_tasacambio.ToList());
        }

        public ActionResult Vw_TasaCambio()
        {
            return View(db.vw_tasacambio.ToList());
        }

        public ActionResult InsertTasaCambio()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertTasaCambio(tbl_tasacambio tbt)
        {
            if (ModelState.IsValid)
            {
                tbl_tasacambio tbm = new tbl_tasacambio();
                tbm.id_monedaO = tbt.id_monedaO;
                tbm.id_monedaC = tbt.id_monedaC;
                tbm.mes = tbt.mes;
                tbm.anio = tbt.anio;
                tbm.estado = 1;

                db.tbl_tasacambio.Add(tbm);
                db.SaveChanges();
            }

            ModelState.Clear();

            var list = db.tbl_tasacambio.ToList();

            return View("Tbl_TasaCambio", list);
        }

    }
}