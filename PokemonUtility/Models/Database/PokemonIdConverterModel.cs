using MySql.Data.MySqlClient;
using PokemonUtility.Const;
using System;
using System.Data;

namespace PokemonUtility.Models.Database
{
    class PokemonIdConverterModel : DatabaseConnectModel
    {
        private int PREDICT_VERSION = 560;

        public int ToOriginalPokemonId(int new_pokemon_id)
        {
            string query = @"
            SELECT
                original_pokemon_icon_id
            FROM
                pokemon_icon_id_convert_new_to_original
            WHERE
                version = '{0}' AND
                new_pokemon_icon_id = '{1}'
            ;
            ";

            // ポケモンIDをオリジナルに変換
            query = string.Format(query, PREDICT_VERSION, new_pokemon_id);
            using (var con = new MySqlConnection(_connectionString))
            {
                // コマンド
                var command = new MySqlCommand(query, con);

                // SQLを実行します。
                using (var executeReader = command.ExecuteReader())
                {
                    while (executeReader.Read())
                    {
                        return (int)executeReader["original_pokemon_icon_id"];
                    }
                }
            }
            
            return PokemonConst.POKEMON_ID_NO;
        }
    }
}
