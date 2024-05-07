using System;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class ConexionCD
    {
        private static readonly string cadenaConexion = "Data Source=DESKTOP-E77K5U7\\SQLEXPRESS;Initial Catalog=BD_Densy;Integrated Security=True";

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}
