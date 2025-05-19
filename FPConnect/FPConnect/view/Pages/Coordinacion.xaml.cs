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

        }
    }
}
