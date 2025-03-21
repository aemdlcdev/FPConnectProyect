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
using FPConnect.view.Pages.Forms;
using FPConnect.view.Pages.Forms.Alumnos;

namespace FPConnect.view.Pages.AlumnosSubPages
{
    /// <summary>
    /// Lógica de interacción para AlumnosActuales.xaml
    /// </summary>
    public partial class AlumnosActuales : Page
    {
        private ObservableCollection<Member> members { get; set; }
        private Member selectedMember;
        public AlumnosActuales()
        {
            InitializeComponent();

            var converter = new BrushConverter();
            members = new ObservableCollection<Member>();

            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "John Doe", Position = "González", Email = "john.doe@gmail.com", Phone = "415-954-1475" });
            members.Add(new Member { Number = "2", Character = "R", BgColor = (Brush)converter.ConvertFromString("#1E88E5"), Name = "Reza Alavi", Position = "Fernández", Email = "reza110@hotmail.com", Phone = "254-451-7893" });
            members.Add(new Member { Number = "3", Character = "D", BgColor = (Brush)converter.ConvertFromString("#FF8F00"), Name = "Dennis Castillo", Position = "Rodríguez", Email = "deny.cast@gmail.com", Phone = "125-520-0141" });
            members.Add(new Member { Number = "4", Character = "G", BgColor = (Brush)converter.ConvertFromString("#FF5252"), Name = "Gabriel Cox", Position = "López", Email = "coxcox@gmail.com", Phone = "808-635-1221" });
            members.Add(new Member { Number = "5", Character = "L", BgColor = (Brush)converter.ConvertFromString("#0CA678"), Name = "Lena Jones", Position = "Martínez", Email = "lena.offi@hotmail.com", Phone = "320-658-9174" });
            members.Add(new Member { Number = "6", Character = "B", BgColor = (Brush)converter.ConvertFromString("#6741D9"), Name = "Benjamin Caliword", Position = "Pérez", Email = "beni12@hotmail.com", Phone = "114-203-6258" });
            members.Add(new Member { Number = "7", Character = "S", BgColor = (Brush)converter.ConvertFromString("#FF6D00"), Name = "Sophia Muris", Position = "Sánchez", Email = "sophi.muri@gmail.com", Phone = "852-233-6854" });
            members.Add(new Member { Number = "8", Character = "A", BgColor = (Brush)converter.ConvertFromString("#FF5252"), Name = "Ali Pormand", Position = "Ramírez", Email = "alipor@yahoo.com", Phone = "968-378-4849" });
            members.Add(new Member { Number = "9", Character = "F", BgColor = (Brush)converter.ConvertFromString("#1E88E5"), Name = "Frank Underwood", Position = "Torres", Email = "frank@yahoo.com", Phone = "301-584-6966" });
            members.Add(new Member { Number = "10", Character = "S", BgColor = (Brush)converter.ConvertFromString("#0CA678"), Name = "Saeed Dasman", Position = "Vásquez", Email = "saeed.dasi@hotmail.com", Phone = "817-320-5052" });

            members.Add(new Member { Number = "11", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "John Doe", Position = "Castillo", Email = "john.doe@gmail.com", Phone = "415-954-1475" });
            members.Add(new Member { Number = "12", Character = "R", BgColor = (Brush)converter.ConvertFromString("#1E88E5"), Name = "Reza Alavi", Position = "Jiménez", Email = "reza110@hotmail.com", Phone = "254-451-7893" });
            members.Add(new Member { Number = "13", Character = "D", BgColor = (Brush)converter.ConvertFromString("#FF8F00"), Name = "Dennis Castillo", Position = "Morales", Email = "deny.cast@gmail.com", Phone = "125-520-0141" });
            members.Add(new Member { Number = "14", Character = "G", BgColor = (Brush)converter.ConvertFromString("#FF5252"), Name = "Gabriel Cox", Position = "Hernández", Email = "coxcox@gmail.com", Phone = "808-635-1221" });
            members.Add(new Member { Number = "15", Character = "L", BgColor = (Brush)converter.ConvertFromString("#0CA678"), Name = "Lena Jones", Position = "Ruiz", Email = "lena.offi@hotmail.com", Phone = "320-658-9174" });
            members.Add(new Member { Number = "16", Character = "B", BgColor = (Brush)converter.ConvertFromString("#6741D9"), Name = "Benjamin Caliword", Position = "Ortiz", Email = "beni12@hotmail.com", Phone = "114-203-6258" });
            members.Add(new Member { Number = "17", Character = "S", BgColor = (Brush)converter.ConvertFromString("#FF6D00"), Name = "Sophia Muris", Position = "Delgado", Email = "sophi.muri@gmail.com", Phone = "852-233-6854" });
            members.Add(new Member { Number = "18", Character = "A", BgColor = (Brush)converter.ConvertFromString("#FF5252"), Name = "Ali Pormand", Position = "Mendoza", Email = "alipor@yahoo.com", Phone = "968-378-4849" });
            members.Add(new Member { Number = "19", Character = "F", BgColor = (Brush)converter.ConvertFromString("#1E88E5"), Name = "Frank Underwood", Position = "Guerrero", Email = "frank@yahoo.com", Phone = "301-584-6966" });
            members.Add(new Member { Number = "20", Character = "S", BgColor = (Brush)converter.ConvertFromString("#0CA678"), Name = "Saeed Dasman", Position = "Castro", Email = "saeed.dasi@hotmail.com", Phone = "817-320-5052" });

            members.Add(new Member { Number = "21", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "John Doe", Position = "Rojas", Email = "john.doe@gmail.com", Phone = "415-954-1475" });
            members.Add(new Member { Number = "22", Character = "R", BgColor = (Brush)converter.ConvertFromString("#1E88E5"), Name = "Reza Alavi", Position = "Cabrera", Email = "reza110@hotmail.com", Phone = "254-451-7893" });
            members.Add(new Member { Number = "23", Character = "D", BgColor = (Brush)converter.ConvertFromString("#FF8F00"), Name = "Dennis Castillo", Position = "Navarro", Email = "deny.cast@gmail.com", Phone = "125-520-0141" });
            members.Add(new Member { Number = "24", Character = "G", BgColor = (Brush)converter.ConvertFromString("#FF5252"), Name = "Gabriel Cox", Position = "Molina", Email = "coxcox@gmail.com", Phone = "808-635-1221" });
            members.Add(new Member { Number = "25", Character = "L", BgColor = (Brush)converter.ConvertFromString("#0CA678"), Name = "Lena Jones", Position = "Silva", Email = "lena.offi@hotmail.com", Phone = "320-658-9174" });
            members.Add(new Member { Number = "26", Character = "B", BgColor = (Brush)converter.ConvertFromString("#6741D9"), Name = "Benjamin Caliword", Position = "Flores", Email = "beni12@hotmail.com", Phone = "114-203-6258" });
            members.Add(new Member { Number = "27", Character = "S", BgColor = (Brush)converter.ConvertFromString("#FF6D00"), Name = "Sophia Muris", Position = "Méndez", Email = "sophi.muri@gmail.com", Phone = "852-233-6854" });
            members.Add(new Member { Number = "28", Character = "A", BgColor = (Brush)converter.ConvertFromString("#FF5252"), Name = "Ali Pormand", Position = "Reyes", Email = "alipor@yahoo.com", Phone = "968-378-4849" });
            members.Add(new Member { Number = "29", Character = "F", BgColor = (Brush)converter.ConvertFromString("#1E88E5"), Name = "Frank Underwood", Position = "Romero", Email = "frank@yahoo.com", Phone = "301-584-6966" });
            members.Add(new Member { Number = "30", Character = "S", BgColor = (Brush)converter.ConvertFromString("#0CA678"), Name = "Saeed Dasman", Position = "Iglesias", Email = "saeed.dasi@hotmail.com", Phone = "817-320-5052" });

            membersDataGrid.ItemsSource = members;
            txtNumAlumnos.Text = GetAlumnosActuales().ToString();
            selectedMember = new Member();
        }

        private int GetAlumnosActuales()
        {
            return membersDataGrid.Items.Count;
        }

        private void membersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedMember = membersDataGrid.SelectedItem as Member;
        }

        public class Member
        {
            public string Number { get; set; }
            public string Character { get; set; }
            public Brush BgColor { get; set; }
            public string Name { get; set; }
            public string Position { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public bool IsSelected { get; set; }
        }

        private void btnEditarAlumno_Click(object sender, RoutedEventArgs e)
        {
            FormModAlumno formModAlumno = new FormModAlumno();

            formModAlumno.txtNombre.Text = selectedMember.Name;
            formModAlumno.txtApellido.Text = selectedMember.Position;
            formModAlumno.txtCorreo.Text = selectedMember.Email;

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
                members.Remove(selectedMember);
                // Se hace en memoria para probar
                // Implementar logica en base de datos
            }
        }


        // Arreglar checked

        private void checkAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var member in members)
            {
                member.IsSelected = true;
            }
            membersDataGrid.Items.Refresh();
        }

        private void checkAll_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var member in members)
            {
                member.IsSelected = false;
            }
            membersDataGrid.Items.Refresh();
        }

    }
}
