using PokemonUtility.Models;
using PokemonUtility.Models.Manegement;
using PokemonUtility.Models.WaitState;

namespace PokemonUtility.ViewModels
{
    class OpponentPartyWindowViewModel : PartyWindowViewModel
    {
        public OpponentPartyWindowViewModel() : base(OpponentPartyWindowModel.GetInstance(), OpponentPartyManegementModel.GetInstance(), OpponentPartyWaitStateModel.GetInstance())
        {
        }
    }
}
