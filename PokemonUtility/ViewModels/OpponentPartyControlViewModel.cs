using PokemonUtility.Models;
using PokemonUtility.ViewModels.Abstract;

namespace PokemonUtility.ViewModels
{
    class OpponentPartyControlViewModel : PartyControllViewModel
    {
        public OpponentPartyControlViewModel() : base(ModelConnector.BattleHistoryOpponentParty)
        {

        }
	}
}
