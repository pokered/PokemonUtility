using PokemonUtility.Models.Party;
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

        private MyPartyManegementModel _myPartyManegementModel = MyPartyManegementModel.GetInstance();
        private OpponentPartyManegementModel _opponentPartyManegementModel = OpponentPartyManegementModel.GetInstance();

        public void Save()
        {
            // 勝敗
            int battleResult = _mainWindowModel.BattleResult;

            // 世代
            int softGenerationId = _mainWindowModel.SoftGenerationId;

            // 整合性チェック

            // 各パーティーのポケモンをリスト化
            List<int> myPartyList = new List<int>() { };
            List<int> opponentPartyList = new List<int>() { };

            for (int i = 0; i < 6; i++)
            {
                myPartyList.Add(_myPartyManegementModel.GetPokemonId(i));
                opponentPartyList.Add(_opponentPartyManegementModel.GetPokemonId(i));
            }
            
            

        }

        private void PrepareParty()
        {

        }
    }
}
