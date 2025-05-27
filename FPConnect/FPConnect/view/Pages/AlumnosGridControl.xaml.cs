using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FPConnect.domain;
using FPConnect.HelperClasses;
using FPConnect.view.Pages.AlumnosSubPages;
using FPConnect.view.Pages.Forms;
using FPConnect.view.Pages.Forms.Empresas;
using Microsoft.SqlServer.Server;

namespace FPConnect.view.Pages
{
    /// <summary>
    /// Lógica de interacción para AlumnosGridControl.xaml
    /// </summary>
    public partial class AlumnosGridControl : Page
    {
       
        public bool IsAlumnosButtonPressed { get; set; }
        public bool IsArchivadosButtonPressed { get; set; }
        public bool IsInfoButtonPressed { get; set; }
        public AlumnosGridControl()
        {
            InitializeComponent();
            
            mainFrameA.Source = new Uri("AlumnosSubPages/AlumnosActuales.xaml", UriKind.Relative);

        }

        private void btnAlumnosActual_Click(object sender, RoutedEventArgs e)
        {
            mainFrameA.Source = new Uri("AlumnosSubPages/AlumnosActuales.xaml", UriKind.Relative);
            btnAddAlumno.Visibility = Visibility.Visible;

            IsArchivadosButtonPressed = false;
            IsAlumnosButtonPressed = true;
            IsInfoButtonPressed = false;

            UpdateButtonStyles();
        }

        private void btnArchivados_Click(object sender, RoutedEventArgs e)
        {
            mainFrameA.Source = new Uri("AlumnosSubPages/Archivados.xaml", UriKind.Relative);
            btnAddAlumno.Visibility = Visibility.Collapsed;

            IsArchivadosButtonPressed = true;
            IsAlumnosButtonPressed = false;
            IsInfoButtonPressed = false;

            UpdateButtonStyles();
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            mainFrameA.Source = new Uri("AlumnosSubPages/AlumnosValidados.xaml", UriKind.Relative);
            btnAddAlumno.Visibility = Visibility.Collapsed;

            IsInfoButtonPressed = true;
            IsArchivadosButtonPressed = false;
            IsAlumnosButtonPressed = false;

            UpdateButtonStyles();
        }
        private void UpdateButtonStyles()
        {

            btnAlumnosActual.Style = (Style)FindResource(IsAlumnosButtonPressed ? "tabButtonPressed" : "tabButton");
            btnArchivados.Style = (Style)FindResource(IsArchivadosButtonPressed ? "tabButtonPressed" : "tabButton");
            btnInfo.Style = (Style)FindResource(IsInfoButtonPressed ? "tabButtonPressed" : "tabButton");
        }

        private void btnAddAlumno_Click(object sender, RoutedEventArgs e)
        {
            Alumno nuevoAlumno = new Alumno();
            int tipoOperacion = 1; // 1 add, 2 mod
            FormAddAlumno formAddEmpresa = new FormAddAlumno(nuevoAlumno, tipoOperacion);
            formAddEmpresa.ShowDialog();
            if (formAddEmpresa.DialogResult == true)
            {               

            }

        }
    }
}

