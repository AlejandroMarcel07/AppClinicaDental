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

            ObtenerPacientes();

            PopulateTimeComboBox();

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

        public void ObtenerPacientes()
        {

            DataTable tabla = new DataTable();
            tabla = pacientecn.ObtenerPacientes();
            DataView dataview = new DataView(tabla);

            ListBoxCitas.ItemsSource = dataview;
        }


        //Permitir manipular nuestrar hora y mostrarla en un combo
        private void PopulateTimeComboBox()
        {
            var timeList = new List<string>();
            for (DateTime dt = DateTime.Today.AddHours(8); dt <= DateTime.Today.AddHours(17); dt = dt.AddMinutes(30))
            {
                timeList.Add(dt.ToString("hh:mm tt"));
            }

            timeComboBox01.ItemsSource = timeList;
            timeComboBox01.SelectedIndex = 0; // Selecciona la primera hora por defecto

            timeComboBox.ItemsSource = timeList;
            timeComboBox.SelectedIndex = 0; // Selecciona la primera hora por defecto
        }


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
            this.NavigationService.Navigate(new PagHistorialClinico(paciente));
        }


        //Obtener la fecha seleccionada en el caledario y comvertirla en otro tipo de formato y mostrarla en un textbox
        private void ClendarName_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClendarName.SelectedDate.HasValue)
            {
                DateTime selectedDate = ClendarName.SelectedDate.Value;
                string formattedDate = selectedDate.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("es-ES"));

                txtFechaSeleccionada.Text = formattedDate;
            }
        }
    }
}
