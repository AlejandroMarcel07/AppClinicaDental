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
    /// Lógica de interacción para ContrasenaOlvidada.xaml
    /// </summary>
    public partial class ContrasenaOlvidada : Window
    {
        public ContrasenaOlvidada()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimizarAplicacion_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }



        private void btnCerrarLogin_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }



        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.Show();
        }

        private void btnEnviarCorreoDeVerificacion_Click(object sender, RoutedEventArgs e)
        {
            string MensajeUsuarioIncompleto = "¡nombre de usuario!";
            string MensajeRolIncompleto = "¡rol (admin o asistente)!";

            if (txtUsuario.Text == "")
            {
                textMensaje.Text = MensajeUsuarioIncompleto;
                textMensaje.Foreground = new SolidColorBrush(Colors.OrangeRed);
                return; // Sale del método si el usuario está incompleto
            }

            if (txtRol.Text == "")
            {
                textMensaje.Text = MensajeRolIncompleto;
                textMensaje.Foreground = new SolidColorBrush(Colors.OrangeRed);
                return;
            }
        }




        //Metodo evalucion de campos
        private void EvaluarCamposCompletos()
        {

            if (!string.IsNullOrEmpty(txtUsuario.Text) && !string.IsNullOrEmpty(txtRol.Text))
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

        private void txtUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            EvaluarCamposCompletos();
        }

        private void txtRol_TextChanged(object sender, TextChangedEventArgs e)
        {
            EvaluarCamposCompletos();
        }
    }
}
