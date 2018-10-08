using PokemonUtility.Const;
using PokemonUtility.Models.Database;
using PokemonUtility.Models.Party;
using System.Collections.Generic;
using System.Linq;

namespace PokemonUtility.Models.Main
{
    class BattleRecordSaveModel
    {
        // メインモデル
        private MainWindowModel _mainWindowModel = MainWindowModel.GetInstance();

        private MyPartyManegementModel _myPartyManegementModel = MyPartyManegementModel.GetInstance();
        private OpponentPartyManegementModel _opponentPartyManegementModel = OpponentPartyManegementModel.GetInstance();

        BattleRecordDatabaseModel _battleRecordDatabaseModel = new BattleRecordDatabaseModel();

        // 戦績レコードID
        private int battleRecordId = -1;

        // エラーコード
        private int error_code = ErrorConst.ERROR_NO;

        public void Save()
        {
            // 保存に失敗した場合関連するレコードをすべて削除
            if (!Run())
            {
                _battleRecordDatabaseModel.DeleteBattleRecord(battleRecordId);
            }

            // ログ記載
            if (error_code == ErrorConst.ERROR_NO)
            {
                _mainWindowModel.AddLog("戦績を保存しました。");
            }
            else if (error_code == ErrorConst.ERROR_INSERT_FAIL)
            {
                _mainWindowModel.AddLog("戦績を保存できませんでした。");
            }
            else if (error_code == ErrorConst.ERROR_PARTY_DUPRICATED)
            {
                _mainWindowModel.AddLog("パーティーのポケモンが重複しています。");
            }
            else if (error_code == ErrorConst.ERROR_PARTY_POKEMON_LACK)
            {
                _mainWindowModel.AddLog("パーティーには3匹ポケモンが必要です。");
            }
        }

        private bool Run()
        {
            // TODO トレーナーIDをどうするか 起動時にＤＢに登録する？

            // 勝敗
            int battleResult = _mainWindowModel.BattleResult;

            // 世代
            int softGenerationId = _mainWindowModel.SoftGenerationId;

            // 整合性チェック
            if (!PrepareParty(_myPartyManegementModel) || !PrepareParty(_opponentPartyManegementModel)) return false;

            // 戦績保存
            battleRecordId = _battleRecordDatabaseModel.InsertBattleRecord(softGenerationId);

            if (battleRecordId == DatabaseConst.INSERT_FAIL) return false;

            // 自分のパーティー保存
            if (!SaveBattleParty(battleRecordId, battleResult, TrainerConst.TRAINER_MYSELF, _myPartyManegementModel)) return false;

            // 相手の勝敗
            if (battleResult == BattleResultConst.WIN)
            {
                battleResult = BattleResultConst.LOSE;
            }
            else if(battleResult == BattleResultConst.LOSE)
            {
                battleResult = BattleResultConst.WIN;
            }

            // 相手のパーティー保存
            if (!SaveBattleParty(battleRecordId, battleResult, TrainerConst.TRAINER_UNKNOWN, _opponentPartyManegementModel)) return false;

            return true;
        }

        private bool SaveBattleParty(int battleRecordId, int battleResult, int trainerId, PartyManegementModel party)
        {
            // パーティー保存
            int battlePartyId = _battleRecordDatabaseModel.InsertBattleParty(battleRecordId, battleResult, trainerId);

            // ポケモン保存
            for (int i = PartyConst.PARTY_INDEX_FIRST; i <= PartyConst.PARTY_INDEX_SIXTH; i++)
            {
                int insertId = _battleRecordDatabaseModel.InsertBattlePokemon(battlePartyId, party.GetOrder(i), party.GetPokemonId(i));
            }

            return true;
        }

        // パーティー整合性チェック
        private bool PrepareParty(PartyManegementModel party)
        {
            List<int> partyList = new List<int>();

            for (int i = 0; i < 6; i++)
            {
                partyList.Add(party.GetPokemonId(i));
            }

            // 存在するポケモンが3匹未満の場合保存できない
            var partyExistList = partyList.Where(pokemonId => ImageFactoryModel.ExistPokemonImage(pokemonId)).ToArray();
            if (partyExistList.Length < 3)
            {
                _mainWindowModel.AddLog("パーティーには3匹ポケモンが必要です。");
                return false;
            }

            // ポケモン重複チェック
            var duplicatedList = partyExistList.GroupBy(pokemonId => pokemonId).Where(g => g.Count() > 1).ToArray();
            if (duplicatedList.Length > 0)
            {
                _mainWindowModel.AddLog("パーティーのポケモンが重複しています。");
                return false;
            }

            return true;
        }
    }
}
