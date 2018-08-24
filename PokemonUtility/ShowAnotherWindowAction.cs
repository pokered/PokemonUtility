using PokemonUtility.ViewModels;
using PokemonUtility.Views;
using Prism.Interactivity.InteractionRequest;
using System.Windows;
using System.Windows.Interactivity;

namespace PokemonUtility
{
    class ShowAnotherWindowAction : TriggerAction<DependencyObject>
    {
        protected override void Invoke(object parameter)
        {
            var args = parameter as InteractionRequestedEventArgs;
            //var vm = args.Context.Content;
            var vm = args.Context as CaptureWindowViewModel;
            var callerWindow = this.AssociatedObject as Window;

            //親ウィンドウ非表示
            callerWindow.Hide();

            //別ウィンドウを生成、表示
            new CaptureWindow() { DataContext = vm }.ShowDialog();

            //親ウィンドウ再表示
            callerWindow.Show();

            //呼び出しが終わったことを伝えるコールバック
            args.Callback.Invoke();
        }
    }
}
