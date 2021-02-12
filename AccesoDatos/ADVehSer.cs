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
                SqlCommand query = new SqlCommand("INSERT INTO Vehiculo-Servicio(ID_Servicio, ID_Vehiculo) VALUES (@p1, @p2)");
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

        public string Modificar(Vehiculo_Servicio veh_ser, int codigo)
        {
            try
            {
                string Cadena = ConfigurationManager.ConnectionStrings["Prueba"].ConnectionString;
                DA.Conexion Con = new DA.Conexion(Cadena);
                SqlCommand query = new SqlCommand("UPDATE Vehiculo-Servicio SET ID_Servicio = @p1, ID_Vehiculo = @p2 WHERE ID_Vehiculo-Servicio = @p3");

                query.Parameters.AddWithValue("@p1", veh_ser.ID_Servicio);
                query.Parameters.AddWithValue("@p2", veh_ser.ID_Vehiculo);
                query.Parameters.AddWithValue("@p3", codigo);

                Con.ejecutarSQLSeguro(query);
                if (Con.IsError)
                {
                    Con.Destruir();
                    throw new System.InvalidOperationException(Con.ErrorDescripcion);
                }

                Con.Destruir();
            }
            catch (Exception ex)
            {
                isError = true;
                errorDescripcion = ex.Message;
                return errorDescripcion;
            }
            return "Datos modificados correctamente.";
        }

        public string Eliminar(int codigo)
        {
            try
            {
                string Cadena = ConfigurationManager.ConnectionStrings["Prueba"].ConnectionString;
                DA.Conexion Con = new DA.Conexion(Cadena);
                SqlCommand query = new SqlCommand("DELETE FROM Vehiculo-Servicio WHERE ID_Vehiculo-Servicio =  @p1");

                query.Parameters.AddWithValue("@p1", codigo);
                Con.ejecutarSQLSeguro(query);

                if (Con.IsError)
                {
                    Con.Destruir();
                    throw new System.InvalidOperationException(Con.ErrorDescripcion);
                }

                Con.Destruir();
            }
            catch (Exception ex)
            {
                isError = true;
                errorDescripcion = ex.Message;
                return errorDescripcion;
            }
            return "Los datos se eliminaron correctamente.";
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

        public DataTable Obtener(int codigo)
        {
            try
            {
                string Cadena = ConfigurationManager.ConnectionStrings["Prueba"].ConnectionString;
                DA.Conexion Con = new DA.Conexion(Cadena);
                SqlCommand query = new SqlCommand("SELECT vs.[ID_Vehiculo-Servicio] AS Consecutivo, s.ID_Servicio AS[Servicio], s.Descripcion, v.Placa, v.Dueno AS Dueño FROM Servicios s, Vehiculo v, [Vehiculo-Servicio] vs  WHERE s.ID_Servicio = vs.[ID_Servicio] and v.ID_Vehiculo = vs.[ID_Vehiculo] and vs.[ID_Vehiculo-Servicio] = @p1");
                query.Parameters.AddWithValue("@p1", codigo);
                DataTable Tabla = Con.ejecutarConsultaSQLTablaSegura(query);

                if (Con.IsError)
                {
                    Con.Destruir();
                    throw new System.InvalidOperationException(Con.ErrorDescripcion);
                }
                else
                {
                    Con.Destruir();
                    return Tabla;
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
