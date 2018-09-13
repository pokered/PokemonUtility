using System.Windows;
using System.Windows.Interactivity;

namespace PokemonUtility.Behaviors
{
    class MainWIndowBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            // AssociatedObjectのイベントを購読する
            //AssociatedObject.Closed += LeftButtonClicked;
        }

        protected override void OnDetaching()
        {
            // イベントの購読解除
            //AssociatedObject.PreviewMouseLeftButtonDown -= LeftButtonClicked;
        }


    }
}
