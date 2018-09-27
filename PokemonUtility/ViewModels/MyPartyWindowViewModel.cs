using PokemonUtility.Models;
using PokemonUtility.Models.WaitState;

namespace PokemonUtility.ViewModels
{
    class MyPartyWindowViewModel : PartyWindowViewModel
    {
        public MyPartyWindowViewModel() : base(MyPartyWindowModel.GetInstance(), MyPartyManegementModel.GetInstance(), MyPartyWaitStateModel.GetInstance())
        {
        }
    }
}
