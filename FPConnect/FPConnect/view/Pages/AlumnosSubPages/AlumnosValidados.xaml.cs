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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FPConnect.view.Pages.Forms.Alumnos;
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
        private ObservableCollection<Alumno> listaAlumnos { get; set; }
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

        private void membersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



        private void btnEditarAlumno_Click(object sender, RoutedEventArgs e)
        {
            FormModAlumno formModAlumno = new FormModAlumno();



            //Console.WriteLine(selectedMember.Name); <- [Traza]

            if (formModAlumno.ShowDialog() == true) // Muestra como modal
            {
                // Implementar logica
            }
        }

        private void btnEliminarAlumno_Click(object sender, RoutedEventArgs e)
        {
            FormDelete formDelAlumno = new FormDelete();

            if (formDelAlumno.ShowDialog() == true) // Muestra como modal
            {

                // Se hace en memoria para probar
                // Implementar logica en base de datos
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
