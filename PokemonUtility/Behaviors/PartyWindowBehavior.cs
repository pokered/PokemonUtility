using PokemonUtility.Models.Manegement;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace PokemonUtility.Behaviors
{
    class PartyWindowBehavior : Behavior<Image>
    {
        PartyManegementModel _partyManegementModel;

        protected override void OnAttached()
        {
            AssociatedObject.PreviewMouseDown += ChangeOrder;

            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewMouseDown -= ChangeOrder;

            base.OnAttached();
        }

        public PartyManegementModel()
        { }

        // 選出順変更
        private void ChangeOrder(object sender, EventArgs e)
        {
            MyPartyManegementModel aaa = MyPartyManegementModel.GetInstance();
        }
    }
}
