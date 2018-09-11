using PokemonUtility.Views;
using Prism.Interactivity.InteractionRequest;
using System.Windows;

namespace PokemonUtility.TriggerActions
{
    abstract class PartyWindowAction : PopupWindowActionBase
    {
        protected override void ApplyWindowToNotification(Window windown, INotification notification)
        {
        }
    }

    class MyPartyWindowAction : PartyWindowAction
    {
        protected override Window CreateWindow(INotification notification)
        {
            // モーダル表示
            IsModal = true;

            // キャプチャ画面生成
            return new MyPartyWindow() { };
        }
    }
}
