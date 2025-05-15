using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Input;
using FPConnect.domain;
using FPConnect.HelperClasses;
using FPConnect.persistence.Manages;

namespace FPConnect.view
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        
        private bool enviado = false;
        public Login()
        {
            InitializeComponent();
            
            
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

            string email = txtUsername.Text;
            string password = txtPassword.Password;

            Profesor usuario = new Profesor();
            usuario = usuario.autentificarUsuario(email, password);

            if (usuario!=null)
            {
                SesionUsuario.NombreUsuario = usuario.nombre;
                SesionUsuario.id_profesor = usuario.id_profesor;
                SesionUsuario.sexo = usuario.sexo;
                SesionUsuario.IdCentro = usuario.id_centro;
                SesionUsuario.IdFamilia = usuario.id_familia;
                SesionUsuario.IdGrado = usuario.id_grado;
                MainWindow mainWindow = new MainWindow(usuario);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Email o contraseña incorrectos!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void txtForgotPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var apiUrl = "https://localhost:7264/api/user";

            if (enviado) 
            {
                txtForgotPassword.Text = "Ya se ha enviado el correo electrónico";
            }
            else 
            {
                // Llamar a la API para solicitar el restablecimiento de la contraseña
                var resetPasswordRequest = new ResetPasswordRequest
                {
                    Email = txtUsername.Text,
                };
                try
                {
                    HttpClient client = new HttpClient();
                    HttpResponseMessage resetPasswordResponse = await client.PostAsJsonAsync($"{apiUrl}/reset-password", resetPasswordRequest);

                    if (resetPasswordResponse.IsSuccessStatusCode)
                    {
                        txtForgotPassword.Text = "Revisa el buzón de tu email";
                        enviado = true;
                    }
                    else
                    {
                        enviado = false;
                        txtForgotPassword.Text = "Email no encontrado";
                    }
                }
                catch (System.Net.Http.HttpRequestException ex) // Si la api no esta funcionando notificará un error interno
                {
                    txtForgotPassword.Text="Error interno, reinténtalo más tarde";
                }
                
            }        
        }
        public class ResetPasswordRequest
        {
            public string Email { get; set; }
        }

    }
}
