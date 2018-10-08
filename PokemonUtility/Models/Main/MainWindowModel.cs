using PokemonUtility.Const;
using PokemonUtility.Models.Abstract;
using System;

namespace PokemonUtility.Models.Main
{
    class MainWindowModel : WindowModel
    {
        #region Singleton

        static MainWindowModel Instance;
        public static MainWindowModel GetInstance()
        {
            if (Instance == null)
                Instance = new MainWindowModel();
            return Instance;
        }

        #endregion

        // 対戦結果
        private int _battleResult = BattleResultConst.WIN;
        public int BattleResult
        {
            get { return _battleResult; }
            set { SetProperty(ref _battleResult, value); }
        }

        // 世代
        private int _softGenerationId = 0;
        public int SoftGenerationId
        {
            get { return _softGenerationId; }
            set { SetProperty(ref _softGenerationId, value); }
        }

        // ログ
        private string _log = "";
        public string Log
        {
            get { return _log; }
            set { SetProperty(ref _log, value); }
        }

        public void AddLog(string message)
        {
            // メッセージに時間を付加
            message += " " + DateTime.Now.ToString("HH:mm");

            // メッセージを繋げる
            string log = message + "\n" + Log;

            Log = log.Trim();
        }
    }
}
