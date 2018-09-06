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

        // ウィンドウをスクリーン内に収める
        private void FitWithinScreen(ref Window window, ref RectangleNotification rec)
        {
            // 初期値の場合は中央にデフォルトサイズで表示
            if (rec.X == 0 && rec.Y == 0 && rec.Width == 0 && rec.Height == 0)
            {
                window.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                rec.Width = 640;
                rec.Height = 360;
            }

            // 領域内に収める
            Rect re = new Rect();
            //double aa = SystemParameters.PrimaryScreenWidth;

            // width
            if (rec.Width < 50) rec.Width = 50;

            // height
            if (rec.Width < 50) rec.Width = 50;

        }
    }
}
