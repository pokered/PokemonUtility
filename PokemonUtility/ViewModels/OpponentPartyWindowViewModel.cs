using PokemonUtility.Models;
using PokemonUtility.Models.WaitState;

namespace PokemonUtility.ViewModels
{
    class OpponentPartyWindowViewModel : PartyWindowViewModel
    {
        public OpponentPartyWindowViewModel() : base(OpponentPartyWindowModel.GetInstance(), OpponentPartyWaitStateModel.GetInstance(), OpponentPartyManegementModel.GetInstance())
        {
        }
    }
}
