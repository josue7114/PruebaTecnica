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
            string msj = "";
            if (txtdescripcion.Equals(""))
            {
                msj = "Debe ingresar una descripción";
            }
            else if (txtmonto.Equals(""))
            {
                msj = "Debe ingresar un monto";
            }
            else {
                servicio.Descripcion = txtdescripcion;
                servicio.Monto = Convert.ToInt32(txtmonto);
                msj = BLS.Agregar(servicio);
            }
            string content = "";
            if (msj.Equals("Servicio agregado correctamente."))
            {
                content = "<script languaje='javascript' type='text/javascript'> alert('"+msj+"'); window.location.href='/Servicio/ListaServicios'; </script>'";
            }
            else
            {
                content = "<script languaje='javascript' type='text/javascript'> alert('"+msj+"'); window.location.href='/Servicio/AgregarServicio'; </script>'";
            }
            return Content(content);
        }

        public ActionResult ModificarServicio(int codigo) {
            return View(BLS.Obtener(codigo));
        }

        public ActionResult ActualizarServicio(string txtID, string txtdescripcion, string txtmonto) {
            Servicios servicio = new Servicios();
            servicio.ID_Servicio = Convert.ToInt32(txtID);
            servicio.Descripcion = txtdescripcion;
            servicio.Monto = Convert.ToInt32(txtmonto);
            string msj = BLS.Modificar(servicio, servicio.ID_Servicio);
            string content = "";

            if (msj.Equals("Datos de servicio modificados correctamente."))
            {
                content = "<script languaje='javascript' type='text/javascript'> alert('"+msj+"'); window.location.href='/Servicio/ListaServicios'; </script>'";
            }
            else
            {
                content = "<script languaje='javascript' type='text/javascript'> alert('"+msj+"'); window.location.href='/Servicio/ModificarServicio'; </script>'";
            }
            return Content(content);
        }

        public ActionResult EliminarServicio(int codigo) {
            string msj = BLS.Eliminar(codigo);
            string content = "";

            if (msj.Equals("Los datos del servicio, se eliminaron correctamente."))
            {
                content = "<script languaje='javascript' type='text/javascript'> alert('"+msj+"'); window.location.href='/Servicio/ListaServicios'; </script>'";
            }
            else
            {
                content = "<script languaje='javascript' type='text/javascript'> alert('"+msj+"'); window.location.href='/Servicio/ListaServicios'; </script>'";
            }
            return Content(content);
        }
    }
}