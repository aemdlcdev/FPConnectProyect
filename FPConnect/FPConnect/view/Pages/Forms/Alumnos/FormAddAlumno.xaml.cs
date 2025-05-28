
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using FPConnect.domain;
using FPConnect.HelperClasses;

namespace FPConnect.view.Pages.Forms
{
    /// <summary>
    /// Lógica de interacción para FormAddAlumno.xaml
    /// </summary>
    public partial class FormAddAlumno : Window
    {
        private Alumno nuevoAlumno;
        private int tipoOperacion; // 1 add, 2 mod
        private Alumno alumnoInsertar;
        private ObservableCollection<Empresa> listaEmpresa;
        public FormAddAlumno()
        {
            InitializeComponent();
        }

        public FormAddAlumno(Alumno alumno,int operacion)
        {
            InitializeComponent();
            nuevoAlumno = alumno;
            tipoOperacion = operacion; // 1 add, 2 mod
            listaEmpresa = Empresa.ObtenerActivasPorCentro(SesionUsuario.IdCentro);

            foreach(Empresa empresa in listaEmpresa)
            {
                cbEmpresa.Items.Add(empresa);
            }

            if (operacion == 2) // Modificar
            {
                txtTitulo.Text = "Modificar Alumno";
                txtNombre.Text = nuevoAlumno.nombre;
                txtApellidos.Text = nuevoAlumno.apellidos;
                txtCorreo.Text = nuevoAlumno.email;
                cbEmpresa.SelectedItem = listaEmpresa.FirstOrDefault(e => e.id_empresa ==
                    (nuevoAlumno.InfoAdicional.ContainsKey("id_empresa") ?
                    Convert.ToInt32(nuevoAlumno.InfoAdicional["id_empresa"]) : 0));
                cbConvocatoria.Items.Add("Ordinaria");
                cbConvocatoria.Items.Add("Extraordinaria");
                if(alumno.id_convocatoria == 1)
                {
                    cbConvocatoria.SelectedIndex = 0; // Ordinaria
                }
                else if(alumno.id_convocatoria == 2)
                {
                    cbConvocatoria.SelectedIndex = 1; // Extraordinaria
                }
            }
            else if (operacion == 1) // Añadir
            {
                txtTitulo.Text = "Añadir Alumno";
                cbEmpresa.SelectedItem = listaEmpresa.FirstOrDefault(e => e.id_empresa ==
                    (nuevoAlumno.InfoAdicional.ContainsKey("id_empresa") ?
                    Convert.ToInt32(nuevoAlumno.InfoAdicional["id_empresa"]) : 0));
                cbConvocatoria.Visibility = Visibility.Collapsed; // Oculto el combo de convocatoria al añadir
            }
            else if (operacion == 3) // Modificar en validados
            {
                txtTitulo.Text = "Modificar Alumno";
                txtNombre.Text = nuevoAlumno.nombre;
                txtApellidos.Text = nuevoAlumno.apellidos;
                txtCorreo.Text = nuevoAlumno.email;
                cbConvocatoria.Visibility = Visibility.Collapsed;
            }
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            // Valido que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellidos.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) 
            )
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

           
            if (tipoOperacion == 1) // Añadir
            {
                alumnoInsertar = new Alumno
                (
                    txtNombre.Text,
                    txtApellidos.Text,
                    txtCorreo.Text,
                    txtNombre.Text.Substring(0,1).ToUpper(),
                    Colores.GetRandomColor(),
                    1,
                    SesionUsuario.IdCurso,
                    1,
                    1// Asignamos el curso del usuario que lo crea
                );
                var empresaSeleccionada = (Empresa)cbEmpresa.SelectedItem;
                int id_empresa = 0; // Por defecto 0 si no se selecciona empresa
                if (empresaSeleccionada != null) 
                {
                    id_empresa = empresaSeleccionada.id_empresa;
                } else
                {
                    MessageBox.Show("Debe seleccionar una empresa.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (alumnoInsertar.InsertarAlumnoConEmpresa(alumnoInsertar, id_empresa) == true) // inserto el nuevo alumno)
                    MessageBox.Show("Alumno agregado correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Error al agregar el alumno", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (tipoOperacion == 2) // Modificar
            {              
                nuevoAlumno.nombre = txtNombre.Text;
                nuevoAlumno.apellidos = txtApellidos.Text;
                nuevoAlumno.email = txtCorreo.Text;
                nuevoAlumno.id_convocatoria = cbConvocatoria.SelectedIndex == 0 ? 1 : 2; 

            } else if (tipoOperacion == 3) //modificar en validados
            {
                nuevoAlumno.nombre = txtNombre.Text;
                nuevoAlumno.apellidos = txtApellidos.Text;
                nuevoAlumno.email = txtCorreo.Text;               
            }

            this.DialogResult = true; 
            

        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
