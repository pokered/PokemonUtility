using PokemonUtility.Models;
using PokemonUtility.ViewModels;
using PokemonUtility.Views;
using Prism.Interactivity.InteractionRequest;
using System.Windows;

namespace PokemonUtility.TriggerActions
{
    class CaptureWindowAction : PopupWindowActionBase
    {
        protected override Window CreateWindow(INotification notification)
        {
            return new CaptureWindow() { };
        }

        protected override void ApplyNotificationToWindow(Window window, INotification notification)
        {
            RectangleNotification rec = ((RectangleNotification)notification);
            CaptureWindowViewModel vm = (CaptureWindowViewModel)window.DataContext;
            window.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
            //vm.X = rec.X;
            //vm.Y = rec.Y;
            //vm.Width = rec.Width;
            //vm.Height = rec.Height;
            window.Left = rec.X;
            window.Top = rec.Y;
            window.Width = rec.Width;
            window.Height = rec.Height;
        }

        protected override void ApplyWindowToNotification(Window windown, INotification notification)
        {
            RectangleNotification rec = ((RectangleNotification)notification);
            CaptureWindowViewModel vm = (CaptureWindowViewModel)windown.DataContext;
            rec.X = vm.X;
            rec.Y = vm.Y;
            rec.Width = vm.Width;
            rec.Height = vm.Height;
        }
    }
}
