using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapaEntidad
{
    public class Cita
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraEntrada { get; set; }
        public TimeSpan HoraSalida { get; set; }

        public Cita()
        {

        }

        public Cita(int id, int idPaciente, DateTime fecha, TimeSpan horaEntrada, TimeSpan horaSalida)
        {
            Id = id;
            IdPaciente = idPaciente;
            Fecha = fecha;
            HoraEntrada = horaEntrada;
            HoraSalida = horaSalida;
        }
    }
}
