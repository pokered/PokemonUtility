using Prism.Mvvm;
using Prism.Commands;
using PokemonUtility.Models;
using PokemonUtility.Views;
using System.Collections.Generic;
using Prism.Interactivity.InteractionRequest;

namespace PokemonUtility.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private MainModel model;

        private MyPartyWindow _myPartyWindow;

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


            this.NotificationCommand = new DelegateCommand(this.NotificationCommandExecute);
            
        }

        private DelegateCommand calcComamnd;
        public DelegateCommand calcCommand
        {
            get { return calcComamnd = calcComamnd ?? new DelegateCommand(CHangeTitle); }
        }

        private void CHangeTitle()
        {
            Title = model.CHangeTitle();
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
        public DelegateCommand CheckMyPartyComamnd
        {
            get { return calcComamnd = calcComamnd ?? new DelegateCommand(CHangeTitle); }
        }

        private void ShowMyPartyWindow()
        {
            model.ShowMyPartyWindow(true);
        }

        //InteractionRequestクラスのプロパティ
        public InteractionRequest<Notification> NotificationRequest { get; } = new InteractionRequest<Notification>();


        public DelegateCommand NotificationCommand { get; }
        //コンストラクタでDelegateCommand にNotificationCommandExecuteメソッドを指定


        //Raiseイベントの実装
        private void NotificationCommandExecute()
        {
            this.NotificationRequest.Raise(new Notification { Title = "Alert", Content = "Notification message." });
        }
    }
}
