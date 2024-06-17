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
using System.Windows.Threading;

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
            txtNombreCompleto.Text = paciente.NombreCompleto;
            txtCedula.Text = paciente.Cedula;
            txtEdad.Text = paciente.Edad.ToString();
            txtDireccion.Text = paciente.Direccino;
            txtTelefono.Text = paciente.Telefono.ToString();
            txtOcupacion.Text = paciente.Ocupacion;
            txtAntecedentes.Text = "Ninguno";


            // Seleccionar el valor correcto en el ComboBox
            foreach (ComboBoxItem item in GeneroComboBox.Items)
            {
                if (item.Tag.ToString() == paciente.IdGenero.ToString())
                {
                    GeneroComboBox.SelectedItem = item;
                    break;
                }
            }
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

        private void btnMostrarCitaEspecifica_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PagHistorialClinico(paciente));
        }

        private void btnEditarPaciente_Click(object sender, RoutedEventArgs e)
        {
            btnEditarPaciente.Visibility = Visibility.Collapsed;
            btnGuardarCambiosPaciente.Visibility = Visibility.Visible;

            TextMensajeInformacion.Visibility = Visibility.Visible;
            TextMensajeInformacion.Text = "¡Modo Editar!";
            TextMensajeInformacion.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C58D1C"));
            TextMensajeInformacion.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FEDB9B"));
            borderInformacionPersonal.BorderBrush = (Brush)new SolidColorBrush(Colors.Orange);

            HabilitarCampos();
        }

        private void HabilitarCampos()
        {
            txtNombreCompleto.IsEnabled = true;
            txtCedula.IsEnabled = true;
            txtEdad.IsEnabled = true;
            txtDireccion.IsEnabled = true;
            txtTelefono.IsEnabled = true;
            txtOcupacion.IsEnabled = true;
            txtAntecedentes.IsEnabled = true;
            GeneroComboBox.IsEnabled = true;

        }

        private void DesahabilitarCampos()
        {
            txtNombreCompleto.IsEnabled = false;
            txtCedula.IsEnabled = false;
            txtEdad.IsEnabled = false;
            txtDireccion.IsEnabled = false;
            txtTelefono.IsEnabled = false;
            txtOcupacion.IsEnabled = false;
            txtAntecedentes.IsEnabled = false;
            GeneroComboBox.IsEnabled = false;

        }

        private void btnGuardarCambiosPaciente_Click(object sender, RoutedEventArgs e)
        {
            btnGuardarCambiosPaciente.Visibility = Visibility.Collapsed;
            btnEditarPaciente.Visibility = Visibility.Visible;
            MostrarMensaje("¡Cambios Aplicado!", Colors.Green, 2, Colors.LightGreen);
            borderInformacionPersonal.BorderBrush = (Brush)new SolidColorBrush(Colors.Green);

            DesahabilitarCampos();
        }


        public void MostrarMensaje(string texto, Color color, int duracionSegundos, Color Background)
        {
            TextMensajeInformacion.Text = texto;
            TextMensajeInformacion.Foreground = new SolidColorBrush(color);
            TextMensajeInformacion.Background = new SolidColorBrush(Background);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(duracionSegundos);
            timer.Tick += (sender, args) =>
            {
                TextMensajeInformacion.Text = "";
                TextMensajeInformacion.Visibility = Visibility.Collapsed;
                borderInformacionPersonal.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#d4d5d6");
                timer.Stop();
            };
            timer.Start();
        }
    }
}
