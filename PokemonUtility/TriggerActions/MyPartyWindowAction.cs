using PokemonUtility.Views;
using Prism.Interactivity.InteractionRequest;
using System.Windows;

namespace PokemonUtility.TriggerActions
{
    class MyPartyWindowAction : PopupWindowActionBase
    {
        protected override Window CreateWindow(INotification notification)
        {
            // モーダル表示
            IsModal = false;

            // 画面生成
            return new MyPartyWindow() { };
        }

        protected override void ApplyNotificationToWindow(Window window, INotification notification)
        {
        }
    }
}
