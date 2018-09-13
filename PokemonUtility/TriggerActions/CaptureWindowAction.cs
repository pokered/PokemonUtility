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
            // モーダル表示
            IsModal = true;

            // キャプチャ画面生成
            return new CaptureWindow() {};
        }
        
        protected override void ApplyNotificationToWindow(Window window, INotification notification)
        {
            RectangleNotification rec = ((RectangleNotification)notification);
            CaptureWindowViewModel vm = (CaptureWindowViewModel)window.DataContext;

            FitWithinScreen(ref window, ref rec);

            if (window.WindowStartupLocation == System.Windows.WindowStartupLocation.Manual)
            {
                window.Left = rec.X;
                window.Top = rec.Y;
            }
            
            window.Width = rec.Width;
            window.Height = rec.Height;
        }

        protected override void ApplyWindowToNotification(Window windown, INotification notification)
        {
            RectangleNotification rec = ((RectangleNotification)notification);

            rec.X = (int)windown.Left;
            rec.Y = (int)windown.Top;
            rec.Width = (int)windown.Width;
            rec.Height = (int)windown.Height;
        }

        // ウィンドウをスクリーン内に収める
        private void FitWithinScreen(ref Window window, ref RectangleNotification CaptureRect)
        {
            // スクリーンの矩形情報
            Rect screenRect = new Rect();
            screenRect.Width = (int)SystemParameters.PrimaryScreenWidth;
            screenRect.Height = (int)SystemParameters.PrimaryScreenHeight;
            
            // キャプチャ範囲がスクリーン外の場合中央に表示する
            Point captureLeftTop = new Point(CaptureRect.X, CaptureRect.Y);
            Point captureRightBottom = new Point(CaptureRect.X + CaptureRect.Width, CaptureRect.Y + CaptureRect.Height);

            if (!screenRect.Contains(captureLeftTop) || !screenRect.Contains(captureRightBottom))
            {
                window.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            }
        }
    }
}
