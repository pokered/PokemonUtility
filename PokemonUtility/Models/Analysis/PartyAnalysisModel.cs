using PokemonUtility.Const;
using Prism.Mvvm;
using System.Threading.Tasks;

namespace PokemonUtility.Models.Analysis
{
    class PartyAnalysisModel : BindableBase
    {
        private bool[] _isAnalyzingPokemonList = new bool[] { false, false, false, false, false, false };

        // 分析中か否か
        public bool IsAnalyzingPokemon1
        {
            get { return _isAnalyzingPokemonList[PartyConst.PARTY_INDEX1]; }
            set { SetProperty(ref _isAnalyzingPokemonList[PartyConst.PARTY_INDEX1], value); }
        }
        
        public bool IsAnalyzingPokemon2
        {
            get { return _isAnalyzingPokemonList[PartyConst.PARTY_INDEX2]; }
            set { SetProperty(ref _isAnalyzingPokemonList[PartyConst.PARTY_INDEX2], value); }
        }
        
        public bool IsAnalyzingPokemon3
        {
            get { return _isAnalyzingPokemonList[PartyConst.PARTY_INDEX3]; }
            set { SetProperty(ref _isAnalyzingPokemonList[PartyConst.PARTY_INDEX3], value); }
        }
        
        public bool IsAnalyzingPokemon4
        {
            get { return _isAnalyzingPokemonList[PartyConst.PARTY_INDEX4]; }
            set { SetProperty(ref _isAnalyzingPokemonList[PartyConst.PARTY_INDEX4], value); }
        }
        
        public bool IsAnalyzingPokemon5
        {
            get { return _isAnalyzingPokemonList[PartyConst.PARTY_INDEX5]; }
            set { SetProperty(ref _isAnalyzingPokemonList[PartyConst.PARTY_INDEX5], value); }
        }
        
        public bool IsAnalyzingPokemon6
        {
            get { return _isAnalyzingPokemonList[PartyConst.PARTY_INDEX6]; }
            set { SetProperty(ref _isAnalyzingPokemonList[PartyConst.PARTY_INDEX6], value); }
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
            get { return _waitStateList[PartyConst.PARTY_INDEX1]; }
            set
            {
                SetProperty(ref _waitStateList[PartyConst.PARTY_INDEX1], value);
            }
        }

        public int WaitState2
        {
            get { return _waitStateList[PartyConst.PARTY_INDEX2]; }
            set
            {
                SetProperty(ref _waitStateList[PartyConst.PARTY_INDEX2], value);
            }
        }

        public int WaitState3
        {
            get { return _waitStateList[PartyConst.PARTY_INDEX3]; }
            set
            {
                SetProperty(ref _waitStateList[PartyConst.PARTY_INDEX3], value);
            }
        }

        public int WaitState4
        {
            get { return _waitStateList[PartyConst.PARTY_INDEX4]; }
            set
            {
                SetProperty(ref _waitStateList[PartyConst.PARTY_INDEX4], value);
            }
        }

        public int WaitState5
        {
            get { return _waitStateList[PartyConst.PARTY_INDEX5]; }
            set
            {
                SetProperty(ref _waitStateList[PartyConst.PARTY_INDEX5], value);
            }
        }

        public int WaitState6
        {
            get { return _waitStateList[PartyConst.PARTY_INDEX6]; }
            set
            {
                SetProperty(ref _waitStateList[PartyConst.PARTY_INDEX6], value);
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
