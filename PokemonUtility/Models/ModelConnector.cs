using PokemonUtility.Models.Analysis;
using PokemonUtility.Models.BattleHistory;
using PokemonUtility.Models.Capture;
using PokemonUtility.Models.Main;
using PokemonUtility.Models.Party;
using PokemonUtility.Models.Search;
using PokemonUtility.Models.TodayBattleRecord;

namespace PokemonUtility.Models
{
    class ModelConnector
    {
        public static MainWindowModel MainWindow
        {
            get { return MainWindowModel.GetInstance(); }
        }


        public static CaptureWindowModel CaptureWindow
        {
            get { return CaptureWindowModel.GetInstance(); }
        }

        public static MyCaptureImageManegementModel MyCaptureImageManegement
        {
            get { return MyCaptureImageManegementModel.GetInstance(); }
        }

        public static OpponentCaptureImageManegementModel OpponentCaptureImageManegement
        {
            get { return OpponentCaptureImageManegementModel.GetInstance(); }
        }


        public static MyPartyWindowModel MyPartyWindow
        {
            get { return MyPartyWindowModel.GetInstance(); }
        }

        public static MyPartyManegementModel MyPartyManegement
        {
            get { return MyPartyManegementModel.GetInstance(); }
        }

        public static MyPartyWaitStateModel MyPartyWaitState
        {
            get { return MyPartyWaitStateModel.GetInstance(); }
        }

        public static OpponentPartyWindowModel OpponentPartyWindow
        {
            get { return OpponentPartyWindowModel.GetInstance(); }
        }

        public static OpponentPartyManegementModel OpponentPartyManegement
        {
            get { return OpponentPartyManegementModel.GetInstance(); }
        }

        public static OpponentPartyWaitStateModel OpponentPartyWaitState
        {
            get { return OpponentPartyWaitStateModel.GetInstance(); }
        }


        public static TodayBattleRecordWindowModel TodayBattleRecordWindow
        {
            get { return TodayBattleRecordWindowModel.GetInstance(); }
        }


        public static BattleHistoryWindowModel BattleHistoryWindow
        {
            get { return BattleHistoryWindowModel.GetInstance(); }
        }

        public static BattleHistoryMyPartyModel BattleHistoryMyParty
        {
            get { return BattleHistoryMyPartyModel.GetInstance(); }
        }

        public static BattleHistoryOpponentPartyModel BattleHistoryOpponentParty
        {
            get { return BattleHistoryOpponentPartyModel.GetInstance(); }
        }

        public static PokemonSearchWindowModel PokemonSearchWindow
        {
            get { return PokemonSearchWindowModel.GetInstance(); }
        }


        public static AnalysisModel Analysis
        {
            get { return AnalysisModel.GetInstance(); }
        }
    }
}
