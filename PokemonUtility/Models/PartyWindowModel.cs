namespace PokemonUtility.Models
{
    class PartyWindowModel : WindowModel
    {
        private bool _isShowWindow = false;
        public bool IsShowWindow
        {
            get { return _isShowWindow; }
            set { SetProperty(ref _isShowWindow, value); }
        }

        private bool _isAnalysisPokemon1 = false;
        public bool IsAnalysisPokemon1
        {
            get { return _isAnalysisPokemon1; }
            set { SetProperty(ref _isAnalysisPokemon1, value); }
        }

        private bool _isAnalysisPokemon2 = false;
        public bool IsAnalysisPokemon2
        {
            get { return _isAnalysisPokemon2; }
            set { SetProperty(ref _isAnalysisPokemon2, value); }
        }

        private bool _isAnalysisPokemon3 = false;
        public bool IsAnalysisPokemon3
        {
            get { return _isAnalysisPokemon3; }
            set { SetProperty(ref _isAnalysisPokemon3, value); }
        }

        private bool _isAnalysisPokemon4 = false;
        public bool IsAnalysisPokemon4
        {
            get { return _isAnalysisPokemon4; }
            set { SetProperty(ref _isAnalysisPokemon4, value); }
        }

        private bool _isAnalysisPokemon5 = false;
        public bool IsAnalysisPokemon5
        {
            get { return _isAnalysisPokemon5; }
            set { SetProperty(ref _isAnalysisPokemon5, value); }
        }

        private bool _isAnalysisPokemon6 = false;
        public bool IsAnalysisPokemon6
        {
            get { return _isAnalysisPokemon6; }
            set { SetProperty(ref _isAnalysisPokemon6, value); }
        }
    }
}
