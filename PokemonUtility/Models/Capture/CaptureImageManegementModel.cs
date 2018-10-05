using PokemonUtility.Models.Image;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace PokemonUtility.Models.Capture
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
        private BitmapImage _pokemonMarkedCaptureImage;
        public BitmapImage PokemonMarkedCaptureImage
        {
            get { return _pokemonMarkedCaptureImage; }
            set { SetProperty(ref _pokemonMarkedCaptureImage, value); }
        }

        // モデル
        CaptureWindowModel _captureWindowModel = CaptureWindowModel.GetInstance();

        // リスト
        private List<RelativeRectangle> _myPartyMarkedList;
        private List<RelativeRectangle> _opponentPartyMarkedList;

        public CaptureImageManegementModel()
        {
            // 自分のパーティーの相対的位置
            _myPartyMarkedList = new List<RelativeRectangle>();
            _myPartyMarkedList.Add(new RelativeRectangle(0.178, 0.16, 0.065, 0.125));
            _myPartyMarkedList.Add(new RelativeRectangle(0.32, 0.16, 0.065, 0.125));
            _myPartyMarkedList.Add(new RelativeRectangle(0.178, 0.37, 0.065, 0.125));
            _myPartyMarkedList.Add(new RelativeRectangle(0.32, 0.37, 0.065, 0.125));
            _myPartyMarkedList.Add(new RelativeRectangle(0.178, 0.58, 0.065, 0.125));
            _myPartyMarkedList.Add(new RelativeRectangle(0.32, 0.58, 0.065, 0.125));

            // 相手のパーティーの相対的位置
            _opponentPartyMarkedList = new List<RelativeRectangle>();
            _opponentPartyMarkedList.Add(new RelativeRectangle(0.61, 0.15, 0.08, 0.13));
            _opponentPartyMarkedList.Add(new RelativeRectangle(0.755, 0.15, 0.08, 0.13));
            _opponentPartyMarkedList.Add(new RelativeRectangle(0.61, 0.37, 0.08, 0.13));
            _opponentPartyMarkedList.Add(new RelativeRectangle(0.755, 0.37, 0.08, 0.13));
            _opponentPartyMarkedList.Add(new RelativeRectangle(0.61, 0.58, 0.08, 0.13));
            _opponentPartyMarkedList.Add(new RelativeRectangle(0.755, 0.58, 0.08, 0.13));
        }

        // アイコン部分に目印を付けたキャプチャが画面
        public void CreatePokemonMarkedCaptureImage()
        {
            // キャプチャ範囲
            int x = _captureWindowModel.X;
            int y = _captureWindowModel.Y;
            int width = _captureWindowModel.Width;
            int height = _captureWindowModel.Height;

            // アイコン部分を囲む矩形リストを作成
            List<Rectangle> iconSurroundRects = new List<Rectangle>();

            // 自分
            foreach (RelativeRectangle myPartyRect in _myPartyMarkedList)
            {
                iconSurroundRects.Add(new Rectangle(
                    (int)(width * myPartyRect.X),
                    (int)(height * myPartyRect.Y),
                    (int)(width * myPartyRect.Width),
                    (int)(height * myPartyRect.Height)
                    ));
            }

            // 相手
            foreach (RelativeRectangle opponentPartyRect in _opponentPartyMarkedList)
            {
                iconSurroundRects.Add(new Rectangle(
                    (int)(width * opponentPartyRect.X),
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

            PokemonMarkedCaptureImage = BitmapConverterModel.ToBitmapImage(screenBmp);
        }

        public Bitmap MyPartyPokemonImage(int pokemonIndex)
        {
            return CutImage(_myPartyMarkedList[pokemonIndex]);
        }

        public Bitmap OpponentPartyPokemonImage(int pokemonIndex)
        {
            return CutImage(_opponentPartyMarkedList[pokemonIndex]);
        }

        private Bitmap CutImage(RelativeRectangle relativeRectangle)
        {
            // キャプチャ範囲
            int x = _captureWindowModel.X;
            int y = _captureWindowModel.Y;
            int width = _captureWindowModel.Width;
            int height = _captureWindowModel.Height;

            // 画像キャプチャ
            Bitmap captureBitmap = ScreenCaptureModel.ScreenCapture(x, y, width, height);

            // 切り抜く範囲
            Rectangle cutRect = new Rectangle(
                (int)(width * relativeRectangle.X),
                (int)(height * relativeRectangle.Y),
                (int)(width * relativeRectangle.Width),
                (int)(height * relativeRectangle.Height)
                );

            Bitmap cutBitmap = captureBitmap.Clone(cutRect, captureBitmap.PixelFormat);

            return cutBitmap;
        }
    }
}
