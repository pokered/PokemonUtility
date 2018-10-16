using PokemonUtility.Struct;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PokemonUtility.Models.Database
{
    class BattleRecordDatabaseModel : DatabaseConnectModel
    {
        public List<int> MyPokemonIdList { get; set; } = new List<int>();
        public List<int> OpponentPokemonIdList { get; set; } = new List<int>();
        public bool IsWhereTrainerId { get; set; } = false;
        public bool IsWhereBattleResultId { get; set; } = false;
        public int TrainerId { get; set; }
        public int BattleResultId { get; set; }
        public int BattleRecordNumber { get; set; } = 50;

        public List<BattleRecord> SelectBattleRecordExecute()
        {
            string query = @"
                SELECT
                my_party.battle_result_id AS result,
                (
                    SELECT
                    GROUP_CONCAT(pokemon_icon_id)
                    FROM battle_pokemons
                    WHERE battle_party_id = my_party.battle_party_id
                ) AS my_pokemon_icon_id,
                (
                    SELECT
                    GROUP_CONCAT(election)
                    FROM battle_pokemons
                    WHERE battle_party_id = my_party.battle_party_id
                ) AS my_election,
                (
                    SELECT
                    GROUP_CONCAT(pokemon_icon_id)
                    FROM battle_pokemons
                    WHERE battle_party_id = opponent_party.battle_party_id
                ) AS opponent_pokemon_icon_id,
                (
                    SELECT
                    GROUP_CONCAT(election)
                    FROM battle_pokemons
                    WHERE battle_party_id = opponent_party.battle_party_id
                ) AS opponent_election,
                record.insert_at
                FROM
                    battle_records AS record
                    INNER JOIN battle_parties AS my_party
                    ON record.battle_record_id = my_party.battle_record_id
                    INNER JOIN battle_parties AS opponent_party
                    ON record.battle_record_id = opponent_party.battle_record_id AND
                    opponent_party.battle_party_id <> my_party.battle_party_id
                WHERE (1=1)
            ";

            // トレーナー指定
            if (IsWhereTrainerId)
            {
                string addQuery = " AND my_party.trainer_id = {0} ";
                query += string.Format(addQuery, TrainerId);
            }

            // 勝敗条件
            if (IsWhereBattleResultId)
            {
                string addQuery = " AND my_party.battle_result_id = {0} ";
                query += string.Format(addQuery, BattleResultId);
            }

            // having
            string havingQuery = "";

            // 自分のパーティー
            foreach (int myPokemonId in MyPokemonIdList)
            {
                // 存在しなければ飛ばす
                if (!ImageFactoryModel.ExistPokemonImage(myPokemonId)) continue;

                string addQuery = @"
                            AND FIND_IN_SET(
                                (SELECT
                                    pokemon_id
                                FROM
                                    pokemon_icon_master
                                WHERE
                                    pokemon_icon_id = {0}),
                            my_pokemon_id)";

                havingQuery += string.Format(addQuery, myPokemonId);
            }

            // 相手のパーティー
            foreach (int opponentPokemonId in OpponentPokemonIdList)
            {
                // 存在しなければ飛ばす
                if (!ImageFactoryModel.ExistPokemonImage(opponentPokemonId)) continue;

                string addQuery = @"
                            AND FIND_IN_SET(
                                (SELECT
                                    pokemon_id
                                FROM
                                    pokemon_icon_master
                                WHERE
                                    pokemon_icon_id = {0}),
                            opponent_pokemon_id)";

                havingQuery += string.Format(addQuery, opponentPokemonId);
            }

            // having句追加
            if (havingQuery.Length > 0) query += " HAVING (1=1) " + havingQuery;

            // レコードが重複するので削除
            query += " GROUP BY record.battle_record_id";

            // ソート
            query += " ORDER BY record.insert_at DESC ";

            // 取得件数
            query += string.Format(" LIMIT {0} ", BattleRecordNumber);

            var data = Select(query);

            List<BattleRecord> BattleRecordList = new List<BattleRecord>();

            foreach (DataRow dataRow in data.Rows)
            {
                // 構造体に入れる
                BattleRecord battleRecord = new BattleRecord();
                battleRecord.ChangeBattleResult(ObjectConverter.ToInt(dataRow[0]));

                // 自分のパーティー
                ObjectConverter.ToString(dataRow[1])
                    .Split(',')
                    .Select((id, index) => new { Id = ObjectConverter.ToInt(id), Index = index })
                    .ToList().ForEach(e => battleRecord.ChangeMyPokemonId(e.Index, e.Id));

                // 自分のオーダー
                ObjectConverter.ToString(dataRow[2])
                    .Split(',')
                    .Select((order, index) => new { Order = ObjectConverter.ToInt(order), Index = index })
                    .ToList().ForEach(e => battleRecord.ChangeMyPokemonOrder(e.Index, e.Order));

                // 相手のパーティー
                ObjectConverter.ToString(dataRow[3])
                    .Split(',')
                    .Select((id, index) => new { Id = ObjectConverter.ToInt(id), Index = index })
                    .ToList().ForEach(e => battleRecord.ChangeOpponentPokemonId(e.Index, e.Id));

                // 相手のオーダー
                ObjectConverter.ToString(dataRow[4])
                    .Split(',')
                    .Select((order, index) => new { Order = ObjectConverter.ToInt(order), Index = index })
                    .ToList().ForEach(e => battleRecord.ChangeOpponentPokemonOrder(e.Index, e.Order));

                BattleRecordList.Add(battleRecord);
            }

            return BattleRecordList;
        }
        
        public List<BattleAggregate> SelectBattleAggregates(bool myViewPoint=true)
        {
            string query = @"
                SELECT
                    (SELECT
                        pokemon_icon_id
	                FROM
                        pokemon_icon_master
	                WHERE
                        pokemon_id = pokemon.pokemon_id
	                LIMIT 1) AS pokemon_icon_id,
                    count(*) AS overlap_number,
	                SUM( 0 <= election AND election <= 2 ) AS election_num,
	                SUM( election = 0 ) AS top_num,
	                SUM( 0 <= election AND election <= 2 AND battle_result_id = 0) As win_num
                FROM
                    battle_pokemons AS pokemon
                INNER JOIN
                    battle_parties AS party
                ON
                    pokemon.battle_party_id = party.battle_party_id
                WHERE (1=1)
            ";

            // トレーナー指定
            if (IsWhereTrainerId)
            {
                string trainerQuery = (myViewPoint) ? " AND party.trainer_id = {0} " : " AND party.trainer_id <> {0} ";
                query += string.Format(trainerQuery, TrainerId);
            }

            // 勝敗条件
            if (IsWhereBattleResultId)
            {
                int tmpBattleResultId = (myViewPoint) ? BattleResultId : BattleResult.FlipBattleResult(BattleResultId);
                query += string.Format(" AND party.battle_result_id = {0} ", tmpBattleResultId);
            }

            // 集計
            query += "GROUP BY pokemon.pokemon_id";

            // 取得件数
            query += string.Format(" LIMIT {0} ", BattleRecordNumber);

            // データ取得
            var data = Select(query);

            // データ変換
            List<BattleAggregate> battleAggregateList = new List<BattleAggregate>();

            foreach(DataRow dataRow in data.Rows)
            {
                BattleAggregate battleAggregate = new BattleAggregate();
                battleAggregate.PokemonIconId = ObjectConverter.ToInt(dataRow[0]);
                battleAggregate.OverlapNumber = ObjectConverter.ToInt(dataRow[1]);
                battleAggregate.ElectionNumber = ObjectConverter.ToInt(dataRow[2]);
                battleAggregate.LeadNumber = ObjectConverter.ToInt(dataRow[3]);
                battleAggregate.WinNumber = ObjectConverter.ToInt(dataRow[4]);
                battleAggregateList.Add(battleAggregate);
            }

            return battleAggregateList;
        }

        public int InsertBattleRecord(int softGeneration)
        {
            string query = @"
                INSERT INTO battle_records (generation)
                VALUES (100);
            ";

            query = string.Format(query, softGeneration);
            
            return Insert(query);
        }

        public int InsertBattleParty(int battle_record_id, int battle_result_id, int trainer_id)
        {
            string query = @"
                INSERT INTO battle_parties (battle_record_id, battle_result_id, trainer_id)
                VALUES ({0}, {1}, {2});
            ";

            query = string.Format(query, battle_record_id, battle_result_id, trainer_id);

            // SQLログ
            Console.WriteLine(query);

            return Insert(query);
        }

        public int InsertBattlePokemon(int battle_party_id, int election, int pokemon_icon_id)
        {
            string query = @"
                INSERT INTO battle_pokemons (battle_party_id, election, pokemon_icon_id, pokemon_id)
                VALUES ({0}, {1}, {2},
                    (
                    SELECT
                        pokemon_id
                    FROM
                        pokemon_icon_master
                    WHERE
                        pokemon_icon_id = {2}
                    )
                );
            ";

            query = string.Format(query, battle_party_id, election, pokemon_icon_id);

            return Insert(query);
        }

        public bool DeleteBattleRecord(int battleRecordId)
        {
            string query = @"
                DELETE FROM battle_records
                WHERE battle_record_id = {0};
            ";

            query = string.Format(query, battleRecordId);

            return Delete(query);
        }
    }
}
