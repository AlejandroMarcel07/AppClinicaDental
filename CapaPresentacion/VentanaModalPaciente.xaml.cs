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
    /// Lógica de interacción para VentanaModalPaciente.xaml
    /// </summary>
    public partial class VentanaModalPaciente : Window
    {
        public VentanaModalPaciente()
        {
            InitializeComponent();
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

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void EvaluarCamposCompletos()
        {

            if (!string.IsNullOrEmpty(txtNombreCompleto.Text) && !string.IsNullOrEmpty(txtEdad.Text) && cmbGenero.SelectedItem != null
                && !string.IsNullOrEmpty(txtDireccion.Text) && !string.IsNullOrEmpty(txtCedula.Text) && !string.IsNullOrEmpty(txtTelefono.Text)
                && !string.IsNullOrEmpty(txtGmail.Text) && !string.IsNullOrEmpty(txtOcupacion.Text))
            {
                textMensaje.Text = "Completado*";
                textMensaje.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                textMensaje.Text = "Incompleto*";
                textMensaje.Foreground = new SolidColorBrush(Colors.OrangeRed);
            }
        }

        private void btnGuardarPaciente_Click(object sender, RoutedEventArgs e)
        {
            EvaluarCamposCompletos();
        }

        private void txtNombreCompleto_TextChanged(object sender, TextChangedEventArgs e)
        {
            EvaluarCamposCompletos();
        }

        private void txtEdad_TextChanged(object sender, TextChangedEventArgs e)
        {
            EvaluarCamposCompletos();
        }

        private void cmbGenero_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EvaluarCamposCompletos();
        }

        private void txtDireccion_TextChanged(object sender, TextChangedEventArgs e)
        {
            EvaluarCamposCompletos();
        }

        private void txtCedula_TextChanged(object sender, TextChangedEventArgs e)
        {
            EvaluarCamposCompletos();
        }

        private void txtTelefono_TextChanged(object sender, TextChangedEventArgs e)
        {
            EvaluarCamposCompletos();
        }

        private void txtGmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            EvaluarCamposCompletos();
        }

        private void txtOcupacion_TextChanged(object sender, TextChangedEventArgs e)
        {
            EvaluarCamposCompletos();
        }
    }
}
