using Prism.Mvvm;
using System.Threading.Tasks;

namespace PokemonUtility.Models.Analysis
{
    class PartyAnalysisModel : BindableBase
    {
        private const int POKEMON_INDEX1 = 0;
        private const int POKEMON_INDEX2 = 1;
        private const int POKEMON_INDEX3 = 2;
        private const int POKEMON_INDEX4 = 3;
        private const int POKEMON_INDEX5 = 4;
        private const int POKEMON_INDEX6 = 5;

        // 分析中か否か
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

        public void StartAnalysis(int index)
        {
            IsAnalysisPokemon1 = true;
        }

        public void EndAnalysis(int index)
        {
            IsAnalysisPokemon1 = false;
        }
        
        int[] _waitStateList = new[] { -1, -1, -1, -1, -1, -1 };

        public int WaitState1
        {
            get { return _waitStateList[POKEMON_INDEX1]; }
            set
            {
                SetProperty(ref _waitStateList[POKEMON_INDEX1], value);
            }
        }

        public int WaitState2
        {
            get { return _waitStateList[POKEMON_INDEX2]; }
            set
            {
                SetProperty(ref _waitStateList[POKEMON_INDEX2], value);
            }
        }

        public int WaitState3
        {
            get { return _waitStateList[POKEMON_INDEX3]; }
            set
            {
                SetProperty(ref _waitStateList[POKEMON_INDEX3], value);
            }
        }

        public int WaitState4
        {
            get { return _waitStateList[POKEMON_INDEX4]; }
            set
            {
                SetProperty(ref _waitStateList[POKEMON_INDEX4], value);
            }
        }

        public int WaitState5
        {
            get { return _waitStateList[POKEMON_INDEX5]; }
            set
            {
                SetProperty(ref _waitStateList[POKEMON_INDEX5], value);
            }
        }

        public int WaitState6
        {
            get { return _waitStateList[POKEMON_INDEX6]; }
            set
            {
                SetProperty(ref _waitStateList[POKEMON_INDEX6], value);
            }
        }

        public PartyAnalysisModel()
        {
        }

        // 待機状態を変更
        public async Task WaitAnimation()
        {
            while (IsAnalysisPokemon1)
            {
                WaitState1 = 0;
                await Task.Delay(300);
                WaitState1 = 1;
                await Task.Delay(300);
                WaitState1 = 2;
                await Task.Delay(300);
                WaitState1 = 3;
            }

            // 終了したら消す
            WaitState1 = -1;
        }
    }
}
