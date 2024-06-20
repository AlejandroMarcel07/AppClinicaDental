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

        public Paciente ObtenerPacienteCompleto(string cedula)
        {
            // Llamar a la capa de datos para obtener el paciente
            Paciente paciente = pacienteccd.ObtenerPacienteCompleto(cedula);

            if (paciente == null)
            {
                throw new Exception("Paciente no encontrado.");
            }

            return paciente;

        }

        public bool ActualizarPaciente(Paciente pacienteNuevo, Paciente pacienteActual)
        {
            try
            {
                string CedulaNueva = pacienteNuevo.Cedula;
                string CedulaActual = pacienteActual.Cedula;

                bool validarExistencia = pacienteccd.ValidarExistenteModificada(CedulaActual, CedulaNueva);

                if (validarExistencia)
                {
                    throw new ArgumentException("¡Esta paciente ya existe!");
                }

                return pacienteccd.ActualizarPaciente(pacienteNuevo);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }


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

        public void ValidacionCamposReglas(Paciente paciente)
        {
            if (paciente.NombreCompleto.Length > 50)
                throw new ArgumentException("¡Nombre no exceder!");
            if (paciente.NombreCompleto.Length < 15)
                throw new ArgumentException("¡Requiere nombre completo!");
            if (paciente.Cedula.Length != 14)
                throw new ArgumentException("¡Cedula invalida!");
            if (paciente.Edad > 99 || paciente.Edad <= 0)
                throw new ArgumentException("¡Edad invalida!");
            if (paciente.Direccion.Length > 100)
                throw new ArgumentException("¡Direccion no exceder!");
            if (paciente.Telefono.ToString().Length != 8)
                throw new ArgumentException("¡Telefono invalido!");
            if (paciente.Ocupacion.Length > 70)
                throw new ArgumentException("¡Ocupacion no exceder!");
            if (paciente.Antecedentes.Length > 500)
                throw new ArgumentException("¡Antecedentes no exceder!");
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



    }
}
