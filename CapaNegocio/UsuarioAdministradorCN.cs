using System;
using CapaDatos.CRUD;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class UsuarioAdministradorCN
    {
        //Instancia de la clase
        UsuarioAdministradorCD usuarioadministradorcd = new UsuarioAdministradorCD();

        //Metodo Capa validar inicio sesion
        public bool ValidarInicioSesion(string nombreUsuario, string contrasena, out string tipoRol)
        {
            return usuarioadministradorcd.ValidarInicioDeSesion(nombreUsuario, contrasena, out tipoRol);
        }
    }
}
