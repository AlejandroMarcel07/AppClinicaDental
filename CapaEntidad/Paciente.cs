using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Paciente
    {

        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Cedula { get; set; }
        public int Edad { get; set; }
        public int IdGenero { get; set; }
        public string Direccino { get; set; }
        public int Telefono { get; set; }
        public string Gmail { get; set; }
        public string Ocupacion { get; set; }

        public Paciente()
        {

        }

        public Paciente(int id, string nombreCompleto, string cedula, int edad, int idGenero, string direccino, int telefono, string gmail, string ocupacion)
        {
            Id = id;
            NombreCompleto = nombreCompleto;
            Cedula = cedula;
            Edad = edad;
            IdGenero = idGenero;
            Direccino = direccino;
            Telefono = telefono;
            Gmail = gmail;
            Ocupacion = ocupacion;
        }
    }
}
