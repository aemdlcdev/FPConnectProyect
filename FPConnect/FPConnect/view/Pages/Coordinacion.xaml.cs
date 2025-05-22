using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Formatting;
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
using FPConnect.view.Pages.Forms.Tareas;

namespace FPConnect.view.Pages
{
    /// <summary>
    /// Lógica de interacción para Coordinacion.xaml
    /// </summary>
    public partial class Coordinacion : Page
    {
        private TareaCoordinacion tc;
        private ObservableCollection<TareaCoordinacion> listaTareas;
        public Coordinacion()
        {
            InitializeComponent();
            tc = new TareaCoordinacion();
            listaTareas = tc.ObtenerPorFamilia(SesionUsuario.IdFamilia);
            tareasDataGrid.ItemsSource = null;
            tareasDataGrid.ItemsSource = listaTareas;
        }

        private void membersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (tareasDataGrid.SelectedItem != null)
            {
                TareaCoordinacion tareaSeleccionada = (TareaCoordinacion)tareasDataGrid.SelectedItem;
            }

        }

        private void btnAddTarea_Click(object sender, RoutedEventArgs e)
        {

            TareaCoordinacion nuevaTarea = new TareaCoordinacion();
            FormAddTarea formAddTarea = new FormAddTarea(nuevaTarea,1); // 1 add, 2 mod, 3 view
            formAddTarea.ShowDialog();
            if (formAddTarea.DialogResult == true)
            {
                nuevaTarea.Insertar(nuevaTarea);

                listaTareas.Clear();
                // Refrescar la lista de tareas después de la inserción
                listaTareas = tc.ObtenerPorFamilia(SesionUsuario.IdFamilia);

                // Actualizar el DataGrid de tareas
                tareasDataGrid.ItemsSource = null;
                tareasDataGrid.ItemsSource = listaTareas;

                MessageBox.Show("Empresa agregada correctamente.");
            }
        }

        private void btnEditarTarea_Click(object sender, RoutedEventArgs e)
        {
            if (tareasDataGrid.SelectedItem != null)
            {
                TareaCoordinacion tareaSeleccionada = (TareaCoordinacion)tareasDataGrid.SelectedItem;
                FormAddTarea formAddTarea = new FormAddTarea(tareaSeleccionada, 2); // 1 add, 2 mod, 3 view
                formAddTarea.ShowDialog();
                if (formAddTarea.DialogResult == true)
                {
                    tareaSeleccionada.Actualizar();
                    listaTareas.Clear();
                    // Refrescar la lista de tareas después de la actualización
                    listaTareas = tc.ObtenerPorFamilia(SesionUsuario.IdFamilia);
                    // Actualizar el DataGrid de tareas
                    tareasDataGrid.ItemsSource = null;
                    tareasDataGrid.ItemsSource = listaTareas;
                    MessageBox.Show("Empresa actualizada correctamente.");
                }
            }
            else
            {
                MessageBox.Show("Por favor selecciona una tarea.");
            }
        }

        private void btnDelTarea_Click(object sender, RoutedEventArgs e) 
        {
            var tareaSeleccionada = (TareaCoordinacion)tareasDataGrid.SelectedItem;
            if (tareaSeleccionada != null) 
            { 
                FormDelete form = new FormDelete();
                form.ShowDialog();
                if (form.DialogResult == true) 
                {
                    tareaSeleccionada.Eliminar(tareaSeleccionada);
                    listaTareas.Clear();
                    // Refrescar la lista de tareas después de la eliminación
                    listaTareas = tc.ObtenerPorFamilia(SesionUsuario.IdFamilia);
                    // Actualizar el DataGrid de tareas
                    tareasDataGrid.ItemsSource = null;
                    tareasDataGrid.ItemsSource = listaTareas;
                    MessageBox.Show("Tarea eliminada correctamente.");
                }
            }
        }

        private void btnInfoTarea_Click(object sender, RoutedEventArgs e)
        {
            if (tareasDataGrid.SelectedItem != null)
            {
                TareaCoordinacion tareaSeleccionada = (TareaCoordinacion)tareasDataGrid.SelectedItem;
                FormAddTarea formAddTarea = new FormAddTarea(tareaSeleccionada, 3); // 1 add, 2 mod, 3 view
                formAddTarea.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor selecciona una tarea.");
            }
        }


    }
}
