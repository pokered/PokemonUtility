using PokemonUtility.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace PokemonUtility.Behaviors
{
    class BattleHistoryBehavior : Behavior<Image>
    {
        // 依存関係プロパティの登録
        public static readonly DependencyProperty pokemonIndexProperty =
            DependencyProperty.Register("PartyIndex", typeof(int), typeof(OpponentPartyImageBehavior),
                                        new UIPropertyMetadata(null));

        // 登録される依存関係プロパティ
        public int PartyIndex
        {
            get { return (int)GetValue(pokemonIndexProperty); }
            set { SetValue(pokemonIndexProperty, value); }
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
            PokemonSearchWindow window = new PokemonSearchWindow();
            window.ShowDialog();


        }


    }
}
