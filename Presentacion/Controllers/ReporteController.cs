using Entidades;
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

        public ActionResult InsertarServicios() {
            List<Vehiculo> listV = new List<Vehiculo>();
            List<Servicios> listS = new List<Servicios>();
            BLServicio BLS = new BLServicio();
            BLVehiculo BLV = new BLVehiculo();
            listV = BLV.ObtenerTodo();
            listS = BLS.ObtenerTodo();
            ViewBag.listadoV = listV;
            ViewBag.listadoS = listS;
            return View();
        }

        public ActionResult AgregarDatos(int cboVehiculo, int cboServicio) {
            Vehiculo_Servicio vs = new Vehiculo_Servicio();

            vs.ID_Servicio = cboServicio;
            vs.ID_Vehiculo = cboVehiculo;
            string msj = BLVS.Agregar(vs);
            string content = "";
            if (msj.Equals("Datos agregados correctamente."))
            {
                content = "<script languaje='javascript' type='text/javascript'> alert('"+msj+"'); window.location.href='/Reporte/ListarReporte'; </script>'";
            }
            else
            {
                content = "<script languaje='javascript' type='text/javascript'> alert('"+msj+"'); window.location.href='/Reporte/InsertarServicios'; </script>'";
            }
            return Content(content);
        }
    }
}