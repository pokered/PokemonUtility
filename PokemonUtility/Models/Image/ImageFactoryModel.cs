﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace PokemonUtility.Models
{
    class ImageFactoryModel
    {
        // 選出順定数
        public static readonly int ORDER_NO = -1;
        public static readonly int ORDER_FIRST = 0;
        public static readonly int ORDER_SECOND = 1;
        public static readonly int ORDER_THIRD = 2;

        // 待機状態
        public static readonly int WAIT_END = -1;
        public static readonly int WAIT_ONE = 0;
        public static readonly int WAIT_TWO = 1;
        public static readonly int WAIT_THREE = 2;
        public static readonly int WAIT_FOUR = 3;

        public static Bitmap CreatePokemonBitmap(int pokemonId)
        {
            // 現在のディレクトリ
            string currentDir = Directory.GetCurrentDirectory();

            // 画像の有無
            if (!ExistPokemonImage(pokemonId)) return new Bitmap(Path.Combine(currentDir, "Images/progress/progress3.png"));

            // ポケモンイメージパス作成
            string pokemonImagePath = string.Format("Images/pokemon/{0}/icon.png", pokemonId);
            string relativePokemonImagePath = Path.Combine(currentDir, pokemonImagePath);

            return new Bitmap(relativePokemonImagePath);
        }

        public static Bitmap CreateFrameBitmap(int order)
        {
            string frameName = "not_selected";

            if (order < 0)
            {
                frameName = "normal_frame";
            }
            else if (order == ORDER_FIRST)
            {
                frameName = "order_first";
            }
            else if (order == ORDER_SECOND || order == ORDER_THIRD)
            {
                frameName = "order_second";
            }

            // フレームイメージパス作成
            string currentDir = Directory.GetCurrentDirectory();
            string frameImagePath = string.Format("Images/frame/{0}.png", frameName);
            string relativeFrameImagePath = Path.Combine(currentDir, frameImagePath);

            if (!File.Exists(relativeFrameImagePath)) return new Bitmap(40, 40);

            return new Bitmap(relativeFrameImagePath);
        }

        public static BitmapImage CreateWaitImage(int waitState)
        {
            // 待機イメージパス取得
            string WaitImagePath = string.Format("Images/WaitState/WaitState{0}.png", waitState);
            string relativeWaitImagePath = Path.Combine(Directory.GetCurrentDirectory(), WaitImagePath);

            // パスが存在しなければ空の画像を返す
            if (!File.Exists(relativeWaitImagePath)) return new BitmapImage();

            // イメージ取得
            BitmapImage WaitImage = new BitmapImage();
            WaitImage.BeginInit();
            WaitImage.UriSource = new Uri(relativeWaitImagePath);
            WaitImage.EndInit();

            return WaitImage;
        }

        public static bool ExistPokemonImage(int pokemonId)
        {
            string currentDir = Directory.GetCurrentDirectory();
            string pokemonImagePath = string.Format("Images/pokemon/{0}/icon.png", pokemonId);
            string relativePokemonImagePath = Path.Combine(currentDir, pokemonImagePath);

            if (File.Exists(relativePokemonImagePath)) return true;

            return false;
        }

        public static Bitmap ResizeBitmap(Bitmap bitmap, int resizeWidth, int resizeHeight)
        {
            Bitmap resizeBitmap = new Bitmap(resizeWidth, resizeHeight);

            using (Graphics g = Graphics.FromImage(resizeBitmap))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bitmap, 0, 0, resizeWidth, resizeHeight);
            }

            return resizeBitmap;
        }

        public static BitmapImage CreatePokemonImage(int pokemonId, int order=-1, int imageSize=40)
        {
            // ポケモンイメージ
            Bitmap pokemonImage = CreatePokemonBitmap(pokemonId);
            pokemonImage = ResizeBitmap(pokemonImage, imageSize, imageSize);

            Bitmap frameImage = CreateFrameBitmap(order);
            frameImage = ResizeBitmap(frameImage, imageSize, imageSize);

            Bitmap newImage = OverlayImage(frameImage, pokemonImage);

            // bitmapからbitmapImageに変換
            using (Stream stream = new MemoryStream())
            {
                newImage.Save(stream, ImageFormat.Png);
                stream.Seek(0, SeekOrigin.Begin);
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.StreamSource = stream;
                img.EndInit();
                return img;
            }
        }

        // 画像を重ねる
        private static Bitmap OverlayImage(Bitmap foregroundImage, Bitmap backgroundImage)
        {
            using (Graphics g = Graphics.FromImage(backgroundImage))
            {
                g.CompositingMode = CompositingMode.SourceOver;
                g.DrawImage(foregroundImage, 0, 0);
                return backgroundImage;
            }
        }
    }
}
