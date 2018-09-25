namespace PokemonUtility.Models.Analysis
{
    class OpponentPartyAnalysisModel : PartyAnalysisModel
    {
        #region Singleton

        static OpponentPartyAnalysisModel Instance;
        public static OpponentPartyAnalysisModel GetInstance()
        {
            if (Instance == null)
                Instance = new OpponentPartyAnalysisModel();
            return Instance;
        }

        #endregion
    }
}
