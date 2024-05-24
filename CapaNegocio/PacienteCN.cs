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

        public DataTable BuscarPorNombre(Paciente paciente)
        {
            return pacienteccd.BuscarPorNombre(paciente);
        }

        public DataTable BuscarPorCedula(Paciente paciente)
        {
            return pacienteccd.BuscarPorCedula(paciente);
        }
  
        public DataTable ObtenerPacientes()
        {
            DataTable Tabla = new DataTable();
            Tabla = pacienteccd.ObtenerPacientes();
            return Tabla;              
        }

        public bool RegistrarPaciente(Paciente paciente)
        {
            return pacienteccd.RegistrarPaciente(paciente);
        }

        public bool ValidarExistente (Paciente paciente)
        {
            return pacienteccd.ValidarExistente(paciente);
        }
    }
}
