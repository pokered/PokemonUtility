using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Threading;

namespace PokemonUtility.Behaviors
{
    class MyPartyBehavior : Behavior<Image>
    {
        protected override void OnAttached()
        {
            // AssociatedObjectのイベントを購読する
            AssociatedObject.SourceUpdated += DoEvent1;
        }

        protected override void OnDetaching()
        {
            // イベントの購読解除
            AssociatedObject.SourceUpdated -= DoEvent1;
        }

        private void DoEvent1(object sender, EventArgs e)
        {
            if (AssociatedObject == null) return;

            //AssociatedObject.UpdateLayout();

            DispatcherFrame frame = new DispatcherFrame();
            var callback = new DispatcherOperationCallback(obj =>
            {
                ((DispatcherFrame)obj).Continue = false;
                return null;
            });
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, callback, frame);
            Dispatcher.PushFrame(frame);
        }
    }
}
