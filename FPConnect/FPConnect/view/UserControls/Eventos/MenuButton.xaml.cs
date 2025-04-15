using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FontAwesome.WPF;

namespace FPConnect.view.UserControls.Eventos
{
    public partial class MenuButton : UserControl
    {
        // Evento que se activará cuando se haga clic en el MenuButton
        public event EventHandler MenuButtonClicked;

        public MenuButton()
        {
            InitializeComponent();

            // Es importante acceder al botón real dentro del template
            this.Loaded += (s, e) =>
            {
                if (this.Content is Button button)
                {
                    // Primero eliminar cualquier suscripción anterior
                    button.Click -= Button_Click;
                    button.Click += Button_Click;
                }
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Marcar el evento como manejado para evitar propagación
            e.Handled = true;

            // Notificar solo una vez
            MenuButtonClicked?.Invoke(this, EventArgs.Empty);
        }



        public FontAwesomeIcon Icon
        {
            get { return (FontAwesomeIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(FontAwesomeIcon), typeof(MenuButton));

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof(string), typeof(MenuButton));
    }

}
