using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaNegocio;
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
using System.Data;
using System.Globalization;
using System.Windows.Threading;

namespace CapaPresentacion.Paginas
{
    /// <summary>
    /// Lógica de interacción para PagCita.xaml
    /// </summary>
    public partial class PagCita : Page
    {
        private Paciente paciente;
        public PagCita(Paciente paciente)
        {
            InitializeComponent();
            //Asignamos el parametro recivido a un objeto creado en esta clase
            this.paciente = paciente;

            MostrarDatosPaciente();

            ConfigureCalendar();
        }

        private void MostrarDatosPaciente()
        {
            // Ejemplo: Mostrar los datos del paciente en los controles de la página
            NombreTextBlock.Text = "Nombre: " + paciente.NombreCompleto;
            CedulaTextBlock.Text = "Cedula: " + paciente.Cedula;
        }

        private void btnAtrasTablaPaciente_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Pag01());
        }

        private void btnAtrasRegistro_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PagPerfilPaciente(paciente));
        }

        private PacienteCN pacientecn = new PacienteCN();



        //Permitir manipular nuestrar hora y mostrarla en un combo



        //Metodo que permitira renderizar nuestro calendario en la cual mostrara fecha actual y a futuro, fechas anteriores no
        private void ConfigureCalendar()
        {
            // Establecer la fecha de inicio de la visualización del calendario
            ClendarName.DisplayDateStart = DateTime.Today;

            // Bloquear todas las fechas anteriores a hoy
            ClendarName.BlackoutDates.AddDatesInPast();
        }

        private void btnCrearCitaYA_Click(object sender, RoutedEventArgs e)
        {
            //Validamos Campos
            if (!ValidarCampos(out string mensajeError))
            {
                MensajeError(mensajeError);
                return;
            }

            //Instania de la clase
            CitaCN citacn = new CitaCN();

            //Obtenemos datos de los controles
            Cita citaObtenido = ObtenrDatosControles();

            try
            {
                //Si todo esta bien lo mandamos a la capa negocio para evaluar que haiga citas con el mismo dia y la misma hora 
                //Y sale bien en la capa negocio creamos la cita y obtenemos el objeto de ella.
                Cita citaRegistrada = citacn.RegistrarCita(citaObtenido);
                //Hacemos el siguiente paso a realizar
                this.NavigationService.Navigate(new PagHistorialClinico(paciente, citaRegistrada));
            }
            catch (ArgumentException ex)
            {
                MensajeError(ex.Message);
            }
        }

        private Cita ObtenrDatosControles()
        {

            string horaSeleccionadaStringEntrada = ((ComboBoxItem)timeComboBox.SelectedItem).Content.ToString();

            string horaSeleccionadaStringSalida = ((ComboBoxItem)timeComboBox01.SelectedItem).Content.ToString();


            // Fecha seleccionada
            DateTime fechaSeleccionada = (ClendarName.SelectedDate ?? DateTime.MinValue).Date;


            DateTime dateTimeEntrada = DateTime.ParseExact(horaSeleccionadaStringEntrada, "hh:mm tt", CultureInfo.InvariantCulture);
            TimeSpan HoraEntrada = dateTimeEntrada.TimeOfDay;

            DateTime dateTimeSalida = DateTime.ParseExact(horaSeleccionadaStringSalida, "hh:mm tt", CultureInfo.InvariantCulture);
            TimeSpan HoraSalida = dateTimeSalida.TimeOfDay;


            // Crear instancia de Cita con los datos obtenidos
            Cita nuevaCita = new Cita
            {
                IdPaciente = paciente.Id,
                Fecha = fechaSeleccionada,
                HoraEntrada = HoraEntrada,
                HoraSalida = HoraSalida
            };

            return nuevaCita;
        }


        private bool ValidarCampos(out string mensajeError)
        {
            mensajeError = string.Empty;

            if (string.IsNullOrEmpty(txtFechaSeleccionada.Text))
            {
                mensajeError = "¡Seleccionar Fecha!";
                return false;
            }

            if (timeComboBox.SelectedItem == null)
            {
                mensajeError = "¡Seleccione Hora de Entrada!";
                return false;
            }

            if (timeComboBox01.SelectedItem == null)
            {
                mensajeError = "¡Seleccione Hora de Salida!";
                return false;
            }

            return true;
        }

        private void MensajeError(string Mensaje)
        {
            textMensaje.Text = Mensaje;
            textMensaje.Visibility = Visibility.Visible;
            textMensaje.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FA465B"));
            textMensaje.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE2E0"));

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += (sender, args) =>
            {
                textMensaje.Visibility = Visibility.Collapsed;
                timer.Stop();
            };
            timer.Start();
        }

        //Obtener la fecha seleccionada en el caledario y comvertirla en otro tipo de formato y mostrarla en un textbox
        // Obtener la fecha seleccionada en el calendario y convertirla en otro tipo de formato y mostrarla en un TextBox
        private void ClendarName_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime fechaSeleccionada = (ClendarName.SelectedDate ?? DateTime.MinValue).Date;
            txtFechaSeleccionada.Text = fechaSeleccionada.ToString("yyyy-MM-dd");
        }

    }
}
