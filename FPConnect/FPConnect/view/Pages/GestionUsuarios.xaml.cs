using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using FPConnect.domain;
using FPConnect.HelperClasses;
using FPConnect.view.Pages.Forms;
using FPConnect.view.Pages.Forms.Usuarios;
using MaterialDesignThemes.Wpf;
using static FPConnect.view.Pages.AlumnosSubPages.AlumnosActuales;

namespace FPConnect.view.Pages
{
    /// <summary>
    /// Lógica de interacción para GestionUsuarios.xaml
    /// </summary>
    public partial class GestionUsuarios : Page
    {
        private Profesor selectedUsuario;
        ObservableCollection<Profesor> profesores;
        int[] grados = new int[3];
        private Profesor profesor;

        private FamiliaProfesional fp;
        ObservableCollection<FamiliaProfesional> familiaProfesionales;

        private Rol rol;
        private ObservableCollection<Rol> listaRoles;

        public GestionUsuarios()
        {
            
            InitializeComponent();
            profesores = new ObservableCollection<Profesor>();
            var converter = new BrushConverter();
            
            profesor = new Profesor();
            fp = new FamiliaProfesional();
            familiaProfesionales = new ObservableCollection<FamiliaProfesional>();
            rol = new Rol();
            listaRoles = new ObservableCollection<Rol>();

            profesores = profesor.LeerProfesoresPorCentro(SesionUsuario.IdCentro);


            familiaProfesionales = fp.LeerFamiliasCentro(SesionUsuario.IdCentro);
            listaRoles = rol.LeerRolesPorCentro(SesionUsuario.IdCentro);
            foreach (FamiliaProfesional fp in familiaProfesionales)
            {               
                cbDept.Items.Add(fp);
            }

            foreach(Rol rol in listaRoles)
            {
                cbRol.Items.Add(rol);
            }

            profesoresDataGrid.ItemsSource = profesores;
            selectedUsuario = new Profesor();
            
        }

        private void usuariosDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedUsuario = profesoresDataGrid.SelectedItem as Profesor;

            if (selectedUsuario != null)
            {
                Console.WriteLine("Traza " + selectedUsuario.id_profesor + " " + selectedUsuario.nombre);
            }
        }

        private void btnEliminarUsuario_Click(object sender, RoutedEventArgs e)
        {
            selectedUsuario = profesoresDataGrid.SelectedItem as Profesor;

            if (selectedUsuario == null || selectedUsuario.id_profesor == 0)
            {
                MessageBox.Show("Seleccione un usuario válido antes de eliminarlo.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            FormDelete formDelUsuario = new FormDelete();

            if (formDelUsuario.ShowDialog() == true)
            {
                profesores.Remove(selectedUsuario);
                
                profesor.EliminarProfesor(selectedUsuario.id_profesor);
            }
        }


        private void btnModificarUsuario_Click(object sender, RoutedEventArgs e)
        {
            FormModUsuario formModUsuario = new FormModUsuario();
            if (formModUsuario.ShowDialog() == true)
            { 
                // implementar logica
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtNombreUsuario.Text.Trim();
            string apellidos = txtApellido.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Password.Trim();
            string sexo = cbSexo.Text.Trim();
            string character = nombre.Substring(0, 1).ToUpper();

            Rol rolSeleccionado = cbRol.SelectedItem as Rol;
            FamiliaProfesional fpSeleccionada = cbDept.SelectedItem as FamiliaProfesional;

            if (sexo.Equals("Masculino"))
            {
                sexo = "m";
            }
            else
            {
                sexo = "f";
            }

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellidos) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(sexo))
            {
                MessageBox.Show("Rellene todos los campos", "Información" ,MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (cbxBasica.IsChecked == true)
            {
                grados[0] = 1;
            }
            if (cbxMedia.IsChecked == true)
            {
                grados[1] = 2;
            }
            if (cbxSuperior.IsChecked == true)
            {
                grados[2] = 3;
            }

            if (cbxBasica.IsChecked != true && cbxMedia.IsChecked != true && cbxSuperior.IsChecked != true)
            {
                MessageBox.Show("Seleccione al menos un grado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (rolSeleccionado == null || fpSeleccionada == null)
                {
                    MessageBox.Show("Seleccione un rol y un departamento válidos", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                Console.WriteLine(rolSeleccionado.id_rol + " " + fpSeleccionada.id_familia);

                Profesor nuevoProfesor = new Profesor(
                    rolSeleccionado.id_rol,
                    SesionUsuario.IdCentro,
                    fpSeleccionada.id_familia,
                    nombre,
                    apellidos,
                    email,
                    password,
                    sexo,
                    character,
                    Colores.GetRandomColor()
                );

                profesor.InsertarProfesor(nuevoProfesor, grados);

                MessageBox.Show("Usuario insertado correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}
