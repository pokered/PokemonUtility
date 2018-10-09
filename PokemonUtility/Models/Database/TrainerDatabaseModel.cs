using PokemonUtility.Struct;
using System.Collections.Generic;
using System.Data;

namespace PokemonUtility.Models.Database
{
    class TrainerDatabaseModel : DatabaseConnectModel
    {
        public List<TrainerInfo> GetTrainers(int trainerId=-1)
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

            List<TrainerInfo> TrainerInfoList = new List<TrainerInfo>();

            DataTable dt = Select(query);

            foreach(DataRow dataRow in dt.Rows)
            {
                TrainerInfo trainerInfo = new TrainerInfo();

                trainerInfo.TrainerId = ObjectConverter.ToInt(dataRow[0]);
                trainerInfo.Name = ObjectConverter.ToString(dataRow[1]);

                TrainerInfoList.Add(trainerInfo);
            }

            return TrainerInfoList;
        }
    }
}
