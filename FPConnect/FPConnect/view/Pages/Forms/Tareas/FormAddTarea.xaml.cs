using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using FPConnect.domain;
using FPConnect.HelperClasses;

namespace FPConnect.view.Pages.Forms.Tareas
{
    /// <summary>
    /// Lógica de interacción para FormAddUsuario.xaml
    /// </summary>
    public partial class FormAddTarea : Window
    {
        public TareaCoordinacion tareaSeleccionada { get; set; }
        private ObservableCollection<Empresa> listaEmpresas;
        private int opcion;

        public FormAddTarea()
        {
            InitializeComponent();
            
        }


        public FormAddTarea(TareaCoordinacion tarea, int opcion)
        {
            InitializeComponent();
            tareaSeleccionada = tarea;
            this.opcion = opcion;
            listaEmpresas = Empresa.ObtenerPorCentroFamiliaYGrado(SesionUsuario.IdCentro, SesionUsuario.IdFamilia, SesionUsuario.IdGrado);
            foreach (Empresa empresa in listaEmpresas)
            {
                Console.WriteLine(empresa.nombre);
                cbEmpresa.Items.Add(empresa);
            }
          
            cbEstado.Items.Add("Pendiente");
            cbEstado.Items.Add("En progreso");
            cbEstado.Items.Add("Finalizada");

            if (opcion == 2) // mod
            {
                txtTitulo.Text = "Modificar tarea";
                txtNombre.Text = tareaSeleccionada.titulo;
                txtDescripcion.Text = tareaSeleccionada.descripcion;
                cbEmpresa.SelectedItem = listaEmpresas.Where(x => x.id_empresa == tareaSeleccionada.id_empresa).FirstOrDefault();
                cbEstado.Visibility = Visibility.Visible;
                int id_estado = tareaSeleccionada.estado;
                Console.WriteLine("Estado: " + id_estado);
                switch (id_estado)
                {
                    case 1:
                        cbEstado.SelectedIndex = 0; // Pendiente
                        break;
                    case 2:
                        cbEstado.SelectedIndex = 1; // En progreso
                        break;
                    case 3:
                        cbEstado.SelectedIndex = 2; // Finalizada
                        break;
                }
            }

            if (opcion == 3) // info
            {
                txtTitulo.Text = "Información de la tarea";
                txtNombre.IsEnabled = false;
                txtDescripcion.IsEnabled = false;
                cbEmpresa.IsEnabled = false;
                btnGuardar.IsEnabled = false;
                btnGuardar.Visibility = Visibility.Collapsed;

                txtNombre.Text = tareaSeleccionada.titulo;
                txtDescripcion.Text = tareaSeleccionada.descripcion;
                cbEmpresa.SelectedItem = listaEmpresas.Where(x => x.id_empresa == tareaSeleccionada.id_empresa).FirstOrDefault();
            }

        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            
            if (tareaSeleccionada != null)
            {              
                tareaSeleccionada.titulo = txtNombre.Text;
                tareaSeleccionada.descripcion = txtDescripcion.Text;
                tareaSeleccionada.id_empresa = ((Empresa)cbEmpresa.SelectedItem).id_empresa;
                tareaSeleccionada.id_familia = SesionUsuario.IdFamilia;

                if(opcion == 2) 
                {
                    if (cbEstado.SelectedItem.Equals("Pendiente"))
                    {
                        tareaSeleccionada.estado = 1;
                    }
                    else if (cbEstado.SelectedItem.Equals("En progreso"))
                    {
                        tareaSeleccionada.estado = 2;
                    }
                    else if (cbEstado.SelectedItem.Equals("Finalizada"))
                    {
                        tareaSeleccionada.estado = 3;
                    }
                }

            }

            this.DialogResult = true;
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }

}
