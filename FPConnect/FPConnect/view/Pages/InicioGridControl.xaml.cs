
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web.UI.WebControls;
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


namespace DataGrid.view
{
    /// <summary>
    /// Lógica de interacción para InicioGridControl.xaml
    /// </summary>
    public partial class InicioGridControl : Page
    {
        
        private Centro operacionesCentro;
        public InicioGridControl()
        {
            InitializeComponent();
            operacionesCentro = new Centro();
            //Console.WriteLine("Id centro" + SesionUsuario.IdCentro);
            Centro nuevoCentro = new Centro();
            nuevoCentro = operacionesCentro.LeerCentroPorId(SesionUsuario.IdCentro);

            txtDireccion.Text = nuevoCentro.direccion;
            txtHorario.Text = nuevoCentro.horario;
            txtTelefono.Text = nuevoCentro.telefono;
            logo.Source = nuevoCentro.logo;

        }
        
    }
}
