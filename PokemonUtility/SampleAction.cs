using PokemonUtility.Views;
using Prism.Interactivity.InteractionRequest;
using System.Windows;

namespace PokemonUtility
{
    class SampleAction : PopupWindowActionBase
    {
        protected override Window CreateWindow(INotification notification)
        {
            return new CaptureWindow() { Title = notification.Title };
        }

        protected override void ApplyWindowToNotification(Window windown, INotification notification)
        {
            MessageBox.Show("aa");
        }
    }
}
