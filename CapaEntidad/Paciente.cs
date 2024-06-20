using System;

namespace CapaEntidad
{
    public class Paciente
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Cedula { get; set; }
        public int Edad { get; set; }
        public int IdGenero { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public string Ocupacion { get; set; }
        public string Antecedentes { get; set; }
        public bool Activo { get; set; } // Propiedad para indicar si el paciente está activo o no

        public Paciente()
        {

        }

        public Paciente(int id, string nombreCompleto, string cedula, int edad, int idGenero, string direccion, int telefono, string ocupacion, string antecedentes, bool activo)
        {
            Id = id;
            NombreCompleto = nombreCompleto;
            Cedula = cedula;
            Edad = edad;
            IdGenero = idGenero;
            Direccion = direccion;
            Telefono = telefono;
            Ocupacion = ocupacion;
            Antecedentes = antecedentes;
            Activo = activo; // Asignar el valor de la propiedad Activo
        }
    }
}
