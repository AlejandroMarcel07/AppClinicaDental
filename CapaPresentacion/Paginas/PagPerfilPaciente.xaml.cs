using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CapaPresentacion.Paginas
{
    /// <summary>
    /// Lógica de interacción para PagPerfilPaciente.xaml
    /// </summary>
    public partial class PagPerfilPaciente : Page
    {
        private Paciente paciente;

        //Esta pagina resivira como parametro un paciente por lo que necesita mostrar informacion
        public PagPerfilPaciente(Paciente paciente)
        {
            InitializeComponent();
            //Asignamos el parametro recivido a un objeto creado en esta clase
            this.paciente = paciente;
            ObtenerPacientes();

            //Llmamos al metodo que contiene la asignacion de objetos a sus controles
            MostrarDatosPaciente();

        }

        private void MostrarDatosPaciente()
        {
            string GeneroVer;

            if (paciente.IdGenero.ToString() == "1")
            {
                GeneroVer = "Masculino";
            }
            else
            {
                GeneroVer = "Femenino";
            }
        

            // Ejemplo: Mostrar los datos del paciente en los controles de la página
            NombreTextBlock.Text =  paciente.NombreCompleto;
            CedulaTextBlock.Text = "Cedula: " + paciente.Cedula;
            EdadTextBlock.Text = "Edad: " + paciente.Edad.ToString();
            GeneroTextBlock.Text = "Genero: " + GeneroVer;
            DireccionTextBlock.Text = "Dirección: " + paciente.Direccino;
            TelefonoTextBlock.Text = "Telefono: " + paciente.Telefono.ToString();
            OcupacionTextBlock.Text = "Ocupación: " + paciente.Ocupacion;
        }


        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Pag01());
        }

        private PacienteCN pacientecn = new PacienteCN();

        public void ObtenerPacientes()
        {

            DataTable tabla = new DataTable();
            tabla = pacientecn.ObtenerPacientes();
            DataView dataview = new DataView(tabla);

            ListBoxCitas.ItemsSource = dataview;
        }

        private void btnCrearCita_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PagCita(paciente));

        }


        private void toggleButton_Checked(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
        }

        private void toggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
        }
    }
}
