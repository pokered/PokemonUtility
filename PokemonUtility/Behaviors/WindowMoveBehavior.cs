using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace PokemonUtility.Behaviors
{
    public class WindowMoveBehavior : Behavior<Window>
    {
        private Point mousePoint;
        protected override void OnAttached()
        {
            // AssociatedObjectのイベントを購読する
            AssociatedObject.PreviewMouseLeftButtonDown += LeftButtonClicked;
            AssociatedObject.PreviewMouseMove += MouseMove;
        }

        protected override void OnDetaching()
        {
            // イベントの購読解除
            AssociatedObject.PreviewMouseLeftButtonDown -= LeftButtonClicked;
            AssociatedObject.PreviewMouseMove -= MouseMove;
        }

        // ウィンドウの位置を記憶する
        private void LeftButtonClicked(object sender, MouseEventArgs e)
        {
            //マウスボタン押下状態でなければ何もしない
            if (e.LeftButton != MouseButtonState.Pressed) return;

            UIElement el = sender as UIElement;
            mousePoint = e.GetPosition(el);
        }

        // ウィンドウを移動させる
        private void MouseMove(object sender, MouseEventArgs e)
        {
            // マウスボタン押下状態でなければ何もしない
            if (e.LeftButton != MouseButtonState.Pressed) return;

            UIElement el = sender as UIElement;
            Point point = e.GetPosition(el);

            AssociatedObject.Left += e.GetPosition(el).X - mousePoint.X;
            AssociatedObject.Top += e.GetPosition(el).Y - mousePoint.Y;
        }
    }
}
