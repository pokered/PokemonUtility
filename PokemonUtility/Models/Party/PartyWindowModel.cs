using PokemonUtility.Models.Abstract;

namespace PokemonUtility.Models.Party
{
    class PartyWindowModel : SubWindowModel
    { }

    class MyPartyWindowModel : PartyWindowModel
    {
        #region Singleton

        static MyPartyWindowModel Instance;
        public static MyPartyWindowModel GetInstance()
        {
            if (Instance == null)
                Instance = new MyPartyWindowModel();
            return Instance;
        }

        #endregion
    }

    class OpponentPartyWindowModel : PartyWindowModel
    {
        #region Singleton

        static OpponentPartyWindowModel Instance;
        public static OpponentPartyWindowModel GetInstance()
        {
            if (Instance == null)
                Instance = new OpponentPartyWindowModel();
            return Instance;
        }

        #endregion
    }
}
