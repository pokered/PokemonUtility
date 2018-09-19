using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Threading;

namespace PokemonUtility.TriggerActions
{
    class WindowReDisplayAction : TriggerAction<Image>
    {
        /// <summary>Windowを「閉じる」アクションクラス</summary>
        protected override void Invoke(object parameter)
        {
            if (AssociatedObject == null) return;

            //AssociatedObject.UpdateLayout();

            //DispatcherFrame frame = new DispatcherFrame();
            //var callback = new DispatcherOperationCallback(obj =>
            //{
            //    ((DispatcherFrame)obj).Continue = false;
            //    return null;
            //});
            //Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, callback, frame);
            //Dispatcher.PushFrame(frame);
        }
    }
}
