using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonUtility.Models.Image
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

    class PokemonIconCutModel
    {
        // モデル
        CaptureWindowModel _captureWindowModel = CaptureWindowModel.GetInstance();

        // リスト
        List<RelativeRectangle> _myPartyList;
        List<RelativeRectangle> _opponentPartyList;

        public PokemonIconCutModel()
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

        public void CreateImages(int x, int y, int width, int height)
        {
            string saveDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Icons");

            // 保存ディレクトリがなければ作る
            if (!Directory.Exists(saveDirectory)) Directory.CreateDirectory(saveDirectory);

            // 画像キャプチャ
            Bitmap bmpBase = ScreenCaptureModel.ScreenCapture(x, y, width, height);

            // 画像切り取り
            for (int i = 0; i < _myPartyList.Count; i++)
            {
                // 切り抜く範囲
                Rectangle cutRect = new Rectangle(
                    (int)(width * _myPartyList[i].X),
                    (int)(height * _myPartyList[i].Y),
                    (int)(width * _myPartyList[i].Width),
                    (int)(height * _myPartyList[i].Height)
                    );

                using (Bitmap bmpNew = bmpBase.Clone(cutRect, bmpBase.PixelFormat))
                {
                    // 保存
                    string saveImagePath = Path.Combine(saveDirectory, string.Format("myPokemon{0}.png", i));

                    bmpNew.Save(saveImagePath, ImageFormat.Png);
                }
            }
        }
    }
}
