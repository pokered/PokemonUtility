using PokemonUtility.Const;
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

            DataTable dt = Select(query);

            if (dt.Rows.Count > 0) return ObjectConverter.ToInt(dt.Rows[0][0]);

            return PokemonConst.POKEMON_ID_NO;
        }
    }
}
