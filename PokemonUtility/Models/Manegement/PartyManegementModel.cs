using PokemonUtility.Const;
using Prism.Mvvm;
using System.Collections.Generic;

namespace PokemonUtility.Models
{
    class PartyManegementModel : BindableBase
    {
        private int[] _pokemonIdList = new int[] { -1, -1, -1, -1, -1, -1 };

        public int PokemonId1
        {
            get { return _pokemonIdList[PartyConst.PARTY_INDEX1]; }
            set
            {
                SetProperty(ref _pokemonIdList[PartyConst.PARTY_INDEX1], value);
                CorrectOrder(PartyConst.PARTY_INDEX1);
            }
        }

        public int PokemonId2
        {
            get { return _pokemonIdList[PartyConst.PARTY_INDEX2]; }
            set
            {
                SetProperty(ref _pokemonIdList[PartyConst.PARTY_INDEX2], value);
                CorrectOrder(PartyConst.PARTY_INDEX2);
            }
        }

        public int PokemonId3
        {
            get { return _pokemonIdList[PartyConst.PARTY_INDEX3]; }
            set
            {
                SetProperty(ref _pokemonIdList[PartyConst.PARTY_INDEX3], value);
                CorrectOrder(PartyConst.PARTY_INDEX3);
            }
        }

        public int PokemonId4
        {
            get { return _pokemonIdList[PartyConst.PARTY_INDEX4]; }
            set
            {
                SetProperty(ref _pokemonIdList[PartyConst.PARTY_INDEX4], value);
                CorrectOrder(PartyConst.PARTY_INDEX4);
            }
        }

        public int PokemonId5
        {
            get { return _pokemonIdList[PartyConst.PARTY_INDEX5]; }
            set
            {
                SetProperty(ref _pokemonIdList[PartyConst.PARTY_INDEX5], value);
                CorrectOrder(PartyConst.PARTY_INDEX5);
            }
        }

        public int PokemonId6
        {
            get { return _pokemonIdList[PartyConst.PARTY_INDEX6]; }
            set
            {
                SetProperty(ref _pokemonIdList[PartyConst.PARTY_INDEX6], value);
                CorrectOrder(PartyConst.PARTY_INDEX6);
            }
        }

        // 選出・非選出順
        private int[] _pokemonOrderList = new int[] { -1, -1, -1, -1, -1, -1 };
        private List<int> _selectedOrder = new List<int> { };
        private List<int> _notSelectedOrder = new List<int> { };

        public int PokemonOrder1
        {
            get { return _pokemonOrderList[PartyConst.PARTY_INDEX1]; }
            set { SetProperty(ref _pokemonOrderList[PartyConst.PARTY_INDEX1], value); }
        }

        public int PokemonOrder2
        {
            get { return _pokemonOrderList[PartyConst.PARTY_INDEX2]; }
            set { SetProperty(ref _pokemonOrderList[PartyConst.PARTY_INDEX2], value); }
        }

        public int PokemonOrder3
        {
            get { return _pokemonOrderList[PartyConst.PARTY_INDEX3]; }
            set { SetProperty(ref _pokemonOrderList[PartyConst.PARTY_INDEX3], value); }
        }

        public int PokemonOrder4
        {
            get { return _pokemonOrderList[PartyConst.PARTY_INDEX4]; }
            set { SetProperty(ref _pokemonOrderList[PartyConst.PARTY_INDEX4], value); }
        }

        public int PokemonOrder5
        {
            get { return _pokemonOrderList[PartyConst.PARTY_INDEX5]; }
            set { SetProperty(ref _pokemonOrderList[PartyConst.PARTY_INDEX5], value); }
        }

        public int PokemonOrder6
        {
            get { return _pokemonOrderList[PartyConst.PARTY_INDEX6]; }
            set { SetProperty(ref _pokemonOrderList[PartyConst.PARTY_INDEX6], value); }
        }

        public PartyManegementModel()
        {
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

        // 選出リスト更新
        private void UpdateOrder()
        {
            PokemonOrder1 = GetOrder(PartyConst.PARTY_INDEX1);
            PokemonOrder2 = GetOrder(PartyConst.PARTY_INDEX2);
            PokemonOrder3 = GetOrder(PartyConst.PARTY_INDEX3);
            PokemonOrder4 = GetOrder(PartyConst.PARTY_INDEX4);
            PokemonOrder5 = GetOrder(PartyConst.PARTY_INDEX5);
            PokemonOrder6 = GetOrder(PartyConst.PARTY_INDEX6);
        }
    }
}
