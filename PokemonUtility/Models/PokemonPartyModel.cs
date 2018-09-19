using Prism.Mvvm;
using System.Collections.Generic;

namespace PokemonUtility.Models
{
    class PokemonPartyModel : BindableBase
    {
        private int[] _pokemonList = new int[] { -1, -1, -1, -1, -1, -1 };
        public int PokemonId1
        {
            get { return _pokemonList[0]; }
            set
            {
                SetProperty(ref _pokemonList[0], value);
                RemoveForSelection(0, value);
            }
        }

        public int OrderOfIndex1
        {
            get { return GetOrder(1); }
        }

        
        private List<int> _selectedOrder = new List<int> { };
        private List<int> _notSelectedOrder = new List<int> { 0, 1, 2, 3, 4, 5 };
        
        public PokemonPartyModel()
        {
        }

        private int GetOrder(int pokemonIndex)
        {
            if (_selectedOrder.IndexOf(pokemonIndex) != -1) return _selectedOrder.IndexOf(pokemonIndex);

            if (_notSelectedOrder.Contains(pokemonIndex)) return _notSelectedOrder.IndexOf(pokemonIndex) + 3;

            return 10;
        }

        private void RemoveForSelection(int pokemonIndex, int pokemonId)
        {
            if (!ImageFactoryModel.ExistPokemonImage(pokemonId))
            {
                // 存在しないポケモンの場合、選出から外す
                if (_selectedOrder.Contains(pokemonIndex))
                {
                    _selectedOrder.Remove(pokemonIndex);
                    _notSelectedOrder.Add(pokemonIndex);
                }
            }
        }

    }
}
