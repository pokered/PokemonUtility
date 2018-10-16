using System;
using System.Windows.Media.Imaging;

namespace PokemonUtility.Models.BattleHistory
{
    class BattleHistroyRow
    {
        public BitmapImage MyPokemonImage0 { get; set; }
        public BitmapImage MyPokemonImage1 { get; set; }
        public BitmapImage MyPokemonImage2 { get; set; }
        public BitmapImage MyPokemonImage3 { get; set; }
        public BitmapImage MyPokemonImage4 { get; set; }
        public BitmapImage MyPokemonImage5 { get; set; }

        public BitmapImage OpponentPokemonImage0 { get; set; }
        public BitmapImage OpponentPokemonImage1 { get; set; }
        public BitmapImage OpponentPokemonImage2 { get; set; }
        public BitmapImage OpponentPokemonImage3 { get; set; }
        public BitmapImage OpponentPokemonImage4 { get; set; }
        public BitmapImage OpponentPokemonImage5 { get; set; }

        public string BattleResult { get; set; }
        public DateTime BattleDate { get; set; }
    }
}
