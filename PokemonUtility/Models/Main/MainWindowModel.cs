using PokemonUtility.Const;
using PokemonUtility.Models.Abstract;

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
    }
}
