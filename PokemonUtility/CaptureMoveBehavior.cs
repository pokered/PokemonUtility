using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace PokemonUtility
{
    class CaptureMoveBehavior : Behavior<Button>
    {
        protected override void OnAttached()
        {
            // AssociatedObjectのイベントを購読する
            AssociatedObject.PreviewMouseLeftButtonDown += MouseMove;
        }

        protected override void OnDetaching()
        {
            // イベントの購読解除
            AssociatedObject.PreviewMouseLeftButtonDown += MouseMove;
        }

        // イベントで処理をする
        private void ButtonClicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello world");
        }

        // イベントで処理をする
        private void MouseMove(object sender, MouseButtonEventArgs e)
        {
            UIElement el = sender as UIElement;
            Point point = e.GetPosition(el);
            MessageBox.Show(point.ToString());
        }
    }
}
