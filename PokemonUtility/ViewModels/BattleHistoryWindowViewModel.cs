using PokemonUtility.Models.BattleHistory;
using PokemonUtility.Struct;
using PokemonUtility.ViewModels.Abstract;
using Reactive.Bindings;
using System.Collections.ObjectModel;
using System.Data;

namespace PokemonUtility.ViewModels
{
    class BattleHistoryWindowViewModel : SubWindowViewModel
    {
        public DataTable _DataTable { get; set; } = new DataTable();

        private BattleHistoryWindowModel _battleHistoryModel = BattleHistoryWindowModel.GetInstance();

        // トレーナー一覧
        public ObservableCollection<TrainerInfo> CmbTrainers { get; } = new ObservableCollection<TrainerInfo>();

        // トレーナー一覧
        public ReactiveProperty<TrainerInfo> SelectedTrainer { get; } = new ReactiveProperty<TrainerInfo>();

        public BattleHistoryWindowViewModel() : base(BattleHistoryWindowModel.GetInstance())
        {
            _battleHistoryModel = BattleHistoryWindowModel.GetInstance();
            // トレーナーコンボボックス
            _battleHistoryModel.Trainers.ForEach(e => CmbTrainers.Add(e));

            // コンボボックス初期選択
            SelectedTrainer.Value = CmbTrainers[0];

            //for (int i = 0; i < 10; i++)
            //{
            //    _DataTable.Columns.Add(i + "列目");
            //}
            //for (int i = 0; i < 10; i++)
            //{
            //    var row = _DataTable.NewRow();
            //    foreach (DataColumn col in _DataTable.Columns)
            //    {
            //        row[col] = col.ColumnName + "-" + i + "行目";
            //    }
            //    _DataTable.Rows.Add(row);
            //}
        }
    }
}
