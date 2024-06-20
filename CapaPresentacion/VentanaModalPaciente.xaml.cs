using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Paginas;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para VentanaModalPaciente.xaml
    /// </summary>
    public partial class VentanaModalPaciente : Window
    {
        private PacienteCN pacientenegocio = new PacienteCN();
        private Pag01 _pag01;
        public VentanaModalPaciente(Pag01 pag)
        {

            InitializeComponent();
            _pag01 = pag;
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

        private void btnGuardarPaciente_Click(object sender, RoutedEventArgs e)
        {
            //Valirdar campos vacios
            if (!ValidarCampos(out string mensajeError))
            {
                MensajeError(mensajeError);
                return;
            }

            //Intanciamos clase de capa negocio y creamos un objeto de ella
            PacienteCN pacientecn = new PacienteCN();

            // Si sale todo bien el proceso continuará
            string generoSeleccionado = ((ComboBoxItem)GeneroComboBox.SelectedItem).Content.ToString();
            int valorAlmacenado = generoSeleccionado == "Masculino" ? 1 : 2;

            // Validamos reglas de campos

            Paciente paciente = new Paciente
            {
                NombreCompleto = txtNombreCompleto.Text,
                Cedula = txtCedula.Text,
                Edad = int.Parse(txtEdad.Text),
                IdGenero = valorAlmacenado,
                Telefono = int.Parse(txtTelefono.Text),
                Ocupacion = txtOcupacion.Text,
                Direccion = txtDireccion.Text,
                Antecedentes = txtAntecedentes.Text
            };


            try
            {
                pacientecn.ValidacionCamposReglas(paciente);

                // Mandamos los datos y se validará existencia de este usuario
                bool registroExitoso = pacientecn.RegistrarPaciente(paciente);

                // Verificamos si hubo error al registrar el paciente
                if (registroExitoso)
                {
                    LimpiarCampos();
                    textMensaje.Text = "¡Registro Exitoso!";
                    textMensaje.Foreground = Brushes.Green;
                    _pag01.ObtenerPacientes();
                    textMensaje.Visibility = Visibility.Collapsed;
                    _pag01.ObtenerPacienteCompleto(paciente);
                    this.Close();

                }
                else
                {
                    textMensaje.Text = "¡Error de insercción!";                 
                }
            }
            catch (ArgumentException ex)
            {
                string Mensaje = ex.Message;
                MensajeError(Mensaje);
            }
        }


        private void MensajeError(string Mensaje)
        {
            textMensaje.Visibility = Visibility.Visible;
            textMensaje.Text = Mensaje;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += (sender, args) =>
            {
                textMensaje.Visibility = Visibility.Collapsed;
                timer.Stop();
            };
            timer.Start();
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


    }
}
