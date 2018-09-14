using Prism.Mvvm;

namespace PokemonUtility.Models
{
    class PartyImageModel : BindableBase
    {
        //private PokemonImageModel _pokemon1;
        public PokemonImageModel Pokemon1 { get; }
        public PokemonImageModel Pokemon2 { get; }
        public PokemonImageModel Pokemon3 { get; }
        public PokemonImageModel Pokemon4 { get; }
        public PokemonImageModel Pokemon5 { get; }
        public PokemonImageModel Pokemon6 { get; }

        public PartyImageModel()
        {
            Pokemon1 = new PokemonImageModel();
            Pokemon2 = new PokemonImageModel();
            Pokemon3 = new PokemonImageModel();
            Pokemon4 = new PokemonImageModel();
            Pokemon5 = new PokemonImageModel();
            Pokemon6 = new PokemonImageModel();
        }
    }
}
