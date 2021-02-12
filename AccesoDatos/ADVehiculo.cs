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
    public class ADVehiculo
    {
        public Boolean isError;
        public String errorDescripcion;

        public string Agregar(Vehiculo vehiculo) {
            try
            {
                string Cadena = ConfigurationManager.ConnectionStrings["Prueba"].ConnectionString;
                DA.Conexion con = new DA.Conexion(Cadena);
                SqlCommand query = new SqlCommand("INSERT INTO Vehiculo(Placa, Dueno, Marca) VALUES (@p1, @p2, @p3)");
                query.Parameters.AddWithValue("@p1", vehiculo.Placa);
                query.Parameters.AddWithValue("@p2", vehiculo.Dueno);
                query.Parameters.AddWithValue("@p3", vehiculo.Marca);

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
            return "Vehículo agregado correctamente.";
        }

        public string Modificar(Vehiculo vehiculo, int codigo)
        {
            try
            {
                string Cadena = ConfigurationManager.ConnectionStrings["Prueba"].ConnectionString;
                DA.Conexion Con = new DA.Conexion(Cadena);
                SqlCommand query = new SqlCommand("UPDATE Vehiculo SET Placa = @p1, Dueno = @p2, Marca = @p3 WHERE ID_Vehiculo = @p4");

                query.Parameters.AddWithValue("@p1", vehiculo.Placa);
                query.Parameters.AddWithValue("@p2", vehiculo.Dueno);
                query.Parameters.AddWithValue("@p3", vehiculo.Marca);
                query.Parameters.AddWithValue("@p4", codigo);

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
            return "Datos de vehículo modificados correctamente.";
        }

        public string Eliminar(int codigo)
        {
            try
            {
                string Cadena = ConfigurationManager.ConnectionStrings["Prueba"].ConnectionString;
                DA.Conexion Con = new DA.Conexion(Cadena);
                SqlCommand query = new SqlCommand("DELETE FROM Vehiculo WHERE ID_Vehiculo =  @p1");

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
            return "Los datos del vehículo, se eliminaron correctamente.";
        }

        public DataTable ObtenerTodo()
        {
            try
            {
                string Cadena = ConfigurationManager.ConnectionStrings["Prueba"].ConnectionString;
                DA.Conexion Con = new DA.Conexion(Cadena);
                SqlCommand query = new SqlCommand("SELECT ID_Vehiculo, Placa, Dueno, Marca FROM Vehiculo ORDER BY ID_Vehiculo");
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

        public Vehiculo Obtener(int codigo)
        {
            try
            {
                string Cadena = ConfigurationManager.ConnectionStrings["Prueba"].ConnectionString;
                DA.Conexion Con = new DA.Conexion(Cadena);
                SqlCommand query = new SqlCommand("SELECT ID_Vehiculo, Placa, Dueno, Marca FROM Vehiculo FROM Vehiculo WHERE ID_Vehiculo= @p1");
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
                    Vehiculo Resultado = new Vehiculo();
                    Resultado.ID_Vehiculo = Convert.ToInt32(Tabla.Rows[0]["ID_Vehiculo"]);
                    Resultado.Placa = Tabla.Rows[0]["Placa"].ToString();
                    Resultado.Dueno = Tabla.Rows[0]["Dueno"].ToString();
                    Resultado.Marca = Tabla.Rows[0]["Marca"].ToString();

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
