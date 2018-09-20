using Prism.Mvvm;
using Prism.Commands;
using PokemonUtility.Models;
using System.Collections.Generic;
using Prism.Interactivity.InteractionRequest;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace PokemonUtility.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        // プロパティ
        public ReactiveProperty<bool> IsShowMyPartyWindow { get; }

        // ウィンドウ位置・サイズ
        private WindowRectangle _captureRectangle = new WindowRectangle();
        public WindowRectangle CaptureRectangle
        {
            get { return _captureRectangle; }
        }

        // モデル
        private MainModel mainModel = new MainModel();
        private MyPartyWindowModel myPartyWindowModel = MyPartyWindowModel.GetInstance();
        
        // リクエスト
        private InteractionRequest<RectangleNotification> _showCaptureWindowRequest = new InteractionRequest<RectangleNotification>();
        public InteractionRequest<RectangleNotification> ShowCaptureWindowRequest { get; } = new InteractionRequest<RectangleNotification>();

        public InteractionRequest<RectangleNotification> ShowMyPartyWindowRequest { get; } = new InteractionRequest<RectangleNotification>();
        
        // コマンド
        public DelegateCommand ShowCaptureWindowCommand { get; }
        public DelegateCommand ShowMyPartyWindowCommand { get; }
        public DelegateCommand AnalysisCommand { get; }

        public MainWindowViewModel()
        {
            IsShowMyPartyWindow = myPartyWindowModel.ToReactivePropertyAsSynchronized(m => m.IsShowWindow);

            // キャプチャ画面の範囲
            CaptureRectangle.X = Properties.Settings.Default.CaptureX;
            CaptureRectangle.Y = Properties.Settings.Default.CaptureY;
            CaptureRectangle.Width = Properties.Settings.Default.CaptureWidth;
            CaptureRectangle.Height = Properties.Settings.Default.CaptureHeight;

            // 世代コンボボックスのアイテム設定
            var softGenerationList = new List<SoftGeneration>();
            softGenerationList.Add(new SoftGeneration(){ID = 0, Name = "赤緑"});
            softGenerationList.Add(new SoftGeneration(){ID = 1, Name = "金銀"});
            softGenerationList.Add(new SoftGeneration(){ID = 2, Name = "ＲＳ"});
            softGenerationList.Add(new SoftGeneration(){ID = 3, Name = "ＤＰ"});
            softGenerationList.Add(new SoftGeneration(){ID = 4, Name = "黒白"});
            softGenerationList.Add(new SoftGeneration(){ID = 5, Name = "ＸＹ"});
            softGenerationList.Add(new SoftGeneration(){ID = 6, Name = "ＳＭ"});
            // 先にselecteditemに値を設定しないとダメ
            SelectedSoftGeneration = softGenerationList[0];
            SoftGenerations = softGenerationList;

            // コマンド
            ShowCaptureWindowCommand = new DelegateCommand(ShowCaptureWindowCommandExecute);
            ShowMyPartyWindowCommand = new DelegateCommand(ShowMyPartyWindowCommandExecute);
            AnalysisCommand = new DelegateCommand(AnalysisCommandExecute);
        }

        // ソフトの世代
        public SoftGeneration SelectedSoftGeneration { get; set; }	// 変更通知
        private List<SoftGeneration> _softGenerations;
        public List<SoftGeneration> SoftGenerations
        {
            get { return _softGenerations; }
            // privateにする
            set { _softGenerations = value; }
        }

        // ラジオボタン　勝
        private bool _radioWin = true;
        public bool RadioWin
        {
            get { return _radioWin; }
            set { SetProperty(ref _radioWin, value); }
        }

        // ラジオボタン　負
        private bool _radioLose = false;
        public bool RadioLose
        {
            get { return _radioLose; }
            set { SetProperty(ref _radioLose, value); }
        }

        // ラジオボタン　引
        private bool _radioDraw = false;
        public bool RadioDraw
        {
            get { return _radioDraw; }
            set { SetProperty(ref _radioDraw, value); }
        }
        
        // キャプチャ
        private void ShowCaptureWindowCommandExecute()
        {
            RectangleNotification rectangleNotification = new RectangleNotification();
            rectangleNotification.X = CaptureRectangle.X;
            rectangleNotification.Y = CaptureRectangle.Y;
            rectangleNotification.Width = CaptureRectangle.Width;
            rectangleNotification.Height = CaptureRectangle.Height;

            ShowCaptureWindowRequest.Raise(rectangleNotification, 
                r => 
                {
                    // キャプチャサイズを設定
                    CaptureRectangle.X = r.X;
                    CaptureRectangle.Y = r.Y;
                    CaptureRectangle.Width = r.Width;
                    CaptureRectangle.Height = r.Height;

                    // SettingPropertyに保存する
                    Properties.Settings.Default.CaptureX = r.X;
                    Properties.Settings.Default.CaptureY = r.Y;
                    Properties.Settings.Default.CaptureWidth = r.Width;
                    Properties.Settings.Default.CaptureHeight = r.Height;

                    // ファイルに保存
                    Properties.Settings.Default.Save();
                });
        }

        // 自分のパーティー画面を表示
        private void ShowMyPartyWindowCommandExecute()
        {
            if (IsShowMyPartyWindow.Value)
            {
                ShowMyPartyWindowRequest.Raise(null);
            }
        }

        // 分析
        private void AnalysisCommandExecute()
        {
            AnalysisModel analysisModel = AnalysisModel.GetInstance();
            int[] pokemonIdList = analysisModel.start();

            MyPartyManegementModel myParty = MyPartyManegementModel.GetInstance();
            myParty.PokemonId1 = pokemonIdList[0];
            myParty.PokemonId2 = pokemonIdList[1];
            myParty.PokemonId3 = pokemonIdList[2];
            myParty.PokemonId4 = pokemonIdList[3];
            myParty.PokemonId5 = pokemonIdList[4];
            myParty.PokemonId6 = pokemonIdList[5];
        }
    }
}
