using System;

namespace PokemonUtility.Models.Database
{
    class BattleRecordDatabaseModel : DatabaseConnectModel
    {
        public int InsertBattleRecord(int generation)
        {
            string query = @"
                INSERT INTO battle_records (generation)
                VALUES (100);
            ";

            query = string.Format(query, generation);
            
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
