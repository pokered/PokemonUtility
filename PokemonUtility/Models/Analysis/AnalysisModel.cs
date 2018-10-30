using PokemonUtility.Const;
using PokemonUtility.Models.Capture;
using PokemonUtility.Models.Database;
using PokemonUtility.Models.Party;
using Prism.Mvvm;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PokemonUtility.Models.Analysis
{
    class AnalysisModel : BindableBase
    {
        #region Singleton

        static AnalysisModel Instance;
        public static AnalysisModel GetInstance()
        {
            if (Instance == null)
                Instance = new AnalysisModel();
            return Instance;
        }

        #endregion

        private bool _isAnalyzing = false;
        public bool IsAnalyzing
        {
            get { return _isAnalyzing; }
            set { SetProperty(ref _isAnalyzing, value); }
        }
        
        public async Task<bool> RunAsync()
        {
            PokemonIdConverterModel pokemonIdConverterModel = new PokemonIdConverterModel();

            // 分析開始
            IsAnalyzing = true;

            // パーティー分析
            await PartyAnalysisAsync(ModelConnector.MyCaptureImageManegement, ModelConnector.MyPartyManegement, ModelConnector.MyPartyWaitState);
            await PartyAnalysisAsync(ModelConnector.OpponentCaptureImageManegement, ModelConnector.OpponentPartyManegement, ModelConnector.OpponentPartyWaitState);

            // 分析終了
            IsAnalyzing = false;

            return true;
        }

        // パーティー分析
        private async Task PartyAnalysisAsync(
            CaptureImageManegementModel captureImageManegementModel,
            PartyManegementModel partyManegementModel,
            PartyWaitStateModel partyWaitStateModel)
        {
            // ポケモンIDコンバーター
            PokemonIdConverterModel pokemonIdConverterModel = new PokemonIdConverterModel();

            // パーティー初期化
            partyManegementModel.PartyReset();

            for (int i = PartyConst.FIRST; i <= PartyConst.SIXTH; i++)
            {
                // 待機演出開始
                partyWaitStateModel.Start(i);

                // 画像切り取り
                Bitmap myBitmap = await Task.Run(() => captureImageManegementModel.PartyPokemonImage(i));

                // 画像分析
                int pokemonId = Analysis(myBitmap);

                // ポケモンIDをオリジナルに変換
                int originalPokemonId = pokemonIdConverterModel.ToOriginalPokemonId(pokemonId);

                // 待機演出終了
                bool a = await partyWaitStateModel.End(i);

                Console.WriteLine("OriginalPokemonID = " + originalPokemonId.ToString());

                // 分析結果からポケモンイメージを表示
                partyManegementModel.ChangePokemonId(i, originalPokemonId);
            }
        }

        // 画像分析
        private int Analysis(Bitmap bitmap)
        {
            try
            {
                // bitmapからbyte[]に変換
                MemoryStream img_ms = new MemoryStream();
                bitmap.Save(img_ms, ImageFormat.Png);
                byte[] binary_image = img_ms.GetBuffer();

                //サーバーのIPアドレス（または、ホスト名）とポート番号
                string ipOrHost = "127.0.0.1";
                int port = 50007;

                //TcpClientを作成し、サーバーと接続する
                TcpClient tcpClient = new TcpClient(ipOrHost, port);

                Console.WriteLine("サーバー({0}:{1})と接続しました({2}:{3})。",
                    ((System.Net.IPEndPoint)tcpClient.Client.RemoteEndPoint).Address,
                    ((System.Net.IPEndPoint)tcpClient.Client.RemoteEndPoint).Port,
                    ((System.Net.IPEndPoint)tcpClient.Client.LocalEndPoint).Address,
                    ((System.Net.IPEndPoint)tcpClient.Client.LocalEndPoint).Port);

                //NetworkStreamを取得する
                NetworkStream networkStream = tcpClient.GetStream();

                //読み取り、書き込みのタイムアウトを10秒にする
                //デフォルトはInfiniteで、タイムアウトしない
                //(.NET Framework 2.0以上が必要)
                //ns.ReadTimeout = 20000;
                //ns.WriteTimeout = 20000;

                //サーバーにデータを送信する
                networkStream.Write(binary_image, 0, binary_image.Length);

                //サーバーから送られたデータを受信する
                MemoryStream ms = new MemoryStream();

                // 受信サイズ
                byte[] resBytes = new byte[16384];
                int resSize = 0;

                do
                {
                    //データの一部を受信する
                    resSize = networkStream.Read(resBytes, 0, resBytes.Length);

                    //Readが0を返した時はサーバーが切断したと判断
                    if (resSize == 0)
                    {
                        Console.WriteLine("サーバーが切断しました。");
                        break;
                    }

                    //受信したデータを蓄積する
                    ms.Write(resBytes, 0, resSize);

                    //まだ読み取れるデータがあるか、データの最後が\nでない時は、
                    // 受信を続ける
                } while (networkStream.DataAvailable);

                // 受信したデータを文字列に変換
                Encoding enc = Encoding.UTF8;
                string stringPokemonId = enc.GetString(ms.GetBuffer(), 0, (int)ms.Length);

                // 受信したデータをintに変換
                int pokemonId = Convert.ToInt32(stringPokemonId);

                // ストリーム終了
                ms.Close();

                //閉じる
                networkStream.Close();
                tcpClient.Close();

                Console.WriteLine("PokemonID = " + pokemonId.ToString());
                Console.WriteLine("切断しました。");

                Console.ReadLine();

                return pokemonId;

            } catch(SocketException)
            {
                // 分析サーバーに接続できない場合
                Console.WriteLine("サーバーに接続できませんでした。");

                return PartyConst.POKEMON_ID_NO;
            }
            catch(IOException)
            {
                // 画像が大きすぎる
                Console.WriteLine("画像が大きすぎます。");

                return PartyConst.POKEMON_ID_NO;
            }
        }
    }
}
