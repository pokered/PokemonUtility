using PokemonUtility.Const;
using Prism.Mvvm;
using System.Collections.Generic;

namespace PokemonUtility.Models.Party
{
    class PartyManegementModel : BindableBase
    {
        private int[] _pokemonIdList = new int[] { -1, -1, -1, -1, -1, -1 };

        public int PokemonId0
        {
            get { return _pokemonIdList[PartyConst.FIRST]; }
            set
            {
                SetProperty(ref _pokemonIdList[PartyConst.FIRST], value);
                CorrectOrder(PartyConst.FIRST);
            }
        }

        public int PokemonId1
        {
            get { return _pokemonIdList[PartyConst.SECOND]; }
            set
            {
                SetProperty(ref _pokemonIdList[PartyConst.SECOND], value);
                CorrectOrder(PartyConst.SECOND);
            }
        }

        public int PokemonId2
        {
            get { return _pokemonIdList[PartyConst.THIRD]; }
            set
            {
                SetProperty(ref _pokemonIdList[PartyConst.THIRD], value);
                CorrectOrder(PartyConst.THIRD);
            }
        }

        public int PokemonId3
        {
            get { return _pokemonIdList[PartyConst.FOURTH]; }
            set
            {
                SetProperty(ref _pokemonIdList[PartyConst.FOURTH], value);
                CorrectOrder(PartyConst.FOURTH);
            }
        }

        public int PokemonId4
        {
            get { return _pokemonIdList[PartyConst.FIFTH]; }
            set
            {
                SetProperty(ref _pokemonIdList[PartyConst.FIFTH], value);
                CorrectOrder(PartyConst.FIFTH);
            }
        }

        public int PokemonId5
        {
            get { return _pokemonIdList[PartyConst.SIXTH]; }
            set
            {
                SetProperty(ref _pokemonIdList[PartyConst.SIXTH], value);
                CorrectOrder(PartyConst.SIXTH);
            }
        }

        // 選出・非選出順
        private int[] _pokemonOrderList = new int[] { -1, -1, -1, -1, -1, -1 };
        private List<int> _selectedOrder = new List<int> { };
        private List<int> _notSelectedOrder = new List<int> { };

        public int PokemonOrder0
        {
            get { return _pokemonOrderList[PartyConst.FIRST]; }
            set { SetProperty(ref _pokemonOrderList[PartyConst.FIRST], value); }
        }

        public int PokemonOrder1
        {
            get { return _pokemonOrderList[PartyConst.SECOND]; }
            set { SetProperty(ref _pokemonOrderList[PartyConst.SECOND], value); }
        }

        public int PokemonOrder2
        {
            get { return _pokemonOrderList[PartyConst.THIRD]; }
            set { SetProperty(ref _pokemonOrderList[PartyConst.THIRD], value); }
        }

        public int PokemonOrder3
        {
            get { return _pokemonOrderList[PartyConst.FOURTH]; }
            set { SetProperty(ref _pokemonOrderList[PartyConst.FOURTH], value); }
        }

        public int PokemonOrder4
        {
            get { return _pokemonOrderList[PartyConst.FIFTH]; }
            set { SetProperty(ref _pokemonOrderList[PartyConst.FIFTH], value); }
        }

        public int PokemonOrder5
        {
            get { return _pokemonOrderList[PartyConst.SIXTH]; }
            set { SetProperty(ref _pokemonOrderList[PartyConst.SIXTH], value); }
        }

        // ポケモンIDを取得
        public int GetPokemonId(int pokemonIndex)
        {
            return _pokemonIdList[pokemonIndex];
        }

        // 選出順を取得
        public int GetOrder(int pokemonIndex)
        {
            if (_selectedOrder.Contains(pokemonIndex)) return _selectedOrder.IndexOf(pokemonIndex);

            if (_notSelectedOrder.Contains(pokemonIndex)) return _notSelectedOrder.IndexOf(pokemonIndex) + 3;

            return -1;
        }

        // ポケモンID変更
        public void ChangePokemonId(int partyIndex, int pokemonId)
        {
            // プロパティ情報取得
            var property = GetType().GetProperty(string.Format("PokemonId{0}", partyIndex));

            // プロパティが存在しなければ終了
            if (property == null) return;

            // プロパティの値を取得
            property.SetValue(this, pokemonId);
        }

        // オーダー変更
        public void ChangeOrder(int pokemonIndex)
        {
            // 存在しないポケモンの場合は変更なし
            int pokemonId = _pokemonIdList[pokemonIndex];
            if (!ImageFactoryModel.ExistPokemonImage(pokemonId)) return;

            // 選出されている場合
            if (_selectedOrder.Contains(pokemonIndex))
            {
                // 選出リストから削除
                _selectedOrder.Remove(pokemonIndex);

                // 非選出リストに追加
                _notSelectedOrder.Add(pokemonIndex);

                // 選出変更を反映
                UpdateOrder();

                return;
            }

            // 非選出の場合
            // 既に3匹選出されている場合は変更なし
            if (_selectedOrder.Count >= 3) return;

            // 3匹選出されていなければ追加
            _selectedOrder.Add(pokemonIndex);

            // 選出変更を反映
            UpdateOrder();
        }

        // ポケモンID変更に伴い選出も修正する
        private void CorrectOrder(int pokemonIndex)
        {
            int pokemonId = _pokemonIdList[pokemonIndex];

            // not exist
            if (!ImageFactoryModel.ExistPokemonImage(pokemonId))
            {
                // 選出・非選出からも削除
                _selectedOrder.Remove(pokemonIndex);
                _notSelectedOrder.Remove(pokemonIndex);
                return;
            }

            // 存在するのに選出・非選出のどちらにもidがない場合
            if (!_selectedOrder.Contains(pokemonIndex) & !_notSelectedOrder.Contains(pokemonIndex))
            {
                _notSelectedOrder.Add(pokemonIndex);
            }

            // 更新
            UpdateOrder();
        }

        // 選出リスト更新
        private void UpdateOrder()
        {
            PokemonOrder0 = GetOrder(PartyConst.FIRST);
            PokemonOrder1 = GetOrder(PartyConst.SECOND);
            PokemonOrder2 = GetOrder(PartyConst.THIRD);
            PokemonOrder3 = GetOrder(PartyConst.FOURTH);
            PokemonOrder4 = GetOrder(PartyConst.FIFTH);
            PokemonOrder5 = GetOrder(PartyConst.SIXTH);
        }
    }

    class MyPartyManegementModel : PartyManegementModel
    {
        #region Singleton

        static MyPartyManegementModel Instance;
        public static MyPartyManegementModel GetInstance()
        {
            if (Instance == null)
                Instance = new MyPartyManegementModel();
            return Instance;
        }

        #endregion
    }

    class OpponentPartyManegementModel : PartyManegementModel
    {
        #region Singleton

        static OpponentPartyManegementModel Instance;
        public static OpponentPartyManegementModel GetInstance()
        {
            if (Instance == null)
                Instance = new OpponentPartyManegementModel();
            return Instance;
        }

        #endregion
    }

    class BattleHistoryMyPartyModel : PartyManegementModel
    {
        #region Singleton

        static BattleHistoryMyPartyModel Instance;
        public static BattleHistoryMyPartyModel GetInstance()
        {
            if (Instance == null)
                Instance = new BattleHistoryMyPartyModel();
            return Instance;
        }

        #endregion
    }

    class BattleHistoryOpponentPartyModel : PartyManegementModel
    {
        #region Singleton

        static BattleHistoryOpponentPartyModel Instance;
        public static BattleHistoryOpponentPartyModel GetInstance()
        {
            if (Instance == null)
                Instance = new BattleHistoryOpponentPartyModel();
            return Instance;
        }

        #endregion
    }
}
