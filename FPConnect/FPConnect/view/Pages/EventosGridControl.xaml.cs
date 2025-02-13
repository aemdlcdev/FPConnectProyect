using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Calender.UserControls;

namespace FPConnect.view.UserControls
{
    /// <summary>
    /// Lógica de interacción para EventosGridControl.xaml
    /// </summary>
    public partial class EventosGridControl : Page
    {
        private int currentYear;
        public EventosGridControl()
        {
            InitializeComponent();
            currentYear = DateTime.Now.Year;
            tDayNumber.Text = DateTime.Now.Day.ToString();
            tDay.Text = DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("es-ES"));
            tMonth.Text = DateTime.Now.ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));
            GetMesActualConLetras();
        }


        private void MesButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                // Obtener el mes seleccionado del contenido del botón
                int mesSeleccionado = int.Parse(button.Content.ToString());

                // Obtener la fecha actual
                DateTime fechaActual = DateTime.Now;

                // Verificar si el mes seleccionado es el mes actual
                DateTime nuevaFecha;

                nuevaFecha = new DateTime(fechaActual.Year, mesSeleccionado, fechaActual.Day);

                if (mesSeleccionado == fechaActual.Month)
                {
                    // Si es el mes actual, establecer la fecha seleccionada en el día actual
                    
                }
                else
                {
                    // Si no es el mes actual, establecer la fecha seleccionada en el primer día del mes
                    nuevaFecha = new DateTime(fechaActual.Year, mesSeleccionado, 1);
                }

                // Actualizar el calendario al mes seleccionado
                calendario.DisplayDate = nuevaFecha;
                calendario.SelectedDate = nuevaFecha;

                // Actualizar el TextBlock lblMes con el nombre del mes correspondiente
                lblMes.Text = nuevaFecha.ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));

                // Actualizar el estilo de los botones para reflejar el mes seleccionado
                foreach (var child in ((StackPanel)button.Parent).Children)
                {
                    if (child is Button mesButton)
                    {
                        mesButton.Foreground = mesButton == button ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c73f69")) : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#bababa"));
                        mesButton.FontWeight = mesButton == button ? FontWeights.SemiBold : FontWeights.Normal;
                    }
                }

                // Actualizar las variables tDay y tMonth
                tDayNumber.Text = nuevaFecha.Day.ToString();
                tDay.Text = nuevaFecha.ToString("dddd", new System.Globalization.CultureInfo("es-ES"));
                tMonth.Text = nuevaFecha.ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));
            }
        }

        private void calendario_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (calendario.SelectedDate.HasValue)
            {
                DateTime selectedDate = calendario.SelectedDate.Value;
                tDayNumber.Text = selectedDate.Day.ToString();
                tDay.Text = selectedDate.ToString("dddd", new System.Globalization.CultureInfo("es-ES"));
                tMonth.Text = selectedDate.ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));
            }
        }


        private void btnAnioAnterior_Click(object sender, RoutedEventArgs e)
        {
            currentYear--;
            UpdateYearButtons();
            UpdateCalendarYear();
        }

        private void btnAnioSiguiente_Click(object sender, RoutedEventArgs e)
        {
            currentYear++;
            UpdateYearButtons();
            UpdateCalendarYear();
        }

        private void UpdateYearButtons()
        {
            btnAnio1.Content = (currentYear - 2).ToString();
            btnAnio2.Content = (currentYear - 1).ToString();
            btnAnio3.Content = currentYear.ToString();
            btnAnio4.Content = (currentYear + 1).ToString();
            btnAnio5.Content = (currentYear + 2).ToString();
        }

        private void UpdateCalendarYear()
        {
            DateTime fechaActual = DateTime.Now;
            DateTime nuevaFecha = new DateTime(currentYear, fechaActual.Month, 1);
            calendario.DisplayDate = nuevaFecha;
            calendario.SelectedDate = nuevaFecha;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void lblNota_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtNota.Focus();
        }

        private void txtNota_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNota.Text) && txtNota.Text.Length > 0)
            {
                lblNota.Visibility = Visibility.Collapsed;
            }
            else
            {
                lblNota.Visibility = Visibility.Visible;
            }
        }

        private void lblTime_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtTime.Focus();
        }

        private void txtTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTime.Text) && txtTime.Text.Length > 0)
            {
                lblTime.Visibility = Visibility.Collapsed;
            }
            else
            {
                lblTime.Visibility = Visibility.Visible;
            }
        }

        private void GetMesActualConLetras()
        {
            string mes = DateTime.Now.ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));
            mes = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mes.ToLower()).ToLower();

            lblMes.Text = mes;
        }

        private void btnAddNote_Click(object sender, RoutedEventArgs e)
        {
            // Crear un nuevo item
            var newItem = new Item
            {
                Title = txtNota.Text,
                Time = txtTime.Text,
                Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1")),
                Icon = FontAwesome.WPF.FontAwesomeIcon.CircleThin,
                IconBell = FontAwesome.WPF.FontAwesomeIcon.Bell
            };

            // Agregar el nuevo item al StackPanel
            derecha.Children.Add(newItem);
        }
    }
}
