using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        BLVeh_Ser BLVS = new BLVeh_Ser();
        public ActionResult Inicio()
        {
            return View();
        }

        public ActionResult ListarReporte() {
            return View(BLVS.ObtenerTodo());
        }

        public ActionResult AsignarServicios() {
            return View();
        }
    }
}