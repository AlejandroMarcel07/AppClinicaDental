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
using System.Windows.Controls.Primitives;

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

        public void ObtenerPacienteCompleto (Paciente paciente)
        {
            string Cedula = paciente.Cedula;
            Paciente paciente1 = pacientecn.ObtenerPacienteCompleto(Cedula);
            this.NavigationService.Navigate(new PagPerfilPaciente(paciente1));
        }
             

        public void RefrescarListbox()
        {
            ListBoxPacientes.Items.Refresh();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Cuando este incialize mostrar la lista de datos de lo paciente en la tabla 
            ObtenerPacientes();
        }

        private void BuscarNombreCedula()
        {
            if (!string.IsNullOrEmpty(txtBarraBusquedad.Text))
            {

                Paciente pacienteNombre = new Paciente();
                pacienteNombre.NombreCompleto = txtBarraBusquedad.Text;

                //Resivimos el return
                DataTable tablaNombre = pacientecn.BuscarPorNombre(pacienteNombre);

                Paciente pacienteCedula = new Paciente();
                pacienteCedula.Cedula = txtBarraBusquedad.Text;

                DataTable tablaCedula = pacientecn.BuscarPorCedula(pacienteCedula);

                // Combinar los resultados en una sola tabla
                DataTable tablaCombinada = new DataTable();
                tablaCombinada.Merge(tablaNombre);
                tablaCombinada.Merge(tablaCedula);

                // Compatible con Listbox
                DataView dataview = new DataView(tablaCombinada);
                ListBoxPacientes.ItemsSource = null;
                ListBoxPacientes.ItemsSource = dataview;
            }
            else
            {
                ObtenerPacientes();
            }
        }


        private void txtBarraBusquedad_TextChanged(object sender, TextChangedEventArgs e)
        {
            BuscarNombreCedula();
        }

        private Paciente pacienteSeleccionado;

        private void toggleButton_Checked(object sender, RoutedEventArgs e)
        {
            // Obtener el ToggleButton que fue clicado
            ToggleButton toggleButton = sender as ToggleButton;

            // Obtener el DataContext del ToggleButton (que es el objeto del paciente)
            DataRowView selecItem = toggleButton.DataContext as DataRowView;


            if (selecItem != null)
            {
                // Accede a las propiedades del DataRowView y crea un objeto Paciente
                pacienteSeleccionado = new Paciente
                {
                    //Obtenemos los atributos de objeto por la seleccion y se los pasamos a un nuevo objeto
                    Id = Convert.ToInt32(selecItem["Id"]),
                    NombreCompleto = selecItem["NombreCompleto"].ToString(),
                    Cedula = selecItem["Cedula"].ToString(),
                    Edad = Convert.ToInt32(selecItem["Edad"]),
                    IdGenero = Convert.ToInt32(selecItem["IdGenero"]),
                    Direccion = selecItem["Direccion"].ToString(),
                    Telefono = Convert.ToInt32(selecItem["Telefono"]),
                    Ocupacion = selecItem["Ocupacion"].ToString(),
                    Antecedentes = selecItem["Antecedentes"].ToString(),
                    Activo = Convert.ToBoolean(selecItem["IsDeleted"])
                };
            }

            popup.IsOpen = true;
        }

        private void btnMostrarRegistroPaciente_Click(object sender, RoutedEventArgs e)
        {
            if (pacienteSeleccionado != null)
            {
                this.NavigationService.Navigate(new PagPerfilPaciente(pacienteSeleccionado));
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un paciente de la lista.");
            }
        }

        private void toggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
        }

        private void btnMostrarPantallaMensaje_Click(object sender, RoutedEventArgs e)
        {
            VentanaMensajeSms ventanaMensaje = new VentanaMensajeSms(pacienteSeleccionado);
            ventanaMensaje.ShowDialog();
        }
    }
}
