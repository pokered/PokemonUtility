using PokemonUtility.Models.Manegement;

namespace PokemonUtility.Behaviors
{
    class MyPartyImageBehavior : PartyImageBehavior
    {
        public MyPartyImageBehavior() : base(MyPartyManegementModel.GetInstance())
        {
        }
    }
}
