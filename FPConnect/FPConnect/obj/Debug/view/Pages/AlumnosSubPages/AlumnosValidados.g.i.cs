﻿#pragma checksum "..\..\..\..\..\view\Pages\AlumnosSubPages\AlumnosValidados.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "51913B22FCC47360AA56039660E66B80284B97F40E7F7F5C1C826F319F6AABC8"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using FPConnect.view.Pages.AlumnosSubPages;
using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
using MaterialDesignThemes.MahApps;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace FPConnect.view.Pages.AlumnosSubPages {
    
    
    /// <summary>
    /// AlumnosValidados
    /// </summary>
    public partial class AlumnosValidados : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 26 "..\..\..\..\..\view\Pages\AlumnosSubPages\AlumnosValidados.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtNumAlumnos;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\..\view\Pages\AlumnosSubPages\AlumnosValidados.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbConvocatoria;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\..\view\Pages\AlumnosSubPages\AlumnosValidados.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCargar;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\..\view\Pages\AlumnosSubPages\AlumnosValidados.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid membersDataGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FPConnect;component/view/pages/alumnossubpages/alumnosvalidados.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\view\Pages\AlumnosSubPages\AlumnosValidados.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.txtNumAlumnos = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.cbConvocatoria = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.btnCargar = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.membersDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 29 "..\..\..\..\..\view\Pages\AlumnosSubPages\AlumnosValidados.xaml"
            this.membersDataGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.membersDataGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 5:
            
            #line 34 "..\..\..\..\..\view\Pages\AlumnosSubPages\AlumnosValidados.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.checkAll_Checked);
            
            #line default
            #line hidden
            
            #line 34 "..\..\..\..\..\view\Pages\AlumnosSubPages\AlumnosValidados.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.checkAll_Unchecked);
            
            #line default
            #line hidden
            break;
            case 6:
            
            #line 59 "..\..\..\..\..\view\Pages\AlumnosSubPages\AlumnosValidados.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnEditarAlumno_Click);
            
            #line default
            #line hidden
            break;
            case 7:
            
            #line 64 "..\..\..\..\..\view\Pages\AlumnosSubPages\AlumnosValidados.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnEliminarAlumno_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

