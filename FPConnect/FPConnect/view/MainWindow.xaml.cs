using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using FPConnect.domain;
using FPConnect.HelperClasses;
using FPConnect.view;
using FPConnect.view.UserControls; // Asegúrate de que este espacio de nombres esté importado

namespace FPConnect
{
    public partial class MainWindow : Window
    {
        public bool IsInicioButtonPressed { get; set; }
        public bool IsAlumnosButtonPressed { get; set; }
        public bool IsEventosButtonPressed { get; set; }
        public bool IsEmpresasButtonPressed { get; set; }
        public bool IsCoordinacionButtonPressed { get; set; }
        public bool IsGestionUsuariosButtonPressed { get; set; }

        private Profesor profesor { get; set; }

        public MainWindow(Profesor profesor)
        {
            InitializeComponent();
            
            this.profesor = profesor;
            //Console.WriteLine("Rol: " + this.profesor.id_rol + this.profesor.nombre);
            mainFrame.Source = new Uri("pages/InicioGridControl.xaml", UriKind.Relative);

            IsInicioButtonPressed = true;
            IsAlumnosButtonPressed = false;
            IsEventosButtonPressed = false;
            IsEmpresasButtonPressed = false;
            IsCoordinacionButtonPressed = false;
            IsGestionUsuariosButtonPressed = false;
            UpdateButtonStyles();

            Storyboard slideAnimacion = (Storyboard)FindResource("SlideInStoryboard");
            slideAnimacion.Begin();
            txtNombre.Text = SesionUsuario.NombreUsuario + ", España ";
            
            configurarUsuario();

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
                    this.Height = 750;

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
        /// Maneja el evento Click del botón btnInicio.
        /// Oculta los otros frames y muestra el frame de Inicio.
        /// </summary>
        private void btnInicio_Click(object sender, RoutedEventArgs e)
        {
            
            mainFrame.Source = new Uri("pages/InicioGridControl.xaml", UriKind.Relative);

            IsInicioButtonPressed = true;
            IsAlumnosButtonPressed = false;
            IsEventosButtonPressed = false;
            IsEmpresasButtonPressed = false;
            IsCoordinacionButtonPressed = false;
            IsGestionUsuariosButtonPressed = false;
            UpdateButtonStyles();
        }

        /// <summary>
        /// Maneja el evento Click del botón btnEventos.
        /// Oculta los otros frames y muestra el frame de Eventos.
        /// </summary>
        private void btnEventos_Click(object sender, RoutedEventArgs e)
        {
            
            mainFrame.Source = new Uri("pages/EventosGridControl.xaml", UriKind.Relative);

            IsInicioButtonPressed = false;
            IsAlumnosButtonPressed = false;
            IsGestionUsuariosButtonPressed = false;
            IsCoordinacionButtonPressed = false;
            IsEmpresasButtonPressed = false;
            IsEventosButtonPressed = true;
            UpdateButtonStyles();
        }

        /// <summary>
        /// Maneja el evento Click del botón btnAlumnos.
        /// Oculta los otros frames y muestra el frame de Alumnos.
        /// </summary>
        private void btnAlumnos_Click(object sender, RoutedEventArgs e)
        {
           
            mainFrame.Source = new Uri("pages/AlumnosGridControl.xaml", UriKind.Relative);

            IsInicioButtonPressed = false;
            IsAlumnosButtonPressed = true;
            IsEventosButtonPressed = false;
            IsCoordinacionButtonPressed = false;
            IsEmpresasButtonPressed = false;
            IsGestionUsuariosButtonPressed = false;
            UpdateButtonStyles();
        }

        private void btnEmpresas_Click(object sender, RoutedEventArgs e)
        {
            
            mainFrame.Source = new Uri("pages/Empresas.xaml", UriKind.Relative);

            IsInicioButtonPressed = false;
            IsAlumnosButtonPressed = false;
            IsEventosButtonPressed = false;
            IsEmpresasButtonPressed = true;
            IsCoordinacionButtonPressed = false;
            IsGestionUsuariosButtonPressed = false;
            UpdateButtonStyles();
        }

        private void btnCoordinacion_Click(object sender, RoutedEventArgs e)
        {
            
            mainFrame.Source = new Uri("pages/Coordinacion.xaml", UriKind.Relative);

            IsInicioButtonPressed = false;
            IsAlumnosButtonPressed = false;
            IsEventosButtonPressed = false;
            IsEmpresasButtonPressed = false;
            IsCoordinacionButtonPressed = true;
            IsGestionUsuariosButtonPressed = false;
            UpdateButtonStyles();
        }

        private void btnGestionUsuarios_Click(object sender, RoutedEventArgs e)
        {
            
            mainFrame.Source = new Uri("pages/GestionUsuarios.xaml", UriKind.Relative);

            IsInicioButtonPressed = false;
            IsEventosButtonPressed = false;
            IsAlumnosButtonPressed = false;
            IsEmpresasButtonPressed = false;
            IsCoordinacionButtonPressed = false;
            IsGestionUsuariosButtonPressed = true;
            UpdateButtonStyles();
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
            SesionUsuario.NombreUsuario = null;
            SesionUsuario.id_profesor = 0;
            SesionUsuario.sexo = null;
            SesionUsuario.IdCentro = 0;
            SesionUsuario.IdRol = 0;
        }

        /// <summary>
        /// Maneja el evento Click del botón btnExit.
        /// Cierra la aplicación.
        /// </summary>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Actualiza los estilos de los botones según el estado de los botones presionados.
        /// </summary>
        private void UpdateButtonStyles()
        {
            btnInicio.Style = (Style)FindResource(IsInicioButtonPressed ? "menuButtonPressed" : "menuButton");
            btnEventos.Style = (Style)FindResource(IsEventosButtonPressed ? "menuButtonPressed" : "menuButton");
            btnAlumnos.Style = (Style)FindResource(IsAlumnosButtonPressed ? "menuButtonPressed" : "menuButton");
            btnEmpresas.Style = (Style)FindResource(IsEmpresasButtonPressed ? "menuButtonPressed" : "menuButton");
            btnCoordinacion.Style = (Style)FindResource(IsCoordinacionButtonPressed ? "menuButtonPressed" : "menuButton");
            btnGestionUsuarios.Style = (Style)FindResource(IsGestionUsuariosButtonPressed ? "menuButtonPressed" : "menuButton");
        }

        // ...

        private void configurarUsuario()
        {
            if (SesionUsuario.sexo.Equals("m"))
            {
                imgSexo.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/male.png"));

            }
            else
            {
                imgSexo.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/female.png"));

            }

            if (this.profesor.id_rol == 2)
            {
                btnGestionFamilias.Visibility = Visibility.Collapsed;
                btnGestionFamilias.IsEnabled = false;
            }
            if (this.profesor.id_rol == 3)
            {
                btnGestionUsuarios.Visibility = Visibility.Collapsed;
                btnGestionUsuarios.IsEnabled = false;
                btnGestionFamilias.Visibility = Visibility.Collapsed;
                btnGestionFamilias.IsEnabled = false;
                txtSep1.Visibility = Visibility.Collapsed;
                txtSep2.Visibility = Visibility.Collapsed;
                txtAvanzado.Visibility = Visibility.Collapsed;
            }
        }

    }
}
