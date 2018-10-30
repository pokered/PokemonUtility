using PokemonUtility.Models;
using PokemonUtility.ViewModels.Abstract;

namespace PokemonUtility.ViewModels
{
    class MyPartyWindowViewModel : PartyWindowViewModel
    {
        public MyPartyWindowViewModel() : base(
            ModelConnector.MyPartyWindow,
            ModelConnector.MyPartyManegement,
            ModelConnector.MyPartyWaitState)
        {
        }
    }
}
