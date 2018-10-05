using PokemonUtility.Const;
using System;
using System.Data;

namespace PokemonUtility.Models.Database
{
    class PokemonIdConverterModel
    {
        private static int PREDICT_VERSION = 560;

        public static int ToOriginalPokemonId(int new_pokemon_id)
        {
            DatabaseConnectModel databaseConnectModel = new DatabaseConnectModel();

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
            DataTable data = databaseConnectModel.Select(query);

            // 1件目のidを返す
            if (data.Rows.Count > 0) return Int32.Parse(data.Rows[0][0].ToString());

            return PokemonConst.POKEMON_ID_NO;
        }
    }
}
