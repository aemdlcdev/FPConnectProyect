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

namespace FPConnect.view.Pages.Forms.Empresas
{
    /// <summary>
    /// Lógica de interacción para FormAddEmpresa.xaml
    /// </summary>
    public partial class FormAddEmpresa : Window
    {
        public FormAddEmpresa()
        {
            InitializeComponent();
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

            // Si todas las validaciones pasan, cerrar el diálogo y establecer DialogResult = true
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

        // Evitamos el pegado de contenido no numérico en el campo de año
        private void txtAnio_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste)
            {
                string clipboardText = Clipboard.GetText();
                if (!Regex.IsMatch(clipboardText, @"^\d+$") || clipboardText.Length > 4)
                {
                    e.Handled = true;
                }
            }
        }

        // Limitoo la longitud del campo de año a 4 dígitos
        private void txtAnio_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAnioInicio.Text.Length > 4 || txtAnioFin.Text.Length > 4)
            {
                txtAnioInicio.Text = txtAnioInicio.Text.Substring(0, 4);
                txtAnioInicio.CaretIndex = 4; // Coloco el cursor al final

                txtAnioFin.Text = txtAnioFin.Text.Substring(0, 4);
                txtAnioFin.CaretIndex = 4; // Coloco el cursor al final
            }
        }

    }
}
