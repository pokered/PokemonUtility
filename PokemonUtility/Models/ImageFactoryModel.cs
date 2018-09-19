using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace PokemonUtility.Models
{
    class ImageFactoryModel
    {
        // 選出順定数
        private const int NORMAL = 0;
        private const int ORDER_FIRST = 0;
        private const int ORDER_SECOND = 1;
        private const int ORDER_THIRD = 2;

        public static BitmapImage CreatePokemonImage(int pokemonId)
        {
            // ポケモンイメージパス取得
            String pokemonImagePath = CreatePokemonImagePath(pokemonId);

            // パスが存在しなければ空の画像を返す
            if (!File.Exists(pokemonImagePath)) return new BitmapImage();

            // イメージ取得
            BitmapImage pokemonImage = new BitmapImage();
            pokemonImage.BeginInit();
            pokemonImage.UriSource = new Uri(pokemonImagePath);
            pokemonImage.EndInit();

            return pokemonImage;
        }

        private static string CreatePokemonImagePath(int pokemonId)
        {
            string currentDir = Directory.GetCurrentDirectory();
            string pokemonImagePath = string.Format("Images/pokemon/{0}/icon.png", pokemonId);
            string relativePokemonImagePath = Path.Combine(currentDir, pokemonImagePath);

            if (File.Exists(relativePokemonImagePath)) return relativePokemonImagePath;

            return Path.Combine(currentDir, "Images/progress/progress3.png");
        }

        public static bool ExistPokemonImage(int pokemonId)
        {
            string currentDir = Directory.GetCurrentDirectory();
            string pokemonImagePath = string.Format("Images/pokemon/{0}/icon.png", pokemonId);
            string relativePokemonImagePath = Path.Combine(currentDir, pokemonImagePath);

            if (File.Exists(relativePokemonImagePath)) return true;

            return false;
        }

        public static BitmapImage CreateFrameImage(int pokemonId, int order)
        {
            string frameName = "not_selected";

            if (!ExistPokemonImage(pokemonId))
            {
                frameName = "normal_frame";
            }
            else if(order == ORDER_FIRST)
            {
                frameName = "order_first";
            }
            else if (order == ORDER_SECOND || order == ORDER_THIRD)
            {
                frameName = "order_second";
            }

            // フレームイメージパス取得
            string frameImagePath = CreateFrameImagePath(frameName);

            // パスが存在しなければ空の画像を返す
            if (!File.Exists(frameImagePath)) return new BitmapImage();

            // イメージ取得
            BitmapImage pokemonImage = new BitmapImage();
            pokemonImage.BeginInit();
            pokemonImage.UriSource = new Uri(frameImagePath);
            pokemonImage.EndInit();

            return pokemonImage;
        }

        private static string CreateFrameImagePath(string fileName)
        {
            string currentDir = Directory.GetCurrentDirectory();
            string frameImagePath = string.Format("Images/frame/{0}.png", fileName);
            string relativeFrameImagePath = Path.Combine(currentDir, frameImagePath);

            return relativeFrameImagePath;
        }
    }
}
