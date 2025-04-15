using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using FontAwesome.WPF;

namespace FPConnect.view.UserControls.Eventos
{
    public partial class Item : UserControl
    {
        // Eventos que serán disparados cuando se haga clic en los botones
        public event EventHandler<int> EventoCompletado;
        public event EventHandler<int> EventoBorrado;

        // ID del evento asociado a este item
        public int IdEvento { get; set; }

        public Item()
        {
            InitializeComponent();
            this.Loaded += Item_Loaded;
        }

        private void Item_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btnHecho != null && btnBorrar != null)
                {
                    // Desuscribir y volver a suscribir
                    btnHecho.MenuButtonClicked -= BtnHecho_MenuButtonClicked;
                    btnHecho.MenuButtonClicked += BtnHecho_MenuButtonClicked;

                    btnBorrar.MenuButtonClicked -= BtnBorrar_MenuButtonClicked;
                    btnBorrar.MenuButtonClicked += BtnBorrar_MenuButtonClicked;

                    // Solo suscribir una vez
                    this.Loaded -= Item_Loaded;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al suscribirse a eventos: {ex.Message}");
            }
        }



        private void BtnHecho_MenuButtonClicked(object sender, EventArgs e)
        {
            Console.WriteLine($"BtnHecho_MenuButtonClicked: ID={IdEvento}");
            EventoCompletado?.Invoke(this, IdEvento);
            btnMenu.IsChecked = false;
        }

        private void BtnBorrar_MenuButtonClicked(object sender, EventArgs e)
        {
            Console.WriteLine($"BtnBorrar_MenuButtonClicked: ID={IdEvento}");
            EventoBorrado?.Invoke(this, IdEvento);
            btnMenu.IsChecked = false;
        }

        // Propiedades existentes
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(Item));

        public string Time
        {
            get { return (string)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(string), typeof(Item));

        public SolidColorBrush Color
        {
            get { return (SolidColorBrush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(SolidColorBrush), typeof(Item));

        public FontAwesomeIcon Icon
        {
            get { return (FontAwesomeIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(FontAwesomeIcon), typeof(Item));

        public FontAwesomeIcon IconBell
        {
            get { return (FontAwesomeIcon)GetValue(IconBellProperty); }
            set { SetValue(IconBellProperty, value); }
        }

        public static readonly DependencyProperty IconBellProperty = DependencyProperty.Register("IconBell", typeof(FontAwesomeIcon), typeof(Item));

        public DateTime EventDate
        {
            get { return (DateTime)GetValue(EventDateProperty); }
            set { SetValue(EventDateProperty, value); }
        }

        public static readonly DependencyProperty EventDateProperty = DependencyProperty.Register("EventDate", typeof(DateTime), typeof(Item));
    }
}
