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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CapaPresentacion.Paginas
{
    /// <summary>
    /// Lógica de interacción para Pag01.xaml
    /// </summary>
    public partial class Pag01 : Page
    {
        public Pag01()
        {
            InitializeComponent();


        }

        private void EtiquetaBusquedad_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EtiquetaBusquedad.Visibility = Visibility.Collapsed;
            txtBarraBusquedad.Visibility = Visibility.Visible;
            txtBarraBusquedad.Focus();
        }

        private void txtBarraBusquedad_GotFocus(object sender, RoutedEventArgs e)
        {
            EtiquetaBusquedad.Visibility = Visibility.Collapsed;
            txtBarraBusquedad.Visibility = Visibility.Visible;
        }

        private void txtBarraBusquedad_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBarraBusquedad.Text))
            {
                EtiquetaBusquedad.Visibility = Visibility.Visible;
                txtBarraBusquedad.Visibility = Visibility.Collapsed;
            }
        }

        private void AgregarPaciennte_Click(object sender, RoutedEventArgs e)
        {
            VentanaModalPaciente ventanamodal = new VentanaModalPaciente();
            ventanamodal.ShowDialog();
        }
    }
}
