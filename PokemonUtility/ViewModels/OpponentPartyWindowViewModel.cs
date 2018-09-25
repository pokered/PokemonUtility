using PokemonUtility.Models;
using PokemonUtility.Models.Analysis;

namespace PokemonUtility.ViewModels
{
    class OpponentPartyWindowViewModel : PartyWindowViewModel
    {
        public OpponentPartyWindowViewModel() : base(OpponentPartyWindowModel.GetInstance(), OpponentPartyAnalysisModel.GetInstance(), OpponentPartyManegementModel.GetInstance())
        {
        }
    }
}
