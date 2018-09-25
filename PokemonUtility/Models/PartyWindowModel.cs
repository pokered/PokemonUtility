namespace PokemonUtility.Models
{
    class PartyWindowModel : WindowModel
    {
        bool[] _isAlalyzingList = new bool[] { false, false, false, false, false, false };

        private bool _isShowWindow = false;
        public bool IsShowWindow
        {
            get { return _isShowWindow; }
            set { SetProperty(ref _isShowWindow, value); }
        }
    }
}
