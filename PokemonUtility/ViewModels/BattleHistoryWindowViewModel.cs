using PokemonUtility.Models;
using PokemonUtility.Models.BattleHistory;
using PokemonUtility.Struct;
using PokemonUtility.ViewModels.Abstract;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace PokemonUtility.ViewModels
{
    class BattleHistoryWindowViewModel : SubWindowViewModel
    {
        // 対戦記録
        private List<BattleRecord> _battleRecords = new List<BattleRecord>();
        public List<BattleRecord> BattleRecords
        {
            get { return _battleRecords; }
            set { SetProperty(ref _battleRecords, value); }
        }

        // 自分のパーティー記録
        private List<BattleAggregate> _myBattleAggregates = new List<BattleAggregate>();
        public List<BattleAggregate> MyBattleAggregates
        {
            get { return _myBattleAggregates; }
            set { SetProperty(ref _myBattleAggregates, value); }
        }

        // 相手のパーティー記録
        private List<BattleAggregate> _opponentBattleAggregates = new List<BattleAggregate>();
        public List<BattleAggregate> OpponentBattleAggregates
        {
            get { return _opponentBattleAggregates; }
            set { SetProperty(ref _opponentBattleAggregates, value); }
        }

        // トレーナーチェックボックス
        public ReactiveProperty<bool> IsChkTrainerChecked { get; } = new ReactiveProperty<bool>();

        // トレーナー一覧
        public ObservableCollection<Trainer> CmbTrainer { get; } = new ObservableCollection<Trainer>();

        // 選択中のトレーナー
        public ReactiveProperty<Trainer> SelectedTrainer { get; } = new ReactiveProperty<Trainer>();


        // 勝敗チェックボックス
        public ReactiveProperty<bool> IsChkBattleResultChecked { get; } = new ReactiveProperty<bool>();

        // 勝敗コンボボックス
        public ObservableCollection<BattleResult> CmbBattleResult { get; } = new ObservableCollection<BattleResult>();

        // 選択中の勝敗
        public ReactiveProperty<BattleResult> SelectedBattleResult { get; } = new ReactiveProperty<BattleResult>();


        // 取得件数
        public ObservableCollection<int> CmbBattleRecordNumber { get; } = new ObservableCollection<int>();

        // 選択中の取得件数
        public ReactiveProperty<int> SelectedBattleRecordNumber { get; } = new ReactiveProperty<int>();


        // リクエスト
        public InteractionRequest<INotification> CloseWindowRequest { get; } = new InteractionRequest<INotification>();


        // キャプチャ表示制御コマンド
        public DelegateCommand SearchCommand { get; }

        // ポケモン検索ウィンドウ
        public DelegateCommand ShowPokemonSearchWindowCommand { get; }

        public BattleHistoryWindowViewModel() : base(BattleHistoryWindowModel.GetInstance())
        {
            // チェックボックス紐づけ
            IsChkTrainerChecked = ModelConnector.BattleHistoryWindow.ToReactivePropertyAsSynchronized(m => m.IsWhereTrainerId);
            IsChkBattleResultChecked = ModelConnector.BattleHistoryWindow.ToReactivePropertyAsSynchronized(m => m.IsWhereBattleResultId);

            // コンボボックスにアイテム設定
            ModelConnector.BattleHistoryWindow.Trainers.ForEach(e => CmbTrainer.Add(e));
            ModelConnector.BattleHistoryWindow.BattleResults.ForEach(e => CmbBattleResult.Add(e));
            ModelConnector.BattleHistoryWindow.BattleRecordNumberList.ForEach(e => CmbBattleRecordNumber.Add(e));

            // コンボボックス初期選択
            SelectedTrainer.Value = CmbTrainer[0];
            SelectedBattleResult.Value = CmbBattleResult[0];
            SelectedBattleRecordNumber.Value = CmbBattleRecordNumber[0];

            // コンボボックス選択時の処理
            SelectedTrainer.Subscribe(x => ModelConnector.BattleHistoryWindow.TrainerId = x.TrainerId);
            SelectedBattleResult.Subscribe(x => ModelConnector.BattleHistoryWindow.BattleResultId = x.Id);
            SelectedBattleRecordNumber.Subscribe(x => ModelConnector.BattleHistoryWindow.BattleRecordNumber = x);

            // ウィンドウクローズ
            IsShowWindow.Where(x => !x).Subscribe(_ => CloseWindowRequest.Raise(new Notification()));

            // コマンド
            SearchCommand = new DelegateCommand(SearchBattleRecord);
        }

        private void SearchBattleRecord()
        {
            BattleRecords = ModelConnector.BattleHistoryWindow.GetBattleRecords();
            MyBattleAggregates = ModelConnector.BattleHistoryWindow.GetMyBattleAggregates();
            OpponentBattleAggregates = ModelConnector.BattleHistoryWindow.GetOpponentBattleAggregates();
        }
    }
}
