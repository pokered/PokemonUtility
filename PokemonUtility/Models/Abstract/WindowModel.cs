using Prism.Mvvm;

namespace PokemonUtility.Models.Abstract
{
    abstract class WindowModel : BindableBase
    {
        // 座標X
        private int _x = 0;
        public int X
        {
            get { return _x; }
            set { SetProperty(ref _x, value); }
        }

        // 座標Y
        private int _y = 0;
        public int Y
        {
            get { return _y; }
            set { SetProperty(ref _y, value); }
        }

        // 分析中か否か
        private bool _isAnalyzing = false;
        public bool IsAnalyzing
        {
            get { return _isAnalyzing; }
            set { SetProperty(ref _isAnalyzing, value); }
        }
    }
}
