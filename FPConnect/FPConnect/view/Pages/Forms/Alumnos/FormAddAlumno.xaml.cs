using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
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
        public FormAddAlumno()
        {
            InitializeComponent();
        }

        public FormAddAlumno(Alumno alumno,int operacion)
        {
            InitializeComponent();
            nuevoAlumno = alumno;
            tipoOperacion = operacion; // 1 add, 2 mod
            if (operacion == 2) // Modificar
            {
                txtTitulo.Text = "Modificar Alumno";
                txtNombre.Text = nuevoAlumno.nombre;
                txtApellidos.Text = nuevoAlumno.apellidos;
                txtCorreo.Text = nuevoAlumno.email;
                
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
            else
            {
                txtTitulo.Text = "Añadir Alumno";
                cbConvocatoria.Visibility = Visibility.Collapsed; // Oculto el combo de convocatoria al añadir
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
                if (alumnoInsertar.Insertar(alumnoInsertar) == true) // inserto el nuevo alumno)
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

            }

            this.DialogResult = true; 
            

        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
