using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class BLServicio
    {
        private ADServicio ADS = new ADServicio();

        public string Agregar(Servicios servicio)
        {
            return ADS.Agregar(servicio);
        }

        public Servicios Obtener(int codigo)
        {
            return ADS.Obtener(codigo);
        }

        public string Modificar(Servicios servicio, int codigo)
        {
            return ADS.Modificar(servicio, codigo);
        }

        public string Eliminar(int codigo)
        {
            return ADS.Eliminar(codigo);
        }

        public List<Servicios> ObtenerTodo()
        {
            return ADS.ObtenerTodo();
        }
    }
}
