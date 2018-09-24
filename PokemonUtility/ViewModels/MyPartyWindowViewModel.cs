using System;
using PokemonUtility.Models;

namespace PokemonUtility.ViewModels
{
    class MyPartyWindowViewModel : PartyWindowViewModel
    {
        public MyPartyWindowViewModel() : base(MyPartyWindowModel.GetInstance(), MyPartyManegementModel.GetInstance())
        {
        }
    }
}
