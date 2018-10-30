using PokemonUtility.Models.Image;
using PokemonUtility.Struct;
using Prism.Mvvm;
using System.Drawing;

namespace PokemonUtility.Models.Capture
{
    class CaptureImageManegementModel : BindableBase
    {
        // アイコン矩形範囲
        public RelativeRectangle IconRectangle0 { get; set; }
        public RelativeRectangle IconRectangle1 { get; set; }
        public RelativeRectangle IconRectangle2 { get; set; }
        public RelativeRectangle IconRectangle3 { get; set; }
        public RelativeRectangle IconRectangle4 { get; set; }
        public RelativeRectangle IconRectangle5 { get; set; }

        // モデル
        CaptureWindowModel _captureWindowModel = CaptureWindowModel.GetInstance();

        public RelativeRectangle GetPartyRelativeRectangle(int partyIndex)
        {
            // プロパティ情報取得
            var property = GetType().GetProperty(string.Format("IconRectangle{0}", partyIndex));

            // プロパティが存在しなければ終了
            if (property == null) return new RelativeRectangle();

            // プロパティの値を取得
            return (RelativeRectangle)property.GetValue(this);
        }

        public Bitmap PartyPokemonImage(int partyIndex, int differenceX = 0, int differenceY = 0)
        {
            return CutImage(GetPartyRelativeRectangle(partyIndex), differenceX, differenceY);
        }

        private Bitmap CutImage(RelativeRectangle relativeRectangle, int differenceX = 0, int differenceY = 0)
        {
            // TODO 差分でキャプチャいめえーじからはみ出すとエラー

            // キャプチャ範囲
            var captureWindowModel = ModelConnector.CaptureWindow;

            // 画像キャプチャ
            Bitmap captureBitmap = ScreenCaptureModel.ScreenCapture(
                captureWindowModel.X,
                captureWindowModel.Y,
                captureWindowModel.Width,
                captureWindowModel.Height);

            // 切り抜く範囲
            Rectangle cutRect = new Rectangle(
                (int)(captureWindowModel.Width * relativeRectangle.X),
                (int)(captureWindowModel.Height * relativeRectangle.Y),
                (int)(captureWindowModel.Width * relativeRectangle.Width),
                (int)(captureWindowModel.Height * relativeRectangle.Height)
                );

            Bitmap cutBitmap = captureBitmap.Clone(cutRect, captureBitmap.PixelFormat);

            return cutBitmap;
        }
    }

    class MyCaptureImageManegementModel : CaptureImageManegementModel
    {
        #region Singleton

        static MyCaptureImageManegementModel Instance;
        public static MyCaptureImageManegementModel GetInstance()
        {
            if (Instance == null)
                Instance = new MyCaptureImageManegementModel();
            return Instance;
        }

        #endregion

        public MyCaptureImageManegementModel()
        {
            // パーティーの相対的位置
            IconRectangle0 = new RelativeRectangle(0.165, 0.16, 0.075, 0.125);
            IconRectangle1 = new RelativeRectangle(0.305, 0.16, 0.075, 0.125);
            IconRectangle2 = new RelativeRectangle(0.165, 0.38, 0.075, 0.125);
            IconRectangle3 = new RelativeRectangle(0.305, 0.38, 0.075, 0.125);
            IconRectangle4 = new RelativeRectangle(0.165, 0.59, 0.075, 0.125);
            IconRectangle5 = new RelativeRectangle(0.305, 0.59, 0.075, 0.125);
        }
    }

    class OpponentCaptureImageManegementModel : CaptureImageManegementModel
    {
        #region Singleton

        static OpponentCaptureImageManegementModel Instance;
        public static OpponentCaptureImageManegementModel GetInstance()
        {
            if (Instance == null)
                Instance = new OpponentCaptureImageManegementModel();
            return Instance;
        }

        #endregion

        public OpponentCaptureImageManegementModel()
        {
            // パーティーの相対的位置
            IconRectangle0 = new RelativeRectangle(0.61, 0.15, 0.075, 0.125);
            IconRectangle1 = new RelativeRectangle(0.755, 0.15, 0.075, 0.125);
            IconRectangle2 = new RelativeRectangle(0.61, 0.37, 0.075, 0.125);
            IconRectangle3 = new RelativeRectangle(0.755, 0.37, 0.075, 0.125);
            IconRectangle4 = new RelativeRectangle(0.61, 0.58, 0.075, 0.125);
            IconRectangle5 = new RelativeRectangle(0.755, 0.58, 0.075, 0.125);
        }
    }
}
