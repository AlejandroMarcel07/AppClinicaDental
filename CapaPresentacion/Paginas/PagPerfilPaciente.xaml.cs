using CapaEntidad;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CapaPresentacion.Paginas
{
    /// <summary>
    /// Lógica de interacción para PagPerfilPaciente.xaml
    /// </summary>
    public partial class PagPerfilPaciente : Page
    {
        private Paciente _paciente;

        //Esta pagina resivira como parametro un paciente por lo que necesita mostrar informacion
        public PagPerfilPaciente(Paciente paciente)
        {
            InitializeComponent();
            //Asignamos el parametro recivido a un objeto creado en esta clase
            string Cedula = paciente.Cedula;

            //Llmamos al metodo que contiene la asignacion de objetos a sus controles
            MostrarDatosPaciente(Cedula);

        }

        private PacienteCN pacientecn = new PacienteCN();

        //Metodo de objeto obtenidos por parametro
        private void MostrarDatosPaciente(String Cedula)
        {

            _paciente = pacientecn.ObtenerPacienteCompleto(Cedula);

            txtNombreCompleto.Text = _paciente.NombreCompleto;
            txtCedula.Text = _paciente.Cedula;
            txtEdad.Text = _paciente.Edad.ToString();
            txtDireccion.Text = _paciente.Direccion;
            txtTelefono.Text = _paciente.Telefono.ToString();
            txtOcupacion.Text = _paciente.Ocupacion;
            txtAntecedentes.Text = _paciente.Antecedentes;

            // Seleccionar el valor correcto en el ComboBox
            foreach (ComboBoxItem item in GeneroComboBox.Items)
            {
                if (item.Tag.ToString() == _paciente.IdGenero.ToString())
                {
                    GeneroComboBox.SelectedItem = item;
                    break;
                }
            }
        }


        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Pag01());
        }


        private void btnCrearCita_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PagCita(_paciente));

        }

        private void toggleButton_Checked(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
        }

        private void toggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
        }

        private void btnMostrarCitaEspecifica_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnEditarPaciente_Click(object sender, RoutedEventArgs e)
        {
            btnEditarPaciente.Visibility = Visibility.Collapsed;
            btnGuardarCambiosPaciente.Visibility = Visibility.Visible;
            btnEliminarPaciente.Visibility = Visibility.Collapsed;
            btnEnviarMensajePaciente.Visibility = Visibility.Collapsed;

            btnCancelarEditar.Visibility = Visibility.Visible;
            MensajeEditar("¡Modo Editar!");
            borderInformacionPersonal.BorderBrush = (Brush)new SolidColorBrush(Colors.Orange);

            HabilitarCampos();
        }

        private void HabilitarCampos()
        {
            txtNombreCompleto.IsEnabled = true;
            txtCedula.IsEnabled = true;
            txtEdad.IsEnabled = true;
            txtDireccion.IsEnabled = true;
            txtTelefono.IsEnabled = true;
            txtOcupacion.IsEnabled = true;
            txtAntecedentes.IsEnabled = true;
            GeneroComboBox.IsEnabled = true;

        }

        

        private void DesahabilitarCampos()
        {
            txtNombreCompleto.IsEnabled = false;
            txtCedula.IsEnabled = false;
            txtEdad.IsEnabled = false;
            txtDireccion.IsEnabled = false;
            txtTelefono.IsEnabled = false;
            txtOcupacion.IsEnabled = false;
            txtAntecedentes.IsEnabled = false;
            GeneroComboBox.IsEnabled = false;

        }

        private void LimpiarCampos()
        {
            txtNombreCompleto.Clear();
            txtEdad.Clear();
            GeneroComboBox.SelectedIndex = -1;
            txtDireccion.Clear();
            txtCedula.Clear();
            txtTelefono.Clear();
            txtOcupacion.Clear();
            txtAntecedentes.Clear();
        }

        private Paciente ObtenerValoresControles()
        {
            // Si sale todo bien el proceso continuará
            string generoSeleccionado = ((ComboBoxItem)GeneroComboBox.SelectedItem).Content.ToString();
            int valorAlmacenado = generoSeleccionado == "Masculino" ? 1 : 2;

            // Validamos reglas de campos

            Paciente paciente = new Paciente
            {
                Id = _paciente.Id,
                NombreCompleto = txtNombreCompleto.Text,
                Cedula = txtCedula.Text,
                Edad = int.Parse(txtEdad.Text),
                IdGenero = valorAlmacenado,
                Telefono = int.Parse(txtTelefono.Text),
                Ocupacion = txtOcupacion.Text,
                Direccion = txtDireccion.Text,
                Antecedentes = txtAntecedentes.Text
            };

            return paciente;
        }

        private Paciente pacienteValores;

        private void btnGuardarCambiosPaciente_Click(object sender, RoutedEventArgs e)
        {

            //Valirdar campos vacios
            if (!ValidarCampos(out string mensajeError))
            {
                MensajeError(mensajeError);
                return;
            }
            //Intanciamos clase de capa negocio y creamos un objeto de ella
            PacienteCN pacientecn = new PacienteCN();

            //Obtenemos valores
            pacienteValores =  ObtenerValoresControles();

            try
            {
                //Validamos formatos de campos
                pacientecn.ValidacionCamposReglas(pacienteValores);

                // Mandamos datos y validamos existente
                bool CambioExitoso = pacientecn.ActualizarPaciente(pacienteValores, _paciente);

                if (CambioExitoso)
                {
                    btnGuardarCambiosPaciente.Visibility = Visibility.Collapsed;
                    btnCancelarEditar.Visibility = Visibility.Collapsed;
                    MostrarMensaje("¡Cambios Aplicado!", Colors.Green, 1, Colors.LightGreen);
                    borderInformacionPersonal.BorderBrush = (Brush)new SolidColorBrush(Colors.Green);
                    DesahabilitarCampos();
                    LimpiarCampos();
                }
                else
                {
                    MensajeError("Error inserccion");
                }
            }
            catch (ArgumentException ex)
            {
                String MensajeCath = ex.Message;
                MensajeError(MensajeCath);
              
            }
        }


        public void MostrarMensaje(string texto, Color color, int duracionSegundos, Color Background)
        {
            TextMensajeInformacion.Text = texto;
            TextMensajeInformacion.Foreground = new SolidColorBrush(color);
            TextMensajeInformacion.Background = new SolidColorBrush(Background);
            TextMensajeError.Visibility = Visibility.Collapsed;


            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(duracionSegundos);
            timer.Tick += (sender, args) =>
            {
                TextMensajeInformacion.Text = "";
                TextMensajeInformacion.Visibility = Visibility.Collapsed;
                btnEditarPaciente.Visibility = Visibility.Visible;
                btnEliminarPaciente.Visibility = Visibility.Visible;
                btnEnviarMensajePaciente.Visibility = Visibility;
                borderInformacionPersonal.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#d4d5d6");
                MostrarDatosPaciente(pacienteValores.Cedula);
                timer.Stop();
            };
            timer.Start();
        }

        private void btnEnviarMensajePaciente_Click(object sender, RoutedEventArgs e)
        {
            VentanaMensajeSms ventanamensaje = new VentanaMensajeSms(_paciente);
            ventanamensaje.ShowDialog();
        }


        //Metodo para validar capos
        private bool ValidarCampos(out string mensajeError)
        {
            mensajeError = string.Empty;

            if (string.IsNullOrEmpty(txtNombreCompleto.Text))
            {
                mensajeError = "¡Nombre!";
                return false;
            }

            if (string.IsNullOrEmpty(txtCedula.Text))
            {
                mensajeError = "¡Cedula!";
                return false;
            }

            if (string.IsNullOrEmpty(txtEdad.Text))
            {
                mensajeError = "¡Edad!";
                return false;
            }

            if (!txtEdad.Text.All(char.IsDigit))
            {
                mensajeError = "¡Edad Invalida!";
                return false;
            }

            if (GeneroComboBox.SelectedItem == null)
            {
                mensajeError = "¡Genero!";
                return false;
            }

            if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                mensajeError = "¡Dirección!";
                return false;
            }

            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                mensajeError = "¡Telefono!";
                return false;
            }

            if (!txtTelefono.Text.All(char.IsDigit))
            {
                mensajeError = "¡Numero Invalido!";
                return false;
            }

            if (string.IsNullOrEmpty(txtOcupacion.Text))
            {
                mensajeError = "¡Ocupación!";
                return false;
            }

            if (string.IsNullOrEmpty(txtAntecedentes.Text))
            {
                mensajeError = "¡Antecedentes!";
                return false;
            }

            return true;
        }

        private void MensajeError(string Mensaje)
        {
            TextMensajeError.Text = Mensaje;
            TextMensajeError.Visibility = Visibility.Visible;
            TextMensajeError.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FA465B"));
            TextMensajeError.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE2E0"));

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += (sender, args) =>
            {
                TextMensajeError.Visibility = Visibility.Collapsed;
                timer.Stop();
            };
            timer.Start();
        }

        private void MensajeEditar(String Mensaje)
        {
            TextMensajeInformacion.Visibility = Visibility.Visible;
            TextMensajeInformacion.Text = Mensaje;
            TextMensajeInformacion.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C58D1C"));
            TextMensajeInformacion.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FEDB9B"));
        }

        private void btnCancelarEditar_Click(object sender, RoutedEventArgs e)
        {
            CancelarEditar();
        }

        private void CancelarEditar()
        {
            btnCancelarEditar.Visibility = Visibility.Collapsed;
            btnGuardarCambiosPaciente.Visibility = Visibility.Collapsed;
            TextMensajeInformacion.Visibility = Visibility.Collapsed;
            btnEditarPaciente.Visibility = Visibility.Visible;
            btnEliminarPaciente.Visibility = Visibility.Visible;
            btnEnviarMensajePaciente.Visibility = Visibility;
            borderInformacionPersonal.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#d4d5d6");
            DesahabilitarCampos();
            LimpiarCampos();
            MostrarDatosPaciente(_paciente.Cedula);
        }
    }
}
