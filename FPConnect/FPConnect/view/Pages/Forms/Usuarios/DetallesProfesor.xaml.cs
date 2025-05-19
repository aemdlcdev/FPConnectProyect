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
using FPConnect.persistence.Manages;

namespace FPConnect.view.Pages.Forms.Usuarios
{
    /// <summary>
    /// Lógica de interacción para DetallesProfesor.xaml
    /// </summary>
    public partial class DetallesProfesor : Window
    {
        private Profesor _profesor;
        private ProfesorManage _profesorManage;

        // Constructor predeterminado
        public DetallesProfesor()
        {
            InitializeComponent();
            _profesorManage = new ProfesorManage();
        }

        // Constructor que recibe el profesor cuyos detalles se mostrarán
        public DetallesProfesor(Profesor profesor)
        {
            InitializeComponent();
            _profesor = profesor;
            _profesorManage = new ProfesorManage();
            CargarDatosProfesor();
        }

        private void CargarDatosProfesor()
        {
            if (_profesor == null) return;

            // Cargar información personal
            txtNombre.Text = _profesor.nombre;
            txtApellidos.Text = _profesor.apellidos;
            txtEmail.Text = _profesor.email;
            txtSexo.Text = _profesor.sexo == "m" ? "Masculino" : "Femenino";

            // Cargar información departamental
            txtDepartamento.Text = _profesor.nombre_departamento ?? "No asignado";

            // Cargar información adicional usando los métodos
            try
            {
                // Obtener nombres usando los métodos
                string nombreCentro = _profesorManage.ObtenerNombreCentro(_profesor.id_centro);
                string nombreRol = _profesorManage.ObtenerNombreRol(_profesor.id_rol);
                string nombreTurno = _profesorManage.ObtenerNombreTurno(_profesor.id_turno);
                string nombreCurso = _profesorManage.ObtenerNombreCurso(_profesor.id_curso) + " " + _profesorManage.ObtenerNombrePerfil(_profesor.id_curso) ; 

                txtCentro.Text = nombreCentro;
                txtRol.Text = nombreRol;
                txtTurno.Text = nombreTurno;
                txtCurso.Text = nombreCurso;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar información adicional: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                // En caso de error, mostrar los IDs directamente
                txtRol.Text = $"ID: {_profesor.id_rol}";
                txtCentro.Text = $"ID: {_profesor.id_centro}";
                txtTurno.Text = $"ID: {_profesor.id_turno}";
                txtCurso.Text = $"ID: {_profesor.id_curso}";
            }

            // Configurar estado
            ConfigurarEstado();

            // Configurar representación visual (color y carácter)
            ConfigurarVisual();
        }

        private void ConfigurarEstado()
        {
            // Configurar el texto del estado
            txtEstado.Text = _profesor.activo == 1 ? "Activo" : "Inactivo";

            // Configurar el color del indicador de estado
            if (_profesor.activo == 1)
            {
                estadoIndicator.Fill = new SolidColorBrush(Colors.Green);
            }
            else
            {
                estadoIndicator.Fill = new SolidColorBrush(Colors.Red);
            }
        }

        private void ConfigurarVisual()
        {
            // El color ya viene como Brush en la propiedad bgColor
            borderColor.Background = _profesor.bgColor;

            // Configurar el carácter inicial
            txtFirstChar.Text = _profesor.character;
        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
