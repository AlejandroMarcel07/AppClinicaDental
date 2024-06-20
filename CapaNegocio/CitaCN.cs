using System;
using CapaEntidad;
using CapaDatos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos.CRUD;
using System.Net;

namespace CapaNegocio
{
    public class CitaCN
    {
        CitaCD citacp = new CitaCD();

        public Cita RegistrarCita(Cita cita)
        {

            // Validar Campos de objeto
            ValidarObjeto(cita);

            try
            {

                //Obtenemos la list de cita para validar en el for las citas del dia especifico
                List<Cita> CitasActuales = citacp.ObtenerCitasPorDia(cita);

                // Verificar si hay alguna cita que se superponga con la nueva cita
                for (int i = 0; i < CitasActuales.Count; i++) 
                {                
                    Cita c = CitasActuales[i];
                    if ((cita.HoraEntrada >= c.HoraEntrada && cita.HoraEntrada < c.HoraSalida) ||
                        (cita.HoraSalida > c.HoraEntrada && cita.HoraSalida <= c.HoraSalida) ||
                        (cita.HoraEntrada < c.HoraEntrada && cita.HoraSalida > c.HoraSalida))
                    {
                        throw new ArgumentException("La cita se superpone con una cita existente.");
                    }          
                    if (cita.HoraEntrada == cita.HoraSalida)
                    {
                        throw new ArgumentException("La cita contiene las mismas hora");
                    }
                }


                // Si pasa la validación, registrar la cita
                return citacp.RegistrarCita(cita);

            }
            catch (ArgumentException ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }



        private void ValidarObjeto(Cita cita)
        {
            if (string.IsNullOrWhiteSpace(cita.Id.ToString()))
                throw new ArgumentException("Id null: Error");
            if (string.IsNullOrWhiteSpace(cita.IdPaciente.ToString()))
                throw new ArgumentException("IdPaciente null: Error");
            if (string.IsNullOrWhiteSpace(cita.Fecha.ToString()))
                throw new ArgumentException("Fecha null: Error");
            if (string.IsNullOrWhiteSpace(cita.HoraEntrada.ToString()))
                throw new ArgumentException("Hora null: Error");
            if (string.IsNullOrWhiteSpace(cita.HoraSalida.ToString()))
                throw new ArgumentException("Hora null: Error");
        }

    }
}
