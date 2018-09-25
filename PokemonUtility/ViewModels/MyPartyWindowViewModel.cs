using System;
using PokemonUtility.Models;
using PokemonUtility.Models.Analysis;

namespace PokemonUtility.ViewModels
{
    class MyPartyWindowViewModel : PartyWindowViewModel
    {
        public MyPartyWindowViewModel() : base(MyPartyWindowModel.GetInstance(), MyPartyAnalysisModel.GetInstance(), MyPartyManegementModel.GetInstance())
        {
        }
    }
}
