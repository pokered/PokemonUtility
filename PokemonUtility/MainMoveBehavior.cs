using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace PokemonUtility
{
    class MainMoveBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            // AssociatedObjectのイベントを購読する
            this.AssociatedObject.PreviewMouseDown += this.ButtonClicked;
        }

        protected override void OnDetaching()
        {
            // イベントの購読解除
            this.AssociatedObject.PreviewMouseDown += this.ButtonClicked;
        }

        // イベントで処理をする
        private void ButtonClicked(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("aaa");
        }
    }

}
