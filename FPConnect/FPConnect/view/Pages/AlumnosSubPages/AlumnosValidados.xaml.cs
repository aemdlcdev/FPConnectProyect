using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using FPConnect.view.Pages.Forms;
using FPConnect.domain;
using FPConnect.HelperClasses;

namespace FPConnect.view.Pages.AlumnosSubPages
{
    /// <summary>
    /// Lógica de interacción para Informacion.xaml
    /// </summary>
    public partial class AlumnosValidados : Page
    {
        public ObservableCollection<Alumno> listaAlumnos { get; set; }
        private Alumno alumno;
        private int convocatoriaSeleccionada;
        public AlumnosValidados()
        {
            InitializeComponent();
            alumno = new Alumno();
            cbConvocatoria.Items.Add("Ordinaria");
            cbConvocatoria.Items.Add("Extraordinaria");

        }

        private int GetAlumnosActuales()
        {
            return alumnosDataGrid.Items.Count;
        }

        private void btnEditarAlumno_Click(object sender, RoutedEventArgs e)
        {
            Alumno nuevoAlumno = (Alumno)alumnosDataGrid.SelectedItem;
            int tipoOperacion = 2; // 1 add, 2 mod
            FormAddAlumno formAddEmpresa = new FormAddAlumno(nuevoAlumno, tipoOperacion);
            formAddEmpresa.ShowDialog();
            if (formAddEmpresa.DialogResult == true)
            {
                nuevoAlumno.Actualizar(nuevoAlumno);
                listaAlumnos.Clear();
                listaAlumnos = alumno.ObtenerAlumnosPorCursoConvocatoriaYFase(SesionUsuario.IdCurso, convocatoriaSeleccionada, 2); // Vuelve a cargar los alumnos
                alumnosDataGrid.ItemsSource = null;
                alumnosDataGrid.ItemsSource = listaAlumnos;
                MessageBox.Show("Alumno actualizado correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnEliminarAlumno_Click(object sender, RoutedEventArgs e)
        {
            var selectedAlumno = alumnosDataGrid.SelectedItem as Alumno;

            if (selectedAlumno == null || selectedAlumno.id_alumno == 0)
            {
                MessageBox.Show("Seleccione un alumno válido antes de eliminarlo.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            FormDelete formDelUsuario = new FormDelete();
            try
            {
                if (formDelUsuario.ShowDialog() == true)
                {
                    listaAlumnos.Remove(selectedAlumno); // eliminar de la vista
                    alumno.EliminarLogico(selectedAlumno); // eliminar de la base de datos
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el alumno: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Arreglar checked

        private void checkAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var alumno in listaAlumnos)
            {
                //alumno.IsSelected = true;
            }
            alumnosDataGrid.Items.Refresh();
        }

        private void checkAll_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var alumno in listaAlumnos)
            {
                //alumno.IsSelected = false;
            }
            alumnosDataGrid.Items.Refresh();
        }

        private void btnCargar_Click(object sender, RoutedEventArgs e)
        {
            if (cbConvocatoria.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione una convocatoria.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            convocatoriaSeleccionada = 0; // lo incializo aqui para evitar errores
            if (cbConvocatoria.SelectedItem != null)
            {
                if (cbConvocatoria.SelectedItem.ToString() == "Ordinaria")
                {
                    convocatoriaSeleccionada = 1;
                }
                else if (cbConvocatoria.SelectedItem.ToString() == "Extraordinaria")
                {
                    convocatoriaSeleccionada = 2;
                }
            }
            Console.WriteLine("Convocatoria seleccionada: " + convocatoriaSeleccionada);
            Console.WriteLine("Curso seleccionado: " + SesionUsuario.IdCurso);

            listaAlumnos = alumno.ObtenerAlumnosPorCursoConvocatoriaYFase(SesionUsuario.IdCurso, convocatoriaSeleccionada, 2);
            if (listaAlumnos.Count == 0)
            {
                MessageBox.Show("No hay alumnos para mostrar en esta convocatoria.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                alumnosDataGrid.ItemsSource = null; // Limpiar la fuente de datos actual
                alumnosDataGrid.ItemsSource = listaAlumnos;
                alumnosDataGrid.Items.Refresh();
            }
        }
    }
}
