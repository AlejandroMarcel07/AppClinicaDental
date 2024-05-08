using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CapaNegocio;
using CapaEntidad;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Text.RegularExpressions;

namespace CapaPresentacion.Paginas
{
    /// <summary>
    /// Lógica de interacción para Pag01.xaml
    /// </summary>
    public partial class Pag01 : Page
    {
        private PacienteCN pacientecn = new PacienteCN();


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

        //Btn PRINCIPAL
        private void AgregarPaciennte_Click(object sender, RoutedEventArgs e)
        {
            VentanaModalPaciente ventanamodal = new VentanaModalPaciente(this);
            ventanamodal.ShowDialog();
        }

        public void ObtenerPacientes()
        {

            DataTable tabla = new DataTable();
            tabla = pacientecn.ObtenerPacientes();
            DataView dataview = new DataView(tabla);

            ListBoxPacientes.ItemsSource = dataview;
        }

        //private void BuscarPorNombre()
        //{
        //    if (!string.IsNullOrEmpty(txtBarraBusquedad.Text))
        //    {
        //        Paciente pacienteNombre = new Paciente();
        //        pacienteNombre.NombreCompleto = txtBarraBusquedad.Text;

        //        DataTable tabla = new DataTable();
        //        tabla = pacientecn.BuscarPorNombre(pacienteNombre);
        //        DataView dataview = new DataView(tabla);
        //        ListBoxPacientes.ItemsSource = null;
        //        ListBoxPacientes.ItemsSource = dataview;
        //    }
        //    else
        //    {
        //        ObtenerPacientes();
        //    }
        //}

        //private void BuscarPorCedula()
        //{
        //    if (!string.IsNullOrEmpty(txtBarraBusquedad.Text))
        //    {
        //        Paciente pacienteCedula = new Paciente();
        //        pacienteCedula.Cedula = txtBarraBusquedad.Text;

        //        DataTable tabla = new DataTable();
        //        tabla = pacientecn.BuscarPorCedula(pacienteCedula);
        //        DataView dataview = new DataView(tabla);
        //        ListBoxPacientes.ItemsSource = null;
        //        ListBoxPacientes.ItemsSource = dataview;
        //    }
        //    else
        //    {
        //        ObtenerPacientes();
        //    }
        //}

        public void RefrescarListbox()
        {
            ListBoxPacientes.Items.Refresh();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Cuando este incialize mostrar la lista de datos de lo paciente en la tabla 
            ObtenerPacientes();
        }

        private void txtBarraBusquedad_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBarraBusquedad.Text))
            {
                Paciente pacienteNombre = new Paciente();
                pacienteNombre.NombreCompleto = txtBarraBusquedad.Text;

                DataTable tablaNombre = pacientecn.BuscarPorNombre(pacienteNombre);

                Paciente pacienteCedula = new Paciente();
                pacienteCedula.Cedula = txtBarraBusquedad.Text;

                DataTable tablaCedula = pacientecn.BuscarPorCedula(pacienteCedula);

                // Combinar los resultados en una sola tabla
                DataTable tablaCombinada = new DataTable();
                tablaCombinada.Merge(tablaNombre);
                tablaCombinada.Merge(tablaCedula);

                DataView dataview = new DataView(tablaCombinada);
                ListBoxPacientes.ItemsSource = null;
                ListBoxPacientes.ItemsSource = dataview;
            }
            else
            {
                ObtenerPacientes();
            }
        }

    }
}
