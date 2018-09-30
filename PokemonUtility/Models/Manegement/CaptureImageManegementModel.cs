using PokemonUtility.Models.Image;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace PokemonUtility.Models.Manegement
{
    struct RelativeRectangle
    {
        public double X;
        public double Y;
        public double Width;
        public double Height;

        public RelativeRectangle(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }

    class CaptureImageManegementModel : BindableBase
    {
        #region Singleton

        static CaptureImageManegementModel Instance;
        public static CaptureImageManegementModel GetInstance()
        {
            if (Instance == null)
                Instance = new CaptureImageManegementModel();
            return Instance;
        }

        #endregion

        // キャプチャイメージ加工
        private BitmapImage _captureImage;
        public BitmapImage CaptureImage
        {
            get { return _captureImage; }
            set { SetProperty(ref _captureImage, value); }
        }

        // モデル
        CaptureWindowModel _captureWindowModel = CaptureWindowModel.GetInstance();

        // リスト
        private List<RelativeRectangle> _myPartyList;
        private List<RelativeRectangle> _opponentPartyList;

        public CaptureImageManegementModel()
        {
            // 自分のパーティーの相対的位置
            _myPartyList = new List<RelativeRectangle>();
            _myPartyList.Add(new RelativeRectangle(0.178, 0.16, 0.065, 0.125));
            _myPartyList.Add(new RelativeRectangle(0.32, 0.16, 0.065, 0.125));
            _myPartyList.Add(new RelativeRectangle(0.178, 0.37, 0.065, 0.125));
            _myPartyList.Add(new RelativeRectangle(0.32, 0.37, 0.065, 0.125));
            _myPartyList.Add(new RelativeRectangle(0.178, 0.58, 0.065, 0.125));
            _myPartyList.Add(new RelativeRectangle(0.32, 0.58, 0.065, 0.125));

            // 相手のパーティーの相対的位置
            _opponentPartyList = new List<RelativeRectangle>();
            _opponentPartyList.Add(new RelativeRectangle(0.61, 0.15, 0.08, 0.13));
            _opponentPartyList.Add(new RelativeRectangle(0.755, 0.15, 0.08, 0.13));
            _opponentPartyList.Add(new RelativeRectangle(0.61, 0.37, 0.08, 0.13));
            _opponentPartyList.Add(new RelativeRectangle(0.755, 0.37, 0.08, 0.13));
            _opponentPartyList.Add(new RelativeRectangle(0.61, 0.58, 0.08, 0.13));
            _opponentPartyList.Add(new RelativeRectangle(0.755, 0.58, 0.08, 0.13));
        }

        // アイコン部分に目印を付けたキャプチャが画面
        public void CreateCaptureImage()
        {
            // キャプチャ範囲
            int x = _captureWindowModel.X;
            int y = _captureWindowModel.Y;
            int width = _captureWindowModel.Width;
            int height = _captureWindowModel.Height;


            // アイコン部分を囲む矩形リストを作成
            List<Rectangle> iconSurroundRects = new List<Rectangle>();

            // 自分
            foreach (RelativeRectangle myPartyRect in _myPartyList)
            {
                iconSurroundRects.Add(new Rectangle(
                    (int)(x * myPartyRect.X),
                    (int)(height * myPartyRect.Y),
                    (int)(width * myPartyRect.Width),
                    (int)(height * myPartyRect.Height)
                    ));
            }

            // 相手
            foreach (RelativeRectangle opponentPartyRect in _opponentPartyList)
            {
                iconSurroundRects.Add(new Rectangle(
                    (int)(x * opponentPartyRect.X),
                    (int)(height * opponentPartyRect.Y),
                    (int)(width * opponentPartyRect.Width),
                    (int)(height * opponentPartyRect.Height)
                    ));
            }

            // 画像キャプチャ
            Bitmap screenBmp = ScreenCaptureModel.ScreenCapture(x, y, width, height);

            //ImageオブジェクトのGraphicsオブジェクトを作成する
            using (Graphics g = Graphics.FromImage(screenBmp))
            {
                // ペン
                using (Pen p = new Pen(Color.Red, 3))
                {
                    foreach(Rectangle iconSurroundRect in iconSurroundRects)
                    {
                        g.DrawRectangle(p, iconSurroundRect);
                    }
                }
            }

            CaptureImage = BitmapConverter.ToBitmapImage(screenBmp);
        }

        public void CutIconImages()
        {
            CutPartyIconImages(_myPartyList, "myPokemon");
            CutPartyIconImages(_opponentPartyList, "opponentPokemon");
        }

        private void CutPartyIconImages(List<RelativeRectangle> partyList, string fileName)
        {
            string saveDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Icons");

            // 保存ディレクトリがなければ作る
            if (!Directory.Exists(saveDirectory)) Directory.CreateDirectory(saveDirectory);

            // キャプチャ範囲
            int x = _captureWindowModel.X;
            int y = _captureWindowModel.Y;
            int width = _captureWindowModel.Width;
            int height = _captureWindowModel.Height;

            // 画像キャプチャ
            Bitmap baseBmp = ScreenCaptureModel.ScreenCapture(x, y, width, height);

            // 画像切り取り
            for (int i = 0; i < partyList.Count; i++)
            {
                // 切り抜く範囲
                Rectangle cutRect = new Rectangle(
                    (int)(x * partyList[i].X),
                    (int)(height * partyList[i].Y),
                    (int)(width * partyList[i].Width),
                    (int)(height * partyList[i].Height)
                    );

                using (Bitmap bmpNew = baseBmp.Clone(cutRect, baseBmp.PixelFormat))
                {
                    // 保存
                    string saveImagePath = Path.Combine(saveDirectory, string.Format(fileName + "{0}.png", i));

                    bmpNew.Save(saveImagePath, ImageFormat.Png);
                }
            }
        }
    }
}
