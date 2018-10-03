using Prism.Mvvm;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Threading.Tasks;
using PokemonUtility.Const;
using System.Windows.Media.Imaging;
using PokemonUtility.Models.Capture;
using PokemonUtility.Models.Party;
using PokemonUtility.Models.Main;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using PokemonUtility.Models.Common;
using System.Linq;
using PokemonUtility.Models.TodayBattleRecord;
using PokemonUtility.Models.BattleHistory;
using PokemonUtility.ViewModels.Abstract;
using PokemonUtility.Models.Database;
using System.Data;
using System;

namespace PokemonUtility.ViewModels
{
    class MainWindowViewModel : WindowViewModel
    {
        // 各種サブウィンドウの表示制御
        public ReactiveProperty<bool> IsShowMyPartyWindow { get; }
        public ReactiveProperty<bool> IsShowOpponentPartyWindow { get; }
        public ReactiveProperty<bool> IsShowTodayBattleRecordWindow { get; }
        public ReactiveProperty<bool> IsShowBattleHistoryWindow { get; }

        // キャプチャイメージ
        public ReactiveProperty<BitmapImage> CaptureImage { get; }

        // コントロールの可不可
        public ReactiveProperty<bool> IsControlEnabled { get; }

        // ラジオボタン制御
        public ReactiveProperty<bool> RadioWin { get; }
        public ReactiveProperty<bool> RadioLose { get; }
        public ReactiveProperty<bool> RadioDraw { get; }

        // ソフト世代
        public ObservableCollection<SoftGeneration> CmbSoftGenerations { get; } = new ObservableCollection<SoftGeneration>();

        // 選択されているソフト世代
        public ReactiveProperty<SoftGeneration> SelectedSoftGeneration { get; } = new ReactiveProperty<SoftGeneration>();

        // メインモデル
        private MainWindowModel _mainWindowModel = MainWindowModel.GetInstance();

        // キャプチャモデル
        private CaptureWindowModel _captureWindowModel = CaptureWindowModel.GetInstance();
        private CaptureImageManegementModel _captureManegementModel = CaptureImageManegementModel.GetInstance();

        // 自分のパーティー関連のモデル
        private MyPartyWindowModel _myPartyWindowModel = MyPartyWindowModel.GetInstance();
        private MyPartyManegementModel _myPartyManegementModel = MyPartyManegementModel.GetInstance();
        private MyPartyWaitStateModel _myPartyWaitStateModel = MyPartyWaitStateModel.GetInstance();

        // 相手のパーティー関連のモデル
        private OpponentPartyWindowModel _opponentPartyWindowModel = OpponentPartyWindowModel.GetInstance();
        private OpponentPartyManegementModel _opponentPartyManegementModel = OpponentPartyManegementModel.GetInstance();
        private OpponentPartyWaitStateModel _opponentPartyWaitStateModel = OpponentPartyWaitStateModel.GetInstance();

        // 本日の戦績モデル
        private TodayBattleRecordWindowModel _todayBattleRecordWindowModel = TodayBattleRecordWindowModel.GetInstance();

        // 戦績履歴モデル
        private BattleHistoryWindowModel _battleHistoryWindowModel = BattleHistoryWindowModel.GetInstance();

        // サブウィンドウ表示リクエスト
        public InteractionRequest<INotification> ShowCaptureWindowRequest { get; } = new InteractionRequest<INotification>();
        public InteractionRequest<INotification> ShowMyPartyWindowRequest { get; } = new InteractionRequest<INotification>();
        public InteractionRequest<INotification> ShowOpponentPartyWindowRequest { get; } = new InteractionRequest<INotification>();
        public InteractionRequest<INotification> ShowTodayBattleRecordWindowRequest { get; } = new InteractionRequest<INotification>();
        public InteractionRequest<INotification> ShowBattleHistoryWindowRequest { get; } = new InteractionRequest<INotification>();

        // ウィンドウクローズコマンド
        public DelegateCommand CloseWindowCommand { get; }
        
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
            IsControlEnabled = _mainWindowModel.ObserveProperty(m => m.IsAnalyzing).Select(x => !x).ToReactiveProperty();

            // キャプチャイメージ紐づけ
            CaptureImage = _captureManegementModel.ObserveProperty(m => m.CaptureImage).ToReactiveProperty();

            // サブウィンドウ紐づけ
            IsShowMyPartyWindow = _myPartyWindowModel.ToReactivePropertyAsSynchronized(m => m.IsShowWindow);
            IsShowOpponentPartyWindow = _opponentPartyWindowModel.ToReactivePropertyAsSynchronized(m => m.IsShowWindow);
            IsShowTodayBattleRecordWindow = _todayBattleRecordWindowModel.ToReactivePropertyAsSynchronized(m => m.IsShowWindow);
            IsShowBattleHistoryWindow = _battleHistoryWindowModel.ToReactivePropertyAsSynchronized(m => m.IsShowWindow);

            // ラジオボタン紐づけ
            RadioWin = _mainWindowModel.ObserveProperty(m => m.Battle_result).Select(x => x == BattleResultConst.WIN).ToReactiveProperty();
            RadioLose = _mainWindowModel.ObserveProperty(m => m.Battle_result).Select(x => x == BattleResultConst.LOSE).ToReactiveProperty();
            RadioDraw = _mainWindowModel.ObserveProperty(m => m.Battle_result).Select(x => x == BattleResultConst.DRAW).ToReactiveProperty();

            // 世代コンボボックスのアイテム設定
            SoftGenerations softGenerations = new SoftGenerations();
            softGenerations.GetSoftGenerations().ToList().ForEach(e => CmbSoftGenerations.Add(e));
            SelectedSoftGeneration.Value = CmbSoftGenerations[SoftGenerationConst.SUN_MOON];

            // ウィンドウクローズコマンド
            CloseWindowCommand = new DelegateCommand(SaveProperty);

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
            _mainWindowModel.X = Properties.Settings.Default.MainWindowX;
            _mainWindowModel.Y = Properties.Settings.Default.MainWindowY;

            // キャプチャ画面の範囲
            _captureWindowModel.X = Properties.Settings.Default.CaptureX;
            _captureWindowModel.Y = Properties.Settings.Default.CaptureY;
            _captureWindowModel.Width = Properties.Settings.Default.CaptureWidth;
            _captureWindowModel.Height = Properties.Settings.Default.CaptureHeight;

            // 自分のパーティー座標
            _myPartyWindowModel.X = Properties.Settings.Default.MyPartyWindowX;
            _myPartyWindowModel.Y = Properties.Settings.Default.MyPartyWindowY;

            // 相手のパーティー座標
            _opponentPartyWindowModel.X = Properties.Settings.Default.OpponentPartyWindowX;
            _opponentPartyWindowModel.Y = Properties.Settings.Default.OpponentPartyWindowY;

            // 本日の戦績画面
            _todayBattleRecordWindowModel.X = Properties.Settings.Default.TodayBattleRecordWindowX;
            _todayBattleRecordWindowModel.Y = Properties.Settings.Default.TodayBattleRecordWindowY;

            // 戦績履歴画面
            _battleHistoryWindowModel.X = Properties.Settings.Default.BattleHistoryWindowX;
            _battleHistoryWindowModel.Y = Properties.Settings.Default.BattleHistoryWindowY;
        }

        // 閉じる際に添付プロパティ保存
        private void SaveProperty
            ()
        {
            // メイン画面
            Properties.Settings.Default.MainWindowX = _mainWindowModel.X;
            Properties.Settings.Default.MainWindowY = _mainWindowModel.Y;

            // キャプチャ矩形情報
            Properties.Settings.Default.CaptureX = _captureWindowModel.X;
            Properties.Settings.Default.CaptureY = _captureWindowModel.Y;
            Properties.Settings.Default.CaptureWidth = _captureWindowModel.Width;
            Properties.Settings.Default.CaptureHeight = _captureWindowModel.Height;

            // 自分のパーティー
            Properties.Settings.Default.MyPartyWindowX = _myPartyWindowModel.X;
            Properties.Settings.Default.MyPartyWindowY = _myPartyWindowModel.Y;

            // 相手のパーティー
            Properties.Settings.Default.OpponentPartyWindowX = _opponentPartyWindowModel.X;
            Properties.Settings.Default.OpponentPartyWindowY = _opponentPartyWindowModel.Y;

            // 本日の戦績画面
            Properties.Settings.Default.TodayBattleRecordWindowX = _todayBattleRecordWindowModel.X;
            Properties.Settings.Default.TodayBattleRecordWindowY = _todayBattleRecordWindowModel.Y;

            // 戦績履歴画面
            Properties.Settings.Default.BattleHistoryWindowX = _battleHistoryWindowModel.X;
            Properties.Settings.Default.BattleHistoryWindowY = _battleHistoryWindowModel.Y;

            Properties.Settings.Default.Save();
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
        private async void AnalysisCommandExecute()
        {
            // DB接続
            DatabaseConnectModel test = new DatabaseConnectModel();

            DataTable db = test.db();
            var list = db.AsEnumerable().ToDictionary(
                row => Convert.ToString(row["pokemon_id"]),
                row => Convert.ToString(row["name_ja"])
                );

            var aa = list["0"];

            return;

            // メインウィンドウの一部を非アクティブにする
            _mainWindowModel.IsAnalyzing = true;

            // パーティーウィンドウを非アクティブにする
            _myPartyWindowModel.IsAnalyzing = true;
            _opponentPartyWindowModel.IsAnalyzing = true;

            // キャプチャイメージ作成
            _captureManegementModel.CreateCaptureImage();
            
            for (int i = PartyConst.PARTY_INDEX_FIRST; i <= PartyConst.PARTY_INDEX_SIXTH; i++)
            {
                _myPartyWaitStateModel.Start(i);

                int result = await Task.Run(() => DoWork(1000));

                _myPartyManegementModel.ChangePokemonId(i, i);

                _myPartyWaitStateModel.End(i);
            }

            for (int i = PartyConst.PARTY_INDEX_FIRST; i <= PartyConst.PARTY_INDEX_SIXTH; i++)
            {
                _opponentPartyWaitStateModel.Start(i);

                int result = await Task.Run(() => DoWork(1000));

                _opponentPartyManegementModel.ChangePokemonId(i, i);

                _opponentPartyWaitStateModel.End(i);
            }

            // パーティーウィンドウをアクティブにする
            _myPartyWindowModel.IsAnalyzing = false;
            _opponentPartyWindowModel.IsAnalyzing = false;

            // メインウィンドウの一部をアクティブにする
            _mainWindowModel.IsAnalyzing = false;
        }

        private int DoWork(int n)
        {
            System.Threading.Thread.Sleep(1500);

            // このメソッドからの戻り値
            return 1;
        }
    }
}
