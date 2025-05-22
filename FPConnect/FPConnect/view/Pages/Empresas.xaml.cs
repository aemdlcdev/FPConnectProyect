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
using FPConnect.view.Pages.Forms;
using FPConnect.view.Pages.Forms.Empresas;
using FPConnect.view.Pages.Forms.Familias;

namespace FPConnect.view.Pages
{
    /// <summary>
    /// Lógica de interacción para Empresas.xaml
    /// </summary>
    public partial class Empresas : Page
    {
        private Empresa empresa;
        private ObservableCollection<Empresa> listaEmpresas;
        private Empresa selectedEmpresa;
        public Empresas()
        {
            InitializeComponent();
            txtHola.Text = "Hola " + SesionUsuario.NombreUsuario;
            empresa = new Empresa();
            // Lectura filtrada de empresas por centro, familia y grado segun la sesion del usuario
            listaEmpresas = Empresa.ObtenerPorCentroFamiliaYGrado(SesionUsuario.IdCentro,SesionUsuario.IdFamilia,SesionUsuario.IdGrado);
            Console.WriteLine("Empresas: " + listaEmpresas.Count);
            empresasDataGrid.ItemsSource = null;
            empresasDataGrid.ItemsSource = listaEmpresas;


        }

        private void btnAddEmpresa_Click(object sender, RoutedEventArgs e)
        {
            Empresa nuevaEmpresa = new Empresa();
            int tipoOperacion = 1; // 1 add, 2 mod
            FormAddEmpresa formAddEmpresa = new FormAddEmpresa(nuevaEmpresa,tipoOperacion);
            formAddEmpresa.ShowDialog();
            if (formAddEmpresa.DialogResult == true)
            {
                nuevaEmpresa.Insertar(nuevaEmpresa, SesionUsuario.IdFamilia, SesionUsuario.IdGrado);

                listaEmpresas.Clear();
                // Refrescar la lista de empresas después de la inserción
                listaEmpresas = Empresa.ObtenerPorCentroFamiliaYGrado(
                    SesionUsuario.IdCentro,
                    SesionUsuario.IdFamilia,
                    SesionUsuario.IdGrado);

                // Actualizar el DataGrid
                empresasDataGrid.ItemsSource = null;
                empresasDataGrid.ItemsSource = listaEmpresas;

                MessageBox.Show("Empresa agregada correctamente.");
            }
        }

        private void btnEditarEmpresa_Click(object sender, RoutedEventArgs e) 
        {
            selectedEmpresa = empresasDataGrid.SelectedItem as Empresa;

            if (selectedEmpresa == null || selectedEmpresa.id_empresa == 0)
            {
                MessageBox.Show("Seleccione una empresa válida antes de modificarla.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var empresaAModificar = selectedEmpresa;
            int tipoOperacion = 2; // 1 add, 2 mod
            FormAddEmpresa formAddEmpresa = new FormAddEmpresa(empresaAModificar,tipoOperacion); // Voy a reutilizar el formulario de add para modificar tambien

            try
            {
                if (formAddEmpresa.ShowDialog() == true)
                {

                    // Llamar al método ModificarProfesor con la familia actualizado
                    empresa.Actualizar(empresaAModificar);

                    // Actualizar la lista de empresas
                    listaEmpresas.Clear();
                    listaEmpresas = Empresa.ObtenerPorCentroFamiliaYGrado(SesionUsuario.IdCentro, SesionUsuario.IdFamilia, SesionUsuario.IdGrado);

                    empresasDataGrid.ItemsSource = null;
                    empresasDataGrid.ItemsSource = listaEmpresas;

                    MessageBox.Show("Empresa modificada correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar la empresa: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelEmpresa_Click(object sender, RoutedEventArgs e) 
        {
            selectedEmpresa = empresasDataGrid.SelectedItem as Empresa;

            if (selectedEmpresa == null || selectedEmpresa.id_empresa == 0)
            {
                MessageBox.Show("Seleccione una empresa válida antes de eliminarla.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }


            var empresaAEliminar = selectedEmpresa;

            FormDelete formDelUsuario = new FormDelete();
            try
            {
                if (formDelUsuario.ShowDialog() == true)
                {
                    listaEmpresas.Remove(empresaAEliminar); // eliminar de la vista
                    empresa.Desactivar(empresaAEliminar); // eliminar de la base de datos
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la empresa: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public override string ToString()
        {
            return empresa.nombre + " ["+empresa.id_empresa+"]";
        }
    }
}
