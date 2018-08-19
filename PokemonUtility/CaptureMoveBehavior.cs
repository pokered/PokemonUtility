using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this.AssociatedObject.Click += this.ButtonClicked;
        }

        protected override void OnDetaching()
        {
            // イベントの購読解除
            this.AssociatedObject.Click += this.ButtonClicked;
        }

        // イベントで処理をする
        private void ButtonClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("Hello world");
        }
    }

    class CaptureMoveBehavior2 : Behavior<Border>
    {
        protected override void OnAttached()
        {
            // AssociatedObjectのイベントを購読する
            this.AssociatedObject.MouseDown += this.ButtonClicked;
        }

        // イベントで処理をする
        private void ButtonClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("Hello world");
        }
    }
}
