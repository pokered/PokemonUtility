namespace PokemonUtility.Models
{
    class MyPartyManegementModel : PartyManegementModel
    {
        #region Singleton

        static MyPartyManegementModel Instance;
        public static MyPartyManegementModel GetInstance()
        {
            if (Instance == null)
                Instance = new MyPartyManegementModel();
            return Instance;
        }

        #endregion
    }
}
