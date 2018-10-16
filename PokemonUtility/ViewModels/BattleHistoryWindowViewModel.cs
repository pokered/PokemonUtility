using PokemonUtility.Models;
using PokemonUtility.Models.BattleHistory;
using PokemonUtility.Struct;
using PokemonUtility.ViewModels.Abstract;
using Prism.Commands;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reactive.Linq;
using System.Windows.Media.Imaging;

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

        // ポケモンイメージ
        public ReactiveProperty<BitmapImage> MyPokemonImage0 { get; private set; }
        public ReactiveProperty<BitmapImage> MyPokemonImage1 { get; private set; }
        public ReactiveProperty<BitmapImage> MyPokemonImage2 { get; private set; }
        public ReactiveProperty<BitmapImage> MyPokemonImage3 { get; private set; }
        public ReactiveProperty<BitmapImage> MyPokemonImage4 { get; private set; }
        public ReactiveProperty<BitmapImage> MyPokemonImage5 { get; private set; }

        public ReactiveProperty<BitmapImage> OpponentPokemonImage0 { get; private set; }
        public ReactiveProperty<BitmapImage> OpponentPokemonImage1 { get; private set; }
        public ReactiveProperty<BitmapImage> OpponentPokemonImage2 { get; private set; }
        public ReactiveProperty<BitmapImage> OpponentPokemonImage3 { get; private set; }
        public ReactiveProperty<BitmapImage> OpponentPokemonImage4 { get; private set; }
        public ReactiveProperty<BitmapImage> OpponentPokemonImage5 { get; private set; }

        // キャプチャ表示制御コマンド
        public DelegateCommand SearchCommand { get; }

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

            // イメージ紐づけ
            MyPokemonImage0 = ModelConnector.BattleHistoryMyParty.ObserveProperty(m => m.PokemonId0).Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();
            MyPokemonImage1 = ModelConnector.BattleHistoryMyParty.ObserveProperty(m => m.PokemonId1).Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();
            MyPokemonImage2 = ModelConnector.BattleHistoryMyParty.ObserveProperty(m => m.PokemonId2).Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();
            MyPokemonImage3 = ModelConnector.BattleHistoryMyParty.ObserveProperty(m => m.PokemonId3).Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();
            MyPokemonImage4 = ModelConnector.BattleHistoryMyParty.ObserveProperty(m => m.PokemonId4).Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();
            MyPokemonImage5 = ModelConnector.BattleHistoryMyParty.ObserveProperty(m => m.PokemonId5).Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();

            OpponentPokemonImage0 = ModelConnector.BattleHistoryOpponentParty.ObserveProperty(m => m.PokemonId0).Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();
            OpponentPokemonImage1 = ModelConnector.BattleHistoryOpponentParty.ObserveProperty(m => m.PokemonId1).Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();
            OpponentPokemonImage2 = ModelConnector.BattleHistoryOpponentParty.ObserveProperty(m => m.PokemonId2).Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();
            OpponentPokemonImage3 = ModelConnector.BattleHistoryOpponentParty.ObserveProperty(m => m.PokemonId3).Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();
            OpponentPokemonImage4 = ModelConnector.BattleHistoryOpponentParty.ObserveProperty(m => m.PokemonId4).Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();
            OpponentPokemonImage5 = ModelConnector.BattleHistoryOpponentParty.ObserveProperty(m => m.PokemonId5).Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();
            
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
