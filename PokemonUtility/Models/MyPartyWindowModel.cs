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

        public bool IsEnable
        {
            get { return _isEnable; }
            set { SetProperty(ref _isEnable, value); }
        }
        private bool _isEnable = false;
    }
}
