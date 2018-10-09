using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Threading.Tasks;
using PokemonUtility.Const;
using System.Windows.Media.Imaging;
using PokemonUtility.Models.Main;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using PokemonUtility.Models.Common;
using System.Linq;
using PokemonUtility.ViewModels.Abstract;
using System.Data;
using System;
using PokemonUtility.Models.Analysis;
using PokemonUtility.Models;

namespace PokemonUtility.ViewModels
{
    class MainWindowViewModel : WindowViewModel
    {
        // 各種サブウィンドウの表示制御
        public ReactiveProperty<bool> IsShowMyPartyWindow { get; }
        public ReactiveProperty<bool> IsShowOpponentPartyWindow { get; }
        public ReactiveProperty<bool> IsShowTodayBattleRecordWindow { get; }
        public ReactiveProperty<bool> IsShowBattleHistoryWindow { get; }

        // ログ
        public ReactiveProperty<string> Log { get; }

        // キャプチャイメージ
        public ReactiveProperty<BitmapImage> CaptureImage { get; }

        // コントロールの可不可
        public ReactiveProperty<bool> IsControlEnabled { get; }

        // ラジオボタンのリスト
        public enum BattleResultEnum { Win, Lose, Draw }

        // ラジオボタン制御
        public ReactiveProperty<BattleResultEnum> RdoBattleResult { get; set; } = new ReactiveProperty<BattleResultEnum>();

        // ソフト世代
        public ObservableCollection<SoftGenerationModel> CmbSoftGenerations { get; } = new ObservableCollection<SoftGenerationModel>();

        // 選択されているソフト世代
        public ReactiveProperty<SoftGenerationModel> SelectedSoftGeneration { get; } = new ReactiveProperty<SoftGenerationModel>();
        
        // サブウィンドウ表示リクエスト
        public InteractionRequest<INotification> ShowCaptureWindowRequest { get; } = new InteractionRequest<INotification>();
        public InteractionRequest<INotification> ShowMyPartyWindowRequest { get; } = new InteractionRequest<INotification>();
        public InteractionRequest<INotification> ShowOpponentPartyWindowRequest { get; } = new InteractionRequest<INotification>();
        public InteractionRequest<INotification> ShowTodayBattleRecordWindowRequest { get; } = new InteractionRequest<INotification>();
        public InteractionRequest<INotification> ShowBattleHistoryWindowRequest { get; } = new InteractionRequest<INotification>();

        // ウィンドウクローズコマンド
        public DelegateCommand CloseWindowCommand { get; }

        // 戦績保存コマンド
        public DelegateCommand SaveBattleRecordCommand { get; }

        // 分析コマンド
        public DelegateCommand AnalysisCommand { get; }

        // サブウィンドウ表示制御コマンド
        public DelegateCommand ShowCaptureWindowCommand { get; }
        public DelegateCommand ShowMyPartyWindowCommand { get; }
        public DelegateCommand ShowOpponentPartyWindowCommand { get; }
        public DelegateCommand ShowTodayBattleRecordWindowCommand { get; }
        public DelegateCommand ShowBattleHistoryWindowCommand { get; }

        public MainWindowViewModel() :base(MainWindowModel.GetInstance())
        {
            // 添付プロパティ設定
            LoadProperty();

            // 分析中フラグ紐づけ
            IsControlEnabled = ModelConnector.MainWindow
                .ObserveProperty(m => m.IsAnalyzing)
                .Select(x => !x)
                .ToReactiveProperty();

            // ログ紐づけ
            Log = ModelConnector.MainWindow.ObserveProperty(m => m.Log).ToReactiveProperty();

            // キャプチャイメージ紐づけ
            CaptureImage = ModelConnector.CaptureImageManegement
                .ObserveProperty(m => m.PokemonMarkedCaptureImage)
                .ToReactiveProperty();

            // サブウィンドウ紐づけ
            IsShowMyPartyWindow = ModelConnector.MyPartyWindow
                .ToReactivePropertyAsSynchronized(m => m.IsShowWindow);

            IsShowOpponentPartyWindow = ModelConnector.OpponentPartyWindow
                .ToReactivePropertyAsSynchronized(m => m.IsShowWindow);

            IsShowTodayBattleRecordWindow = ModelConnector.TodayBattleRecordWindow
                .ToReactivePropertyAsSynchronized(m => m.IsShowWindow);

            IsShowBattleHistoryWindow = ModelConnector.BattleHistoryWindow
                .ToReactivePropertyAsSynchronized(m => m.IsShowWindow);

            // ラジオボタン紐づけ
            RdoBattleResult.Value = BattleResultEnum.Win;
            RdoBattleResult.Subscribe(x => ModelConnector.MainWindow.BattleResult = ToBattleResultId(x));

            // 世代コンボボックスのアイテム設定
            SoftGenerations softGenerations = new SoftGenerations();
            softGenerations.GetSoftGenerations().ToList().ForEach(e => CmbSoftGenerations.Add(e));

            // コンボボックス初期選択
            SelectedSoftGeneration.Value = CmbSoftGenerations[0];

            // コンボボックスの値をモデルに格納
            SelectedSoftGeneration.Subscribe(_ => ModelConnector.MainWindow.SoftGenerationId = _.Id);
            
            // ウィンドウクローズコマンド
            CloseWindowCommand = new DelegateCommand(SaveProperty);

            // 戦績保存コマンド
            SaveBattleRecordCommand = new DelegateCommand(SaveBattleRecord);

            // 分析コマンド
            AnalysisCommand = new DelegateCommand(AnalysisCommandExecute);

            // 各種サブウィンドウ表示制御コマンド
            ShowCaptureWindowCommand = new DelegateCommand(ShowCaptureWindowCommandExecute);
            ShowMyPartyWindowCommand = new DelegateCommand(ShowMyPartyWindowCommandExecute);
            ShowOpponentPartyWindowCommand = new DelegateCommand(ShowOpponentPartyWindowCommandExecute);
            ShowTodayBattleRecordWindowCommand = new DelegateCommand(ShowTodayBattleRecordWindowCommandExecute);
            ShowBattleHistoryWindowCommand = new DelegateCommand(ShowBattleHistoryWindowCommandExecute);
        }
        
        // 添付プロパティ設定
        private void LoadProperty()
        {
            // メイン画面
            ModelConnector.MainWindow.X = Properties.Settings.Default.MainWindowX;
            ModelConnector.MainWindow.Y = Properties.Settings.Default.MainWindowY;

            // キャプチャ画面の範囲
            ModelConnector.CaptureWindow.X = Properties.Settings.Default.CaptureX;
            ModelConnector.CaptureWindow.Y = Properties.Settings.Default.CaptureY;
            ModelConnector.CaptureWindow.Width = Properties.Settings.Default.CaptureWidth;
            ModelConnector.CaptureWindow.Height = Properties.Settings.Default.CaptureHeight;
            
            // 自分のパーティー座標
            ModelConnector.MyPartyWindow.X = Properties.Settings.Default.MyPartyWindowX;
            ModelConnector.MyPartyWindow.Y = Properties.Settings.Default.MyPartyWindowY;

            // 相手のパーティー座標
            ModelConnector.OpponentPartyWindow.X = Properties.Settings.Default.OpponentPartyWindowX;
            ModelConnector.OpponentPartyWindow.Y = Properties.Settings.Default.OpponentPartyWindowY;

            // 本日の戦績画面
            ModelConnector.TodayBattleRecordWindow.X = Properties.Settings.Default.TodayBattleRecordWindowX;
            ModelConnector.TodayBattleRecordWindow.Y = Properties.Settings.Default.TodayBattleRecordWindowY;

            // 戦績履歴画面
            ModelConnector.BattleHistoryWindow.X = Properties.Settings.Default.BattleHistoryWindowX;
            ModelConnector.BattleHistoryWindow.Y = Properties.Settings.Default.BattleHistoryWindowY;
        }

        // 閉じる際に添付プロパティ保存
        private void SaveProperty()
        {
            // メイン画面
            Properties.Settings.Default.MainWindowX = ModelConnector.MainWindow.X;
            Properties.Settings.Default.MainWindowY = ModelConnector.MainWindow.Y;

            // キャプチャ矩形情報
            Properties.Settings.Default.CaptureX = ModelConnector.CaptureWindow.X;
            Properties.Settings.Default.CaptureY = ModelConnector.CaptureWindow.Y;
            Properties.Settings.Default.CaptureWidth = ModelConnector.CaptureWindow.Width;
            Properties.Settings.Default.CaptureHeight = ModelConnector.CaptureWindow.Height;

            // 自分のパーティー
            Properties.Settings.Default.MyPartyWindowX = ModelConnector.MyPartyWindow.X;
            Properties.Settings.Default.MyPartyWindowY = ModelConnector.MyPartyWindow.Y;

            // 相手のパーティー
            Properties.Settings.Default.OpponentPartyWindowX = ModelConnector.OpponentPartyWindow.X;
            Properties.Settings.Default.OpponentPartyWindowY = ModelConnector.OpponentPartyWindow.Y;

            // 本日の戦績画面
            Properties.Settings.Default.TodayBattleRecordWindowX = ModelConnector.TodayBattleRecordWindow.X;
            Properties.Settings.Default.TodayBattleRecordWindowY = ModelConnector.TodayBattleRecordWindow.Y;

            // 戦績履歴画面
            Properties.Settings.Default.BattleHistoryWindowX = ModelConnector.BattleHistoryWindow.X;
            Properties.Settings.Default.BattleHistoryWindowY = ModelConnector.BattleHistoryWindow.Y;

            Properties.Settings.Default.Save();
        }

        // 戦績保存
        private void SaveBattleRecord()
        {
            BattleRecordSaveModel battleRecordSaveModel = new BattleRecordSaveModel();
            battleRecordSaveModel.Save();
        }

        // キャプチャ画面を表示
        private void ShowCaptureWindowCommandExecute()
        {
            ShowCaptureWindowRequest.Raise(null);
        }

        // 自分のパーティー画面を表示
        private void ShowMyPartyWindowCommandExecute()
        {
            if (IsShowMyPartyWindow.Value) ShowMyPartyWindowRequest.Raise(null);
        }

        // 相手のパーティー画面を表示
        private void ShowOpponentPartyWindowCommandExecute()
        {
            if (IsShowOpponentPartyWindow.Value) ShowOpponentPartyWindowRequest.Raise(null);
        }

        // 本日の戦績画面を表示
        private void ShowTodayBattleRecordWindowCommandExecute()
        {
            if (IsShowTodayBattleRecordWindow.Value) ShowTodayBattleRecordWindowRequest.Raise(null);
        }

        // 戦績履歴画面を表示
        private void ShowBattleHistoryWindowCommandExecute()
        {
            if (IsShowBattleHistoryWindow.Value) ShowBattleHistoryWindowRequest.Raise(null);
        }

        // 分析
        private void AnalysisCommandExecute()
        {
            AnalysisImage analysisImage = new AnalysisImage();
            Task<bool> resultaa = analysisImage.RunAsync();
        }
        
        // 対戦結果のEnumをidに変換
        private int ToBattleResultId(BattleResultEnum battleResultEnum)
        {
            if (battleResultEnum == BattleResultEnum.Win) return BattleResultConst.WIN;
            if (battleResultEnum == BattleResultEnum.Lose) return BattleResultConst.LOSE;
            return BattleResultConst.DRAW;
        }
    }
}
