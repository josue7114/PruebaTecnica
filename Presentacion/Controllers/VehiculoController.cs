using LogicaNegocio;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class VehiculoController : Controller
    {
        // GET: Vehiculo

        BLVehiculo BLV = new BLVehiculo();
        public ActionResult Inicio()
        {
            return View();
        }

        public ActionResult ListaVehiculos() {
            return View(BLV.ObtenerTodo());
        }

        public ActionResult AgregarVehiculo() {
            return View();
        }

        public ActionResult InsertarVehiculo(string txtplaca, string txtdueno, string txtmarca) {
            Vehiculo vehiculo = new Vehiculo();
            vehiculo.Placa = txtplaca;
            vehiculo.Dueno = txtdueno;
            vehiculo.Marca = txtmarca;
            string msj = "";
            if (BLV.Agregar(vehiculo).Equals("Vehículo agregado correctamente."))
            {
                msj = "<script languaje='javascript' type='text/javascript'> alert('Tarea Existosa'); window.location.href='/Vehiculo/ListaVehiculos'; </script>'";
            }
            else {
                msj = "<script languaje='javascript' type='text/javascript'> alert('Error, Vehiculo no agregado'); window.location.href='/Vehiculo/AgregarVehiculo'; </script>'";
            }
            return Content(msj);
        }
    }
}