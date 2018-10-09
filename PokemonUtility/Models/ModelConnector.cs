using PokemonUtility.Models.BattleHistory;
using PokemonUtility.Models.Capture;
using PokemonUtility.Models.Main;
using PokemonUtility.Models.Party;
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

        public static CaptureImageManegementModel CaptureImageManegement
        {
            get { return CaptureImageManegementModel.GetInstance(); }
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
    }
}
