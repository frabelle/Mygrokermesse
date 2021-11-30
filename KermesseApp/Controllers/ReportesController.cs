using KermesseApp.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KermesseApp.Controllers
{
    public class ReportesController : Controller
    {
        private KERMESSEEntities db = new KERMESSEEntities();

        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerRptArqueoCaja(String tipo, String kermesseArqueo)
        {
            LocalReport rpt = new LocalReport();
            string mt, enc, f;
            string[] s;
            Warning[] w;

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "rptArqueoCaja.rdlc");
            rpt.ReportPath = ruta;

            ReportDataSource rd = null;
            ReportDataSource rd2 = null;

            if (kermesseArqueo == null)
            {
                RedirectToAction("Vw_ArqueoCaja");
            }
            else
            {
                vw_arqueocaja arqueo = new vw_arqueocaja();
                tbl_arqueocaja tbl_arqueo = new tbl_arqueocaja();

                arqueo = db.vw_arqueocaja.Where(x => x.nombre == kermesseArqueo).First();
                //arqueo = db.vw_arqueocaja.Where(x => x.id_ArqueoCaja == tbl_arqueo.id_arqueocaja).First();
                var encabezado = db.vw_arqueocaja.Where(x => x.id_ArqueoCaja == arqueo.id_ArqueoCaja);
                rd = new ReportDataSource("dsRptArqueoCaja", encabezado);

                var detalle = db.vw_arqueocajadet.Where(x => x.id_ArqueoCaja == arqueo.id_ArqueoCaja);
                rd2 = new ReportDataSource("dsRptArqueoCajaDet", detalle);
            }

            rpt.DataSources.Add(rd);
            rpt.DataSources.Add(rd2);

            //String tipo = "PDF";
            var b = rpt.Render(tipo, null, out mt, out enc, out f, out s, out w);
            return new FileContentResult(b, mt);
        }

        [HttpPost]
        public ActionResult VerRptResumen(String tipo, String kermesseResumen)
        {
            LocalReport rpt = new LocalReport();
            string mt, enc, f;
            string[] s;
            Warning[] w;

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "rptResumen.rdlc");
            rpt.ReportPath = ruta;

            ReportDataSource rdArqueo = null;
            ReportDataSource rdIngreso = null;
            ReportDataSource rdEgresos = null;

            if (kermesseResumen == null)
            {
                RedirectToAction("Vw_ArqueoCaja");
            }
            else
            {
                /*Obtengo el de Ingreso*/
                //vw_ingresoscom ingreso = new vw_ingresoscom();

                var ingreso = db.vw_ingresoscom.Where(x => x.Kermesse == kermesseResumen);
                //var ingresoC = db.vw_ingresocomdet.Where(x => x.Id_ingresoM == ingreso.id_ingreso_comunidad);
                rdIngreso = new ReportDataSource("dsIngreso", ingreso);

                /*Obtengo el de Ingreso*/
                var egresos = db.vw_gastos.Where(x => x.Kermesse == kermesseResumen);
                rdEgresos = new ReportDataSource("dsEgresos", egresos);

                /*Obtengo el de Arqueo Caja*/
                vw_arqueocaja arqueo = new vw_arqueocaja();
                tbl_arqueocaja tbl_arqueo = new tbl_arqueocaja();

                arqueo = db.vw_arqueocaja.Where(x => x.nombre == kermesseResumen).First();
                var detalle = db.vw_arqueocajadet.Where(x => x.id_ArqueoCaja == arqueo.id_ArqueoCaja);
                rdArqueo = new ReportDataSource("dsArqueoCaja", detalle);
            }

            rpt.DataSources.Add(rdIngreso);
            rpt.DataSources.Add(rdEgresos);
            rpt.DataSources.Add(rdArqueo);

            //String tipo = "PDF";
            var b = rpt.Render(tipo, null, out mt, out enc, out f, out s, out w);
            return new FileContentResult(b, mt);
        }
    }
}