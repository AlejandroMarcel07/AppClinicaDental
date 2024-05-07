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
                    Id = Convert.ToInt32(fila["Id"]),
                    NombreCompleto = fila["NombreCompleto"].ToString(),
                    Cedula = fila["Cedula"].ToString(),
                    Edad = Convert.ToInt32(fila["Edad"]),
                    IdGenero = Convert.ToInt32(fila["IdGenero"]),
                    Direccino = fila["Direccion"].ToString(),
                    Telefono = Convert.ToInt32(fila["Telefono"]),
                    Gmail = fila["Gmail"].ToString(),
                    Ocupacion = fila["Ocupacion"].ToString()
                };

            pacientes.Add(paciente);
            }

            return pacientes;
        }

        public bool RegistrarPaciente(Paciente paciente)
        {
            return pacienteccd.RegistrarPaciente(paciente);
        }
    }
}
