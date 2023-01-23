using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Myitian.Drawing
{
    /// <summary>
    /// 修改自 https://stackoverflow.com/questions/24701703
    /// </summary>
    public class DirectBitmap : IDisposable
    {
        public Bitmap Bitmap { get; private set; }
        public int[] Bits { get; private set; }
        public bool Disposed { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public int this[int x, int y]
        {
            get => GetPixel(x, y);
            set => SetPixel(x, y, value);
        }
        protected GCHandle BitsHandle { get; private set; }

        public DirectBitmap(Bitmap src) : this(src.Width, src.Height)
        {
            BitmapData srcL = src.LockBits(Rectangle.FromLTRB(0, 0, src.Width, src.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(srcL.Scan0, Bits, 0, Bits.Length);
            src.UnlockBits(srcL);
        }
        public DirectBitmap(int width, int height, Color col) : this(width, height)
        {
            using (Graphics g = Graphics.FromImage(Bitmap))
            {
                using (Brush b = new SolidBrush(col))
                {
                    g.FillRectangle(b, Rectangle.FromLTRB(0, 0, width, height));
                }
            }
        }
        public DirectBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new int[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppArgb, BitsHandle.AddrOfPinnedObject());
        }

        public void SetPixel(int x, int y, int col) => Bits[x + (y * Width)] = col;
        public void SetPixel(int x, int y, Color col) => Bits[x + (y * Width)] = col.ToArgb();

        public int GetPixel(int x, int y) => Bits[x + (y * Width)];

        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
        }
    }
}
