using Prism.Mvvm;

namespace PokemonUtility.Models.Analysis
{
    class AnalysisSynthesis : BindableBase
    {
        // モデル
        MyPartyAnalysisModel _myPartyAnalysisModel = MyPartyAnalysisModel.GetInstance();

        public int Start()
        {
            // キャプチャ範囲をキャプチャ
            for (int i = 0; i < 6; i++)
            {
                // 待機アニメーション

            }

            return 1;
        }
    }
}
