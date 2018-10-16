using PokemonUtility.Struct;
using System.Collections.Generic;
using System.Data;

namespace PokemonUtility.Models.Database
{
    class TrainerDatabaseModel : DatabaseConnectModel
    {
        public List<Trainer> GetTrainers(int trainerId=-1)
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

            List<Trainer> TrainerInfoList = new List<Trainer>();

            DataTable dt = Select(query);

            foreach(DataRow dataRow in dt.Rows)
            {
                Trainer trainer = new Trainer();

                trainer.TrainerId = ObjectConverter.ToInt(dataRow[0]);
                trainer.Name = ObjectConverter.ToString(dataRow[1]);

                TrainerInfoList.Add(trainer);
            }

            return TrainerInfoList;
        }
    }
}
