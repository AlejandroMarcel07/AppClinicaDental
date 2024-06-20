using System;
using CapaEntidad;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos.CRUD
{
    public class CitaCD
    {

        //Instancia de la clase conexion
        private ConexionCD conexioncd = new ConexionCD();

        public Cita RegistrarCita(Cita cita)
        {
            using (SqlConnection connection = conexioncd.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("InsertarCitaNueva", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdPaciente", cita.IdPaciente);
                    command.Parameters.AddWithValue("@Fecha", cita.Fecha.Date);
                    command.Parameters.AddWithValue("@HoraEntrada", cita.HoraEntrada);
                    command.Parameters.AddWithValue("@HoraSalida", cita.HoraSalida);

                    // Añadir un parámetro de salida para obtener el ID de la cita insertada
                    SqlParameter outputIdParam = new SqlParameter("@Id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);

                    command.ExecuteNonQuery();

                    // Obtener el ID de la cita insertada
                    int insertedId = (int)outputIdParam.Value;

                    // Crear un nuevo objeto Cita con el ID asignado
                    Cita nuevaCita = new Cita
                    {
                        Id = insertedId,
                        IdPaciente = cita.IdPaciente,
                        Fecha = cita.Fecha,
                        HoraEntrada = cita.HoraEntrada,
                        HoraSalida = cita.HoraSalida
                    };

                    return nuevaCita;
                }
                catch (Exception ex)
                {

                    throw new ArgumentException(ex.Message);
                }
            }
        }


        // Método para obtener las citas de un día específico
        public List<Cita> ObtenerCitasPorDia(Cita cita)
        {
            List<Cita> citas = new List<Cita>();

            using (SqlConnection connection = conexioncd.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("ObtenerCitasPorDia", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Fecha", cita.Fecha.Date); // Solo la fecha, sin la hora

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cita citaExtraida = new Cita();
                            citaExtraida.Id = (int)reader["Id"];
                            citaExtraida.IdPaciente = (int)reader["IdPaciente"];
                            citaExtraida.Fecha = (DateTime)reader["Fecha"];
                            citaExtraida.HoraEntrada = (TimeSpan)reader["HoraEntrada"];
                            citaExtraida.HoraSalida = (TimeSpan)reader["HoraSalida"];

                            citas.Add(citaExtraida);
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }

            return citas;
        }



    }
}
