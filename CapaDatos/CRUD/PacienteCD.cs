using CapaEntidad;
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

        public DataTable ObtenerPacientes()
        {
            using (SqlConnection connection = conexioncd.ObtenerConexion())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("Select * From Tb_Paciente", connection))
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

        public  bool RegistrarPaciente(Paciente paciente)
        {
            bool agregado = false;
            using (SqlConnection connectio = conexioncd.ObtenerConexion())
            {
                try
                {
                    using (SqlCommand  command = new SqlCommand("INSERT INTO Tb_Paciente (NombreCompleto, Cedula, Edad, IdGenero, Direccion, Telefono, Gmail, Ocupacion) VALUES (@nombrecompleto, @cedula, @edad , @genero, @direccion, @telefono, @gmail, @ocupacion)", connectio))
                    {
                        connectio.Open();
                        command.Parameters.AddWithValue("@nombrecompleto", paciente.NombreCompleto);
                        command.Parameters.AddWithValue("@cedula", paciente.Cedula);
                        command.Parameters.AddWithValue("@edad", paciente.Edad);
                        command.Parameters.AddWithValue("@genero", paciente.IdGenero);
                        command.Parameters.AddWithValue("@direccion", paciente.Direccino);
                        command.Parameters.AddWithValue("@telefono", paciente.Telefono);
                        command.Parameters.AddWithValue("@gmail", paciente.Gmail);
                        command.Parameters.AddWithValue("@ocupacion", paciente.Ocupacion);
                        agregado = command.ExecuteNonQuery() > 0;
                        command.Parameters.Clear();
                        return agregado;
                    }
                }
                catch (Exception ex)
                {
                    string msj = ex.ToString();
                    return false;
                }
            }
        }

         
    }
}
