﻿using Prism.Mvvm;
using Prism.Commands;
using PokemonUtility.Models;
using System.Collections.Generic;
using Prism.Interactivity.InteractionRequest;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Threading.Tasks;
using System.Windows;

namespace PokemonUtility.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        // プロパティ
        public ReactiveProperty<bool> IsShowMyPartyWindow { get; }
        public ReactiveProperty<bool> IsShowOpponentPartyWindow { get; }
        
        // モデル
        private CaptureWindowModel _captureWindowModel = CaptureWindowModel.GetInstance();
        private MyPartyWindowModel _myPartyWindowModel = MyPartyWindowModel.GetInstance();
        private MyPartyManegementModel _myPartyManegementModel = MyPartyManegementModel.GetInstance();
        private OpponentPartyWindowModel _opponentPartyWindowModel = OpponentPartyWindowModel.GetInstance();

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

            IsShowMyPartyWindow = _myPartyWindowModel.ToReactivePropertyAsSynchronized(m => m.IsShowWindow);
            IsShowOpponentPartyWindow = _opponentPartyWindowModel.ToReactivePropertyAsSynchronized(m => m.IsShowWindow);
            
            // 世代コンボボックスのアイテム設定
            var softGenerationList = new List<SoftGeneration>();
            softGenerationList.Add(new SoftGeneration() { ID = 0, Name = "赤緑" });
            softGenerationList.Add(new SoftGeneration() { ID = 1, Name = "金銀" });
            softGenerationList.Add(new SoftGeneration() { ID = 2, Name = "ＲＳ" });
            softGenerationList.Add(new SoftGeneration() { ID = 3, Name = "ＤＰ" });
            softGenerationList.Add(new SoftGeneration() { ID = 4, Name = "黒白" });
            softGenerationList.Add(new SoftGeneration() { ID = 5, Name = "ＸＹ" });
            softGenerationList.Add(new SoftGeneration() { ID = 6, Name = "ＳＭ" });
            // 先にselecteditemに値を設定しないとダメ
            SelectedSoftGeneration = softGenerationList[0];
            SoftGenerations = softGenerationList;

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
            //AnalysisModel analysisModel = AnalysisModel.GetInstance();
            //int[] pokemonIdList = analysisModel.start();

            //MyPartyManegementModel myParty = MyPartyManegementModel.GetInstance();
            //myParty.PokemonId1 = pokemonIdList[0];
            //myParty.PokemonId2 = pokemonIdList[1];
            //myParty.PokemonId3 = pokemonIdList[2];
            //myParty.PokemonId4 = pokemonIdList[3];
            //myParty.PokemonId5 = pokemonIdList[4];
            //myParty.PokemonId6 = pokemonIdList[5];



            _myPartyWindowModel.IsAnalysisPokemon1 = true;

            int result = await Task.Run(() => DoWork(1000));
            _myPartyManegementModel.PokemonId1 = result;

            _myPartyWindowModel.IsAnalysisPokemon1 = false;
        }

        private int DoWork(int n)
        {
            System.Threading.Thread.Sleep(800);

            // このメソッドからの戻り値
            return 1;
        }
    }
}
