using PokemonUtility.Struct;
using System.Collections.Generic;
using System.Data;

namespace PokemonUtility.Models.Database
{
    class PokemonDatabaseModel : DatabaseConnectModel
    {
        public List<Pokemon> GetPokemons()
        {
            string query = @"
                SELECT
                    pokemon_id,
                    name_ja
                FROM
                    pokemon_id_master
            ";

            var data = Select(query);

            List<Pokemon> pokemons = new List<Pokemon>();

            foreach (DataRow dataRow in data.Rows)
            {
                Pokemon pokemon = new Pokemon();
                pokemon.Id = ObjectConverter.ToInt(dataRow[0]);
                pokemon.Name = ObjectConverter.ToString(dataRow[1]);

                pokemons.Add(pokemon);
            }

            return pokemons;
        }

        public int GetPokemonIconId(int pokemonId)
        {
            string query = @"
                SELECT
                    pokemon_icon.pokemon_icon_id
                FROM
                    pokemon_id_master AS pokemon
                INNER JOIN
                    pokemon_icon_master AS pokemon_icon
                ON
                    pokemon.pokemon_id = pokemon_icon.pokemon_icon_id
                WHERE
                    pokemon.pokemon_id = {0}
            ";

            var data = Select(string.Format(query, pokemonId));

            foreach (DataRow dataRow in data.Rows)
                return ObjectConverter.ToInt(dataRow[0]);

            return -1;
        }

        public int GetPokemonIconId(string pokemonName)
        {
            string query = @"
                SELECT
                    pokemon_icon.pokemon_icon_id
                FROM
                    pokemon_id_master AS pokemon
                INNER JOIN
                    pokemon_icon_master AS pokemon_icon
                ON
                    pokemon.pokemon_id = pokemon_icon.pokemon_id
                WHERE
                    pokemon.name_ja = '{0}'
                    OR pokemon_icon.name_ja = '{0}'
            ";

            var data = Select(string.Format(query, pokemonName));

            foreach (DataRow dataRow in data.Rows)
                return ObjectConverter.ToInt(dataRow[0]);

            return -1;
        }
    }
}
