using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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