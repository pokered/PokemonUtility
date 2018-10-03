using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonUtility.Models.Database
{
    class DatabaseConnectModel
    {
        public DataTable db()
        {
            // MySQLへの接続情報
            string server = "public.pflmr.tyo1.database-hosting.conoha.io";        // MySQLサーバホスト名
            string user = "pflmr_pokered";           // MySQLユーザ名
            string pass = "Pokemonpawapuro3827";           // MySQLパスワード
            string database = "pflmr_pokemon_test2";		// 接続するデータベース名
            string connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3}", server, database, user, pass);

            // MySQL の場合
            using (var con = new MySqlConnection(connectionString))
            {
                var command = new MySqlCommand("select * from pokemon_id_master;", con);
                var adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);       // テーブルレコードの取得

                return dt;
            }
        }
    }
}
