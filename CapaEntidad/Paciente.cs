using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Paciente
    {

        private int Id;
        private string NombreCompleto;
        private int Edad;
        private int IdGenero;
        private string Direccino;
        private int Telefono;
        private string Gmail;
        private string Ocupacion;

        public int Id1 { get => Id; set => Id = value; }
        public string NombreCompleto1 { get => NombreCompleto; set => NombreCompleto = value; }
        public int Edad1 { get => Edad; set => Edad = value; }
        public int IdGenero1 { get => IdGenero; set => IdGenero = value; }
        public string Direccino1 { get => Direccino; set => Direccino = value; }
        public int Telefono1 { get => Telefono; set => Telefono = value; }
        public string Gmail1 { get => Gmail; set => Gmail = value; }
        public string Ocupacion1 { get => Ocupacion; set => Ocupacion = value; }

        public Paciente()
        {

        }

        public Paciente(int id, string nombreCompleto, int edad, int idGenero, string direccino, int telefono, string gmail, string ocupacion)
        {
            Id = id;
            NombreCompleto = nombreCompleto;
            Edad = edad;
            IdGenero = idGenero;
            Direccino = direccino;
            Telefono = telefono;
            Gmail = gmail;
            Ocupacion = ocupacion;
        }
    }
}
