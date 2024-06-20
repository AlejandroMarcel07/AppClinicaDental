using CapaEntidad;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para VentanaMensajeSms.xaml
    /// </summary>
    public partial class VentanaMensajeSms : Window
    {
        Paciente _paciente;
        public VentanaMensajeSms(Paciente paciente)
        {
            InitializeComponent();
            this._paciente = paciente;

            MostrarDatos();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnCerrarAplicacion_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimizarAplicacion_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MostrarDatos()
        {
            txtNombrePaciente.Text = "Para: " + _paciente.NombreCompleto;
            txtNumeroPaciente.Text = "Numero: " + _paciente.Telefono;
        }

        private void btnCancelarPantalla_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnEnviarMesnaje_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtMensajeIngresado.Text))
            {
                textMensaje.Visibility = Visibility.Visible;
                textMensaje.Text = "Mensaje Vacio";
                textMensaje.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FA465B"));
                textMensaje.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE2E0"));
            }
            else
            {

                textMensaje.Visibility = Visibility.Visible;
                textMensaje.Text = "Mensaje Enviado";
                textMensaje.Background = new SolidColorBrush(Colors.LightGreen);
                textMensaje.Foreground = new SolidColorBrush(Colors.Green);
            }
        }
    }
}
