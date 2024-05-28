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
        public DateTime Hora { get; set; }
        public String Titulo { get; set; }

    }
}
