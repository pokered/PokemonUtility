namespace PokemonUtility.Models.Abstract
{
    abstract class SubWindowModel : WindowModel
    {
        private bool _isShowWindow = false;
        public bool IsShowWindow
        {
            get { return _isShowWindow; }
            set { SetProperty(ref _isShowWindow, value); }
        }
    }
}
