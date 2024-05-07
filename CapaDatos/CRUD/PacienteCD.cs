using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Tb_Paciente ORDER BY Id DESC", connection))
                    {
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

        public bool ValidarExistente(Paciente paciente)
        {
            bool ExistePaciente = false;

            using (SqlConnection connection = conexioncd.ObtenerConexion())
            {
                try
                {
                    using (SqlCommand comandovalidar = new SqlCommand("SELECT * FROM Tb_Paciente WHERE Cedula = @Cedula", connection))
                    {
                        comandovalidar.Parameters.AddWithValue("@Cedula", paciente.Cedula);
                        ExistePaciente = comandovalidar.ExecuteNonQuery() > 0;
                        comandovalidar.Parameters.Clear();
                        return ExistePaciente;
                    }
                }
                catch (Exception ex)
                {

                    string msj = ex.ToString();
                    return false;
                }
            }

        }

        public  bool RegistrarPaciente(Paciente paciente)
        {
            bool agregado = false;
            using (SqlConnection connectio = conexioncd.ObtenerConexion())
            {
                connectio.Open();

                try
                {
                    using (SqlCommand  command = new SqlCommand("INSERT INTO Tb_Paciente (NombreCompleto, Cedula, Edad, IdGenero, Direccion, Telefono, Gmail, Ocupacion) VALUES (@nombrecompleto, @cedula, @edad , @genero, @direccion, @telefono, @gmail, @ocupacion)", connectio))
                    {
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
