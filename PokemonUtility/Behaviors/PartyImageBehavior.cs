using PokemonUtility.Models.Manegement;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace PokemonUtility.Behaviors
{
    class PartyImageBehavior : Behavior<Image>
    {
        PartyManegementModel _partyManegementModel;

        public PartyImageBehavior(PartyManegementModel partyManegementModel)
        {
            _partyManegementModel = partyManegementModel;
        }

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

        // 選出順変更
        private void ChangeOrder(object sender, EventArgs e)
        {
            //_partyManegementModel.ChangeOrder(0);
        }
    }
}
