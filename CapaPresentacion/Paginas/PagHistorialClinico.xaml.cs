using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CapaEntidad;
using CapaNegocio;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Globalization;

namespace CapaPresentacion.Paginas
{
    /// <summary>
    /// Lógica de interacción para PagHistorialClinico.xaml
    /// </summary>
    public partial class PagHistorialClinico : Page
    {

        private Paciente paciente;
        private Cita cita;
        public PagHistorialClinico(Paciente paciente, Cita cita)
        {
            InitializeComponent();
            this.paciente = paciente;
            this.cita = cita;

            MostrarDatos();
        }

        private void MostrarDatos()
        {


            // Crear una fecha base para convertir el TimeSpan a DateTime
            DateTime baseDateHora01 = DateTime.Today;

            // Crear un nuevo DateTime sumando el TimeSpan a la fecha base
            DateTime entradaDateTime = baseDateHora01.Add(cita.HoraEntrada);

            // Convertir el DateTime resultante a una cadena con el formato deseado
            string horaEntradaFormatted = entradaDateTime.ToString("hh':'mm tt");

            //---------------------------------------

            DateTime baseDateHora02 = DateTime.Today;

            DateTime salidaDateTime = baseDateHora02.Add(cita.HoraSalida);

            string horaSalidaFormatted = salidaDateTime.ToString("hh':'mm tt");


            DateTime fechaSeleccionada = cita.Fecha;
            string fechaFormateada = fechaSeleccionada.ToString("yyyy-MM-dd");
            textFechaCita.Text = "- Fecha Cita: " + fechaFormateada;

            textHoraEntrada.Text = "- Hora Entrada: " + horaEntradaFormatted;
            textHoraSalida.Text = "- Hora Salida: " + horaSalidaFormatted;
            textNombrePaciente.Text = "- Nombre: " + paciente.NombreCompleto;
            textCedulaPaciente.Text = "- Cedula: " + paciente.Cedula;
            textAntecedente.Text = "- Antecedentes: " + paciente.Antecedentes;
        }

        private void btnAtrasRegistro_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PagPerfilPaciente(paciente));
        }

        private void btnAtrasTablaPaciente_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Pag01());
        }


        //Editar y guardar el caso del paciente
        private void btnEditarCasoPaciente_Click(object sender, RoutedEventArgs e)
        {
            btnEditarCasoPaciente.Visibility = Visibility.Collapsed;
            btnGuardarCasoPaciente.Visibility = Visibility.Visible;

            TextMensajeCasoPaciente.Visibility = Visibility.Visible;
            TextMensajeCasoPaciente.Text = "¡Modo Editar!";
            TextMensajeCasoPaciente.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C58D1C"));
            TextMensajeCasoPaciente.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FEDB9B"));
            borderCasoPaciente.BorderBrush = (Brush)new SolidColorBrush(Colors.Orange);

        }

        private void btnGuardarCasoPaciente_Click(object sender, RoutedEventArgs e)
        {
            btnGuardarCasoPaciente.Visibility = Visibility.Collapsed;
            MostrarMensaje("¡Cambios Aplicado!", Colors.Green, 2, Colors.LightGreen);
            borderCasoPaciente.BorderBrush = (Brush)new SolidColorBrush(Colors.Green);
        }


        public void MostrarMensaje(string texto, Color color, int duracionSegundos, Color Background)
        {
            TextMensajeCasoPaciente.Text = texto;
            TextMensajeCasoPaciente.Foreground = new SolidColorBrush(color);
            TextMensajeCasoPaciente.Background = new SolidColorBrush(Background);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(duracionSegundos);
            timer.Tick += (sender, args) =>
            {
                TextMensajeCasoPaciente.Text = "";
                TextMensajeCasoPaciente.Visibility = Visibility.Collapsed;
                borderCasoPaciente.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#d4d5d6");
                btnEditarCasoPaciente.Visibility = Visibility.Visible;
                timer.Stop();
            };
            timer.Start();
        }


    }
}
