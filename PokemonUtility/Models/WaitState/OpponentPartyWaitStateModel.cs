namespace PokemonUtility.Models.WaitState
{
    class OpponentPartyWaitStateModel : PartyWaiStatetModel
    {
        #region Singleton

        static OpponentPartyWaitStateModel Instance;
        public static OpponentPartyWaitStateModel GetInstance()
        {
            if (Instance == null)
                Instance = new OpponentPartyWaitStateModel();
            return Instance;
        }

        #endregion
    }
}
