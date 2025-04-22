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

namespace FPConnect.view.Pages.Forms
{
    /// <summary>
    /// Lógica de interacción para FormAddAlumno.xaml
    /// </summary>
    public partial class FormAddAlumno : Window
    {
        public string Nombre { get; private set; }
        public int Edad { get; private set; }
        public string Correo { get; private set; }
        public bool Guardado { get; private set; }
        public FormAddAlumno()
        {
            InitializeComponent();
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            Nombre = txtNombre.Text;
            if (int.TryParse(txtEdad.Text, out int edad))
                Edad = edad;
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
