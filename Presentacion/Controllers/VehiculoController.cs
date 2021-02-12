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
            string msj = "";
            if (txtplaca.Equals(""))
            {
                msj = "Debe ingresar una placa";
            }
            else if (txtdueno.Equals(""))
            {
                msj = "Debe ingresar el nombre del dueño";
            }
            else if (txtmarca.Equals(""))
            {
                msj = "Debe indicar la marca del vehículo";
            }
            else {
                vehiculo.Placa = txtplaca;
                vehiculo.Dueno = txtdueno;
                vehiculo.Marca = txtmarca;
                msj = BLV.Agregar(vehiculo);
            }
            String content = "";
            if (msj.Equals("Vehículo agregado correctamente."))
            {
                content = "<script languaje='javascript' type='text/javascript'> alert('"+msj+"'); window.location.href='/Vehiculo/ListaVehiculos'; </script>'";
            }
            else {
                content = "<script languaje='javascript' type='text/javascript'> alert('"+msj+"'); window.location.href='/Vehiculo/AgregarVehiculo'; </script>'";
            }
            return Content(content);
        }


        public ActionResult ModificarVehiculo(int codigo) {
            return View(BLV.Obtener(codigo));
        }

        public ActionResult ActualizarVehiculo(string txtID, string txtplaca, string txtdueno, string txtmarca) {
            Vehiculo vehiculo = new Vehiculo();
            string msj = "";
            if (txtplaca.Equals(""))
            {
                msj = "Debe ingresar una placa";
            }
            else if (txtdueno.Equals(""))
            {
                msj = "Debe ingresar el nombre del dueño";
            }
            else if (txtmarca.Equals(""))
            {
                msj = "Debe indicar la marca del vehículo";
            }
            else
            {
                vehiculo.Placa = txtplaca;
                vehiculo.Dueno = txtdueno;
                vehiculo.Marca = txtmarca;
                msj = BLV.Modificar(vehiculo, vehiculo.ID_Vehiculo);
            }
            string content = "";
            if (msj.Equals("Datos de vehículo modificados correctamente."))
            {
                content = "<script languaje='javascript' type='text/javascript'> alert('Tarea Existosa'); window.location.href='/Vehiculo/ListaVehiculos'; </script>'";
            }
            else
            {
                content = "<script languaje='javascript' type='text/javascript'> alert('Error, Vehiculo no modificado'); window.location.href='/Vehiculo/ModificarVehiculo'; </script>'";
            }
            return Content(content);
        }

        public ActionResult EliminarVehiculo(int codigo) {
            string msj = BLV.Eliminar(codigo);
            string content = "";
            if (msj.Equals("Los datos del vehículo, se eliminaron correctamente."))
            {
                content = "<script languaje='javascript' type='text/javascript'> alert('"+msj+"'); window.location.href='/Vehiculo/ListaVehiculos'; </script>'";
            }
            else
            {
                content = "<script languaje='javascript' type='text/javascript'> alert('"+msj+"'); window.location.href='/Vehiculo/ListaVehiculos'; </script>'";
            }
            return Content(content);
        }
    }
}