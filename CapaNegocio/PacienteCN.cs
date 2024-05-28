using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CapaDatos.CRUD;
using System.Text;
using CapaEntidad;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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
            bool validarExistencia = pacienteccd.ValidarExistente(paciente);
            
            if (validarExistencia)
            {
                throw new ArgumentException("¡Esta paciente ya existe!");
            }

            return pacienteccd.RegistrarPaciente(paciente);
        }

        public void ValidacionCamposReglas(Paciente paciente)
        {
            string gmailPattern = @"\b[A-Za-z0-9._%+-]+@gmail\.com\b";

            if (paciente.NombreCompleto.Length > 50)
                throw new ArgumentException("¡Nombre no exceder!");
            if (paciente.NombreCompleto.Length < 15)
                throw new ArgumentException("¡Requiere nombre completo!");
            if (paciente.Cedula.Length != 14)
                throw new ArgumentException("¡Cedula invalida!");
            if (paciente.Edad > 99 || paciente.Edad <= 0)
                throw new ArgumentException("¡Edad invalida!");
            if (paciente.Direccino.Length > 100)
                throw new ArgumentException("¡Direccion no exceder!");
            if (paciente.Telefono.ToString().Length != 8)
                throw new ArgumentException("¡Telefono invalido!");
            if (paciente.Gmail.Length > 100)
                throw new ArgumentException("¡Gmail no exceder!");
            if (!Regex.IsMatch(paciente.Gmail, gmailPattern))
                throw new ArgumentException("¡Gmail Invalido!");
            if (paciente.Ocupacion.Length > 70)
                throw new ArgumentException("¡Ocupacion no exceder!");
        }
    }
}
