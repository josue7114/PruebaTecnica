using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web;


namespace DA
{
    public class Conexion
    {
        private Boolean isError;
        private String errorDescripcion;
        public SqlConnection conexion;
        public string Usuario;

        static int instancias = 0;

        public Conexion(string CadenaConexion)
        {
            string ayudante;
            ayudante = CadenaConexion;

            conexion = new SqlConnection(ayudante.ToString());

            instancias += 1;

            if (instancias > 1)
                return;

            try
            {
                IsError = false;
                conexion.Open();
            }
            catch (Exception error)
            {
                IsError = true;
                ErrorDescripcion = "Error de Conexión \n";
                ErrorDescripcion += error.Message;
                instancias = 0;
            }

        }
        public Boolean estado()
        {
            String mensaje = "";
            switch (conexion.State)
            {
                case System.Data.ConnectionState.Broken:
                    mensaje = "Quebrada";
                    break;
                case System.Data.ConnectionState.Closed:
                    mensaje = "Cerrada";
                    break;
                case System.Data.ConnectionState.Connecting:
                    mensaje = "Conectandose";
                    break;
                case System.Data.ConnectionState.Executing:
                    mensaje = "Ejecutando";
                    break;
                case System.Data.ConnectionState.Fetching:
                    mensaje = "Extrayendo";
                    break;
                case System.Data.ConnectionState.Open:
                    mensaje = "Abierta";
                    break;
            }

            ErrorDescripcion = mensaje;


            if ((conexion.State == ConnectionState.Open) ||
                  (conexion.State == ConnectionState.Executing) ||
                  (conexion.State == ConnectionState.Fetching))
                return true;
            else
                return false;
        }

        ~Conexion()
        {

            try
            {
                IsError = false;
                conexion.Close();
            }
            catch (Exception error)
            {
                IsError = true;
                ErrorDescripcion = "Error de Desconexión \n";
                ErrorDescripcion += error.Message;
            }
        }

        public void Destruir()
        {
            isError = false;
            conexion.Close();
            instancias = 0;
        }

        public SqlDataReader ejecutarConsultaSQL(String pSql)
        {
            SqlCommand cmd;
            SqlDataReader dr = null;

            cmd = new SqlCommand(pSql, conexion);

            try
            {
                IsError = false;
                dr = cmd.ExecuteReader();
            }

            catch (SqlException errorSql)
            {
                IsError = true;
                ErrorDescripcion = "Error en ejecutarSQL \n";
                ErrorDescripcion += errorSql.Message;
            }
            catch (Exception error)
            {
                IsError = true;
                ErrorDescripcion = "Error en ejecutarConsultaSQL \n";
                ErrorDescripcion += error.Message;
            }
            return dr;
        }

        public DataTable ejecutarConsultaSQLTablaSegura(SqlCommand Comando)
        {
            SqlCommand cmd;
            SqlDataReader dr = null;
            DataTable Tabla = new DataTable();

            cmd = Comando;
            Comando.Connection = conexion;

            try
            {
                IsError = false;
                dr = cmd.ExecuteReader();
            }

            catch (SqlException errorSql)
            {
                IsError = true;
                ErrorDescripcion = "Error en ejecutarSQL \n";
                ErrorDescripcion += errorSql.Message;
            }
            catch (Exception error)
            {
                IsError = true;
                ErrorDescripcion = "Error en ejecutarConsultaSQL \n";
                ErrorDescripcion += error.Message;
            }
            if (dr == null) return Tabla;
            Tabla.Load(dr, LoadOption.OverwriteChanges);
            return Tabla;
        }

        public void ejecutarSQLSeguro(SqlCommand Comando)
        {
            SqlCommand cmd = null;
            cmd = Comando;

            try
            {
                IsError = false;
                cmd.Connection = conexion;
                cmd.ExecuteNonQuery();

            }
            catch (SqlException errorSql)
            {
                IsError = true;
                ErrorDescripcion = "Error en ejecutarSQL \n";
                ErrorDescripcion += errorSql.Message;
            }
            catch (Exception error)
            {
                IsError = true;
                ErrorDescripcion = "Error en ejecutarSQL \n";
                ErrorDescripcion += error.Message;
            }
        }

        public Boolean IsError
        {
            set { isError = value; }
            get { return isError; }
        }

        public String ErrorDescripcion
        {
            set { errorDescripcion = value; }
            get { return errorDescripcion; }
        }
    }
}
