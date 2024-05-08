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


        public DataTable ObtenerPacientes()
        {
            DataTable Tabla = new DataTable();
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

        public DataTable BuscarPorNombre(Paciente paciente)
        {
            DataTable Tabla = new DataTable();
            using (SqlConnection connection = conexioncd.ObtenerConexion())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Tb_Paciente WHERE NombreCompleto LIKE @Nombre", connection))
                    {

                        command.Parameters.AddWithValue("@Nombre", "%" + paciente.NombreCompleto + "%");
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

        public DataTable BuscarPorCedula(Paciente paciente)
        {
            DataTable Tabla = new DataTable();
            using (SqlConnection connection = conexioncd.ObtenerConexion())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Tb_Paciente WHERE Cedula LIKE @Cedula", connection))
                    {

                        command.Parameters.AddWithValue("@Cedula", "%" + paciente.Cedula + "%");
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
            bool validcionCedula = false;

            using (SqlConnection connection = conexioncd.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand comandovalidar = new SqlCommand("SELECT COUNT(*) FROM Tb_Paciente WHERE Cedula = @Cedula", connection))
                    {
                        comandovalidar.Parameters.AddWithValue("@Cedula", paciente.Cedula);
                        int count = (int)comandovalidar.ExecuteScalar();
                        validcionCedula = count > 0;
                    }
                }
                catch (Exception ex)
                {

                    string msj = ex.ToString();
                    return false;
                }
            }

            return validcionCedula;
        }

        public bool RegistrarPaciente(Paciente paciente)
        {
            bool agregado = false;
            using (SqlConnection connectio = conexioncd.ObtenerConexion())
            {
                connectio.Open();

                try
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO Tb_Paciente (NombreCompleto, Cedula, Edad, IdGenero, Direccion, Telefono, Gmail, Ocupacion) VALUES (@nombrecompleto, @cedula, @edad , @genero, @direccion, @telefono, @gmail, @ocupacion)", connectio))
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

