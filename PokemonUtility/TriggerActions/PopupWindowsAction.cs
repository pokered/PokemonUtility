using PokemonUtility.Models.Notifications;
using PokemonUtility.ViewModels;
using PokemonUtility.Views;
using Prism.Interactivity.InteractionRequest;
using System.Windows;

namespace PokemonUtility.TriggerActions
{
    class PopupWindowAction : PopupWindowActionBase
    {
        protected override Window CreateWindow(INotification notification)
        {
            WindowNotification windowNotification = (WindowNotification)notification;

            // モーダル
            IsModal = windowNotification.IsModal;

            // 生成するウィンドウ
            if (windowNotification.WindowId == WindowNotification.MAIN_WINDOW) return new MainWindow();
            if (windowNotification.WindowId == WindowNotification.CAPTURE_WINDOW) return new CaptureWindow();
            if (windowNotification.WindowId == WindowNotification.MY_PARTY_WINDOW) return new MyPartyWindow();
            if (windowNotification.WindowId == WindowNotification.OPPONENT_PARTY_WINDOW) return new OpponentPartyWindow();
            if (windowNotification.WindowId == WindowNotification.TODAY_BATTLE_RECORD_WINDOW) return new TodayBattleRecordWindow();
            if (windowNotification.WindowId == WindowNotification.BATTLE_HISTORY_WINDOW) return new BattleHistoryWindow();

            //TODO エラー画面を表示する？
            return new Window();
        }

        protected override void ApplyNotificationToWindow(Window window, INotification notification)
        {
        }
    }
}
