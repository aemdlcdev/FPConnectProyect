using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using FPConnect.domain;
using FPConnect.HelperClasses;

namespace FPConnect.view.Pages.Forms.Familias
{
    /// <summary>
    /// Lógica de interacción para FormAddUsuario.xaml
    /// </summary>
    public partial class FormModFamilia : Window
    {
        public FamiliaProfesional familiaSeleccionada { get; set; }
        public FormModFamilia()
        {
            InitializeComponent();
        }

        public FormModFamilia(FamiliaProfesional familia)
        {
            InitializeComponent();
            familiaSeleccionada = familia;
            
            if (familiaSeleccionada != null)
            {
                txtNombre.Text = familiaSeleccionada.nombre;
            }
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            
            if (familiaSeleccionada != null)
            {
                familiaSeleccionada.nombre = txtNombre.Text.Trim();               
            }

            this.DialogResult = true;
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }

}
