using PokemonUtility.Const;
using Prism.Mvvm;
using System.Collections.Generic;

namespace PokemonUtility.Models.Manegement
{
    class PartyManegementModel : BindableBase
    {
        private int[] _pokemonIdList = new int[] { -1, -1, -1, -1, -1, -1 };

        public int PokemonId0
        {
            get { return _pokemonIdList[PartyConst.PARTY_INDEX_FIRST]; }
            set
            {
                SetProperty(ref _pokemonIdList[PartyConst.PARTY_INDEX_FIRST], value);
                CorrectOrder(PartyConst.PARTY_INDEX_FIRST);
            }
        }

        public int PokemonId1
        {
            get { return _pokemonIdList[PartyConst.PARTY_INDEX_SECOND]; }
            set
            {
                SetProperty(ref _pokemonIdList[PartyConst.PARTY_INDEX_SECOND], value);
                CorrectOrder(PartyConst.PARTY_INDEX_SECOND);
            }
        }

        public int PokemonId2
        {
            get { return _pokemonIdList[PartyConst.PARTY_INDEX_THIRD]; }
            set
            {
                SetProperty(ref _pokemonIdList[PartyConst.PARTY_INDEX_THIRD], value);
                CorrectOrder(PartyConst.PARTY_INDEX_THIRD);
            }
        }

        public int PokemonId3
        {
            get { return _pokemonIdList[PartyConst.PARTY_INDEX_FOUR]; }
            set
            {
                SetProperty(ref _pokemonIdList[PartyConst.PARTY_INDEX_FOUR], value);
                CorrectOrder(PartyConst.PARTY_INDEX_FOUR);
            }
        }

        public int PokemonId4
        {
            get { return _pokemonIdList[PartyConst.PARTY_INDEX_FIFTH]; }
            set
            {
                SetProperty(ref _pokemonIdList[PartyConst.PARTY_INDEX_FIFTH], value);
                CorrectOrder(PartyConst.PARTY_INDEX_FIFTH);
            }
        }

        public int PokemonId5
        {
            get { return _pokemonIdList[PartyConst.PARTY_INDEX_SIXTH]; }
            set
            {
                SetProperty(ref _pokemonIdList[PartyConst.PARTY_INDEX_SIXTH], value);
                CorrectOrder(PartyConst.PARTY_INDEX_SIXTH);
            }
        }

        // 選出・非選出順
        private int[] _pokemonOrderList = new int[] { -1, -1, -1, -1, -1, -1 };
        private List<int> _selectedOrder = new List<int> { };
        private List<int> _notSelectedOrder = new List<int> { };

        public int PokemonOrder0
        {
            get { return _pokemonOrderList[PartyConst.PARTY_INDEX_FIRST]; }
            set { SetProperty(ref _pokemonOrderList[PartyConst.PARTY_INDEX_FIRST], value); }
        }

        public int PokemonOrder1
        {
            get { return _pokemonOrderList[PartyConst.PARTY_INDEX_SECOND]; }
            set { SetProperty(ref _pokemonOrderList[PartyConst.PARTY_INDEX_SECOND], value); }
        }

        public int PokemonOrder2
        {
            get { return _pokemonOrderList[PartyConst.PARTY_INDEX_THIRD]; }
            set { SetProperty(ref _pokemonOrderList[PartyConst.PARTY_INDEX_THIRD], value); }
        }

        public int PokemonOrder3
        {
            get { return _pokemonOrderList[PartyConst.PARTY_INDEX_FOUR]; }
            set { SetProperty(ref _pokemonOrderList[PartyConst.PARTY_INDEX_FOUR], value); }
        }

        public int PokemonOrder4
        {
            get { return _pokemonOrderList[PartyConst.PARTY_INDEX_FIFTH]; }
            set { SetProperty(ref _pokemonOrderList[PartyConst.PARTY_INDEX_FIFTH], value); }
        }

        public int PokemonOrder5
        {
            get { return _pokemonOrderList[PartyConst.PARTY_INDEX_SIXTH]; }
            set { SetProperty(ref _pokemonOrderList[PartyConst.PARTY_INDEX_SIXTH], value); }
        }

        // 選出順を取得
        private int GetOrder(int pokemonIndex)
        {
            if (_selectedOrder.Contains(pokemonIndex)) return _selectedOrder.IndexOf(pokemonIndex);

            if (_notSelectedOrder.Contains(pokemonIndex)) return _notSelectedOrder.IndexOf(pokemonIndex) + 3;

            return -1;
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

        // aa
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

        // 選出リスト更新
        private void UpdateOrder()
        {
            PokemonOrder0 = GetOrder(PartyConst.PARTY_INDEX_FIRST);
            PokemonOrder1 = GetOrder(PartyConst.PARTY_INDEX_SECOND);
            PokemonOrder2 = GetOrder(PartyConst.PARTY_INDEX_THIRD);
            PokemonOrder3 = GetOrder(PartyConst.PARTY_INDEX_FOUR);
            PokemonOrder4 = GetOrder(PartyConst.PARTY_INDEX_FIFTH);
            PokemonOrder5 = GetOrder(PartyConst.PARTY_INDEX_SIXTH);
        }
    }
}
