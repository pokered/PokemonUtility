using PokemonUtility.Models.Abstract;

namespace PokemonUtility.Models.Party
{
    class PartyWindowModel : WindowModel
    {
        private bool _isShowWindow = false;
        public bool IsShowWindow
        {
            get { return _isShowWindow; }
            set { SetProperty(ref _isShowWindow, value); }
        }

        private bool _windowEnabled = true;
        public bool WindowEnabled
        {
            get { return _windowEnabled; }
            set { SetProperty(ref _windowEnabled, value); }
        }
    }

    class MyPartyWindowModel : PartyWindowModel
    {
        #region Singleton

        static MyPartyWindowModel Instance;
        public static MyPartyWindowModel GetInstance()
        {
            if (Instance == null)
                Instance = new MyPartyWindowModel();
            return Instance;
        }

        #endregion
    }

    class OpponentPartyWindowModel : PartyWindowModel
    {
        #region Singleton

        static OpponentPartyWindowModel Instance;
        public static OpponentPartyWindowModel GetInstance()
        {
            if (Instance == null)
                Instance = new OpponentPartyWindowModel();
            return Instance;
        }

        #endregion
    }
}
