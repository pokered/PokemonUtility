using PokemonUtility.Models.Database;
using PokemonUtility.Struct;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PokemonUtility.Models.Common
{
    class PokemonDataModel
    {
        private List<Pokemon> pokemons = new List<Pokemon>();

        private void CreatePokemonsData()
        {
            // データベースからデータを取得
            PokemonDatabaseModel pokemonDatabaseModel = new PokemonDatabaseModel();

            foreach(Pokemon pokemon in pokemonDatabaseModel.GetPokemons())
            {
                pokemons.Add(pokemon);
            }
        }

        public List<Pokemon> GetPokemons()
        {
            pokemons.Clear();
            CreatePokemonsData();
            return pokemons;
        }
    }
}
