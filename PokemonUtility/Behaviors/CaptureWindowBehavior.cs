using System.Windows.Input;

namespace PokemonUtility.Behaviors
{
    public class CaptureWindowBehavior : WindowMoveBehavior
    {
        protected override void OnAttached()
        {
            AssociatedObject.PreviewMouseRightButtonDown += RightButtonClicked;

            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewMouseRightButtonDown -= RightButtonClicked;

            base.OnDetaching();
        }

        // ウィンドウを閉じる
        private void RightButtonClicked(object sender, MouseEventArgs e)
        {
            // 右クリックでなければ何もしない
            if (e.RightButton != MouseButtonState.Pressed) return;

            AssociatedObject.Close();
        }
    }
}
