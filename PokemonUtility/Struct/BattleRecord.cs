using PokemonUtility.Const;
using PokemonUtility.Models;
using System;
using System.Windows.Media.Imaging;

namespace PokemonUtility.Struct
{
    class BattleRecord
    {
        // versus
        public string VersusString { get; set; } = "VS";

        // 戦績ID
        public int BattleRecordId { get; set; }

        // 世代
        public string SoftGeneration { get; set; }

        // 対戦結果
        public int BattleResultId { get; set; } = -1;
        public string BattleResultString { get { return BattleResult.ToBattleResultEnglish(BattleResultId); } }

        // 自分のトレーナー名
        public string MyTrainerName { get; set; }

        // 自分のポケモン
        private int[] _myPokemonIdList = new int[] { -1, -1, -1, -1, -1, -1 };
        private int[] _myPokemonOrderList = new int[] { -1, -1, -1, -1, -1, -1 };

        public BitmapImage MyPokemonImage0
        {
            get { return ImageFactoryModel.CreatePokemonImage(_myPokemonIdList[PartyConst.FIRST], _myPokemonOrderList[PartyConst.FIRST]); }
        }

        public BitmapImage MyPokemonImage1
        {
            get { return ImageFactoryModel.CreatePokemonImage(_myPokemonIdList[PartyConst.SECOND], _myPokemonOrderList[PartyConst.SECOND]); }
        }

        public BitmapImage MyPokemonImage2
        {
            get { return ImageFactoryModel.CreatePokemonImage(_myPokemonIdList[PartyConst.THIRD], _myPokemonOrderList[PartyConst.THIRD]); }
        }

        public BitmapImage MyPokemonImage3
        {
            get { return ImageFactoryModel.CreatePokemonImage(_myPokemonIdList[PartyConst.FOURTH], _myPokemonOrderList[PartyConst.FOURTH]); }
        }

        public BitmapImage MyPokemonImage4
        {
            get { return ImageFactoryModel.CreatePokemonImage(_myPokemonIdList[PartyConst.FIFTH], _myPokemonOrderList[PartyConst.FIFTH]); }
        }

        public BitmapImage MyPokemonImage5
        {
            get { return ImageFactoryModel.CreatePokemonImage(_myPokemonIdList[PartyConst.SIXTH], _myPokemonOrderList[PartyConst.SIXTH]); }
        }

        // 相手のトレーナー
        public string OpponentTrainerName { get; set; }

        // 相手のポケモン
        private int[] _opponentPokemonIdList = new int[] { -1, -1, -1, -1, -1, -1 };
        private int[] _opponentPokemonOrderList = new int[] { -1, -1, -1, -1, -1, -1 };

        public BitmapImage OpponentPokemonImage0
        {
            get { return ImageFactoryModel.CreatePokemonImage(_opponentPokemonIdList[PartyConst.FIRST], _opponentPokemonOrderList[PartyConst.FIRST]); }
        }

        public BitmapImage OpponentPokemonImage1
        {
            get { return ImageFactoryModel.CreatePokemonImage(_opponentPokemonIdList[PartyConst.SECOND], _opponentPokemonOrderList[PartyConst.SECOND]); }
        }

        public BitmapImage OpponentPokemonImage2
        {
            get { return ImageFactoryModel.CreatePokemonImage(_opponentPokemonIdList[PartyConst.THIRD], _opponentPokemonOrderList[PartyConst.THIRD]); }
        }

        public BitmapImage OpponentPokemonImage3
        {
            get { return ImageFactoryModel.CreatePokemonImage(_opponentPokemonIdList[PartyConst.FOURTH], _opponentPokemonOrderList[PartyConst.FOURTH]); }
        }

        public BitmapImage OpponentPokemonImage4
        {
            get { return ImageFactoryModel.CreatePokemonImage(_opponentPokemonIdList[PartyConst.FIFTH], _opponentPokemonOrderList[PartyConst.FIFTH]); }
        }

        public BitmapImage OpponentPokemonImage5
        {
            get { return ImageFactoryModel.CreatePokemonImage(_opponentPokemonIdList[PartyConst.SIXTH], _opponentPokemonOrderList[PartyConst.SIXTH]); }
        }

        // 対戦日
        public DateTime InsertDate { get; set; }

        public void ChangeMyPokemonId(int partyIndex, int pokemonId)
        {
            _myPokemonIdList[partyIndex] = pokemonId;
        }

        public void ChangeMyPokemonOrder(int partyIndex, int order)
        {
            _myPokemonOrderList[partyIndex] = order;
        }

        public void ChangeOpponentPokemonId(int partyIndex, int pokemonId)
        {
            _opponentPokemonIdList[partyIndex] = pokemonId;
        }

        public void ChangeOpponentPokemonOrder(int partyIndex, int order)
        {
            _opponentPokemonOrderList[partyIndex] = order;
        }


        private void SetValue(string propertyName, int value)
        {
            // プロパティ情報取得
            var property = GetType().GetProperty(propertyName);

            // プロパティが存在しなければ終了
            if (property == null) return;

            // プロパティの値を取得
            property.SetValue(this, value);
        }
    }
}
