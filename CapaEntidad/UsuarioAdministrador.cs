using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class UsuarioAdministrador
    {
        private int Id;
        private int IdRol;
        private string NombreCompleto;
        private string NombreUsuario;
        private string Contrasena;
        private string Gmail;


        //Campos encapsulado con atajos de refactorizacion
        public int Id1 { get => Id; set => Id = value; }
        public int IdRol1 { get => IdRol; set => IdRol = value; }
        public string NombreCompleto1 { get => NombreCompleto; set => NombreCompleto = value; }
        public string NombreUsuario1 { get => NombreUsuario; set => NombreUsuario = value; }
        public string Contrasena1 { get => Contrasena; set => Contrasena = value; }
        public string Gmail1 { get => Gmail; set => Gmail = value; }


        //Constructor Vacio
        public UsuarioAdministrador()
        {
            //El constructor vacio permite la flexibilidad, inicializacion parcial y compatibilidad

        }

        //Constructor hecho con atajos de refactorizacion
        public UsuarioAdministrador(int id, int idRol, string nombreCompleto, string nombreUsuario, string contrasena, string gmail)
        {
            Id = id;
            IdRol = idRol;
            NombreCompleto = nombreCompleto;
            NombreUsuario = nombreUsuario;
            Contrasena = contrasena;
            Gmail = gmail;
        }
    }
}
