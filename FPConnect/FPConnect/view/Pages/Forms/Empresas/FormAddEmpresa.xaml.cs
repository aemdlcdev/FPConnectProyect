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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FPConnect.domain;
using FPConnect.HelperClasses;

namespace FPConnect.view.Pages.Forms.Empresas
{
    /// <summary>
    /// Lógica de interacción para FormAddEmpresa.xaml
    /// </summary>
    public partial class FormAddEmpresa : Window
    {
        private Empresa nuevaEmpresa;
        private int tipoOperacion; // 1 add, 2 mod
        public FormAddEmpresa()
        {
            InitializeComponent();
        }

        public FormAddEmpresa(Empresa empresa, int tipo) // 1 add, 2 mod
        {
            InitializeComponent();
            nuevaEmpresa = empresa;
            tipoOperacion = tipo;
            if (tipoOperacion == 2)
            { 
                txtTitulo.Text = "Modificar Empresa";
                txtNombre.Text = nuevaEmpresa.nombre;
                txtEmail.Text = nuevaEmpresa.email;
                txtTlfno.Text = nuevaEmpresa.telefono;
                txtAnioInicio.Text = nuevaEmpresa.anio_inicio_acuerdo.ToString();
                txtAnioFin.Text = nuevaEmpresa.anio_fin_acuerdo.ToString(); // Si no tiene hacemos lo siguiente
                if (nuevaEmpresa.anio_fin_acuerdo == 0)
                {
                    txtAnioFin.Text = string.Empty; // Limpiamos el campo
                }
            }
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            // Valido que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtTlfno.Text) ||
                string.IsNullOrWhiteSpace(txtAnioInicio.Text)
            )
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Valido que el año tenga exactamente 4 dígitos y sea un número entero
            if (!Regex.IsMatch(txtAnioInicio.Text, @"^\d{4}$"))
            {
                MessageBox.Show("El año debe contener exactamente 4 dígitos numéricos (Ejemplo: 2025).", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Intento convertir el año a entero para verificar que sea un número válido
            if (!int.TryParse(txtAnioInicio.Text, out int anio))
            {
                MessageBox.Show("El año debe ser un número entero válido.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Esto debe de ser opcional
            if(!string.IsNullOrWhiteSpace(txtAnioFin.Text))
            {

                if (!Regex.IsMatch(txtAnioFin.Text, @"^\d{4}$"))
                {
                    MessageBox.Show("El año debe contener exactamente 4 dígitos numéricos (Ejemplo: 2025).", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (!int.TryParse(txtAnioFin.Text, out int anioFin))
                {
                    MessageBox.Show("El año de finalización debe ser un número entero válido.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }

            if (nuevaEmpresa.id_centro <= 0)
            {
                nuevaEmpresa.id_centro = SesionUsuario.IdCentro;
            }

            // Establecer los valores directamente
            nuevaEmpresa.nombre = txtNombre.Text;
            nuevaEmpresa.email = txtEmail.Text;
            nuevaEmpresa.telefono = txtTlfno.Text;
            nuevaEmpresa.anio_inicio_acuerdo = int.Parse(txtAnioInicio.Text);
            nuevaEmpresa.anio_fin_acuerdo = string.IsNullOrWhiteSpace(txtAnioFin.Text) ? 0 : int.Parse(txtAnioFin.Text);
            nuevaEmpresa.estado = 1; // Activa por defecto           

            // Registrar datos para depuración
            Console.WriteLine($"Datos de empresa a guardar: Nombre={nuevaEmpresa.nombre}, Email={nuevaEmpresa.email}");

            this.DialogResult = true;

        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        // Evento para restringir la entrada solo a dígitos en el campo de año
        private void txtAnio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Solo permitir caracteres numéricos
            e.Handled = !char.IsDigit(e.Text[0]);
        }
    }
}
