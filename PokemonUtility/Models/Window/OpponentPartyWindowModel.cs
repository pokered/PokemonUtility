namespace PokemonUtility.Models
{
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
