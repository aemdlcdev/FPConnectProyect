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
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using static FPConnect.view.Pages.AlumnosSubPages.AlumnosActuales;

namespace FPConnect.view.Pages
{
    /// <summary>
    /// Lógica de interacción para GestionUsuarios.xaml
    /// </summary>
    public partial class GestionUsuarios : Page
    {
        // Profesores
        private Profesor selectedUsuario;
        private Profesor profesor;
        ObservableCollection<Profesor> profesores;

        // Roles
        private Rol rol;
        private ObservableCollection<Rol> listaRoles;

        // Familias Profesionales
        private FamiliaProfesional fp;
        ObservableCollection<FamiliaProfesional> familiaProfesionales;    

        // Turnos
        private Turno turno;
        private ObservableCollection<Turno> listaTurnos;

        // Cursos
        private Curso curso;
        private ObservableCollection<Curso> listaCursos;



        public GestionUsuarios()
        {
            
            InitializeComponent();
            var converter = new BrushConverter();
            
            // Profesores
            profesor = new Profesor();
            profesores = profesor.LeerPorCentro(SesionUsuario.IdCentro);

            // Familias Profesionales
            fp = new FamiliaProfesional();
            familiaProfesionales = fp.LeerFamiliasCentro(SesionUsuario.IdCentro);

            // Roles
            rol = new Rol();
            listaRoles = new ObservableCollection<Rol>();

            // Cursos
            curso = new Curso();
            listaCursos = curso.LeerPorCentro(SesionUsuario.IdCentro);
            
            // Turnos
            turno = new Turno();
            listaTurnos = turno.LeerTurnos();
            //Sexo
            cbSexo.Items.Add("Masculino");
            cbSexo.Items.Add("Femenino");

            // Manejador de eventos para la selección de familias profesionales
            cbDept.SelectionChanged += cbDept_SelectionChanged;
            cbSexo.SelectionChanged += cbSexo_SelectionChanged;

            // Deshabilito inicialmente el ComboBox de cursos, dept y turno hasta que se seleccione un rol
            cbRol.IsEnabled = false;
            cbCurso.IsEnabled = false;
            cbDept.IsEnabled = false;
            cbTurno.IsEnabled = false;

            profesoresDataGrid.ItemsSource = profesores;
            selectedUsuario = new Profesor();
            
        }

        private void cbRol_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            var rolSeleccionado = cbRol.SelectedItem as Rol;
            if (rolSeleccionado != null)
            {
                Console.WriteLine("Rol seleccionado: " + rolSeleccionado.nombre);
                // Departamentos
                cbDept.IsEnabled = true;
                cbDept.Items.Clear();
                foreach (FamiliaProfesional fp in familiaProfesionales)
                {
                    cbDept.Items.Add(fp);
                }

            }
        }

        private void cbSexo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sexoSeleccionado = cbSexo.SelectedItem as string;
            if (sexoSeleccionado == "Masculino")
            {
                sexoSeleccionado = "m";
            }
            else if (sexoSeleccionado == "Femenino")
            {
                sexoSeleccionado = "f";
            }
            else
            {
                sexoSeleccionado = null;
            }

            if (sexoSeleccionado != null)
            {
                Console.WriteLine("Sexo seleccionado: " + sexoSeleccionado);
                cbRol.IsEnabled = true;
                
                cbRol.Items.Clear();
                listaRoles.Clear();
                listaRoles = rol.LeerRolesPorCentro(SesionUsuario.IdCentro);
                foreach (Rol rol in listaRoles)
                {
                    cbRol.Items.Add(rol);
                }
            }
        }

        private void cbDept_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fpSeleccionada = cbDept.SelectedItem as FamiliaProfesional;
            if (fpSeleccionada != null)
            {
                Console.WriteLine("Familia profesional seleccionada: " + fpSeleccionada.nombre);
                cbCurso.IsEnabled = true;
                cbCurso.Items.Clear();

                try
                {
                    // Obtener cursos directamente de la base de datos 
                    Curso cursoTemp = new Curso();
                    ObservableCollection<Curso> cursosPorFamilia = cursoTemp.LeerPorFamilia(fpSeleccionada.id_familia);

                    if (cursosPorFamilia.Count > 0)
                    {
                        foreach (Curso curso in cursosPorFamilia)
                        {
                            cbCurso.Items.Add(curso);
                        }
                        Console.WriteLine($"Se cargaron {cursosPorFamilia.Count} cursos");
                    }
                    else
                    {
                        Console.WriteLine("No se encontraron cursos para la familia seleccionada");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al cargar cursos: {ex.Message}");
                }
           
                cbTurno.Items.Clear();
                
                foreach (Turno turno in listaTurnos)
                {
                    cbTurno.Items.Add(turno);
                }
                cbTurno.IsEnabled = true;

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
                    profesor.DesactivarProfesor(usuarioAEliminar.id_profesor); // eliminar de la bbdd
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el usuario: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

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
                    profesor.Actualizar(profesorAModificar);

                    // Actualizar la lista de profesores
                    profesores = profesor.LeerPorCentro(SesionUsuario.IdCentro);
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
            

            Rol rolSeleccionado = cbRol.SelectedItem as Rol;
            FamiliaProfesional fpSeleccionada = cbDept.SelectedItem as FamiliaProfesional;
            Curso cursoSeleccionado = cbCurso.SelectedItem as Curso;
            Turno turnoSeleccionado = cbTurno.SelectedItem as Turno;

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

            // Verificar que todos los ComboBox tienen selecciones válidas
            if (rolSeleccionado == null || fpSeleccionada == null || cursoSeleccionado == null || turnoSeleccionado == null)
            {
                string mensaje = "Debe seleccionar:";
                if (rolSeleccionado == null) mensaje += "\n- Un rol";
                if (fpSeleccionada == null) mensaje += "\n- Un departamento";
                if (cursoSeleccionado == null) mensaje += "\n- Un curso";
                if (turnoSeleccionado == null) mensaje += "\n- Un turno";

                MessageBox.Show(mensaje, "Campos incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Console.WriteLine(rolSeleccionado.id_rol + " " + fpSeleccionada.id_familia);
            string character = nombre.Substring(0, 1).ToUpper();
            string colorHex = Colores.GetRandomColor();
            Profesor nuevoProfesor = new Profesor(
            rolSeleccionado.id_rol,
            SesionUsuario.IdCentro,
            fpSeleccionada.id_familia,
            cursoSeleccionado.id_curso,
            turnoSeleccionado.id_turno,
            nombre,
            apellidos,
            email,
            password,
            sexo,
            character,
            colorHex // Pasar el color como string, lo convierto en el constructor
            );

            profesor.Insertar(nuevoProfesor);
            profesores.Clear();
            profesores = profesor.LeerPorCentro(SesionUsuario.IdCentro);
            profesoresDataGrid.ItemsSource = null;
            profesoresDataGrid.ItemsSource = profesores;

            MessageBox.Show("Usuario insertado correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            txtNombreUsuario.Clear();
            txtApellido.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            cbSexo.SelectedIndex = -1;
            cbRol.SelectedIndex = -1;
            cbDept.SelectedIndex = -1;
            cbCurso.SelectedIndex = -1;
            cbTurno.SelectedIndex = -1;
            cbRol.IsEnabled = false;
            cbDept.IsEnabled = false;
            cbCurso.IsEnabled = false;
            cbTurno.IsEnabled = false;
            

        }
    }
}
