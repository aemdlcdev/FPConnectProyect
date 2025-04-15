using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using FPConnect.domain;
using FPConnect.HelperClasses;
using FPConnect.view.UserControls.Eventos;

namespace FPConnect.view.UserControls.Eventos
{
    /// <summary>
    /// Lógica de interacción para EventosGridControl.xaml
    /// </summary>
    public partial class EventosGridControl : Page
    {
        private int currentYear;
        private EventosProfesores ep;
        private bool isProcessingEvent = false;

        // Variable global para la fecha seleccionada
        private DateTime fechaSeleccionada;

        private ObservableCollection<EventosProfesores> todosLosEventos;

        public EventosGridControl()
        {
            InitializeComponent();

            // Inicializar con la fecha actual
            fechaSeleccionada = DateTime.Now;
            currentYear = fechaSeleccionada.Year;

            tDayNumber.Text = fechaSeleccionada.Day.ToString();
            tDay.Text = fechaSeleccionada.ToString("dddd", new System.Globalization.CultureInfo("es-ES"));
            tMonth.Text = fechaSeleccionada.ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));
            GetMesActualConLetras();

            // Aplicar estilo al botón del mes actual
            int mesActual = fechaSeleccionada.Month;
            foreach (var child in mesesStackArriba.Children)
            {
                if (child is Button mesButton && int.Parse(mesButton.Content.ToString()) == mesActual)
                {
                    mesButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c73f69"));
                    mesButton.FontWeight = FontWeights.SemiBold;
                }
            }

            ep = new EventosProfesores();
            CargarEventos();

        }

        private void CargarEventos()
        {
            // Cargar todos los eventos del profesor actual
            todosLosEventos = ep.LeerEventosPorIdProfesor(SesionUsuario.id_profesor);

            // Mostrar eventos para la fecha actual
            MostrarEventosParaFechaSeleccionada();
        }

        private void MostrarEventosParaFechaSeleccionada()
        {
            // Limpiar eventos actuales
            containerNotas.Children.Clear();

            // Filtrar eventos para la fecha seleccionada
            var eventosDia = todosLosEventos.Where(e =>
                e.fecha.Year == fechaSeleccionada.Year &&
                e.fecha.Month == fechaSeleccionada.Month &&
                e.fecha.Day == fechaSeleccionada.Day).ToList();

            // Mostrar los eventos filtrados
            foreach (var evento in eventosDia)
            {
                var icono = evento.id_estado == 1 ? FontAwesome.WPF.FontAwesomeIcon.CheckCircle : FontAwesome.WPF.FontAwesomeIcon.CircleThin;

                var newItem = new Item
                {
                    Title = evento.nombre,
                    Time = evento.hora,
                    EventDate = evento.fecha,
                    IdEvento = evento.id_evento, // Asignar ID
                    Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1")),
                    Icon = icono,
                    IconBell = FontAwesome.WPF.FontAwesomeIcon.Bell
                };

                // Suscribirse a los eventos
                newItem.EventoCompletado -= Item_EventoCompletado; // Quitar cualquier suscripción previa
                newItem.EventoCompletado += Item_EventoCompletado;

                newItem.EventoBorrado -= Item_EventoBorrado; // Quitar cualquier suscripción previa
                newItem.EventoBorrado += Item_EventoBorrado;
                Console.WriteLine($"Añadiendo Item con ID: {evento.id_evento}");
                // Agregar el nuevo item al StackPanel
                containerNotas.Children.Add(newItem);
            }
        }

        private void Item_EventoCompletado(object sender, int idEvento)
        {
            if (isProcessingEvent)
            {
                Console.WriteLine("Ya estamos procesando un evento. Ignorando solicitud adicional.");
                return;
            }

            isProcessingEvent = true;
            Console.WriteLine($"Comenzando a procesar evento completado para ID: {idEvento}");

            try
            {
                // Buscar el evento en la colección
                var evento = todosLosEventos.FirstOrDefault(e => e.id_evento == idEvento);
                if (evento != null)
                {
                    // Actualizar estado (1 = Completado)
                    evento.id_estado = 1;

                    // Actualizar en base de datos
                    ep.ModificarEvento(evento);

                    // Actualizar visualmente
                    if (sender is Item item)
                    {
                        item.Icon = FontAwesome.WPF.FontAwesomeIcon.CheckCircle;
                    }

                    MessageBox.Show("Evento marcado como completado", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            finally
            {
                Console.WriteLine($"Terminando procesamiento para ID: {idEvento}");
                isProcessingEvent = false;
            }
        }

        private void Item_EventoBorrado(object sender, int idEvento)
        {
            if (isProcessingEvent)
            {
                Console.WriteLine("Ya estamos procesando un evento. Ignorando solicitud adicional.");
                return;
            }

            isProcessingEvent = true;
            Console.WriteLine($"Comenzando a procesar evento completado para ID: {idEvento}");

            try
            {
                MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este evento?",
                                                       "Confirmar eliminación",
                                                       MessageBoxButton.YesNo,
                                                       MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Eliminar de la base de datos
                    ep.EliminarEvento(idEvento);

                    // Eliminar de la colección local
                    var eventoAEliminar = todosLosEventos.FirstOrDefault(e => e.id_evento == idEvento);
                    if (eventoAEliminar != null)
                    {
                        todosLosEventos.Remove(eventoAEliminar);
                    }

                    // Eliminar de la UI
                    if (sender is Item item)
                    {
                        containerNotas.Children.Remove(item);
                    }

                    MessageBox.Show("Evento eliminado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            finally
            {
                Console.WriteLine($"Terminando procesamiento para ID: {idEvento}");
                isProcessingEvent = false;
            }
        }


        // Método para obtener la fecha formateada como yyyy/MM/dd
        private string ObtenerFechaFormateada()
        {
            return fechaSeleccionada.ToString("yyyy/MM/dd");
        }

        private void MesButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                // Obtener el mes seleccionado del contenido del botón
                int mesSeleccionado = int.Parse(button.Content.ToString());

                // Verificar si el mes seleccionado es el mes actual
                DateTime nuevaFecha;
                if (mesSeleccionado == DateTime.Now.Month && fechaSeleccionada.Year == DateTime.Now.Year)
                {
                    // Si es el mes actual, establecer la fecha seleccionada en el día actual
                    nuevaFecha = new DateTime(fechaSeleccionada.Year, mesSeleccionado, DateTime.Now.Day);
                }
                else
                {
                    // Si no es el mes actual, o no es el año actual, mantener el día actual si es válido
                    int dia = Math.Min(fechaSeleccionada.Day, DateTime.DaysInMonth(fechaSeleccionada.Year, mesSeleccionado));
                    nuevaFecha = new DateTime(fechaSeleccionada.Year, mesSeleccionado, dia);
                }

                // Actualizar la variable global
                fechaSeleccionada = nuevaFecha;

                // Actualizar el calendario al mes seleccionado
                calendario.DisplayDate = fechaSeleccionada;
                calendario.SelectedDate = fechaSeleccionada;

                // Actualizar el TextBlock lblMes con el nombre del mes correspondiente
                lblMes.Text = fechaSeleccionada.ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));

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
                tDayNumber.Text = fechaSeleccionada.Day.ToString();
                tDay.Text = fechaSeleccionada.ToString("dddd", new System.Globalization.CultureInfo("es-ES"));
                tMonth.Text = fechaSeleccionada.ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));

                MostrarEventosParaFechaSeleccionada();

            }
        }

        private void calendario_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (calendario.SelectedDate.HasValue)
            {
                // Actualizar la variable global con la fecha seleccionada
                fechaSeleccionada = calendario.SelectedDate.Value;

                tDayNumber.Text = fechaSeleccionada.Day.ToString();
                tDay.Text = fechaSeleccionada.ToString("dddd", new System.Globalization.CultureInfo("es-ES"));
                tMonth.Text = fechaSeleccionada.ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));

                MostrarEventosParaFechaSeleccionada();

            }
        }

        private void calendario_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            // Actualizar la variable global con la fecha mostrada
            DateTime displayDate = calendario.DisplayDate;
            fechaSeleccionada = displayDate;

            tDayNumber.Text = displayDate.Day.ToString();
            tDay.Text = displayDate.ToString("dddd", new System.Globalization.CultureInfo("es-ES"));
            tMonth.Text = displayDate.ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));

            MostrarEventosParaFechaSeleccionada();

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
            // Actualizar la variable global con el nuevo año
            int mes = fechaSeleccionada.Month;
            int dia = Math.Min(fechaSeleccionada.Day, DateTime.DaysInMonth(currentYear, mes));
            fechaSeleccionada = new DateTime(currentYear, mes, dia);

            calendario.DisplayDate = fechaSeleccionada;
            calendario.SelectedDate = fechaSeleccionada;
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
            string mes = fechaSeleccionada.ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));
            mes = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mes.ToLower()).ToLower();

            lblMes.Text = mes;
        }

        private void btnAddNote_Click(object sender, RoutedEventArgs e)
        {
            string hora = string.IsNullOrEmpty(txtTime.Text) ? "00:00" : txtTime.Text;

            // Crear nuevo evento
            EventosProfesores nuevoEvento = new EventosProfesores(
                SesionUsuario.id_profesor,
                txtNota.Text,
                fechaSeleccionada,
                hora,
                2  // Estado por defecto (por hacer)
            );

            try
            {
                // Insertar y obtener ID
                int nuevoId = ep.InsertarEvento(nuevoEvento);

                // Actualizar el ID del evento
                nuevoEvento.id_evento = nuevoId;

                // Añadir a la colección
                todosLosEventos.Add(nuevoEvento);

                // Crear item visual con ID correcto
                var newItem = new Item
                {
                    Title = txtNota.Text,
                    Time = txtTime.Text,
                    EventDate = fechaSeleccionada,
                    IdEvento = nuevoId,
                    Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1")),
                    Icon = FontAwesome.WPF.FontAwesomeIcon.CircleThin,
                    IconBell = FontAwesome.WPF.FontAwesomeIcon.Bell
                };

                // Suscribir eventos
                newItem.EventoCompletado += Item_EventoCompletado;
                newItem.EventoBorrado += Item_EventoBorrado;

                containerNotas.Children.Add(newItem);
                txtNota.Text = string.Empty;
                txtTime.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear evento: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
