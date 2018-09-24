using PokemonUtility.Views;
using Prism.Interactivity.InteractionRequest;
using System.Windows;

namespace PokemonUtility.TriggerActions
{
    class OpponentPartyWindowAction : PopupWindowActionBase
    {
        protected override Window CreateWindow(INotification notification)
        {
            // モーダル表示
            IsModal = false;

            // 画面生成
            return new OpponentPartyWindow() { };
        }

        protected override void ApplyNotificationToWindow(Window window, INotification notification)
        {
        }
    }
}
