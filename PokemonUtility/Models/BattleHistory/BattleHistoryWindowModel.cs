using PokemonUtility.Models.Abstract;

namespace PokemonUtility.Models.BattleHistory
{
    class BattleHistoryWindowModel : SubWindowModel
    {
        #region Singleton

        static BattleHistoryWindowModel Instance;
        public static BattleHistoryWindowModel GetInstance()
        {
            if (Instance == null)
                Instance = new BattleHistoryWindowModel();
            return Instance;
        }

        #endregion
    }
}
