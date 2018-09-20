namespace PokemonUtility.Models
{
    class MyPartyModel : PartyManegementModel
    {
        #region Singleton

        static MyPartyModel Instance;
        public static MyPartyModel GetInstance()
        {
            if (Instance == null)
                Instance = new MyPartyModel();
            return Instance;
        }

        #endregion

        public MyPartyModel()
        {
        }
    }
}
