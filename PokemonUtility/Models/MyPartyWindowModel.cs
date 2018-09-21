namespace PokemonUtility.Models
{
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
}
