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
            string MensajeNombre = "¡Nombre!";
            string MensajeCedula = "¡Cedula!";
            string MensajeEdad = "¡Edad!";
            string MensajeGenero = "¡Genero!";
            string MensajeDireccion = "¡Dirección!";
            string MensajeTelefono = "¡Telefono!";
            string MensajeTelefono1 = "¡Numero Invalido!";
            string MensajeEmail = "¡Gmail!";
            string MensajeOcupacion = "¡Ocupación!";

            if (!string.IsNullOrEmpty(txtNombreCompleto.Text))
            {
                if (!string.IsNullOrEmpty(txtCedula.Text))
                {
                    if (!string.IsNullOrEmpty(txtEdad.Text))
                    {
                        if (cmbGenero.SelectedItem != null)
                        {
                            if (!string.IsNullOrEmpty(txtDireccion.Text))
                            {
                                if (!string.IsNullOrEmpty(txtTelefono.Text))
                                {
                                    if (txtTelefono.Text.All(char.IsDigit))
                                    {
                                        if (!string.IsNullOrEmpty(txtGmail.Text))
                                        {
                                            if (!string.IsNullOrEmpty(txtOcupacion.Text))
                                            {

                                                PacienteCN pacientecn = new PacienteCN();

                                                //Si sale todo bien el proceso continuara
                                                string generoSeleccionado = cmbGenero.SelectedValue.ToString();
                                                int valorAlmacenado;

                                                if (generoSeleccionado == "Masculino")
                                                {
                                                    valorAlmacenado = 1;
                                                }
                                                else
                                                {
                                                    valorAlmacenado = 2;
                                                }

                                                Paciente paciente = new Paciente();
                                                paciente.NombreCompleto = txtNombreCompleto.Text;
                                                paciente.Cedula = txtCedula.Text;
                                                paciente.Edad = int.Parse(txtEdad.Text);
                                                paciente.IdGenero = valorAlmacenado;
                                                paciente.Direccino = txtDireccion.Text;
                                                paciente.Telefono = int.Parse(txtTelefono.Text);
                                                paciente.Gmail = txtGmail.Text;
                                                paciente.Ocupacion = txtOcupacion.Text;

                                                try
                                                {
                                                    //Validamos reglas de campos
                                                    pacientecn.ValidacionCamposReglas(paciente);

                                                    //Mandamos los datos y se validara existencia de este usuario
                                                    bool registroExitoso = pacientecn.RegistrarPaciente(paciente);

                                                    //Verificamos si hubo error al registrar el paciente
                                                    if (registroExitoso)
                                                    {
                                                        LimpiarCampos();
                                                        EtiquetaExitoso.Text = "¡Registro Exitoso!";
                                                        EtiquetaExitoso.Foreground = Brushes.Red;
                                                        _pag01.ObtenerPacientes();
                                                        MostrarMensaje("¡Registro Exitoso!", Colors.Green, 1);
                                                    }
                                                    else
                                                    {
                                                        MostrarMensaje("¡Error al registrar paciente!", Colors.Red, 3);
                                                    }
                                                }
                                                catch (ArgumentException ex)
                                                {

                                                    textMensaje.Text = ex.Message;
                                                    textMensaje.Foreground = new SolidColorBrush(Colors.Red);
                                                    return; // Sale del método si el gmail no es válido
                                                }
                                            }
                                            else
                                            {
                                                textMensaje.Text = MensajeOcupacion;
                                                textMensaje.Foreground = new SolidColorBrush(Colors.Red);
                                                return; // Sale del método si el gmail no es válido
                                            }

                                        }
                                        else
                                        {
                                            textMensaje.Text = MensajeEmail;
                                            textMensaje.Foreground = new SolidColorBrush(Colors.OrangeRed);
                                            return;

                                        }
                                    }
                                    else
                                    {
                                        textMensaje.Text = MensajeTelefono1;
                                        textMensaje.Foreground = new SolidColorBrush(Colors.OrangeRed);
                                        return;
                                    }
                                }
                                else
                                {
                                    textMensaje.Text = MensajeTelefono;
                                    textMensaje.Foreground = new SolidColorBrush(Colors.OrangeRed);
                                    return;
                                }
                            }
                            else
                            {
                                textMensaje.Text = MensajeDireccion;
                                textMensaje.Foreground = new SolidColorBrush(Colors.OrangeRed);
                                return;
                            }
                        }
                        else
                        {
                            textMensaje.Text = MensajeGenero;
                            textMensaje.Foreground = new SolidColorBrush(Colors.OrangeRed);
                            return;
                        }
                    }

                    else
                    {
                        textMensaje.Text = MensajeEdad;
                        textMensaje.Foreground = new SolidColorBrush(Colors.OrangeRed);
                        return;
                    }
                }

                else
                {
                    textMensaje.Text = MensajeCedula;
                    textMensaje.Foreground = new SolidColorBrush(Colors.OrangeRed);
                    return;
                }
            }
            else
            {
                textMensaje.Text = MensajeNombre;
                textMensaje.Foreground = new SolidColorBrush(Colors.OrangeRed);
                return;
            }

            EvaluarCamposCompletos();
        }

        public void MostrarMensaje(string texto, Color color, int duracionSegundos)
        {
            EtiquetaExitoso.Text = texto;
            EtiquetaExitoso.Foreground = new SolidColorBrush(color);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(duracionSegundos);
            timer.Tick += (sender, args) =>
            {
                EtiquetaExitoso.Text = "";
                timer.Stop();
                this.Close();
            };
            timer.Start();
        }

        private void LimpiarCampos()
        {
            txtNombreCompleto.Clear();
            txtEdad.Clear();
            cmbGenero.SelectedIndex = -1;
            txtDireccion.Clear();
            txtCedula.Clear();
            txtTelefono.Clear();
            txtGmail.Clear();
            txtOcupacion.Clear();
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
