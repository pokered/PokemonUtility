using PokemonUtility.Models.Abstract;

namespace PokemonUtility.Models.TodayBattleRecord
{
    class TodayBattleRecordWindowModel : SubWindowModel
    {
        #region Singleton

        static TodayBattleRecordWindowModel Instance;
        public static TodayBattleRecordWindowModel GetInstance()
        {
            if (Instance == null)
                Instance = new TodayBattleRecordWindowModel();
            return Instance;
        }

        #endregion
    }
}
