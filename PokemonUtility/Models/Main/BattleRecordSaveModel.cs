using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonUtility.Models.Main
{
    class BattleRecordSaveModel
    {
        // メインモデル
        private MainWindowModel _mainWindowModel = MainWindowModel.GetInstance();

        public void Save()
        {
            // 勝敗
            int battleResult = _mainWindowModel.BattleResult;

            // 世代
            int softGenerationId = _mainWindowModel.SoftGenerationId;
        }
    }
}
