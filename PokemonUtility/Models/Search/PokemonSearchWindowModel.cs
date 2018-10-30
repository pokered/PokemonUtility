using PokemonUtility.Models.Database;
using Prism.Mvvm;

namespace PokemonUtility.Models.Search
{
    class PokemonSearchWindowModel : BindableBase
    {
        #region Singleton

        static PokemonSearchWindowModel Instance;
        public static PokemonSearchWindowModel GetInstance()
        {
            if (Instance == null)
                Instance = new PokemonSearchWindowModel();
            return Instance;
        }

        #endregion

        private int pokemonId = -1;
        public int PokemonId
        {
            get { return pokemonId; }
            set { SetProperty(ref pokemonId, value); }
        }

        public bool IsChangePokemonId { get; set; }

        // DBモデル
        PokemonDatabaseModel _pokemonDatabaseModel = new PokemonDatabaseModel();

        public void ChangePokemonId(string pokemonName)
        {
            PokemonId = _pokemonDatabaseModel.GetPokemonIconId(pokemonName);
        }
    }
}
