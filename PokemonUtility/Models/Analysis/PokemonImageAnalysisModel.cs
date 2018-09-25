namespace PokemonUtility.Models.Analysis
{
    class PokemonImageAnalysisModel
    {
        #region Singleton

        static PokemonImageAnalysisModel Instance;
        public static PokemonImageAnalysisModel GetInstance()
        {
            if (Instance == null)
                Instance = new PokemonImageAnalysisModel();
            return Instance;
        }

        #endregion

        public int Start(string imagePath)
        {
            System.Threading.Thread.Sleep(800);

            // TODO 分析演出などを入れる
            return 1;
        }

    }
}
