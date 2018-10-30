using PokemonUtility.Models;
using PokemonUtility.ViewModels.Abstract;

namespace PokemonUtility.ViewModels
{
    class MyPartyControlViewModel : PartyControllViewModel
    {
        public MyPartyControlViewModel() : base(ModelConnector.BattleHistoryMyParty)
        {

        }
	}
}
