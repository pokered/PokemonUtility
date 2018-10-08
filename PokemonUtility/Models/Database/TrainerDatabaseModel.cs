using MySql.Data.MySqlClient;
using PokemonUtility.Models.Database.Container;
using System.Collections.Generic;

namespace PokemonUtility.Models.Database
{
    class TrainerDatabaseModel : DatabaseConnectModel
    {
        public List<TrainerInfoModel> Select(int trainerId=-1)
        {
            string query = @"
            SELECT *
            FROM trainer_master
            WHERE (1=1)";

            if (trainerId >= 0)
            {
                query += " AND trainer_id = {0} ";
                string.Format(query, trainerId);
            }

            List<TrainerInfoModel> TrainerInfoList = new List<TrainerInfoModel>();

            using (var con = new MySqlConnection(_connectionString))
            {
                // コマンド
                var command = new MySqlCommand(query, con);

                // SQLを実行します。
                using (var executeReader = command.ExecuteReader())
                {
                    while (executeReader.Read())
                    {
                        TrainerInfoModel trainerInfoModel = new TrainerInfoModel();
                        trainerInfoModel.TrainerId = (int)executeReader["trainerId"];
                        trainerInfoModel.name = (string)executeReader["name"];

                        TrainerInfoList.Add(trainerInfoModel);
                    }
                }
            }

            return TrainerInfoList;
        }
    }
}
