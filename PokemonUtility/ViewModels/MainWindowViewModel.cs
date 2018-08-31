using Prism.Mvvm;
using Prism.Commands;
using PokemonUtility.Models;
using PokemonUtility.Views;
using System.Collections.Generic;
using Prism.Interactivity.InteractionRequest;
using System.Drawing;

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

        private Rectangle _captureRectangle = new Rectangle();

        public int CaptureX
        {
            get { return _captureRectangle.X; }
            set { _captureRectangle.X = value; }
        }

        public int CaptureY
        {
            get { return _captureRectangle.Y; }
            set { _captureRectangle.Y = value; }
        }

        public int CaptureWidth
        {
            get { return _captureRectangle.Width; }
            set { _captureRectangle.Width = value; }
        }

        public int CaptureHeight
        {
            get { return _captureRectangle.Height; }
            set { _captureRectangle.Height = value; }
        }

        // モデル
        private MainModel model;
        
        // リクエスト
        private InteractionRequest<RectangleNotification> captureNotificationRequest = new InteractionRequest<RectangleNotification>();
        public InteractionRequest<RectangleNotification> CaptureNotificationRequest { get; } = new InteractionRequest<RectangleNotification>();


        // コマンド
        public DelegateCommand CaptureWindowOpenCommand { get; }

        public MainWindowViewModel()
        {
            model = new MainModel();

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

            CaptureWindowOpenCommand = new DelegateCommand(CaptureWindowOpenCommandExecute);
            
            
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
        private void CaptureWindowOpenCommandExecute()
        {
            RectangleNotification rectangleNotification = new RectangleNotification();
            rectangleNotification.X = CaptureX;
            rectangleNotification.Y = CaptureY;
            rectangleNotification.Width = CaptureWidth;
            rectangleNotification.Height = CaptureHeight;
            rectangleNotification.Title = "aa";

            CaptureNotificationRequest.Raise(rectangleNotification, 
                r => { CaptureX = r.Width; Title = CaptureX.ToString(); });
        }
    }
}
