namespace PokemonUtility.Models.Analysis
{
    class MyPartyAnalysisModel : PartyAnalysisModel
    {
        #region Singleton

        static MyPartyAnalysisModel Instance;
        public static MyPartyAnalysisModel GetInstance()
        {
            if (Instance == null)
                Instance = new MyPartyAnalysisModel();
            return Instance;
        }

        #endregion
    }
}
