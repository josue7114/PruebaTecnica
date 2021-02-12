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
    public class ADServicio
    {
        public Boolean isError;
        public String errorDescripcion;

        public string Agregar(Servicios servicio)
        {
            try
            {
                string Cadena = ConfigurationManager.ConnectionStrings["Prueba"].ConnectionString;
                DA.Conexion con = new DA.Conexion(Cadena);
                SqlCommand query = new SqlCommand("INSERT INTO Servicios(Descripcion, Monto) VALUES (@p1, @p2)");
                query.Parameters.AddWithValue("@p1", servicio.Descripcion);
                query.Parameters.AddWithValue("@p2", servicio.Monto);

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
            return "Servicio agregado correctamente.";
        }

        public string Modificar(Servicios servicio, int codigo)
        {
            try
            {
                string Cadena = ConfigurationManager.ConnectionStrings["Prueba"].ConnectionString;
                DA.Conexion Con = new DA.Conexion(Cadena);
                SqlCommand query = new SqlCommand("UPDATE Servicios SET Descripcion = @p1, Monto = @p2 WHERE ID_Servicio = @p3");

                query.Parameters.AddWithValue("@p1", servicio.Descripcion);
                query.Parameters.AddWithValue("@p2", servicio.Monto);
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
            return "Datos de servicio modificados correctamente.";
        }

        public string Eliminar(int codigo)
        {
            try
            {
                string Cadena = ConfigurationManager.ConnectionStrings["Prueba"].ConnectionString;
                DA.Conexion Con = new DA.Conexion(Cadena);
                SqlCommand query = new SqlCommand("DELETE FROM Servicios WHERE ID_Servicio =  @p1");

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
            return "Los datos del servicio, se eliminaron correctamente.";
        }

        public List<Servicios> ObtenerTodo()
        {
            try
            {
                string Cadena = ConfigurationManager.ConnectionStrings["Prueba"].ConnectionString;
                DA.Conexion Con = new DA.Conexion(Cadena);
                SqlCommand query = new SqlCommand("SELECT ID_Servicio, Descripcion, Monto FROM Servicios ORDER BY ID_Servicio");
                DataTable Tabla = Con.ejecutarConsultaSQLTablaSegura(query);
                List<Servicios> listS = new List<Servicios>();

                foreach (DataRow item in Tabla.Rows)
                {
                    Servicios servicio = new Servicios();
                    servicio.ID_Servicio= Convert.ToInt32(item["ID_Servicio"].ToString());
                    servicio.Descripcion = item["Descripcion"].ToString();
                    servicio.Monto = Convert.ToInt32(item["Monto"].ToString());
                    listS.Add(servicio);
                }
                if (Con.IsError)
                {
                    Con.Destruir();
                    throw new System.InvalidOperationException(Con.ErrorDescripcion);
                }
                else
                {
                    Con.Destruir();
                    return listS;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                errorDescripcion = ex.Message;
                return null;
            }
        }

        public Servicios Obtener(int codigo)
        {
            try
            {
                string Cadena = ConfigurationManager.ConnectionStrings["Prueba"].ConnectionString;
                DA.Conexion Con = new DA.Conexion(Cadena);
                SqlCommand query = new SqlCommand("SELECT ID_Servicio, Descripcion, Monto FROM Servicios WHERE ID_Servicio = @p1");
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
                    Servicios Resultado = new Servicios();
                    Resultado.ID_Servicio = Convert.ToInt32(Tabla.Rows[0]["ID_Servicio"]);
                    Resultado.Descripcion = Tabla.Rows[0]["Descripcion"].ToString();
                    Resultado.Monto = Convert.ToInt32(Tabla.Rows[0]["Monto"]);
                    return Resultado;
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
