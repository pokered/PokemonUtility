using PokemonUtility.Models;
using PokemonUtility.ViewModels.Abstract;

namespace PokemonUtility.ViewModels
{
    class OpponentPartyWindowViewModel : PartyWindowViewModel
    {
        public OpponentPartyWindowViewModel() : base(
            ModelConnector.OpponentPartyWindow,
            ModelConnector.OpponentPartyManegement,
            ModelConnector.OpponentPartyWaitState)
        {
        }
    }
}
