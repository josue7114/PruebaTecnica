using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class BLVehiculo
    {
        private ADVehiculo DAV = new ADVehiculo();
        public string Agregar(Vehiculo vehiculo)
        {
            return DAV.Agregar(vehiculo);
        }

        public Vehiculo Obtener(int codigo) {
            return DAV.Obtener(codigo);
        }

        public string Modificar(Vehiculo vehiculo, int codigo) {
            return DAV.Modificar(vehiculo, codigo);
        }

        public string Eliminar(int codigo) {
            return DAV.Eliminar(codigo);
        }

        public List<Vehiculo> ObtenerTodo() {
            return DAV.ObtenerTodo();
        }
    }
}
