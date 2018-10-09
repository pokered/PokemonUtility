using PokemonUtility.Models.Abstract;
using PokemonUtility.Models.Database;
using PokemonUtility.Struct;
using System.Collections.Generic;

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

        public List<TrainerInfo> Trainers { get; set; }

        public BattleHistoryWindowModel()
        {
            // データベースから一覧を取得
            TrainerDatabaseModel trainerDatabaseModel = new TrainerDatabaseModel();

            Trainers = trainerDatabaseModel.GetTrainers();
        }
    }
}
