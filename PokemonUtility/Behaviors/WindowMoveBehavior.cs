using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace PokemonUtility.Behaviors
{
    class WindowMoveBehavior : Behavior<Window>
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

        // イベントで処理をする
        private void LeftButtonClicked(object sender, MouseButtonEventArgs e)
        {
            //マウスボタン押下状態でなければ何もしない
            if (e.ButtonState != MouseButtonState.Pressed) return;

            UIElement el = sender as UIElement;
            mousePoint = e.GetPosition(el);



            //var win = (Window)AssociatedObject;
            //win.Left = 100;


            //MessageBox.Show(point.ToString());

            //位置を記憶する
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            //マウスボタン押下状態でなければ何もしない
            if (e.LeftButton != MouseButtonState.Pressed) return;

            UIElement el = sender as UIElement;
            Point point = e.GetPosition(el);

            AssociatedObject.Left += e.GetPosition(el).X - mousePoint.X;
            AssociatedObject.Top += e.GetPosition(el).Y - mousePoint.Y;
        }
    }
}
