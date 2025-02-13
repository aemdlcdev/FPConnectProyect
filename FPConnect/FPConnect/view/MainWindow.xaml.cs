using System;
using System.Windows;
using System.Windows.Input;
using FPConnect.view;
using FPConnect.view.UserControls; // Asegúrate de que este espacio de nombres esté importado

namespace FPConnect
{
    public partial class MainWindow : Window
    {
        public bool IsInicioButtonPressed { get; set; }
        public bool IsAlumnosButtonPressed { get; set; }
        public bool IsEventosButtonPressed { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            alumnosFrame.Visibility = Visibility.Collapsed;
            eventosFrame.Visibility = Visibility.Collapsed;

            // Mostrar el Frame de Inicio
            inicioFrame.Visibility = Visibility.Visible;
            inicioFrame.Source = new Uri("pages/InicioGridControl.xaml", UriKind.Relative);

            IsInicioButtonPressed = true;
            IsAlumnosButtonPressed = false;
            IsEventosButtonPressed = false;
            UpdateButtonStyles();
        }

        private bool IsMaximize = false;

        /// <summary>
        /// Maneja el evento MouseLeftButtonDown en el borde de la ventana.
        /// Permite maximizar o restaurar la ventana al hacer doble clic en el borde.
        /// </summary>
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }

        /// <summary>
        /// Maneja el evento MouseDown en el borde de la ventana.
        /// Permite arrastrar la ventana cuando se presiona el botón izquierdo del mouse.
        /// </summary>
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        /// <summary>
        /// Maneja el evento Click del botón btnAlumnos.
        /// Oculta los otros frames y muestra el frame de Alumnos.
        /// </summary>
        private void btnAlumnos_Click(object sender, RoutedEventArgs e)
        {
            // Ocultar otros Frames
            inicioFrame.Visibility = Visibility.Collapsed;
            eventosFrame.Visibility = Visibility.Collapsed;

            // Mostrar el Frame de Alumnos
            alumnosFrame.Visibility = Visibility.Visible;
            alumnosFrame.Source = new Uri("pages/AlumnosGridControl.xaml", UriKind.Relative);

            IsInicioButtonPressed = false;
            IsAlumnosButtonPressed = true;
            IsEventosButtonPressed = false;
            UpdateButtonStyles();
        }

        /// <summary>
        /// Maneja el evento Click del botón btnInicio.
        /// Oculta los otros frames y muestra el frame de Inicio.
        /// </summary>
        private void btnInicio_Click(object sender, RoutedEventArgs e)
        {
            // Ocultar otros Frames
            alumnosFrame.Visibility = Visibility.Collapsed;
            eventosFrame.Visibility = Visibility.Collapsed;

            // Mostrar el Frame de Inicio
            inicioFrame.Visibility = Visibility.Visible;
            inicioFrame.Source = new Uri("pages/InicioGridControl.xaml", UriKind.Relative);

            IsInicioButtonPressed = true;
            IsAlumnosButtonPressed = false;
            IsEventosButtonPressed = false;
            UpdateButtonStyles();
        }

        /// <summary>
        /// Maneja el evento Click del botón btnEventos.
        /// Oculta los otros frames y muestra el frame de Eventos.
        /// </summary>
        private void btnEventos_Click(object sender, RoutedEventArgs e)
        {
            // Ocultar otros Frames
            alumnosFrame.Visibility = Visibility.Collapsed;
            inicioFrame.Visibility = Visibility.Collapsed;

            // Mostrar el Frame de Eventos
            eventosFrame.Visibility = Visibility.Visible;
            eventosFrame.Source = new Uri("pages/EventosGridControl.xaml", UriKind.Relative);

            IsInicioButtonPressed = false;
            IsAlumnosButtonPressed = false;
            IsEventosButtonPressed = true;
            UpdateButtonStyles();
        }

        /// <summary>
        /// Actualiza los estilos de los botones según el estado de los botones presionados.
        /// </summary>
        private void UpdateButtonStyles()
        {
            btnInicio.Style = (Style)FindResource(IsInicioButtonPressed ? "menuButtonPressed" : "menuButton");
            btnAlumnos.Style = (Style)FindResource(IsAlumnosButtonPressed ? "menuButtonPressed" : "menuButton");
            btnEventos.Style = (Style)FindResource(IsEventosButtonPressed ? "menuButtonPressed" : "menuButton");
        }

        /// <summary>
        /// Maneja el evento Click del botón btnLogout.
        /// Muestra la ventana de Login y cierra la ventana actual.
        /// </summary>
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        /// <summary>
        /// Maneja el evento Click del botón btnExit.
        /// Cierra la aplicación.
        /// </summary>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
