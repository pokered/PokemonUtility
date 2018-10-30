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

        // 対戦履歴DB
        private BattleRecordDatabaseModel _battleRecordDatabaseModel = new BattleRecordDatabaseModel();

        // トレーナーId
        public int TrainerId
        {
            get { return _battleRecordDatabaseModel.TrainerId; }
            set { _battleRecordDatabaseModel.TrainerId = value; }
        }

        // トレーナーを条件に加えるか
        public bool IsWhereTrainerId
        {
            get { return _battleRecordDatabaseModel.IsWhereTrainerId; }
            set { _battleRecordDatabaseModel.IsWhereTrainerId = value; }
        }

        // 勝敗
        public int BattleResultId
        {
            get { return _battleRecordDatabaseModel.BattleResultId; }
            set { _battleRecordDatabaseModel.BattleResultId = value; }
        }

        // 勝敗を条件に加えるか
        public bool IsWhereBattleResultId
        {
            get { return _battleRecordDatabaseModel.IsWhereBattleResultId; }
            set { _battleRecordDatabaseModel.IsWhereBattleResultId = value; }
        }

        // 取得件数
        public int BattleRecordNumber
        {
            get { return _battleRecordDatabaseModel.BattleRecordNumber; }
            set { _battleRecordDatabaseModel.BattleRecordNumber = value; }
        }


        // トレーナーリスト
        public List<Trainer> Trainers { get; set; }

        // 勝敗リスト
        public List<BattleResult> BattleResults { get; set; }

        // 取得件数候補
        public List<int> BattleRecordNumberList { get; set; }

        public BattleHistoryWindowModel()
        {
            // データベースから一覧を取得
            TrainerDatabaseModel trainerDatabaseModel = new TrainerDatabaseModel();
            Trainers = trainerDatabaseModel.GetTrainers();

            // 勝敗リスト作成
            BattleResults = new List<BattleResult>();
            BattleResults.Add(new BattleResult() { Id = BattleResult.RESULT_WIN, Name = BattleResult.RESULT_WIN_JAPANESE });
            BattleResults.Add(new BattleResult() { Id = BattleResult.RESULT_LOSE, Name = BattleResult.RESULT_LOSE_JAPANESE });
            BattleResults.Add(new BattleResult() { Id = BattleResult.RESULT_DRAW, Name = BattleResult.RESULT_DRAW_JAPANESE });

            // 取得件数リストを作成
            BattleRecordNumberList = new List<int>() { 50, 100, 150, 200 };

            // トレーナーコンボボックスはアクティブにしておく
            IsWhereTrainerId = true;
        }

        public List<BattleRecord> GetBattleRecords()
        {
            _battleRecordDatabaseModel.MyPokemonIdList.Clear();
            _battleRecordDatabaseModel.OpponentPokemonIdList.Clear();
            // パーティー
            for (int i = 0; i < 6; i++)
            {
                _battleRecordDatabaseModel.MyPokemonIdList.Add(ModelConnector.BattleHistoryMyParty.GetPokemonId(i));
                _battleRecordDatabaseModel.OpponentPokemonIdList.Add(ModelConnector.BattleHistoryOpponentParty.GetPokemonId(i));
            }

            return _battleRecordDatabaseModel.SelectBattleRecords();
        }

        public List<BattleAggregate> GetMyBattleAggregates()
        {
            return _battleRecordDatabaseModel.SelectBattleAggregates();
        }

        public List<BattleAggregate> GetOpponentBattleAggregates()
        {
            return _battleRecordDatabaseModel.SelectBattleAggregates(false);
        }

    }
}
