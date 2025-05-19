using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FPConnect.view.UserControls.TareaCoordiacion
{
    public partial class Estado : UserControl
    {
        public static readonly DependencyProperty EstadoAProperty =
            DependencyProperty.Register(
                "EstadoA",
                typeof(int),
                typeof(Estado),
                new PropertyMetadata(0, OnEstadoAChanged));

        public int EstadoA
        {
            get { return (int)GetValue(EstadoAProperty); }
            set { SetValue(EstadoAProperty, value); }
        }

        public Estado()
        {
            InitializeComponent();
            ActualizarVisualizacion();
        }

        private static void OnEstadoAChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (Estado)d;
            control.ActualizarVisualizacion();
        }

        private void ActualizarVisualizacion()
        {
            // Lógica para cambiar la apariencia según el estado
            // Por ejemplo:
            switch (EstadoA)
            {
                case 1:
                    // Pendiente
                    bColor.Background = (Brush)new BrushConverter().ConvertFromString("#FFD700"); // Amarillo
                    txtEstado.Text = "Pendiente";
                    break;
                case 2:
                    // En proceso
                    bColor.Background = (Brush)new BrushConverter().ConvertFromString("#1E90FF"); // Azul
                    txtEstado.Text = "En progreso";
                    break;
                case 3:
                    // Completado
                    bColor.Background = (Brush)new BrushConverter().ConvertFromString("#32CD32"); // Verde
                    txtEstado.Text = "Completado";
                    break;
            }
        }


    }
}