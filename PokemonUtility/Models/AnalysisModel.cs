namespace PokemonUtility.Models
{
    class AnalysisModel
    {
        #region Singleton

        static AnalysisModel Instance;
        public static AnalysisModel GetInstance()
        {
            if (Instance == null)
                Instance = new AnalysisModel();
            return Instance;
        }

        #endregion

        public int[] start()
        {
            // TODO 分析演出などを入れる
            return new int[] { 0, 1, 2, 3, 4, 5 };
        }
    }
}
