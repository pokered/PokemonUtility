using System.Windows;

namespace PokemonUtility.Views
{
    /// <summary>
    /// Interaction logic for CaptureWindow.xaml
    /// </summary>
    public partial class CaptureWindow : Window
    {
        public CaptureWindow()
        {
            InitializeComponent();

            this.DragMove();
        }
    }
}
