namespace PokemonUtility.Models
{
    class MyPartyImageModel : PartyImageModel
    {
        #region Singleton

        static MyPartyImageModel Instance;
        public static MyPartyImageModel GetInstance()
        {
            if (Instance == null)
                Instance = new MyPartyImageModel();
            return Instance;
        }

        #endregion

        public MyPartyImageModel()
        {
        }
    }
}
