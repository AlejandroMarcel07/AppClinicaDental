using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.CRUD
{
    public class UsuarioAdministradorCD
    {
        private ConexionCD conexionbd = new ConexionCD();

        public bool ValidarInicioDeSesion(string nombreUsuario, string contrasena, out string tipoRol)
        {
            bool inicioSesionExitoso = false;
            tipoRol = null;

            string queryString = "SELECT COUNT(*) FROM Tb_UsuarioAdministrador WHERE NombreUsuario = @nombreUsuario AND Contrasena = @contrasena";

            using (SqlConnection connection = conexionbd.ObtenerConexion())
            {
                //Manejamos la conexion por si encuentra un error de conexion con nuestra base de datos
                try
                {
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                        command.Parameters.AddWithValue("@contrasena", contrasena);

                        connection.Open();
                        int count = (int)command.ExecuteScalar();
                        inicioSesionExitoso = count > 0;

                        if (inicioSesionExitoso)
                        {
                            // Obtener el tipo de rol del usuario
                            string queryStringRol = "SELECT TipoRol FROM Tb_Rol WHERE Id = (SELECT IdRol FROM Tb_UsuarioAdministrador WHERE NombreUsuario = @nombreUsuario)";
                            using (SqlCommand commandRol = new SqlCommand(queryStringRol, connection))
                            {
                                commandRol.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                                tipoRol = commandRol.ExecuteScalar()?.ToString();
                            }
                        }


                    }
                }
                catch (Exception ex)
                {
                    string ms = ex.ToString();
                }
            }

            return inicioSesionExitoso;
        }



    }
}
