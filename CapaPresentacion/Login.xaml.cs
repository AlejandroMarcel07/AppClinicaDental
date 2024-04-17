using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();

        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EtiquetaUsuario.Visibility = Visibility.Collapsed;
            txtUsuario.Visibility = Visibility.Visible;
            txtUsuario.Focus();
        }

        private void txtUsuario_GotFocus(object sender, RoutedEventArgs e)
        {
            EtiquetaUsuario.Visibility = Visibility.Collapsed;
            txtUsuario.Visibility = Visibility.Visible;

        }

        private void txtUsuario_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                EtiquetaUsuario.Visibility = Visibility.Visible;
                txtUsuario.Visibility = Visibility.Collapsed;
            }
        }

        private void EtiquetaContraseña_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EtiquetaContraseña.Visibility = Visibility.Collapsed;
            txtContraseña.Visibility = Visibility.Visible;
            txtContraseña.Focus();
        }

        private void txtContraseña_GotFocus(object sender, RoutedEventArgs e)
        {
            EtiquetaContraseña.Visibility = Visibility.Collapsed;
            txtContraseña.Visibility = Visibility.Visible;
        }

        private void txtContraseña_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtContraseña.Password))
            {
                EtiquetaContraseña.Visibility = Visibility.Visible;
                txtContraseña.Visibility = Visibility.Collapsed;
            }
        }

        private void btnCerrarLogin_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimizarAplicacion_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            string MensajeContrasenaIncompleta = "¡Contraseña!";
            string MensajeUsuarioIncompleto = "¡Usuario!";
            //string MensajeUsuarioNoExiste = "¡Usuario Desconocido!";
            //string MensajeContrasenaMala = "¡Contraseña mala!";

            if(!string.IsNullOrEmpty(txtUsuario.Text))
            {
                if(!string.IsNullOrEmpty(txtContraseña.Password))
                {
                    //Continuare..
                }
                else
                {
                    textMensaje.Text = MensajeContrasenaIncompleta;
                    textMensaje.Foreground = new SolidColorBrush(Colors.OrangeRed);
                    return;
                }
            }
            else
            {
                textMensaje.Text = MensajeUsuarioIncompleto;
                textMensaje.Foreground = new SolidColorBrush(Colors.OrangeRed);
                return; // Sale del método si el usuario está incompleto
            }

        }

        //Metodo de Evaluacio en tiempo real
        private void EvaluarCamposCompletos()
        {
            
            if (!string.IsNullOrEmpty(txtUsuario.Text) && !string.IsNullOrEmpty(txtContraseña.Password))
            {
                textMensaje.Text = "Completado";
                textMensaje.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                textMensaje.Text = "Incompleto";
                textMensaje.Foreground = new SolidColorBrush(Colors.OrangeRed);
            }
        }

        private void txtContraseña_PasswordChanged(object sender, RoutedEventArgs e)
        {
            EvaluarCamposCompletos();
        }

        private void txtUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            EvaluarCamposCompletos();
        }

        private void btnOlvidoContraseña_Click(object sender, RoutedEventArgs e)
        {
            // Crear una instancia de la nueva ventana
            ContrasenaOlvidada nuevaVentana = new ContrasenaOlvidada();

            // Cerrar la ventana actual
            this.Hide();

            // Mostrar la nueva ventana
            nuevaVentana.Show();
        }
    }
}
