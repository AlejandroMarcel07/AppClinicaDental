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

namespace CapaPresentacion.Paginas
{
    /// <summary>
    /// Lógica de interacción para PagHistorialClinico.xaml
    /// </summary>
    public partial class PagHistorialClinico : Page
    {

        private Paciente paciente;
        public PagHistorialClinico(Paciente paciente)
        {
            InitializeComponent();
            this.paciente = paciente;
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
            TextMensajeCasoPaciente.Foreground = new SolidColorBrush(Colors.Orange);
            borderCasoPaciente.BorderBrush = (Brush)new SolidColorBrush(Colors.Orange);

        }

        private void btnGuardarCasoPaciente_Click(object sender, RoutedEventArgs e)
        {
            btnGuardarCasoPaciente.Visibility = Visibility.Collapsed;
            btnEditarCasoPaciente.Visibility = Visibility.Visible;
            MostrarMensaje("¡Cambios Aplicado!", Colors.Green, 2);
            borderCasoPaciente.BorderBrush = (Brush)new SolidColorBrush(Colors.Green);
        }


        public void MostrarMensaje(string texto, Color color, int duracionSegundos)
        {
            TextMensajeCasoPaciente.Text = texto;
            TextMensajeCasoPaciente.Foreground = new SolidColorBrush(color);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(duracionSegundos);
            timer.Tick += (sender, args) =>
            {
                TextMensajeCasoPaciente.Text = "";
                TextMensajeCasoPaciente.Visibility = Visibility.Collapsed;
                borderCasoPaciente.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#d4d5d6");
                timer.Stop();
            };
            timer.Start();
        }


    }
}
