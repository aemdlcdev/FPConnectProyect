using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FPConnect.HelperClasses;
using FPConnect.view.Pages.Forms.Empresas;

namespace FPConnect.view.Pages
{
    /// <summary>
    /// Lógica de interacción para Empresas.xaml
    /// </summary>
    public partial class Empresas : Page
    {
        public Empresas()
        {
            InitializeComponent();
            txtHola.Text = "Hola " + SesionUsuario.NombreUsuario;
        }

        private void btnAddEmpresa_Click(object sender, RoutedEventArgs e)
        {
            FormAddEmpresa formAddEmpresa = new FormAddEmpresa();
            formAddEmpresa.ShowDialog();
            if (formAddEmpresa.DialogResult == true)
            {
                
                MessageBox.Show("Empresa agregada correctamente.");
            }
        }
    }
}
