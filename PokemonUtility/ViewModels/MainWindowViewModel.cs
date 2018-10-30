using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using PokemonUtility.Models.Main;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using System.Linq;
using PokemonUtility.ViewModels.Abstract;
using System.Data;
using System;
using PokemonUtility.Models.Analysis;
using PokemonUtility.Models;
using PokemonUtility.Models.Notifications;
using PokemonUtility.Struct;

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
        public ObservableCollection<SoftGeneration> CmbSoftGenerations { get; } = new ObservableCollection<SoftGeneration>();

        // 選択されているソフト世代
        public ReactiveProperty<SoftGeneration> SelectedSoftGeneration { get; } = new ReactiveProperty<SoftGeneration>();
        
        // サブウィンドウ表示リクエスト
        public InteractionRequest<INotification> ShowWindowRequest { get; } = new InteractionRequest<INotification>();

        // ウィンドウクローズコマンド
        public DelegateCommand CloseWindowCommand { get; }

        // 戦績保存コマンド
        public DelegateCommand SaveBattleRecordCommand { get; }

        // 分析コマンド
        public DelegateCommand AnalysisCommand { get; }

        // キャプチャ表示制御コマンド
        public DelegateCommand ShowCaptureWindowCommand { get; }
        
        public MainWindowViewModel() :base(ModelConnector.MainWindow)
        {
            // 添付プロパティ設定
            LoadProperty();

            // 分析中フラグ紐づけ
            IsControlEnabled = ModelConnector.Analysis
                .ObserveProperty(m => m.IsAnalyzing)
                .Select(x => !x)
                .ToReactiveProperty();

            // ログ紐づけ
            Log = ModelConnector.MainWindow.ObserveProperty(m => m.Log).ToReactiveProperty();

            // キャプチャイメージ紐づけ
            CaptureImage = ModelConnector.MainWindow
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

            // サブウィンドウ処理の登録
            IsShowMyPartyWindow.Where(x => x).Subscribe(_ => ShowSubWindow(WindowNotification.MY_PARTY_WINDOW, false));
            IsShowOpponentPartyWindow.Where(x => x).Subscribe(_ => ShowSubWindow(WindowNotification.OPPONENT_PARTY_WINDOW, false));
            IsShowTodayBattleRecordWindow.Where(x => x).Subscribe(_ => ShowSubWindow(WindowNotification.TODAY_BATTLE_RECORD_WINDOW, false));
            IsShowBattleHistoryWindow.Where(x => x).Subscribe(_ => ShowSubWindow(WindowNotification.BATTLE_HISTORY_WINDOW, false));

            // ラジオボタン紐づけ
            RdoBattleResult.Value = BattleResultEnum.Win;
            RdoBattleResult.Subscribe(x => ModelConnector.MainWindow.BattleResultId = ToBattleResultId(x));

            // 世代コンボボックスのアイテム設定
            ModelConnector.MainWindow.SoftGenerationList.ForEach(e => CmbSoftGenerations.Add(e));

            // コンボボックス初期選択
            SelectedSoftGeneration.Value = CmbSoftGenerations[0];

            // コンボボックスの値をモデルに格納
            SelectedSoftGeneration.Subscribe(_ => ModelConnector.MainWindow.SoftGenerationId = _.Id);
            
            // ウィンドウクローズコマンド
            CloseWindowCommand = new DelegateCommand(SaveProperty);

            // 戦績保存コマンド
            SaveBattleRecordCommand = new DelegateCommand(SaveBattleRecord);

            // 分析コマンド
            AnalysisCommand = new DelegateCommand(Analysis);

            // キャプチャ画面表示コマンド
            ShowCaptureWindowCommand = new DelegateCommand(ShowCaptureWindow);
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
        private void ShowCaptureWindow()
        {
            ShowSubWindow(WindowNotification.CAPTURE_WINDOW);

            // 加工したキャプチャ画面表示
            ModelConnector.MainWindow.CreatePokemonMarkedCaptureImage();
        }

        // サブウィンドウを表示する
        private void ShowSubWindow(int windowId, bool isModal=true)
        {
            WindowNotification windowNotification = new WindowNotification();
            windowNotification.WindowId = windowId;
            windowNotification.IsModal = isModal;
            ShowWindowRequest.Raise(windowNotification);
        }

        // 分析
        private void Analysis()
        {
            // キャプチャイメージ表示
            ModelConnector.MainWindow.CreatePokemonMarkedCaptureImage();

            // キャプチャイメージを分析する
            Task<bool> result = ModelConnector.Analysis.RunAsync();
        }
        
        // 対戦結果のEnumをidに変換
        private int ToBattleResultId(BattleResultEnum battleResultEnum)
        {
            if (battleResultEnum == BattleResultEnum.Win) return BattleResult.RESULT_WIN;
            if (battleResultEnum == BattleResultEnum.Lose) return BattleResult.RESULT_LOSE;
            return BattleResult.RESULT_DRAW;
        }
    }
}
