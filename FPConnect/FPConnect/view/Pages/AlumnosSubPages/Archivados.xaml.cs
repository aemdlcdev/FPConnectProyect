using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
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
using static FPConnect.view.Pages.AlumnosSubPages.AlumnosActuales;

namespace FPConnect.view.Pages.AlumnosSubPages
{
    /// <summary>
    /// Lógica de interacción para Archivados.xaml
    /// </summary>
    public partial class Archivados : Page
    {
        private Alumno alumno;
        private ObservableCollection<Alumno> listaAlumnos { get; set; }
        private Perfil perfil;
        private ObservableCollection<Perfil> listaPerfiles { get; set; }

        private Curso curso;
        private ObservableCollection<Curso> listaCursos { get; set; }
        public Archivados()
        {
            InitializeComponent();
            // perfiles
            perfil = new Perfil();
            listaPerfiles = perfil.LeerPerfilesFiltrados(SesionUsuario.IdCentro,SesionUsuario.IdGrado,SesionUsuario.IdFamilia);
            
            foreach (Perfil p in listaPerfiles) 
            {
                cbPerfil.Items.Add(p);
            }

            // cursos
            curso = new Curso();
            listaCursos = new ObservableCollection<Curso>();
            cbAnio.IsEnabled = false;

            // alumnos
            alumno = new Alumno();
            listaAlumnos = new ObservableCollection<Alumno>();
        }



        private void btnCargar_Click(object sender, RoutedEventArgs e)
        {
            var perfilSeleccionado = cbPerfil.SelectedItem as Perfil;
            var an = cbAnio.SelectedItem as Curso;
            if (perfilSeleccionado == null)
            {
                MessageBox.Show("Por favor, seleccione un perfil.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (an == null)
            {
                MessageBox.Show("Por favor, seleccione un año.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            listaAlumnos.Clear(); // Limpiar la lista antes de cargar nuevos datos
            listaAlumnos = alumno.ObtenerHistoricoAlumnosPorPerfilYAnio(perfilSeleccionado.id_perfil, an.anio_inicio);
            if (listaAlumnos == null || listaAlumnos.Count == 0)
            {
                MessageBox.Show("No hay alumnos para mostrar en este perfil y año.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            alumnosDataGrid.ItemsSource = null; // Limpiar la fuente de datos actual
            alumnosDataGrid.ItemsSource = listaAlumnos;
        }
        private void archivadosDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnDescargar_Click(object sender, RoutedEventArgs e)
        {
            var cursoSeleccionado = cbAnio.SelectedItem as Curso;
            PdfGenerator.GenerarPdfAlumnos(listaAlumnos, "Lista alumnos para " + cursoSeleccionado);
        }

        private void cbPerfil_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbAnio.IsEnabled = true;
            var perfilSeleccionado = cbPerfil.SelectedItem as Perfil;
            cbAnio.Items.Clear();
            if (perfilSeleccionado != null)
            {
                listaCursos.Clear();
                listaCursos = curso.LeerPorPerfil(perfilSeleccionado.id_perfil);     
                foreach (Curso c in listaCursos)
                {
                    cbAnio.Items.Add(c);
                }
            }
            else
            {
                alumnosDataGrid.ItemsSource = null;
            }
        }
    }
}
