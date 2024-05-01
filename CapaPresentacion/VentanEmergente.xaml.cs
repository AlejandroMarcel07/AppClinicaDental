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
    /// Lógica de interacción para VentanEmergente.xaml
    /// </summary>
    public partial class VentanEmergente : Window
    {
        private PantallaPrincipal _pantallaprincipal;

        public VentanEmergente(PantallaPrincipal pantallaprincipal)
        {
            InitializeComponent();
            _pantallaprincipal = pantallaprincipal;
        }

       
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAceptarCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            _pantallaprincipal.Close();
            this.Close();

            login.Show();
        }
    }
}
