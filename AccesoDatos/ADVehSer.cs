using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class ADVeh_Ser
    {
        public Boolean isError;
        public String errorDescripcion;

        public string Agregar(Vehiculo_Servicio veh_ser)
        {
            try
            {
                string Cadena = ConfigurationManager.ConnectionStrings["Prueba"].ConnectionString;
                DA.Conexion con = new DA.Conexion(Cadena);
                SqlCommand query = new SqlCommand("INSERT INTO [Vehiculo-Servicio](ID_Servicio, ID_Vehiculo) VALUES (@p1, @p2)");
                query.Parameters.AddWithValue("@p1", veh_ser.ID_Servicio);
                query.Parameters.AddWithValue("@p2", veh_ser.ID_Vehiculo);

                con.ejecutarSQLSeguro(query);
                if (con.IsError)
                {
                    con.Destruir();
                    throw new System.InvalidOperationException(con.ErrorDescripcion);
                }

                con.Destruir();
            }
            catch (Exception ex)
            {
                isError = true;
                errorDescripcion = ex.Message;
                return errorDescripcion;
            }
            return "Datos agregados correctamente.";
        }

        public List<Vehiculo_Servicio> ObtenerTodo()
        {
            try
            {
                string Cadena = ConfigurationManager.ConnectionStrings["Prueba"].ConnectionString;
                DA.Conexion Con = new DA.Conexion(Cadena);
                SqlCommand query = new SqlCommand("SELECT vs.[ID_Vehiculo-Servicio] AS Consecutivo, s.ID_Servicio AS IDservicio, v.ID_Vehiculo AS IDvehiculo FROM Servicios s, Vehiculo v, [Vehiculo-Servicio] vs  WHERE s.ID_Servicio = vs.[ID_Servicio] and v.ID_Vehiculo = vs.[ID_Vehiculo]");
                DataTable Tabla = Con.ejecutarConsultaSQLTablaSegura(query);
                List<Vehiculo_Servicio> listVS = new List<Vehiculo_Servicio>();

                for (int i = 0; i < Tabla.Rows.Count; i++)
                {
                    Vehiculo_Servicio vs = new Vehiculo_Servicio();
                    vs.ID_Vehiculo_Servicio = Convert.ToInt32(Tabla.Rows[i]["Consecutivo"].ToString());
                    vs.ID_Servicio = Convert.ToInt32(Tabla.Rows[i]["IDservicio"].ToString());
                    vs.ID_Vehiculo = Convert.ToInt32(Tabla.Rows[i]["IDvehiculo"].ToString());
                    listVS.Add(vs);
                }

                if (Con.IsError)
                {
                    Con.Destruir();
                    throw new System.InvalidOperationException(Con.ErrorDescripcion);
                }
                else
                {
                    Con.Destruir();
                    return listVS;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                errorDescripcion = ex.Message;
                return null;
            }
        }
    }
}
