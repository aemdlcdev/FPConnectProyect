using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using FPConnect.view.Pages.Forms.Usuarios;
using MaterialDesignThemes.Wpf;
using static FPConnect.view.Pages.AlumnosSubPages.AlumnosActuales;

namespace FPConnect.view.Pages
{
    /// <summary>
    /// Lógica de interacción para GestionUsuarios.xaml
    /// </summary>
    public partial class GestionUsuarios : Page
    {
        private Member selectedUsuario;
        ObservableCollection<Member> members;
        int[] grados = new int[3];
        private Profesor profesor;

        private FamiliaProfesional fp;
        ObservableCollection<FamiliaProfesional> familiaProfesionales;

        private Rol rol;
        private ObservableCollection<Rol> listaRoles;

        public GestionUsuarios()
        {
            
            InitializeComponent();
            members = new ObservableCollection<Member>();
            var converter = new BrushConverter();
            members = new ObservableCollection<Member>();
            profesor = new Profesor();
            fp = new FamiliaProfesional();
            familiaProfesionales = new ObservableCollection<FamiliaProfesional>();
            rol = new Rol();
            listaRoles = new ObservableCollection<Rol>();

            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "John Doe", Position = "Murillo", Email = "john.doe@gmail.com", Phone = "415-954-1475" });
            members.Add(new Member { Number = "2", Character = "R", BgColor = (Brush)converter.ConvertFromString("#1E88E5"), Name = "Reza Alavi", Position = "Administrator", Email = "reza110@hotmail.com", Phone = "254-451-7893" });
            members.Add(new Member { Number = "3", Character = "D", BgColor = (Brush)converter.ConvertFromString("#FF8F00"), Name = "Dennis Castillo", Position = "Murillo", Email = "deny.cast@gmail.com", Phone = "125-520-0141" });
            members.Add(new Member { Number = "4", Character = "G", BgColor = (Brush)converter.ConvertFromString("#FF5252"), Name = "Gabriel Cox", Position = "Murillo", Email = "coxcox@gmail.com", Phone = "808-635-1221" });
            members.Add(new Member { Number = "5", Character = "L", BgColor = (Brush)converter.ConvertFromString("#0CA678"), Name = "Lena Jones", Position = "Manager", Email = "lena.offi@hotmail.com", Phone = "320-658-9174" });
            members.Add(new Member { Number = "6", Character = "B", BgColor = (Brush)converter.ConvertFromString("#6741D9"), Name = "Benjamin Caliword", Position = "Administrator", Email = "beni12@hotmail.com", Phone = "114-203-6258" });
            members.Add(new Member { Number = "7", Character = "S", BgColor = (Brush)converter.ConvertFromString("#FF6D00"), Name = "Sophia Muris", Position = "Murillo", Email = "sophi.muri@gmail.com", Phone = "852-233-6854" });
            members.Add(new Member { Number = "8", Character = "A", BgColor = (Brush)converter.ConvertFromString("#FF5252"), Name = "Ali Pormand", Position = "Manager", Email = "alipor@yahoo.com", Phone = "968-378-4849" });
            members.Add(new Member { Number = "9", Character = "F", BgColor = (Brush)converter.ConvertFromString("#1E88E5"), Name = "Frank Underwood", Position = "Manager", Email = "frank@yahoo.com", Phone = "301-584-6966" });
            members.Add(new Member { Number = "10", Character = "S", BgColor = (Brush)converter.ConvertFromString("#0CA678"), Name = "Saeed Dasman", Position = "Murillo", Email = "saeed.dasi@hotmail.com", Phone = "817-320-5052" });

            members.Add(new Member { Number = "11", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "John Doe", Position = "Murillo", Email = "john.doe@gmail.com", Phone = "415-954-1475" });
            members.Add(new Member { Number = "12", Character = "R", BgColor = (Brush)converter.ConvertFromString("#1E88E5"), Name = "Reza Alavi", Position = "Administrator", Email = "reza110@hotmail.com", Phone = "254-451-7893" });
            members.Add(new Member { Number = "13", Character = "D", BgColor = (Brush)converter.ConvertFromString("#FF8F00"), Name = "Dennis Castillo", Position = "Murillo", Email = "deny.cast@gmail.com", Phone = "125-520-0141" });
            members.Add(new Member { Number = "14", Character = "G", BgColor = (Brush)converter.ConvertFromString("#FF5252"), Name = "Gabriel Cox", Position = "Murillo", Email = "coxcox@gmail.com", Phone = "808-635-1221" });
            members.Add(new Member { Number = "15", Character = "L", BgColor = (Brush)converter.ConvertFromString("#0CA678"), Name = "Lena Jones", Position = "Manager", Email = "lena.offi@hotmail.com", Phone = "320-658-9174" });
            members.Add(new Member { Number = "16", Character = "B", BgColor = (Brush)converter.ConvertFromString("#6741D9"), Name = "Benjamin Caliword", Position = "Administrator", Email = "beni12@hotmail.com", Phone = "114-203-6258" });
            members.Add(new Member { Number = "17", Character = "S", BgColor = (Brush)converter.ConvertFromString("#FF6D00"), Name = "Sophia Muris", Position = "Murillo", Email = "sophi.muri@gmail.com", Phone = "852-233-6854" });
            members.Add(new Member { Number = "18", Character = "A", BgColor = (Brush)converter.ConvertFromString("#FF5252"), Name = "Ali Pormand", Position = "Manager", Email = "alipor@yahoo.com", Phone = "968-378-4849" });
            members.Add(new Member { Number = "19", Character = "F", BgColor = (Brush)converter.ConvertFromString("#1E88E5"), Name = "Frank Underwood", Position = "Manager", Email = "frank@yahoo.com", Phone = "301-584-6966" });
            members.Add(new Member { Number = "20", Character = "S", BgColor = (Brush)converter.ConvertFromString("#0CA678"), Name = "Saeed Dasman", Position = "Murillo", Email = "saeed.dasi@hotmail.com", Phone = "817-320-5052" });

            members.Add(new Member { Number = "21", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "John Doe", Position = "Murillo", Email = "john.doe@gmail.com", Phone = "415-954-1475" });
            members.Add(new Member { Number = "22", Character = "R", BgColor = (Brush)converter.ConvertFromString("#1E88E5"), Name = "Reza Alavi", Position = "Administrator", Email = "reza110@hotmail.com", Phone = "254-451-7893" });
            members.Add(new Member { Number = "23", Character = "D", BgColor = (Brush)converter.ConvertFromString("#FF8F00"), Name = "Dennis Castillo", Position = "Murillo", Email = "deny.cast@gmail.com", Phone = "125-520-0141" });
            members.Add(new Member { Number = "24", Character = "G", BgColor = (Brush)converter.ConvertFromString("#FF5252"), Name = "Gabriel Cox", Position = "Murillo", Email = "coxcox@gmail.com", Phone = "808-635-1221" });
            members.Add(new Member { Number = "25", Character = "L", BgColor = (Brush)converter.ConvertFromString("#0CA678"), Name = "Lena Jones", Position = "Manager", Email = "lena.offi@hotmail.com", Phone = "320-658-9174" });
            members.Add(new Member { Number = "26", Character = "B", BgColor = (Brush)converter.ConvertFromString("#6741D9"), Name = "Benjamin Caliword", Position = "Administrator", Email = "beni12@hotmail.com", Phone = "114-203-6258" });
            members.Add(new Member { Number = "27", Character = "S", BgColor = (Brush)converter.ConvertFromString("#FF6D00"), Name = "Sophia Muris", Position = "Murillo", Email = "sophi.muri@gmail.com", Phone = "852-233-6854" });
            members.Add(new Member { Number = "28", Character = "A", BgColor = (Brush)converter.ConvertFromString("#FF5252"), Name = "Ali Pormand", Position = "Manager", Email = "alipor@yahoo.com", Phone = "968-378-4849" });
            members.Add(new Member { Number = "29", Character = "F", BgColor = (Brush)converter.ConvertFromString("#1E88E5"), Name = "Frank Underwood", Position = "Manager", Email = "frank@yahoo.com", Phone = "301-584-6966" });
            members.Add(new Member { Number = "30", Character = "S", BgColor = (Brush)converter.ConvertFromString("#0CA678"), Name = "Saeed Dasman", Position = "Murillo", Email = "saeed.dasi@hotmail.com", Phone = "817-320-5052" });


            familiaProfesionales = fp.LeerFamiliasCentro(SesionUsuario.IdCentro);
            listaRoles = rol.LeerRolesPorCentro(SesionUsuario.IdCentro);
            foreach (FamiliaProfesional fp in familiaProfesionales)
            {               
                cbDept.Items.Add(fp);
            }

            foreach(Rol rol in listaRoles)
            {
                cbRol.Items.Add(rol);
            }

            usuariosDataGrid.ItemsSource = members;
            selectedUsuario = new Member();
            
        }

        private void btnEliminarUsuario_Click(object sender, RoutedEventArgs e)
        {
            FormDelete formDelUsuario = new FormDelete();

            if (formDelUsuario.ShowDialog() == true)
            {
                members.Remove(selectedUsuario);
            }
        }

        private void usuariosDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedUsuario = usuariosDataGrid.SelectedItem as Member;
        }

        private void btnModificarUsuario_Click(object sender, RoutedEventArgs e)
        {
            FormModUsuario formModUsuario = new FormModUsuario();
            if (formModUsuario.ShowDialog() == true)
            { 
                // implementar logica
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            string nombre = txtNombreUsuario.Text;  
            string apellidos = txtApellido.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Password;
            string sexo = cbSexo.Text;

            Rol rolSeleccionado = new Rol();
            rolSeleccionado=cbRol.SelectedItem as Rol;
            FamiliaProfesional fpSeleccionada = new FamiliaProfesional();
            fpSeleccionada = cbDept.SelectedItem as FamiliaProfesional;

            

            if (sexo.Equals("Masculino")) 
            {
                sexo = "m";
            } else 
            {
                sexo = "f";
            }

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellidos) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(sexo))
            {
                MessageBox.Show("Información", "Rellene todos los campos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (cbxBasica.IsChecked == true) 
            {
                grados[0] = 1;
            }
            if (cbxMedia.IsChecked == true)
            {
                grados[1] = 2;
            }
            if (cbxSuperior.IsChecked == true)
            {
                grados[2] = 3;
            }

            if(cbxBasica.IsChecked!=true && cbxMedia.IsChecked!=true && cbxSuperior.IsChecked != true) 
            {
                MessageBox.Show("Información", "Seleccione al menos un grado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else 
            {
                Console.WriteLine(rolSeleccionado.id_rol + " " + fpSeleccionada.id_familia);

                Profesor nuevoProfesor = new Profesor(
                rolSeleccionado.id_rol,
                SesionUsuario.IdCentro,
                fpSeleccionada.id_familia,
                nombre,
                apellidos,
                email,
                password,
                sexo);

                profesor.InsertarProfesor(nuevoProfesor, grados);

                MessageBox.Show("Información","Usuario insertado correctamente",MessageBoxButton.OK,MessageBoxImage.Information);

            }
            
        }
    }
}
