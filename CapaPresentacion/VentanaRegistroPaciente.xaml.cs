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
using System.Windows.Shapes;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para VentanaRegistroPaciente.xaml
    /// </summary>
    public partial class VentanaRegistroPaciente : Window
    {
        public VentanaRegistroPaciente()
        {
            InitializeComponent();
            ObtenerPacientes();
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

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGuardarCambios_Click(object sender, RoutedEventArgs e)
        {

        }

        private PacienteCN pacientecn = new PacienteCN();

        public void ObtenerPacientes()
        {

            DataTable tabla = new DataTable();
            tabla = pacientecn.ObtenerPacientes();
            DataView dataview = new DataView(tabla);

            ListBoxPacientes.ItemsSource = dataview;
        }
    }
}
