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
        
        int[] grados = new int[3];
        private Profesor profesor;
        ObservableCollection<Profesor> profesores;

        private FamiliaProfesional fp;
        ObservableCollection<FamiliaProfesional> familiaProfesionales;

        private Rol rol;
        private ObservableCollection<Rol> listaRoles;

        private Grado grado;
        private ObservableCollection<Grado> listaGrados;

        private Turno turno;
        private ObservableCollection<Turno> listaTurnos;

        private Perfil perfilUsuario;
        private ObservableCollection<Perfil> listaPerfiles;

        public GestionUsuarios()
        {
            
            InitializeComponent();
            profesores = new ObservableCollection<Profesor>();
            var converter = new BrushConverter();
            
            profesor = new Profesor();
            profesores = profesor.LeerProfesoresPorCentro(SesionUsuario.IdCentro);

            fp = new FamiliaProfesional();
            familiaProfesionales = fp.LeerFamiliasCentro(SesionUsuario.IdCentro);

            rol = new Rol();
            listaRoles = rol.LeerRolesPorCentro(SesionUsuario.IdCentro);

            grado = new Grado();
            listaGrados = grado.LeerGrados();

            turno = new Turno();
            listaTurnos = turno.LeerTurnos();

            perfilUsuario = new Perfil();
            listaPerfiles = new ObservableCollection<Perfil>();


            foreach (FamiliaProfesional fp in familiaProfesionales)
            {               
                cbDept.Items.Add(fp);
            }

            foreach(Rol rol in listaRoles)
            {
                cbRol.Items.Add(rol);
            }

            foreach (Grado g in listaGrados)
            {
                cbGrado.Items.Add(g);
            }

            foreach (Turno t in listaTurnos)
            {
                cbTurno.Items.Add(t);
            }

            foreach (Perfil p in listaPerfiles)
            {
                cbPerfil.Items.Add(p);
            }

            // Manejador de eventos para la selección de familias profesionales
            cbDept.SelectionChanged += cbDept_SelectionChanged;

            // Manejador de eventos para la selección de grados 
            cbGrado.SelectionChanged += cbGrado_SelectionChanged;

            // Deshabilito inicialmente el ComboBox de cursos hasta que se seleccione un grado
            cbCurso.IsEnabled = false;

            // Deshabilito inicialmente el ComboBox de perfiles hasta que se seleccione una familia
            cbPerfil.IsEnabled = false;

            cbDept.IsEnabled = false;

            profesoresDataGrid.ItemsSource = profesores;
            selectedUsuario = new Profesor();
            
        }

        private void cbDept_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Limpiar el ComboBox de perfiles
            cbPerfil.Items.Clear();
            listaPerfiles.Clear();
            FamiliaProfesional familiaSeleccionada = cbDept.SelectedItem as FamiliaProfesional;
            Grado gradoSeleccionado = cbGrado.SelectedItem as Grado;

            if (familiaSeleccionada != null)
            {             

                listaPerfiles = perfilUsuario.LeerPerfilCentro(familiaSeleccionada.id_familia, gradoSeleccionado.id_grado);

                foreach (Perfil p in listaPerfiles)
                {
                    cbPerfil.Items.Add(p);
                }

                // Habilitar el ComboBox de perfiles
                cbPerfil.IsEnabled = true;
            }
            else
            {
                // Si no hay familia seleccionada, deshabilitar el ComboBox de perfiles
                cbPerfil.IsEnabled = false;
            }
        }


        private void cbGrado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Limpiar el ComboBox de cursos
            cbCurso.Items.Clear();
               

            // Obtener el grado seleccionado
            Grado gradoSeleccionado = cbGrado.SelectedItem as Grado;

            if (gradoSeleccionado != null)
            {
                // Crear instancia de Curso para acceder a los métodos
                Curso curso = new Curso();

                // Obtener los cursos asociados al grado seleccionado
                ObservableCollection<Curso> cursosPorGrado = curso.LeerCursosPorGrado(gradoSeleccionado.id_grado);

                // Poblar el ComboBox de cursos con los cursos del grado seleccionado
                foreach (Curso c in cursosPorGrado)
                {
                    cbCurso.Items.Add(c);
                }

                // Habilitar el ComboBox de cursos
                cbCurso.IsEnabled = true;
                cbDept.IsEnabled = true;
                cbDept.Items.Clear();
            }
            else
            {
                // Si no hay grado seleccionado, deshabilitar el ComboBox de cursos
                cbCurso.IsEnabled = false;
            }
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

            
            var usuarioAEliminar = selectedUsuario;

            FormDelete formDelUsuario = new FormDelete();
            try
            {
                if (formDelUsuario.ShowDialog() == true)
                {
                    profesores.Remove(usuarioAEliminar); // quitarlo de la lista en memoria usando la variable de copia
                    profesor.EliminarProfesor(usuarioAEliminar.id_profesor); // eliminar de la bbdd
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el usuario: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // REVISAR METODOS, VER SI ES NECESARIO HACER LA ASIGNACION DE SELECTEDUSUARIO DENTRO DE ELIMINAR Y MODIFICAR

        private void btnModificarUsuario_Click(object sender, RoutedEventArgs e)
        {
            selectedUsuario = profesoresDataGrid.SelectedItem as Profesor;

            if (selectedUsuario == null || selectedUsuario.id_profesor == 0)
            {
                MessageBox.Show("Seleccione un usuario válido antes de modificarlo.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var profesorAModificar = selectedUsuario;

            FormModUsuario formModUsuario = new FormModUsuario(profesorAModificar);

            try
            {
                if (formModUsuario.ShowDialog() == true)
                {
                    
                    int[] grados = new int[3]; // Deberías obtener los grados seleccionados del formulario

                    // Llamar al método ModificarProfesor con el profesor actualizado
                    profesor.ModificarProfesor(profesorAModificar, grados);

                    // Actualizar la lista de profesores
                    profesores = profesor.LeerProfesoresPorCentro(SesionUsuario.IdCentro);
                    profesoresDataGrid.ItemsSource = profesores;

                    MessageBox.Show("Usuario modificado correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el usuario: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

            Grado gradoSeleccionado = cbGrado.SelectedItem as Grado;
            Curso cursoSeleccionado = cbCurso.SelectedItem as Curso;
            Turno turnoSeleccionado = cbTurno.SelectedItem as Turno;
            Perfil perfilSeleccionado = cbPerfil.SelectedItem as Perfil;

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
                MessageBox.Show("Rellene todos los campos", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
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
                    turnoSeleccionado.id_turno, 
                    nombre,
                    apellidos,
                    email,
                    password,
                    sexo,
                    character,
                    Colores.GetRandomColor()
                );
                profesores.Add(nuevoProfesor);
                profesor.InsertarProfesor(nuevoProfesor, grado.id_grado,cursoSeleccionado.id_curso,perfilSeleccionado.id_perfil);

                MessageBox.Show("Usuario insertado correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}
