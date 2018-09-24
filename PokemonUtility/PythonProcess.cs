using System;
using System.Diagnostics;
using System.Windows;

namespace PokemonUtility
{
    class PythonProcess
    {
        public void test()
        {
            string fileName = @"D:\test.py";

            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(@"C:\Anaconda3\envs\pokemon\python.exe", fileName)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            // 起動
            p.Start();

            //出力を読み取る
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            MessageBox.Show(output);
        }
    }
}
