using CapaPresentacion.Paginas;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Threading;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para PantallaPrincipal.xaml
    /// </summary>
    public partial class PantallaPrincipal : Window
    {
        private DispatcherTimer timer;
        public PantallaPrincipal()
        {
            InitializeComponent();

            //Inicializar frame
            PagO1.IsChecked = true;

            //Parametros de extender
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;

            // Inicializar el timer para actualizar la hora cada segundo
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Actualizar el TextBlock con la hora actual en formato de 12 horas (con AM/PM)
            txtTextBlokTiempo.Text = DateTime.Now.ToString("| " + "hh:mm tt");
            textBlockFecha.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek).ToUpper().Substring(0, 1) + CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek).Substring(1) + " " + DateTime.Now.ToString("dd 'de' MMMM 'del' yyyy");

        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnCerrarAplicacion_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimizarAplicacion_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnVolverALoguear_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.Show();
        }


        //Metodo de mostrar y evaluar pagina
        private void MostrarPagina<T>(T pagina) where T : Page
        {
            if (!(FrmPrincipal.Content is T))
            {
                OcultarPaginaActual();
                FrmPrincipal.Content = pagina;
            }
        }

        //Si se encuentra una pagina en el frame la colapsara
        private void OcultarPaginaActual()
        {
            if (FrmPrincipal.Content is Page paginaActual)
            {
                paginaActual.Visibility = Visibility.Collapsed;
            }
        }


        private void Pag01_Click(object sender, RoutedEventArgs e)
        {
            MostrarPagina(new Pag01());
        }

        private void Pag02_Click(object sender, RoutedEventArgs e)
        {
            MostrarPagina(new Pag02());
        }

        private void Pag03_Click(object sender, RoutedEventArgs e)
        {
            MostrarPagina(new Page3());
        }

        private void Pag04_Click(object sender, RoutedEventArgs e)
        {
            MostrarPagina(new Page4());
        }

        private void Pag05_Click(object sender, RoutedEventArgs e)
        {
            MostrarPagina(new Page5());
        }

        private void btnExpandir_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {

                BordePrincipalSombra.Margin = new Thickness(0);
                this.WindowState = WindowState.Maximized;

            }
            else
            {
                BordePrincipalSombra.Margin = new Thickness(30);
                this.WindowState = WindowState.Normal;

            }
        }
    }
}
