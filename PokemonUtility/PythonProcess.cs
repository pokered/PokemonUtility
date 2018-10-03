using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace PokemonUtility
{
    class PythonProcess
    {
        public int test()
        {
            // string fileName = @"C:\Users\pokered\PycharmProjects\AnalysisPokemonImage\analysis_pokemon_image.py";
            string fileName = @"python D:\test.py";

            if (!File.Exists(fileName))
                return -1;

            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(@"C:\Anaconda3\envs\pokemon\python.exe", fileName)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = false,
                Arguments = @"D:\icon.png"
            };

            // 起動
            p.Start();

            //出力を読み取る
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();

            return Int32.Parse(output);
            // MessageBox.Show(output);
        }

        public int test2()
        {
            // プロセスクラスのインスタンス化
            var p = new Process();

            // cmd.exeのパスを取得
            p.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");

            // 出力を読み取れるようにする
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = false;

            // ウインドウを表示しないようにする
            p.StartInfo.CreateNoWindow = true;

            // hello.pyを実行する（ファイルにはprint('Hello')と書かれている）
            p.StartInfo.Arguments = @"/d python test.py";

            // 起動
            p.Start();

            // 出力を読み取る
            string result = p.StandardOutput.ReadToEnd();

            // プロセス終了まで待機する
            p.WaitForExit();
            p.Close();

            return -1;

            // コンソールにHelloと表示される
            //Console.WriteLine(result);
        }
    }
}
