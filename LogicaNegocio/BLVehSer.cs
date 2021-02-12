using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class BLVeh_Ser
    {
        ADVeh_Ser ADVS = new ADVeh_Ser();
        ADServicio ADS = new ADServicio();
        ADVehiculo ADV = new ADVehiculo();
        public List<Vehiculo_Servicio> ObtenerTodo()
        {
            List<Vehiculo_Servicio> vs = ADVS.ObtenerTodo();
            List<Vehiculo_Servicio> nvs = new List<Vehiculo_Servicio>();
            foreach (var item in vs)
            {
                Servicios serv = ADS.Obtener(item.ID_Servicio);
                Vehiculo veh = ADV.Obtener(item.ID_Vehiculo);
                Vehiculo_Servicio Ve_Se = new Vehiculo_Servicio();
                Ve_Se.Servicios = serv;
                Ve_Se.Vehiculo = veh;
                Ve_Se.ID_Vehiculo_Servicio = Convert.ToInt32(item.ID_Vehiculo_Servicio);
                nvs.Add(Ve_Se);
            }
            return nvs;
        }
    }
}
