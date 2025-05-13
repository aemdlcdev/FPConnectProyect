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
using FPConnect.domain;
using FPConnect.HelperClasses;
using FPConnect.view.Pages.Forms.Familias;
using FPConnect.view.Pages.Forms.Perfiles;
using FPConnect.view.Pages.Forms.Usuarios;

namespace FPConnect.view.Pages
{
    /// <summary>
    /// Lógica de interacción para Familias.xaml
    /// </summary>
    public partial class Familias : Page
    {

        //Familias
        private FamiliaProfesional familia;
        private ObservableCollection<FamiliaProfesional> listaFamilias;
        private FamiliaProfesional selectedFamilia;

        //Grados

        private Grado grado;
        private ObservableCollection<Grado> listaGrados;


        //Perfiles
        Perfil perfil = null;
        ObservableCollection<Perfil> listaPerfiles = null;
        Perfil selectedPerfil = null;


        public Familias()
        {
            InitializeComponent();

            //Familias
            familia = new FamiliaProfesional();
            listaFamilias = familia.LeerFamiliasCentro(SesionUsuario.IdCentro);
            selectedFamilia = new FamiliaProfesional();

            familiasDataGrid.ItemsSource = null;
            familiasDataGrid.ItemsSource = listaFamilias;

            cbxFamilia.ItemsSource = null;
            cbxFamilia.ItemsSource = listaFamilias;

            //Grados

            grado = new Grado();
            listaGrados = grado.LeerGradosPorCentro(SesionUsuario.IdCentro);

            cbxGrado.ItemsSource = null;
            cbxGrado.ItemsSource = listaGrados;

            //Perfiles
            perfil = new Perfil();
            listaPerfiles = perfil.LeerPerfilesPorCentro(SesionUsuario.IdCentro);
            perfilesDataGrid.ItemsSource = null;
            perfilesDataGrid.ItemsSource = listaPerfiles;
            selectedPerfil = new Perfil();

        }

        private void btnGuardarFamilia_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombreFamilia.Text.Trim() == "")
            {
                MessageBox.Show("El nombre de la familia no puede estar vacío", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string nombreFamilia = txtNombreFamilia.Text.Trim();

            FamiliaProfesional familiaNueva = new FamiliaProfesional(
                SesionUsuario.IdCentro,
                nombreFamilia
            );
            familia.InsertarFamilia(familiaNueva);
            listaFamilias.Add(familiaNueva);
            MessageBox.Show("Familia insertada correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            txtNombreFamilia.Text = "";
        }

        private void cbxFamilia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FamiliaProfesional familiaSeleccionada;
            if (cbxFamilia.SelectedItem != null)
            {
                familiaSeleccionada = (FamiliaProfesional)cbxFamilia.SelectedItem;
            }
        }

        private void cbxGrado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Grado gradoSeleccionado;
            if (cbxGrado.SelectedItem != null)
            {
                gradoSeleccionado = (Grado)cbxGrado.SelectedItem;
            }
        }

        private void btnGuardarPefil_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombrePefil.Text.Trim() == "")
            {
                MessageBox.Show("El nombre del perfil no puede estar vacío", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string nombrePerfil = txtNombrePefil.Text.Trim();
            int idFamilia = ((FamiliaProfesional)cbxFamilia.SelectedItem).id_familia;
            int idGrado = ((Grado)cbxGrado.SelectedItem).id_grado;
            int idCentro = SesionUsuario.IdCentro;

            Perfil nuevoPerfil = new Perfil(SesionUsuario.IdCentro, idFamilia, idGrado, nombrePerfil);
            perfil.InsertarPerfil(nuevoPerfil);
            listaPerfiles.Add(nuevoPerfil);

            MessageBox.Show("Perfil insertado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void perfilesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Perfil perfilSeleccionado;
            if (perfilesDataGrid.SelectedItem != null)
            {
                perfilSeleccionado = (Perfil)perfilesDataGrid.SelectedItem;

            }
        }

        private void btnEliminarFamilia_Click(object sender, RoutedEventArgs e)
        {
            if (familiasDataGrid.SelectedItem != null)
            {
                FamiliaProfesional familiaSeleccionada = (FamiliaProfesional)familiasDataGrid.SelectedItem;
                familia.EliminarFamilia(familiaSeleccionada.id_familia);
                listaFamilias.Remove(familiaSeleccionada);
                cbxFamilia.ItemsSource = null;
                cbxFamilia.ItemsSource = listaFamilias;
                MessageBox.Show("Familia eliminada correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Seleccione una familia para eliminar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnEliminarPerfil_Click(object sender, RoutedEventArgs e)
        {
            if (perfilesDataGrid.SelectedItem != null)
            {
                Perfil perfilSeleccionado = (Perfil)perfilesDataGrid.SelectedItem;
                perfil.EleminarPerfil(perfilSeleccionado);
                listaPerfiles.Remove(perfilSeleccionado);
                MessageBox.Show("Perfil eliminado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Seleccione un perfil para eliminar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void familiasDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FamiliaProfesional familiaSeleccionada;
            if (familiasDataGrid.SelectedItem != null)
            {
                familiaSeleccionada = (FamiliaProfesional)familiasDataGrid.SelectedItem;
            }
        }

        private void btnModificarFamilia_Click(object sender, RoutedEventArgs e)
        {
            selectedFamilia = familiasDataGrid.SelectedItem as FamiliaProfesional;

            if (selectedFamilia == null || selectedFamilia.id_familia == 0)
            {
                MessageBox.Show("Seleccione una familia válida antes de modificarla.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var familiaAModificar = selectedFamilia;

            FormModFamilia formModUsuario = new FormModFamilia(familiaAModificar);

            try
            {
                if (formModUsuario.ShowDialog() == true)
                {

                    // Llamar al método ModificarProfesor con la familia actualizado
                    familia.ModificarFamilia(familiaAModificar);

                    // Actualizar la lista de familias
                    listaFamilias.Clear();
                    listaFamilias = familia.LeerFamiliasCentro(SesionUsuario.IdCentro);
                    familiasDataGrid.ItemsSource = listaFamilias;

                    MessageBox.Show("Familia modificada correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar la familia: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnModificarPerfil_Click(object sender, RoutedEventArgs e)
        {
            selectedPerfil = perfilesDataGrid.SelectedItem as Perfil;

            if (selectedPerfil == null || selectedPerfil.id_perfil == 0)
            {
                MessageBox.Show("Seleccione un perfil válido antes de modificarlo.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var perfilAModificar = selectedPerfil;

            FormModPerfil formModPerfil = new FormModPerfil(perfilAModificar);

            try
            {
                if (formModPerfil.ShowDialog() == true)
                {

                    // Llamar al método ModificarPerfil con el perfil actrualizado
                    perfil.ModificarPerfil(perfilAModificar);

                    // Actualizar la lista de perfiles
                    listaPerfiles.Clear();
                    listaPerfiles = perfil.LeerPerfilesPorCentro(SesionUsuario.IdCentro);
                    perfilesDataGrid.ItemsSource = listaPerfiles;

                    MessageBox.Show("Perfil modificado correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el perfil: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
