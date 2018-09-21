using PokemonUtility.Models;

namespace PokemonUtility.ViewModels
{
    class OpponentPartyWindowViewModel : PartyWindowViewModel
    {
        public OpponentPartyWindowViewModel() : base(OpponentPartyWindowModel.GetInstance(), OpponentPartyManegementModel.GetInstance())
        {

        }
    }
}
