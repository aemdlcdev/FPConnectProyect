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
        public AlumnosValidados()
        {
            InitializeComponent();
            alumno = new Alumno();
            listaAlumnos = alumno.ObtenerAlumnosPorCursoYFase(SesionUsuario.IdCurso, 2); 
            alumnosDataGrid.ItemsSource = null;
            alumnosDataGrid.ItemsSource = listaAlumnos;
            txtNumAlumnos.Text = GetAlumnosActuales().ToString();

        }

        private int GetAlumnosActuales()
        {
            return alumnosDataGrid.Items.Count;
        }

        private void btnEditarAlumno_Click(object sender, RoutedEventArgs e)
        {
            Alumno nuevoAlumno = (Alumno)alumnosDataGrid.SelectedItem;
            int tipoOperacion = 3; // mod alumnos validados
            FormAddAlumno formAddEmpresa = new FormAddAlumno(nuevoAlumno, tipoOperacion);
            formAddEmpresa.ShowDialog();
            if (formAddEmpresa.DialogResult == true)
            {
                nuevoAlumno.Actualizar(nuevoAlumno);
                listaAlumnos.Clear();
                listaAlumnos = alumno.ObtenerAlumnosPorCursoYFase(SesionUsuario.IdCurso, 2);
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
        
    }
}
