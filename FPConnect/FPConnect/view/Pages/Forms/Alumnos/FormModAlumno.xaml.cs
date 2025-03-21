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
using System.Windows.Shapes;

namespace FPConnect.view.Pages.Forms.Alumnos
{
    /// <summary>
    /// Lógica de interacción para FormModAlumno.xaml
    /// </summary>
    public partial class FormModAlumno : Window
    {

        public string Nombre { get; set; }
        public string Apellido{ get; set; }
        public string Correo { get; set; }
        public bool Guardado { get; set; }

        public FormModAlumno()
        {
            InitializeComponent();
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            Nombre = txtNombre.Text;            
            Apellido = txtApellido.Text;
            Correo = txtCorreo.Text;

            Guardado = true;
            this.DialogResult = true; // Cierra la ventana modal
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
