using PokemonUtility.Struct;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

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

        public List<BattleRecord> SelectBattleRecords()
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT");
            query.AppendLine("record.battle_record_id,");
            query.AppendLine("my_party.battle_result_id AS result,");
            query.AppendLine("CASE WHEN");
            query.AppendLine("  EXISTS (SELECT * FROM trainer_master WHERE trainer_id = my_party.trainer_id)");
            query.AppendLine("  THEN (SELECT name FROM trainer_master WHERE trainer_id = my_party.trainer_id)");
            query.AppendLine("  ELSE ''");
            query.AppendLine("  END my_trainer_name,");

            query.AppendLine("(");
            query.AppendLine("  SELECT");
            query.AppendLine("  GROUP_CONCAT(pokemon_id)");
            query.AppendLine("  FROM battle_pokemons");
            query.AppendLine("  WHERE battle_party_id = my_party.battle_party_id");
            query.AppendLine(") AS my_pokemon_id,");

            query.AppendLine("(");
            query.AppendLine("  SELECT");
            query.AppendLine("  GROUP_CONCAT(pokemon_icon_id)");
            query.AppendLine("  FROM battle_pokemons");
            query.AppendLine("  WHERE battle_party_id = my_party.battle_party_id");
            query.AppendLine(") AS my_pokemon_icon_id,");

            query.AppendLine("(");
            query.AppendLine("  SELECT");
            query.AppendLine("  GROUP_CONCAT(election)");
            query.AppendLine("  FROM battle_pokemons");
            query.AppendLine("  WHERE battle_party_id = my_party.battle_party_id");
            query.AppendLine(") AS my_election,");

            query.AppendLine("CASE WHEN");
            query.AppendLine("  EXISTS (SELECT * FROM trainer_master WHERE trainer_id = opponent_party.trainer_id)");
            query.AppendLine("  THEN (SELECT name FROM trainer_master WHERE trainer_id = opponent_party.trainer_id)");
            query.AppendLine("  ELSE ''");
            query.AppendLine("  END my_trainer_name,");

            query.AppendLine("(");
            query.AppendLine("  SELECT");
            query.AppendLine("  GROUP_CONCAT(pokemon_id)");
            query.AppendLine("  FROM battle_pokemons");
            query.AppendLine("  WHERE battle_party_id = opponent_party.battle_party_id");
            query.AppendLine(") AS opponent_pokemon_id,");

            query.AppendLine("(");
            query.AppendLine("  SELECT");
            query.AppendLine("  GROUP_CONCAT(pokemon_icon_id)");
            query.AppendLine("  FROM battle_pokemons");
            query.AppendLine("  WHERE battle_party_id = opponent_party.battle_party_id");
            query.AppendLine(") AS opponent_pokemon_icon_id,");
            query.AppendLine("(");
            query.AppendLine("  SELECT");
            query.AppendLine("  GROUP_CONCAT(election)");
            query.AppendLine("  FROM battle_pokemons");
            query.AppendLine("  WHERE battle_party_id = opponent_party.battle_party_id");
            query.AppendLine(") AS opponent_election,");
            query.AppendLine("record.insert_at");
            query.AppendLine("FROM");
            query.AppendLine(" battle_records AS record");
            query.AppendLine(" INNER JOIN battle_parties AS my_party");
            query.AppendLine(" ON record.battle_record_id = my_party.battle_record_id");
            query.AppendLine(" INNER JOIN battle_parties AS opponent_party");
            query.AppendLine(" ON record.battle_record_id = opponent_party.battle_record_id AND");
            query.AppendLine(" opponent_party.battle_party_id <> my_party.battle_party_id");
            query.AppendLine("WHERE (1=1)");
            


            string query1 = @"
                SELECT
                record.battle_record_id,
                my_party.battle_result_id AS result,

                CASE WHEN
                    EXISTS (SELECT * FROM trainer_master WHERE trainer_id = my_party.trainer_id)
                    THEN (SELECT name FROM trainer_master WHERE trainer_id = my_party.trainer_id)
                    ELSE ''
                    END my_trainer_name,

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

                CASE WHEN
                    EXISTS (SELECT * FROM trainer_master WHERE trainer_id = opponent_party.trainer_id)
                    THEN (SELECT name FROM trainer_master WHERE trainer_id = opponent_party.trainer_id)
                    ELSE ''
                    END opponent_trainer_name,

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
                string addQuery = string.Format(" AND my_party.trainer_id = {0} ", TrainerId);
                query.AppendLine(addQuery);
            }

            // 勝敗条件
            if (IsWhereBattleResultId)
            {
                string addQuery = string.Format(" AND my_party.battle_result_id = {0} ", BattleResultId);
                query.AppendLine(addQuery);
            }

            // having
            StringBuilder havingQuery = new StringBuilder();

            // 自分のパーティー
            foreach (int myPokemonId in MyPokemonIdList)
            {
                // 存在しなければ飛ばす
                if (!ImageFactoryModel.ExistPokemonImage(myPokemonId)) continue;

                havingQuery.AppendLine("AND FIND_IN_SET(");
                havingQuery.AppendLine("   (SELECT");
                havingQuery.AppendLine("       pokemon_id");
                havingQuery.AppendLine("   FROM");
                havingQuery.AppendLine("       pokemon_icon_master");
                havingQuery.AppendLine("   WHERE");
                havingQuery.AppendLine(string.Format("pokemon_icon_id = {0}),", myPokemonId));
                havingQuery.AppendLine("my_pokemon_id)");
            }

            // 相手のパーティー
            foreach (int opponentPokemonId in OpponentPokemonIdList)
            {
                // 存在しなければ飛ばす
                if (!ImageFactoryModel.ExistPokemonImage(opponentPokemonId)) continue;

                havingQuery.AppendLine("AND FIND_IN_SET(");
                havingQuery.AppendLine("   (SELECT");
                havingQuery.AppendLine("       pokemon_id");
                havingQuery.AppendLine("   FROM");
                havingQuery.AppendLine("       pokemon_icon_master");
                havingQuery.AppendLine("   WHERE");
                havingQuery.AppendLine(string.Format("pokemon_icon_id = {0}),", opponentPokemonId));
                havingQuery.AppendLine("opponent_pokemon_id)");
            }

            // having句追加
            if (havingQuery.Length > 0)
            {
                query.AppendLine("HAVING (1=1)");
                query.AppendLine(havingQuery.ToString());
            }

            //// レコードが重複するので削除
            //query.AppendLine("GROUP BY record.battle_record_id");

            // ソート
            query.AppendLine("ORDER BY record.insert_at DESC");

            // 取得件数
            query.AppendLine(string.Format(" LIMIT {0} ", BattleRecordNumber));

            var data = Select(query.ToString());

            List<BattleRecord> BattleRecordList = new List<BattleRecord>();

            foreach (DataRow dataRow in data.Rows)
            {
                // 構造体に入れる
                BattleRecord battleRecord = new BattleRecord();

                battleRecord.BattleRecordId = ObjectConverter.ToInt(dataRow[0]);
                battleRecord.BattleResultId = ObjectConverter.ToInt(dataRow[1]);

                // 自分のトレーナー名
                battleRecord.MyTrainerName = ObjectConverter.ToString(dataRow[2]);

                // 自分のパーティー
                ObjectConverter.ToString(dataRow[4])
                    .Split(',')
                    .Select((id, index) => new { Id = ObjectConverter.ToInt(id), Index = index })
                    .ToList().ForEach(e => battleRecord.ChangeMyPokemonId(e.Index, e.Id));

                // 自分のオーダー
                ObjectConverter.ToString(dataRow[5])
                    .Split(',')
                    .Select((order, index) => new { Order = ObjectConverter.ToInt(order), Index = index })
                    .ToList().ForEach(e => battleRecord.ChangeMyPokemonOrder(e.Index, e.Order));

                // 相手のトレーナー名
                battleRecord.OpponentTrainerName = ObjectConverter.ToString(dataRow[6]);


                // 相手のパーティー
                ObjectConverter.ToString(dataRow[8])
                    .Split(',')
                    .Select((id, index) => new { Id = ObjectConverter.ToInt(id), Index = index })
                    .ToList().ForEach(e => battleRecord.ChangeOpponentPokemonId(e.Index, e.Id));

                // 相手のオーダー
                ObjectConverter.ToString(dataRow[9])
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
