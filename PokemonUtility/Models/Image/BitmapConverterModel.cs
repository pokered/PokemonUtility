using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Microsoft.Win32.SafeHandles;

namespace PokemonUtility.Models.Image
{
    public static class BitmapConverterModel
    {
        public static BitmapSource ToBitmapSource(this Bitmap source)
        {
            using (var handle = new SafeHBitmapHandle(source))
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle.DangerousGetHandle(),
                    IntPtr.Zero, Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
        }

        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        private sealed class SafeHBitmapHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            [SecurityCritical]
            public SafeHBitmapHandle(Bitmap bitmap)
                : base(true)
            {
                SetHandle(bitmap.GetHbitmap());
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                return DeleteObject(handle) > 0;
            }
        }

        public static BitmapImage ToBitmapImage(this Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}
