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
            Window window = new MyPartyWindow() { };

            // 位置設定
            window.Left = Properties.Settings.Default.MyPartyWindowX;
            window.Top = Properties.Settings.Default.MyPartyWindowY;
            
            return window;
        }

        protected override void ApplyNotificationToWindow(Window window, INotification notification)
        {
        }

        protected override void ApplyWindowToNotification(Window windown, INotification notification)
        {
            // 位置保存
            Properties.Settings.Default.MyPartyWindowX = windown.Left;
            Properties.Settings.Default.MyPartyWindowY = windown.Top;

            // ファイルに保存
            Properties.Settings.Default.Save();
        }
    }
}
