using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace PokemonUtility.Behaviors
{
    class CloseWindowBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            AssociatedObject.PreviewMouseDoubleClick += CloseWindow;

            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewMouseDoubleClick -= CloseWindow;

            base.OnDetaching();
        }

        // ウィンドウを閉じる
        private void CloseWindow(object sender, MouseEventArgs e)
        {
            // 左ダブルクリックでなければ何もしない
            if (e.LeftButton != MouseButtonState.Pressed) return;

            AssociatedObject.Close();
        }
    }
}
