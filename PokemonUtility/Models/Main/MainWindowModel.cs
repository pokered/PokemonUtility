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

        // 分析中
        private bool _isAnalyzing = false;
        public bool IsAnalyzing
        {
            get { return _isAnalyzing; }
            set { SetProperty(ref _isAnalyzing, value); }
        }



        // 対戦結果
        private int _battle_result = BattleResultConst.WIN;
        public int Battle_result
        {
            get { return _battle_result; }
            set { SetProperty(ref _battle_result, value); }
        }
    }
}
