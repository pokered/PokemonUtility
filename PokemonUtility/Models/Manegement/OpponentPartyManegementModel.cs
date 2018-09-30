namespace PokemonUtility.Models.Manegement
{
    class OpponentPartyManegementModel : PartyManegementModel
    {
        #region Singleton

        static OpponentPartyManegementModel Instance;
        public static OpponentPartyManegementModel GetInstance()
        {
            if (Instance == null)
                Instance = new OpponentPartyManegementModel();
            return Instance;
        }

        #endregion
    }
}
