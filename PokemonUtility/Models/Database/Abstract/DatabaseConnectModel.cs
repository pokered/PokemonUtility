using MySql.Data.MySqlClient;
using PokemonUtility.Const;
using System.Data;

namespace PokemonUtility.Models.Database
{
    abstract class DatabaseConnectModel
    {
        protected string _connectionString = "";

        public DatabaseConnectModel()
        {
            // MySQLへの接続情報
            string server = "public.pflmr.tyo1.database-hosting.conoha.io";        // MySQLサーバホスト名
            string user = "pflmr_pokered";           // MySQLユーザ名
            string pass = "Pokemonpawapuro3827";           // MySQLパスワード
            string database = "pflmr_pokemon_test2";		// 接続するデータベース名
            _connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3}", server, database, user, pass);
        }

        public DataTable Select(string query)
        {
            // MySQL の場合
            using (var con = new MySqlConnection(_connectionString))
            {

                var command = new MySqlCommand(query, con);
                var adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);       // テーブルレコードの取得

                return dataTable;
            }
        }

        protected int Insert(string query)
        {
            try
            {
                // MySQL の場合
                using (var con = new MySqlConnection(_connectionString))
                {
                    var command = new MySqlCommand(query, con);

                    // オープン
                    command.Connection.Open();

                    // 実行
                    command.ExecuteNonQuery();

                    // クローズ
                    command.Connection.Close();

                    // 挿入ID
                    return (int)command.LastInsertedId;
                }
            }
            catch(MySqlException)
            {
                return DatabaseConst.INSERT_FAIL;
            }
        }

        protected bool Delete(string query)
        {
            try
            {
                // MySQL の場合
                using (var con = new MySqlConnection(_connectionString))
                {
                    var command = new MySqlCommand(query, con);

                    // オープン
                    command.Connection.Open();

                    // 実行
                    command.ExecuteNonQuery();

                    // クローズ
                    command.Connection.Close();

                    // 挿入ID
                    return true;
                }
            }
            catch (MySqlException)
            {
                return false;
            }
        }
    }
}
