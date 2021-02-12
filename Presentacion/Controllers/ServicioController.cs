using Entidades;
using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class ServicioController : Controller
    {
        // GET: Servicio
        BLServicio BLS = new BLServicio();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListaServicios()
        {
            return View(BLS.ObtenerTodo());
        }

        public ActionResult AgregarServicio()
        {
            return View();
        }

        public ActionResult InsertarServicio(string txtdescripcion, string txtmonto) {
            Servicios servicio = new Servicios();
            servicio.Descripcion = txtdescripcion;
            servicio.Monto = Convert.ToInt32(txtmonto);
            string msj = "";
            if (BLS.Agregar(servicio).Equals("Servicio agregado correctamente."))
            {
                msj = "<script languaje='javascript' type='text/javascript'> alert('Tarea Existosa'); window.location.href='/Servicio/ListaServicios'; </script>'";
            }
            else
            {
                msj = "<script languaje='javascript' type='text/javascript'> alert('Error, Servicio no agregado'); window.location.href='/Servicio/AgregarServicio'; </script>'";
            }
            return Content(msj);
        }

        public ActionResult ModificarServicio(int codigo) {
            return View(BLS.Obtener(codigo));
        }

        public ActionResult ActualizarServicio(string txtID, string txtdescripcion, string txtmonto) {
            Servicios servicio = new Servicios();
            servicio.ID_Servicio = Convert.ToInt32(txtID);
            servicio.Descripcion = txtdescripcion;
            servicio.Monto = Convert.ToInt32(txtmonto);
            string msj = "";
            if (BLS.Modificar(servicio, servicio.ID_Servicio).Equals("Datos de servicio modificados correctamente."))
            {
                msj = "<script languaje='javascript' type='text/javascript'> alert('Tarea Existosa'); window.location.href='/Servicio/ListaServicios'; </script>'";
            }
            else
            {
                msj = "<script languaje='javascript' type='text/javascript'> alert('Error, Servicio no modificado'); window.location.href='/Servicio/ModificarServicio'; </script>'";
            }
            return Content(msj);
        }
    }
}