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

        public bool RegistrarPaciente(Paciente paciente)
        {
            using (SqlConnection connectio = conexioncd.ObtenerConexion())
            {
                try
                {
                    connectio.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Tb_Paciente (NombreCompleto, Cedula, Edad, IdGenero, Direccion, Telefono, Gmail, Ocupacion) VALUES (@nombrecompleto, @cedula, @edad , @genero, @direccion, @telefono, @gmail, @ocupacion)", connectio);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@nombrecompleto", paciente.NombreCompleto);
                    command.Parameters.AddWithValue("@cedula", paciente.Cedula);
                    command.Parameters.AddWithValue("@edad", paciente.Edad);
                    command.Parameters.AddWithValue("@genero", paciente.IdGenero);
                    command.Parameters.AddWithValue("@direccion", paciente.Direccino);
                    command.Parameters.AddWithValue("@telefono", paciente.Telefono);
                    command.Parameters.AddWithValue("@gmail", paciente.Gmail);
                    command.Parameters.AddWithValue("@ocupacion", paciente.Ocupacion);
                    bool agregado = false;
                    agregado = command.ExecuteNonQuery() > 0;
                    command.Parameters.Clear();
                    return agregado;

                }
                catch (Exception ex)
                {
                    string msj = ex.ToString();
                    return false;
                }
            }
        }


        //Mostrar informacion
        //Editar informacion
    }
}

