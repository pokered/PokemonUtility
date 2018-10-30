using PokemonUtility.Const;
using PokemonUtility.Models.Abstract;
using PokemonUtility.Models.Capture;
using PokemonUtility.Models.Image;
using PokemonUtility.Struct;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace PokemonUtility.Models.Main
{
    class MainWindowModel : WindowModel
    {
        #region Singleton

        static MainWindowModel Instance;
        public static MainWindowModel GetInstance()
        {
            if (Instance == null)
                Instance = new MainWindowModel();
            return Instance;
        }

        #endregion

        // 対戦結果
        private int _battleResult = BattleResult.RESULT_WIN;
        public int BattleResultId
        {
            get { return _battleResult; }
            set { SetProperty(ref _battleResult, value); }
        }

        // 世代リスト
        public List<SoftGeneration> SoftGenerationList { get; set; }

        // 世代ID
        private int _softGenerationId = 0;
        public int SoftGenerationId
        {
            get { return _softGenerationId; }
            set { SetProperty(ref _softGenerationId, value); }
        }

        // ログ
        private string _log = "";
        public string Log
        {
            get { return _log; }
            set { SetProperty(ref _log, value); }
        }

        // キャプチャイメージ
        private BitmapImage _pokemonMarkedCaptureImage;
        public BitmapImage PokemonMarkedCaptureImage
        {
            get { return _pokemonMarkedCaptureImage; }
            set { SetProperty(ref _pokemonMarkedCaptureImage, value); }
        }

        public MainWindowModel()
        {
            // 世代リスト作成
            SoftGenerationList = new List<SoftGeneration>();
            SoftGenerationList.Add(new SoftGeneration() { Id = SoftGeneration.SUN_MOON, Name = SoftGeneration.SUN_MOON_NAME });
            SoftGenerationList.Add(new SoftGeneration() { Id = SoftGeneration.X_Y, Name = SoftGeneration.X_Y_NAME });
            SoftGenerationList.Add(new SoftGeneration() { Id = SoftGeneration.BLACK_WHITE, Name = SoftGeneration.BLACK_WHITE_NAME });
            SoftGenerationList.Add(new SoftGeneration() { Id = SoftGeneration.DIAMOND_PEARL, Name = SoftGeneration.DIAMOND_PEARL_NAME });
            SoftGenerationList.Add(new SoftGeneration() { Id = SoftGeneration.RUBY_SAPPHIRE, Name = SoftGeneration.RUBY_SAPPHIRE_NAME });
            SoftGenerationList.Add(new SoftGeneration() { Id = SoftGeneration.GOLD_SILVER, Name = SoftGeneration.GOLD_SILVER_NAME });
            SoftGenerationList.Add(new SoftGeneration() { Id = SoftGeneration.RED_GREEN, Name = SoftGeneration.RED_GREEN_NAME });
        }

        public void AddLog(string message)
        {
            // メッセージに時間を付加
            message += " " + DateTime.Now.ToString("HH:mm");

            // メッセージを繋げる
            string log = message + "\n" + Log;

            Log = log.Trim();
        }

        public void CreatePokemonMarkedCaptureImage()
        {
            var captureWindowModel = ModelConnector.CaptureWindow;
            var myCaptureImageManegementModel = ModelConnector.MyCaptureImageManegement;
            var opponentCaptureImageManegementModel = ModelConnector.OpponentCaptureImageManegement;

            // キャプチャ範囲
            var captureRect = new Rectangle(captureWindowModel.X, captureWindowModel.Y, captureWindowModel.Width, captureWindowModel.Height);

            // アイコン部分を囲む矩形リストを作成
            List<Rectangle> iconSurroundRects = new List<Rectangle>();
            iconSurroundRects.AddRange(CreateIconSurroundRects(captureRect, myCaptureImageManegementModel));
            iconSurroundRects.AddRange(CreateIconSurroundRects(captureRect, opponentCaptureImageManegementModel));

            // スクリーンキャプチャ
            var captureImage = ScreenCaptureModel.ScreenCapture(captureRect);

            //ImageオブジェクトのGraphicsオブジェクトを作成する
            using (Graphics g = Graphics.FromImage(captureImage))
            {
                // ペン
                using (Pen p = new Pen(Color.Red, 3))
                {
                    foreach (Rectangle iconSurroundRect in iconSurroundRects)
                    {
                        g.DrawRectangle(p, iconSurroundRect);
                    }
                }
            }

            PokemonMarkedCaptureImage = BitmapConverterModel.ToBitmapImage(captureImage);
        }

        private List<Rectangle> CreateIconSurroundRects(Rectangle rectangle, CaptureImageManegementModel captureImageManegementModel)
        {
            // アイコン部分を囲む矩形リストを作成
            List<Rectangle> iconSurroundRects = new List<Rectangle>();

            for (int i = PartyConst.FIRST; i <= PartyConst.SIXTH; i++)
            {
                // 矩形範囲
                var relativeRectangle = captureImageManegementModel.GetPartyRelativeRectangle(i);

                // パーティー
                iconSurroundRects.Add(new Rectangle(
                    (int)(rectangle.Width * relativeRectangle.X),
                    (int)(rectangle.Height * relativeRectangle.Y),
                    (int)(rectangle.Width * relativeRectangle.Width),
                    (int)(rectangle.Height * relativeRectangle.Height)
                    ));
            }

            return iconSurroundRects;
        }
    }
}
