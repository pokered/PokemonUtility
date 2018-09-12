using Prism.Mvvm;

namespace PokemonUtility.Models
{
    class MyPartyWindowModel : BindableBase
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

        private MyPartyWindowModel()
        {
        }

        public string Mess
        {
            get { return _mess; }
            set { SetProperty(ref _mess, value); }
        }
        private string _mess = "bbb";

        public bool IsShowWindow
        {
            get { return _isShowWindow; }
            set { SetProperty(ref _isShowWindow, value); }
        }
        private bool _isShowWindow = false;
    }
}
