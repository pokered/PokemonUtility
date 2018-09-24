using System;
using System.Windows;
using System.Windows.Interactivity;

namespace PokemonUtility.Behaviors
{
    class FitWithinScreenBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Loaded += FitWithinScreen;

            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= FitWithinScreen;

            base.OnDetaching();
        }

        // ウィンドウをモニター内に収める
        private void FitWithinScreen(object sender, EventArgs e)
        {
            Rect screenRect = new Rect(0, 0, SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight);

            Point rightBottom = new Point(AssociatedObject.Left + AssociatedObject.Width, AssociatedObject.Top + AssociatedObject.Height);

            if (!screenRect.Contains(rightBottom))
            {
                AssociatedObject.Left -= rightBottom.X - screenRect.Width;
                AssociatedObject.Top -= rightBottom.Y - screenRect.Height;
            }

            if (!screenRect.Contains(new Point(AssociatedObject.Left, AssociatedObject.Top)))
            {
                AssociatedObject.Left = 0;
                AssociatedObject.Top = 0;
            }
        }
    }
}
