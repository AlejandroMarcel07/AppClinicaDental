using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.CRUD
{
    public class PacienteCD
    {
        private ConexionCD conexioncd = new ConexionCD();
        DataTable Tabla = new DataTable();
        private string querystring = "Select * From Tb_Paciente";

        public DataTable ObtenerPacientes()
        {
            using (SqlConnection connection = conexioncd.ObtenerConexion())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(querystring, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Tabla.Load(reader);
                        }                   
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return Tabla;
            }
        }
    }
}
