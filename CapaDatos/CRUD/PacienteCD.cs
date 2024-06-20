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
        //Instancia de la clase conexion
        private ConexionCD conexioncd = new ConexionCD();

        public DataTable ObtenerPacientes()
        {
            using (SqlConnection connection = conexioncd.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("ObtenerPacientesOrdenadosPorId", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable Tabla = new DataTable();
                    Tabla.Load(reader);
                    return Tabla;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public DataTable BuscarPorNombre(Paciente paciente)
        {
            using (SqlConnection connection = conexioncd.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("BuscarPacientesPorNombre", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Nombre", "%" + paciente.NombreCompleto + "%");
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable Tabla = new DataTable();
                    Tabla.Load(reader);
                    return Tabla;

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public DataTable BuscarPorCedula(Paciente paciente)
        {
            using (SqlConnection connection = conexioncd.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("BuscarPacientesPorCedula", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Cedula", "%" + paciente.Cedula + "%");
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable Tabla = new DataTable();
                    Tabla.Load(reader);
                    return Tabla;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public bool ValidarExistente(Paciente paciente)
        {
            using (SqlConnection connection = conexioncd.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    SqlCommand comandovalidar = new SqlCommand("ContarPacientesPorCedula", connection);
                    comandovalidar.CommandType = CommandType.StoredProcedure;
                    comandovalidar.Parameters.AddWithValue("@Cedula", paciente.Cedula);
                    int count = (int)comandovalidar.ExecuteScalar();
                    bool validcionCedula = false;
                    validcionCedula = count > 0;
                    return validcionCedula;
                }
                catch (Exception ex)
                {
                    string msj = ex.ToString();
                    return false;
                }
            }
        }

        public bool ValidarExistenteModificada(string cedulaActual, string cedulaNueva)
        {
            using (SqlConnection connection = conexioncd.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("ValidarExistenciaCedulaModificada", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@cedulaActual", cedulaActual);
                    command.Parameters.AddWithValue("@cedulaNueva", cedulaNueva);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    // Manejo de la excepción, puedes mostrar un mensaje de error o registrar el error en algún lugar
                    return false;
                }
            }
        }


        public bool RegistrarPaciente(Paciente paciente)
        {
            using (SqlConnection connection = conexioncd.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("InsertarPacienteNuevo", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@nombrecompleto", paciente.NombreCompleto);
                    command.Parameters.AddWithValue("@cedula", paciente.Cedula);
                    command.Parameters.AddWithValue("@edad", paciente.Edad);
                    command.Parameters.AddWithValue("@genero", paciente.IdGenero);
                    command.Parameters.AddWithValue("@direccion", paciente.Direccion);
                    command.Parameters.AddWithValue("@telefono", paciente.Telefono);
                    command.Parameters.AddWithValue("@ocupacion", paciente.Ocupacion);
                    command.Parameters.AddWithValue("@antecedentes", paciente.Antecedentes);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Manejo de la excepción, puedes mostrar un mensaje de error o registrar el error en algún lugar
                    return false;
                }
            }
        }

        public bool ActualizarPaciente(Paciente paciente)
        {
            using (SqlConnection connection = conexioncd.ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("ActualizarPaciente", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", paciente.Id);
                    command.Parameters.AddWithValue("@nombrecompleto", paciente.NombreCompleto);
                    command.Parameters.AddWithValue("@cedula", paciente.Cedula);
                    command.Parameters.AddWithValue("@edad", paciente.Edad);
                    command.Parameters.AddWithValue("@genero", paciente.IdGenero);
                    command.Parameters.AddWithValue("@direccion", paciente.Direccion);
                    command.Parameters.AddWithValue("@telefono", paciente.Telefono);
                    command.Parameters.AddWithValue("@ocupacion", paciente.Ocupacion);
                    command.Parameters.AddWithValue("@antecedentes", paciente.Antecedentes);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }


        public Paciente ObtenerPacienteCompleto(string cedula)
        {
            Paciente paciente = null;

            using (SqlConnection connection = conexioncd.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("ObtenerPacienteCompleto", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@cedula", cedula);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            paciente = new Paciente
                            {
                                Id = (int)reader["Id"],
                                NombreCompleto = (string)reader["NombreCompleto"],
                                Cedula = (string)reader["Cedula"],
                                Edad = (int)reader["Edad"],
                                IdGenero = (int)reader["IdGenero"],
                                Direccion = (string)reader["Direccion"],
                                Telefono = (int)reader["Telefono"],
                                Ocupacion = (string)reader["Ocupacion"],
                                Antecedentes = (string)reader["Antecedentes"],
                                Activo = (bool)reader["IsDeleted"]
                            };
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return paciente;
        }


    }
}

