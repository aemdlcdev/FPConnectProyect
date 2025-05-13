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
using System.Windows.Shapes;
using FPConnect.domain;
using FPConnect.HelperClasses;

namespace FPConnect.view.Pages.Forms.Perfiles
{
    /// <summary>
    /// Lógica de interacción para FormAddUsuario.xaml
    /// </summary>
    public partial class FormModPerfil : Window
    {
        // Perfil
        public Perfil perfilSeleccionado { get; set; }
        
        // Grados
        private Grado grado { get; set; }
        private ObservableCollection<Grado> listaGrados;
        // Familias
        private FamiliaProfesional familia;
        private ObservableCollection<FamiliaProfesional> listaFamilias;
        public FormModPerfil()
        {
            InitializeComponent();
        }

        public FormModPerfil(Perfil perfil)
        {
            InitializeComponent();
            perfilSeleccionado = perfil;

            familia = new FamiliaProfesional();
            grado = new Grado();

            if (perfilSeleccionado != null)
            {
                listaFamilias = new ObservableCollection<FamiliaProfesional>();
                familia = new FamiliaProfesional();
                listaFamilias = familia.LeerFamiliasCentro(SesionUsuario.IdCentro);
                listaGrados = grado.LeerGradosPorCentro(SesionUsuario.IdCentro);

                foreach (FamiliaProfesional fam in listaFamilias)
                {
                    cbFamilia.Items.Add(fam);
                }

                foreach (Grado grad in listaGrados)
                {
                    cbGrado.Items.Add(grad);
                }

                txtNombre.Text = perfilSeleccionado.nombre;
                
                cbFamilia.SelectedItem = listaFamilias.FirstOrDefault(f => f.id_familia == perfilSeleccionado.id_familia);
                cbGrado.SelectedItem = listaGrados.FirstOrDefault(g => g.id_grado == perfilSeleccionado.id_grado);
            }
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            
            if (perfilSeleccionado != null)
            {
                
                perfilSeleccionado.nombre = txtNombre.Text;
                perfilSeleccionado.id_familia = ((FamiliaProfesional)cbFamilia.SelectedItem).id_familia;
                perfilSeleccionado.id_grado = ((Grado)cbGrado.SelectedItem).id_grado;

            }

            this.DialogResult = true;
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }

}
