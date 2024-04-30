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

        private SqlConnection Conexion = new SqlConnection("Data Source= DESKTOP-E77K5U7\\SQLEXPRESS; Database= NombreBaseDeDatos; Integrated Security = True");

        public SqlConnection AbrirConexiono()
        {
            if (Conexion.State == ConnectionState.Closed)

                Conexion.Open();
            return Conexion;

        }

        public SqlConnection CerrarConexionn()
        {
            if (Conexion.State == ConnectionState.Open)

                Conexion.Close();
            return Conexion;

        }

    }
}
