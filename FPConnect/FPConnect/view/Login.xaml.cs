using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using FPConnect.domain;
using FPConnect.persistence.Manages;

namespace FPConnect.view
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private ObservableCollection<Usuario> users ;
        private UsuarioManage usuarioManage;
        public Login()
        {
            InitializeComponent();
            usuarioManage = new UsuarioManage();
            users = usuarioManage.LeerUsuarios();
            foreach (Usuario usuario in users) 
            {
                Console.WriteLine(usuario.idUsuario + " " + usuario.email + " " + usuario.password);
            }
            
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text == "admin" && txtPassword.Password == "admin")
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtForgotPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtForgotPassword.Text = "Revisa la bandeja de tu email.";
        }
    }
}
