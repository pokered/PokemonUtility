using System.Windows;
using System.Windows.Interactivity;

namespace PokemonUtility
{
    class MainMoveBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            // AssociatedObjectのイベントを購読する
            this.AssociatedObject.MouseLeftButtonDown += this.ButtonClicked;
        }

        protected override void OnDetaching()
        {
            // イベントの購読解除
            this.AssociatedObject.MouseLeftButtonDown += this.ButtonClicked;
        }

        // イベントで処理をする
        private void ButtonClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("aaa");
        }
    }

}
