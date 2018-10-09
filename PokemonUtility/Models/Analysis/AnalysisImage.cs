using PokemonUtility.Const;
using PokemonUtility.Models.Capture;
using PokemonUtility.Models.Database;
using PokemonUtility.Models.Main;
using PokemonUtility.Models.Party;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PokemonUtility.Models.Analysis
{
    class AnalysisImage
    {
        // メインモデル
        private MainWindowModel _mainWindowModel = MainWindowModel.GetInstance();

        // キャプチャモデル
        private CaptureWindowModel _captureWindowModel = CaptureWindowModel.GetInstance();
        private CaptureImageManegementModel _captureManegementModel = CaptureImageManegementModel.GetInstance();

        // 自分のパーティー関連のモデル
        private MyPartyWindowModel _myPartyWindowModel = MyPartyWindowModel.GetInstance();
        private MyPartyManegementModel _myPartyManegementModel = MyPartyManegementModel.GetInstance();
        private MyPartyWaitStateModel _myPartyWaitStateModel = MyPartyWaitStateModel.GetInstance();

        // 相手のパーティー関連のモデル
        private OpponentPartyWindowModel _opponentPartyWindowModel = OpponentPartyWindowModel.GetInstance();
        private OpponentPartyManegementModel _opponentPartyManegementModel = OpponentPartyManegementModel.GetInstance();
        private OpponentPartyWaitStateModel _opponentPartyWaitStateModel = OpponentPartyWaitStateModel.GetInstance();

        public async Task<bool> RunAsync()
        {
            // キャプチャ全体画面を表示
            _captureManegementModel.CreatePokemonMarkedCaptureImage();

            // 各ウィンドウを分析中にする
            _mainWindowModel.IsAnalyzing = true;
            _myPartyWindowModel.IsAnalyzing = true;
            _opponentPartyWindowModel.IsAnalyzing = true;

            PokemonIdConverterModel pokemonIdConverterModel = new PokemonIdConverterModel();

            // 自分のパーティー
            for (int i = PartyConst.PARTY_INDEX_FIRST; i <= PartyConst.PARTY_INDEX_SIXTH; i++)
            {
                // 待機演出開始
                //_myPartyWaitStateModel.Start(i);

                // 画像切り取り
                Bitmap myBitmap = await Task.Run(() => _captureManegementModel.MyPartyPokemonImage(i));

                // 画像分析
                int pokemonId = Analysis(myBitmap);

                // ポケモンIDをオリジナルに変換
                int originalPokemonId = pokemonIdConverterModel.ToOriginalPokemonId(pokemonId);
                
                // 待機演出終了
                //_myPartyWaitStateModel.End(i);

                Console.WriteLine("OriginalPokemonID = " + originalPokemonId.ToString());

                // 分析結果からポケモンイメージを表示
                _myPartyManegementModel.ChangePokemonId(i, originalPokemonId);
            }

            // 相手のパーティー
            for (int i = PartyConst.PARTY_INDEX_FIRST; i <= PartyConst.PARTY_INDEX_SIXTH; i++)
            {
                //_opponentPartyWaitStateModel.Start(i);

                // 画像切り取り
                Bitmap opponentBitmap = _captureManegementModel.OpponentPartyPokemonImage(i);

                // 画像分析
                int pokemonId = Analysis(opponentBitmap);

                // ポケモンIDをオリジナルに変換
                int originalPokemonId = pokemonIdConverterModel.ToOriginalPokemonId(pokemonId);

                // 待機演出終了
                //_myPartyWaitStateModel.End(i);

                Console.WriteLine("OriginalPokemonID = " + originalPokemonId.ToString());

                // 分析結果からポケモンイメージを表示
                _opponentPartyManegementModel.ChangePokemonId(i, originalPokemonId);

                //_opponentPartyWaitStateModel.End(i);
            }

            // 各ウィンドウの分析終了
            _mainWindowModel.IsAnalyzing = false;
            _myPartyWindowModel.IsAnalyzing = false;
            _opponentPartyWindowModel.IsAnalyzing = false;

            return true;
        }

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

                return PokemonConst.POKEMON_ID_NO;
            }
            catch(IOException)
            {
                // 画像が大きすぎる
                Console.WriteLine("画像が大きすぎます。");

                return PokemonConst.POKEMON_ID_NO;
            }
        }
    }
}
