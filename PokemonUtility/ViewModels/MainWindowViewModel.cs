using Prism.Mvvm;
using Prism.Commands;
using PokemonUtility.Models;
using System.Collections.Generic;
using Prism.Interactivity.InteractionRequest;
using PokemonUtility.Views;

namespace PokemonUtility.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        // プロパティ
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        // ウィンドウ位置・サイズ
        private WindowRectangle _captureRectangle = new WindowRectangle();
        public WindowRectangle CaptureRectangle
        {
            get { return _captureRectangle; }
        }

        // モデル
        private MainModel model;
        private MyPartyWindowModel mymodel;
        
        // リクエスト
        private InteractionRequest<RectangleNotification> _showCaptureWindowRequest = new InteractionRequest<RectangleNotification>();
        public InteractionRequest<RectangleNotification> ShowCaptureWindowRequest { get; } = new InteractionRequest<RectangleNotification>();

        //private InteractionRequest<RectangleNotification> _showMyPartyWindowRequest = new InteractionRequest<RectangleNotification>();
        //public InteractionRequest<RectangleNotification> ShowMyPartyWindowRequest { get; } = new InteractionRequest<RectangleNotification>();


        // コマンド
        public DelegateCommand ShowCaptureWindowCommand { get; }
        public DelegateCommand ShowMyWindowWindowCommand { get; }

        public MainWindowViewModel()
        {
            model = new MainModel();
            mymodel = MyPartyWindowModel.GetInstance();

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
            //ShowCaptureWindowNotificationCommand = new DelegateCommand(ShowCaptureWindow);

            ShowCaptureWindowCommand = new DelegateCommand(ShowCaptureWindowCommandExecute);
            ShowMyWindowWindowCommand = new DelegateCommand(IsMyPartyWindowCommandExecute);

            PartyWindow = new MyPartyWindow();
            PartyWindow.Show();
        }

        MyPartyWindow PartyWindow;

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

        // 自分のパーティー画面の表示切替
        private void IsMyPartyWindowCommandExecute()
        {
            mymodel.Mess = "test";
        }
    }
}
