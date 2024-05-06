using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CapaDatos.CRUD;
using System.Text;
using CapaEntidad;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class PacienteCN
    {
        private PacienteCD pacienteccd = new PacienteCD();

        public List<Paciente> ObtenerPaciente()
        {
            List<Paciente> pacientes = new List<Paciente>();
            DataTable tabla = pacienteccd.ObtenerPacientes();

            foreach (DataRow fila in tabla.Rows)
            {
                Paciente paciente = new Paciente
                {
                    Id1 = Convert.ToInt32(fila["Id"]),
                    NombreCompleto1 = fila["NombreCompleto"].ToString(),
                    Edad1 = Convert.ToInt32(fila["Edad"]),
                    IdGenero1 = Convert.ToInt32(fila["IdGenero"]),
                    Direccino1 = fila["Direccion"].ToString(),
                    Telefono1 = Convert.ToInt32(fila["Telefono"]),
                    Gmail1 = fila["Gmail"].ToString(),
                    Ocupacion1 = fila["Ocupacion"].ToString()
                };

            pacientes.Add(paciente);
            }

            return pacientes;
        }
    }
}
