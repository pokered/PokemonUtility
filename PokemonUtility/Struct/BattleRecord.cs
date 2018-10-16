using PokemonUtility.Const;
using PokemonUtility.Models;
using System;
using System.Windows.Media.Imaging;

namespace PokemonUtility.Struct
{
    class BattleRecord
    {
        // 対戦結果ID
        public string VersusString { get; set; } = "VS";

        // 対戦結果ID
        public int BattleRecordId { get; set; }

        // 世代
        public string SoftGeneration { get; set; }

        // 対戦結果
        private int _battleResult = -1;
        public string BattleResultString { get { return BattleResult.ToBattleResultEnglish(_battleResult); } }

        // 自分のポケモン
        private int[] _myPokemonIdList = new int[] { -1, -1, -1, -1, -1, -1 };
        private int[] _myPokemonOrderList = new int[] { -1, -1, -1, -1, -1, -1 };

        public BitmapImage MyPokemonImage0
        {
            get { return ImageFactoryModel.PokemonImageAddFrameImage(_myPokemonIdList[PartyConst.FIRST], _myPokemonOrderList[PartyConst.FIRST]); }
        }

        public BitmapImage MyPokemonImage1
        {
            get { return ImageFactoryModel.PokemonImageAddFrameImage(_myPokemonIdList[PartyConst.SECOND], _myPokemonOrderList[PartyConst.SECOND]); }
        }

        public BitmapImage MyPokemonImage2
        {
            get { return ImageFactoryModel.PokemonImageAddFrameImage(_myPokemonIdList[PartyConst.THIRD], _myPokemonOrderList[PartyConst.THIRD]); }
        }

        public BitmapImage MyPokemonImage3
        {
            get { return ImageFactoryModel.PokemonImageAddFrameImage(_myPokemonIdList[PartyConst.FOURTH], _myPokemonOrderList[PartyConst.FOURTH]); }
        }

        public BitmapImage MyPokemonImage4
        {
            get { return ImageFactoryModel.PokemonImageAddFrameImage(_myPokemonIdList[PartyConst.FIFTH], _myPokemonOrderList[PartyConst.FIFTH]); }
        }

        public BitmapImage MyPokemonImage5
        {
            get { return ImageFactoryModel.PokemonImageAddFrameImage(_myPokemonIdList[PartyConst.SIXTH], _myPokemonOrderList[PartyConst.SIXTH]); }
        }

        // 相手のポケモン
        private int[] _opponentPokemonIdList = new int[] { -1, -1, -1, -1, -1, -1 };
        private int[] _opponentPokemonOrderList = new int[] { -1, -1, -1, -1, -1, -1 };

        public BitmapImage OpponentPokemonImage0
        {
            get { return ImageFactoryModel.PokemonImageAddFrameImage(_opponentPokemonIdList[PartyConst.FIRST], _opponentPokemonOrderList[PartyConst.FIRST]); }
        }

        public BitmapImage OpponentPokemonImage1
        {
            get { return ImageFactoryModel.PokemonImageAddFrameImage(_opponentPokemonIdList[PartyConst.SECOND], _opponentPokemonOrderList[PartyConst.SECOND]); }
        }

        public BitmapImage OpponentPokemonImage2
        {
            get { return ImageFactoryModel.PokemonImageAddFrameImage(_opponentPokemonIdList[PartyConst.THIRD], _opponentPokemonOrderList[PartyConst.THIRD]); }
        }

        public BitmapImage OpponentPokemonImage3
        {
            get { return ImageFactoryModel.PokemonImageAddFrameImage(_opponentPokemonIdList[PartyConst.FOURTH], _opponentPokemonOrderList[PartyConst.FOURTH]); }
        }

        public BitmapImage OpponentPokemonImage4
        {
            get { return ImageFactoryModel.PokemonImageAddFrameImage(_opponentPokemonIdList[PartyConst.FIFTH], _opponentPokemonOrderList[PartyConst.FIFTH]); }
        }

        public BitmapImage OpponentPokemonImage5
        {
            get { return ImageFactoryModel.PokemonImageAddFrameImage(_opponentPokemonIdList[PartyConst.SIXTH], _opponentPokemonOrderList[PartyConst.SIXTH]); }
        }

        // 対戦日
        public DateTime InsertDate { get; set; }

        public void ChangeBattleResult(int battleResultId)
        {
            _battleResult = battleResultId;
        }

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
