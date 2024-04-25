using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Lógica de interacción para Page5.xaml
    /// </summary>
    public partial class Page5 : Page
    {
        public Page5()
        {
            InitializeComponent();
        }

        private void btnVerificarIdentidad_Click(object sender, RoutedEventArgs e)
        {
            string MensajeUsuarioIncompleto = "¡Usuario!";
            string MensajeContrasenaIncompleto = "¡Contraseña!";

            if (!string.IsNullOrEmpty(txtUsuarioVerificar.Text))
            {
                if (!string.IsNullOrEmpty(txtContrasenaVerificar.Text))
                {


                }
                else
                {
                    txtMensaje.Text = MensajeContrasenaIncompleto;
                    txtMensaje.Foreground = new SolidColorBrush(Colors.OrangeRed);
                    return;
                }
            }
            else
            {
                txtMensaje.Text = MensajeUsuarioIncompleto;
                txtMensaje.Foreground = new SolidColorBrush(Colors.OrangeRed);
                return; // Sale del método si el usuario está incompleto
            }
        }

        private void EvaluarCamposCompletos()
        {

            if (!string.IsNullOrEmpty(txtUsuarioVerificar.Text) && !string.IsNullOrEmpty(txtContrasenaVerificar.Text))
            {
                txtMensaje.Text = "Completado";
                txtMensaje.Foreground = new SolidColorBrush(Colors.Green);

                // Color en formato hexadecimal (por ejemplo, azul)
                string colorHexadecimal = "#0A3EBD";

                // Convertir el color hexadecimal a un objeto Color
                Color color = (Color)ColorConverter.ConvertFromString(colorHexadecimal);

                // Asignar el color al fondo del botón
                btnVerificarIdentidad.Background = new SolidColorBrush(color);
            }
            else
            {
                txtMensaje.Text = "Incompleto";
                txtMensaje.Foreground = new SolidColorBrush(Colors.OrangeRed);

                // Color en formato hexadecimal (por ejemplo, azul)
                string colorHexadecimal = "#B9B9B9";

                // Convertir el color hexadecimal a un objeto Color
                Color color = (Color)ColorConverter.ConvertFromString(colorHexadecimal);

                // Asignar el color al fondo del botón
                btnVerificarIdentidad.Background = new SolidColorBrush(color);
            }
        }

        private void txtUsuarioVerificar_TextChanged(object sender, TextChangedEventArgs e)
        {
            EvaluarCamposCompletos();
        }

        private void txtContrasenaVerificar_TextChanged(object sender, TextChangedEventArgs e)
        {
            EvaluarCamposCompletos();
        }
    }
}
