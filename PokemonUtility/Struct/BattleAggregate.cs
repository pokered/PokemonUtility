using PokemonUtility.Models;
using System;
using System.Windows.Media.Imaging;

namespace PokemonUtility.Struct
{
    class BattleAggregate
    {
        public int PokemonIconId { get; set; }
        public int OverlapNumber { get; set; }
        public int ElectionNumber { get; set; }
        public int LeadNumber { get; set; }
        public int WinNumber { get; set; }

        public BitmapImage PokemonImage
        {
            get { return ImageFactoryModel.CreatePokemonImage(PokemonIconId, ImageFactoryModel.ORDER_NO); }
        }

        public string OverlapNumberString
        {
            get { return OverlapNumber.ToString() + "回"; }
        }

        public string ElectionPercent
        {
            get
            {
                if (OverlapNumber == 0) return "0%";
                return CalcuratePercent(ElectionNumber, OverlapNumber).ToString() + "%";
            }
        }

        public string LeadPercent
        {
            get
            {
                if (OverlapNumber == 0) return "0%";
                return CalcuratePercent(LeadNumber, OverlapNumber).ToString() + "%";
            }
        }

        public string WinPercent
        {
            get
            {
                if (ElectionNumber == 0) return "0%";
                return CalcuratePercent(WinNumber, ElectionNumber) + "%";
            }
        }

        private int CalcuratePercent(int dividedNumber, int divideNumber)
        {
            return (int)Math.Round((double)(dividedNumber * 100 / divideNumber));
        }
    }
}
