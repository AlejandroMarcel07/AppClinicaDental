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

        private void btnEditarCasoPaciente_Click(object sender, RoutedEventArgs e)
        {
            btnEditarCasoPaciente.Visibility = Visibility.Collapsed;
            btnGuardarCasoPaciente.Visibility = Visibility.Visible;
        }

        private void btnGuardarCasoPaciente_Click(object sender, RoutedEventArgs e)
        {
            btnGuardarCasoPaciente.Visibility = Visibility.Collapsed;
            btnEditarCasoPaciente.Visibility = Visibility.Visible;
        }

        private void btnGuardarFactura_Click(object sender, RoutedEventArgs e)
        {
            btnGuardarFactura.Visibility = Visibility.Collapsed;
            btnEditaFactura.Visibility = Visibility.Visible;
        }

        private void btnEditaFactura_Click(object sender, RoutedEventArgs e)
        {
            btnEditaFactura.Visibility = Visibility.Collapsed;
            btnGuardarFactura.Visibility = Visibility.Visible;
        }

        private void btnEditarReceta_Click(object sender, RoutedEventArgs e)
        {
            btnEditarReceta.Visibility = Visibility.Collapsed;
            btnGuardarReceta.Visibility = Visibility.Visible;
        }

        private void btnGuardarReceta_Click(object sender, RoutedEventArgs e)
        {
            btnGuardarReceta.Visibility = Visibility.Collapsed;
            btnEditarReceta.Visibility = Visibility.Visible;
        }
    }
}
