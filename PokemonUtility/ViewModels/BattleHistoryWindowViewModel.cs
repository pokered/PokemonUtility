using PokemonUtility.Models.BattleHistory;
using PokemonUtility.Models.Common;
using PokemonUtility.ViewModels.Abstract;
using Reactive.Bindings;
using System.Collections.ObjectModel;
using System.Data;

namespace PokemonUtility.ViewModels
{
    class BattleHistoryWindowViewModel : SubWindowViewModel
    {
        public DataTable _DataTable { get; set; } = new DataTable();

        // ソフト世代
        public ObservableCollection<SoftGenerationModel> CmbTrainers { get; } = new ObservableCollection<SoftGenerationModel>();

        // トレーナー一覧
        public ReactiveProperty<SoftGenerationModel> SelectedSoftGeneration { get; } = new ReactiveProperty<SoftGenerationModel>();

        public BattleHistoryWindowViewModel() : base(BattleHistoryWindowModel.GetInstance())
        {
            for (int i = 0; i < 10; i++)
            {
                _DataTable.Columns.Add(i + "列目");
            }
            for (int i = 0; i < 10; i++)
            {
                var row = _DataTable.NewRow();
                foreach (DataColumn col in _DataTable.Columns)
                {
                    row[col] = col.ColumnName + "-" + i + "行目";
                }
                _DataTable.Rows.Add(row);
            }
        }
    }
}
