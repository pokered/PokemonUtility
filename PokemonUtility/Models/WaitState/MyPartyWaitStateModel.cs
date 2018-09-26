namespace PokemonUtility.Models.WaitState
{
    class MyPartyWaitStateModel : PartyWaiStatetModel
    {
        #region Singleton

        static MyPartyWaitStateModel Instance;
        public static MyPartyWaitStateModel GetInstance()
        {
            if (Instance == null)
                Instance = new MyPartyWaitStateModel();
            return Instance;
        }

        #endregion
    }
}
