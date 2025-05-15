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

namespace FPConnect.view.Pages.Forms.Usuarios
{
    /// <summary>
    /// Lógica de interacción para FormAddUsuario.xaml
    /// </summary>
    public partial class FormModUsuario : Window
    {
        public Profesor ProfesorSeleccionado { get; set; }
        private Rol rol;
        private ObservableCollection<Rol> listaRoles;
        public FormModUsuario()
        {
            InitializeComponent();
        }

        public FormModUsuario(Profesor profesor)
        {
            InitializeComponent();
            ProfesorSeleccionado = profesor;
            
            if (ProfesorSeleccionado != null)
            {
                listaRoles = new ObservableCollection<Rol>();
                rol = new Rol();
                listaRoles = rol.LeerRolesPorCentro(SesionUsuario.IdCentro);

                foreach (Rol rol in listaRoles)
                {
                    cbRol.Items.Add(rol);
                }

                txtNombre.Text = ProfesorSeleccionado.nombre;
                txtApellidos.Text = ProfesorSeleccionado.apellidos;
                txtEmail.Text = ProfesorSeleccionado.email;
                cbRol.SelectedItem = listaRoles.FirstOrDefault(r => r.id_rol == ProfesorSeleccionado.id_rol);
            }
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            
            if (ProfesorSeleccionado != null)
            {              
                ProfesorSeleccionado.nombre = txtNombre.Text;
                ProfesorSeleccionado.apellidos = txtApellidos.Text;
                ProfesorSeleccionado.email = txtEmail.Text;
                ProfesorSeleccionado.id_rol = ((Rol)cbRol.SelectedItem).id_rol;
            }

            this.DialogResult = true;
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }

}
