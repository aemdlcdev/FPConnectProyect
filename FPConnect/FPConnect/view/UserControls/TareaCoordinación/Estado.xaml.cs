using System.Windows.Controls;
using System.Windows.Media;

namespace FPConnect.view.UserControls.TareaCoordiacion
{
    public partial class Estado : UserControl
    {
        public Estado()
        {
            InitializeComponent();
        }

        public Estado(int estado)
        {
            InitializeComponent();
            if (estado == 1) // Por hacer
            {
                bColor.Background = (Brush)new BrushConverter().ConvertFromString("#FFD700"); // Amarillo
                txtEstado.Text = "Pendiente";
            }
            else if (estado == 2) // Completado
            {
                bColor.Background = (Brush)new BrushConverter().ConvertFromString("#1E90FF"); // Azul
                txtEstado.Text = "En progreso";
            }
            else if (estado == 3)
            {
                bColor.Background = (Brush)new BrushConverter().ConvertFromString("#32CD32"); // Verde
                txtEstado.Text = "Completado";
            }
        }
    }
}