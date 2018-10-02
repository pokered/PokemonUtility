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

namespace PokemonUtility.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        // プロパティ
        public ReactiveProperty<bool> IsShowMyPartyWindow { get; }
        public ReactiveProperty<bool> IsShowOpponentPartyWindow { get; }
        public ReactiveProperty<BitmapImage> CaptureImage { get; }
        public ReactiveProperty<bool> IsEnabled { get; }
        public ReactiveProperty<bool> RadioWin { get; }
        public ReactiveProperty<bool> RadioLose { get; }
        public ReactiveProperty<bool> RadioDraw { get; }

        // ソフト世代
        public ObservableCollection<SoftGeneration> CmbSoftGenerations { get; } = new ObservableCollection<SoftGeneration>();

        // 選択されているソフト世代
        public ReactiveProperty<SoftGeneration> SelectedSoftGeneration { get; } = new ReactiveProperty<SoftGeneration>();

        // モデル
        private MainWindowModel _mainWindowModel = MainWindowModel.GetInstance();

        private CaptureWindowModel _captureWindowModel = CaptureWindowModel.GetInstance();
        private CaptureImageManegementModel _captureManegementModel = CaptureImageManegementModel.GetInstance();

        private MyPartyWindowModel _myPartyWindowModel = MyPartyWindowModel.GetInstance();
        private MyPartyManegementModel _myPartyManegementModel = MyPartyManegementModel.GetInstance();
        private MyPartyWaitStateModel _myPartyWaitStateModel = MyPartyWaitStateModel.GetInstance();

        private OpponentPartyWindowModel _opponentPartyWindowModel = OpponentPartyWindowModel.GetInstance();
        private OpponentPartyManegementModel _opponentPartyManegementModel = OpponentPartyManegementModel.GetInstance();
        private OpponentPartyWaitStateModel _opponentPartyWaitStateModel = OpponentPartyWaitStateModel.GetInstance();

        // リクエスト
        public InteractionRequest<INotification> ShowCaptureWindowRequest { get; } = new InteractionRequest<INotification>();
        public InteractionRequest<INotification> ShowMyPartyWindowRequest { get; } = new InteractionRequest<INotification>();
        public InteractionRequest<INotification> ShowOpponentPartyWindowRequest { get; } = new InteractionRequest<INotification>();

        // コマンド
        public DelegateCommand CloseWindowCommand { get; }
        public DelegateCommand ShowCaptureWindowCommand { get; }
        public DelegateCommand ShowMyPartyWindowCommand { get; }
        public DelegateCommand ShowOpponentPartyWindowCommand { get; }
        public DelegateCommand AnalysisCommand { get; }

        public MainWindowViewModel()
        {
            // 添付プロパティ設定
            SettingProperty();

            // 分析中フラグ紐づけ
            IsEnabled = _mainWindowModel.ObserveProperty(m => m.IsAnalyzing).Select(x => !x).ToReactiveProperty();

            // ラジオボタン紐づけ
            RadioWin = _mainWindowModel.ObserveProperty(m => m.Battle_result).Select(x => x == BattleResultConst.WIN).ToReactiveProperty();
            RadioLose = _mainWindowModel.ObserveProperty(m => m.Battle_result).Select(x => x == BattleResultConst.LOSE).ToReactiveProperty();
            RadioDraw = _mainWindowModel.ObserveProperty(m => m.Battle_result).Select(x => x == BattleResultConst.DRAW).ToReactiveProperty();

            // キャプチャイメージ紐づけ
            CaptureImage = _captureManegementModel.ObserveProperty(m => m.CaptureImage).ToReactiveProperty();

            // サブウィンドウ紐づけ
            IsShowMyPartyWindow = _myPartyWindowModel.ToReactivePropertyAsSynchronized(m => m.IsShowWindow);
            IsShowOpponentPartyWindow = _opponentPartyWindowModel.ToReactivePropertyAsSynchronized(m => m.IsShowWindow);

            // 世代コンボボックスのアイテム設定
            SoftGenerations softGenerations = new SoftGenerations();
            softGenerations.GetSoftGenerations().ToList().ForEach(e => CmbSoftGenerations.Add(e));
            SelectedSoftGeneration.Value = CmbSoftGenerations[SoftGenerationConst.SUN_MOON];

            // コマンド
            CloseWindowCommand = new DelegateCommand(CloseWindowCommandExecute);
            ShowCaptureWindowCommand = new DelegateCommand(ShowCaptureWindowCommandExecute);
            ShowMyPartyWindowCommand = new DelegateCommand(ShowMyPartyWindowCommandExecute);
            ShowOpponentPartyWindowCommand = new DelegateCommand(ShowOpponentPartyWindowCommandExecute);
            AnalysisCommand = new DelegateCommand(AnalysisCommandExecute);
        }

        // 添付プロパティ設定
        private void SettingProperty()
        {
            // キャプチャ画面の範囲
            _captureWindowModel.X = Properties.Settings.Default.CaptureX;
            _captureWindowModel.Y = Properties.Settings.Default.CaptureY;
            _captureWindowModel.Width = Properties.Settings.Default.CaptureWidth;
            _captureWindowModel.Height = Properties.Settings.Default.CaptureHeight;

            // 自分のパーティー位置
            _myPartyWindowModel.X = Properties.Settings.Default.MyPartyWindowX;
            _myPartyWindowModel.Y = Properties.Settings.Default.MyPartyWindowY;

            // 相手のパーティー位置
            _opponentPartyWindowModel.X = Properties.Settings.Default.OpponentPartyWindowX;
            _opponentPartyWindowModel.Y = Properties.Settings.Default.OpponentPartyWindowY;
        }

        // 閉じる際に添付プロパティ保存
        private void CloseWindowCommandExecute()
        {
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

        // 分析
        private async void AnalysisCommandExecute()
        {
            // メインウィンドウの一部を非アクティブにする
            _mainWindowModel.IsAnalyzing = true;

            // パーティーウィンドウを非アクティブにする
            _myPartyWindowModel.WindowEnabled = false;
            _opponentPartyWindowModel.WindowEnabled = false;

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
            _myPartyWindowModel.WindowEnabled = true;
            _opponentPartyWindowModel.WindowEnabled = true;

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
