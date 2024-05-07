﻿using System;
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

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para VentanaModalPaciente.xaml
    /// </summary>
    public partial class VentanaModalPaciente : Window
    {
        public VentanaModalPaciente()
        {
            InitializeComponent();
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
            string MensajeEmail1 = "¡Gmail invalido!";
            string MensajeOcupacion = "¡Ocupación!";
            string gmailPattern = @"\b[A-Za-z0-9._%+-]+@gmail\.com\b";

            if (!string.IsNullOrEmpty(txtNombreCompleto.Text))
            {
                if (!string.IsNullOrEmpty(txtCedula.Text))
                {
                    if (!string.IsNullOrEmpty(txtEdad.Text))
                    {
                        if (int.Parse(txtEdad.Text) <= 99)
                        {
                            if (cmbGenero.SelectedItem != null)
                            {
                                if (!string.IsNullOrEmpty(txtDireccion.Text))
                                {
                                    if (!string.IsNullOrEmpty(txtTelefono.Text))
                                    {
                                        if (txtTelefono.Text.Length == 8 && txtTelefono.Text.All(char.IsDigit))
                                        {
                                            if (!string.IsNullOrEmpty(txtGmail.Text))
                                            {
                                                if (!Regex.IsMatch(txtGmail.Text, gmailPattern))
                                                {
                                                    textMensaje.Text = MensajeEmail1;
                                                    textMensaje.Foreground = new SolidColorBrush(Colors.Red);
                                                    return; // Sale del método si el gmail no es válido
                                                }
                                                else
                                                {
                                                    if (!string.IsNullOrEmpty(txtOcupacion.Text))
                                                    {
                                                        //Continuara
                                                    }
                                                    else
                                                    {
                                                        textMensaje.Text = MensajeOcupacion;
                                                        textMensaje.Foreground = new SolidColorBrush(Colors.Red);
                                                        return; // Sale del método si el gmail no es válido
                                                    }
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
