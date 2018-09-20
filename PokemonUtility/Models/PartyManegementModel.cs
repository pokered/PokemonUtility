using Prism.Mvvm;
using System.Collections.Generic;

namespace PokemonUtility.Models
{
    class PartyManegementModel : BindableBase
    {
        private const int POKEMON_INDEX1 = 0;
        private const int POKEMON_INDEX2 = 1;
        private const int POKEMON_INDEX3 = 2;
        private const int POKEMON_INDEX4 = 3;
        private const int POKEMON_INDEX5 = 4;
        private const int POKEMON_INDEX6 = 5;

        private int[] _pokemonList = new int[] { -1, -1, -1, -1, -1, -1 };

        public int PokemonId1
        {
            get { return _pokemonList[POKEMON_INDEX1]; }
            set
            {
                SetProperty(ref _pokemonList[POKEMON_INDEX1], value);
                ChangeSelectedOrder(POKEMON_INDEX1);
            }
        }

        public int PokemonId2
        {
            get { return _pokemonList[POKEMON_INDEX2]; }
            set
            {
                SetProperty(ref _pokemonList[POKEMON_INDEX2], value);
                ChangeSelectedOrder(POKEMON_INDEX2);
            }
        }

        public int PokemonId3
        {
            get { return _pokemonList[POKEMON_INDEX3]; }
            set
            {
                SetProperty(ref _pokemonList[POKEMON_INDEX3], value);
                ChangeSelectedOrder(POKEMON_INDEX3);
            }
        }

        public int PokemonId4
        {
            get { return _pokemonList[POKEMON_INDEX4]; }
            set
            {
                SetProperty(ref _pokemonList[POKEMON_INDEX4], value);
                ChangeSelectedOrder(POKEMON_INDEX4);
            }
        }

        public int PokemonId5
        {
            get { return _pokemonList[POKEMON_INDEX5]; }
            set
            {
                SetProperty(ref _pokemonList[POKEMON_INDEX5], value);
                ChangeSelectedOrder(POKEMON_INDEX5);
            }
        }

        public int PokemonId6
        {
            get { return _pokemonList[POKEMON_INDEX6]; }
            set
            {
                SetProperty(ref _pokemonList[POKEMON_INDEX6], value);
                ChangeSelectedOrder(POKEMON_INDEX6);
            }
        }

        // 選出・非選出順
        private List<int> _selectedOrder = new List<int> { };
        private List<int> _notSelectedOrder = new List<int> { };

        public int PokemonOrder1
        {
            get { return GetOrder(POKEMON_INDEX1); }
        }

        public int PokemonOrder2
        {
            get { return GetOrder(POKEMON_INDEX2); }
        }

        public int PokemonOrder3
        {
            get { return GetOrder(POKEMON_INDEX3); }
        }

        public int PokemonOrder4
        {
            get { return GetOrder(POKEMON_INDEX4); }
        }

        public int PokemonOrder5
        {
            get { return GetOrder(POKEMON_INDEX5); }
        }

        public int PokemonOrder6
        {
            get { return GetOrder(POKEMON_INDEX6); }
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

        private void ChangeSelectedOrder(int pokemonIndex)
        {
            int pokemonId = _pokemonList[pokemonIndex];

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
        }
    }
}
