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

        private int _capture = 0;
        public int Capture
        {
            get { return _capture; }
            set { SetProperty(ref _capture, value); }
        }

        private MainModel model;

        private MyPartyWindow _myPartyWindow;

        // キャプチャウィンドウの矩形情報
        public InteractionRequest<CaptureRectangleNotification> CaptureRectangleNotificationRequest { get; } = new InteractionRequest<CaptureRectangleNotification>();

        // コマンド
        public DelegateCommand ShowCaptureWindowNotificationCommand { get; }

        public MainWindowViewModel()
        {
            model = new MainModel();

            // 自分のパーティーウィンドウ
            _myPartyWindow = new MyPartyWindow();

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

            NotificationCommand = new DelegateCommand(NotificationCommandExecute);
            
            
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

        // 自分のパーティー表示
        private DelegateCommand _checkMyPartyComamnd;
        private void ShowMyPartyWindow()
        {
            model.ShowMyPartyWindow(true);
        }

        public DelegateCommand NotificationCommand { get; }
        //コンストラクタでDelegateCommand にNotificationCommandExecuteメソッドを指定

        // キャプチャ
        private InteractionRequest<CaptureRectangleNotification> capNotificationRequest = new InteractionRequest<CaptureRectangleNotification>();
        public InteractionRequest<CaptureRectangleNotification> CapNotificationRequest { get; } = new InteractionRequest<CaptureRectangleNotification>();

        // キャプチャ
        private void NotificationCommandExecute()
        {
            CapNotificationRequest.Raise(new CaptureRectangleNotification{ Title = "aa"}, 
                r => { Capture = r.CaptureRectangle.Width; Title = Capture.ToString(); });
        }


    }
}
