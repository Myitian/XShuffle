using Myitian.Drawing;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace PicShuffle
{
    internal class Program
    {
        static int XorshiftX = 26;
        static int XorshiftY = 15;
        static int XorshiftZ = 17;
        static uint Seed = 233333;

        static readonly string[] Help = { "按Esc退出程序", "按H显示帮助", "按I显示参数", "按S设置参数", "按E打乱图片", "按D恢复图片" };

        static void Main(string[] args)
        {
            Console.WriteLine("PicShuffle 图像打乱 v1.0 by Myitian");
            Console.WriteLine();
            DirectBitmap dbmp;
            string saveFolder, saveFileName, saveFilePath;
            Console.WriteLine(string.Join(Environment.NewLine, Help));
            while (true)
            {
                Console.WriteLine();
                Console.Write("*输入操作：");
                ConsoleKey key = Console.ReadKey().Key;
                Console.WriteLine();
                try
                {
                    switch (key)
                    {
                        case ConsoleKey.Escape:
                            return;

                        case ConsoleKey.H:
                            Console.WriteLine(string.Join(Environment.NewLine, Help));
                            break;

                        case ConsoleKey.I:
                            Console.WriteLine("种子：" + Seed);
                            Console.WriteLine("Xorshift参数x：" + XorshiftX);
                            Console.WriteLine("Xorshift参数y：" + XorshiftY);
                            Console.WriteLine("Xorshift参数z：" + XorshiftZ);
                            break;

                        case ConsoleKey.S:
                            Seed = (uint)Int32Input("种子（留空使用默认值）：", 114514);
                            XorshiftX = Int32Input("Xorshift参数x（留空使用默认值）：", 26, 0, 31);
                            XorshiftY = Int32Input("Xorshift参数y（留空使用默认值）：", 15, 0, 31);
                            XorshiftZ = Int32Input("Xorshift参数z（留空使用默认值）：", 17, 0, 31);
                            Seed &= 0x7FFFFFFF;
                            break;

                        case ConsoleKey.E:
                            string originalPic = OpenFileInput("源图路径：");

                            Console.WriteLine("#读取图片中……");
                            using (Bitmap bmp = new Bitmap(originalPic))
                            {
                                dbmp = new DirectBitmap(bmp);
                            }
                            Console.WriteLine("#打乱图片中……");
                            KnuthDurstenfeld(dbmp.Bits);
                            Console.WriteLine("#处理完成！");

                            saveFolder = OpenDirectoryInput("保存到文件夹：");
                            saveFileName = SaveFileInput("保存的文件名（留空使用默认值）：", $"{Path.GetFileName(originalPic)}_s{Seed}-x{XorshiftX}-y{XorshiftY}-z{XorshiftZ}.png");
                            saveFilePath = Path.Combine(saveFolder, saveFileName);
                            dbmp.Bitmap.Save(saveFilePath, ImageFormat.Png);
                            Console.WriteLine("成功保存到 " + saveFilePath);
                            dbmp.Dispose();
                            break;

                        case ConsoleKey.D:
                            string shuffledPic = OpenFileInput("乱序图路径：");

                            Console.WriteLine("#读取图片中……");
                            using (Bitmap bmp = new Bitmap(shuffledPic))
                            {
                                dbmp = new DirectBitmap(bmp);
                            }
                            Console.WriteLine("#恢复图片中……");
                            ReversedKnuthDurstenfeld(dbmp.Bits);
                            Console.WriteLine("#处理完成！");

                            saveFolder = OpenDirectoryInput("保存到文件夹：");
                            saveFileName = SaveFileInput("保存的文件名（留空使用默认值）：", $"{Path.GetFileName(shuffledPic)}_s{Seed}-x{XorshiftX}-y{XorshiftY}-z{XorshiftZ}_reversed.png");
                            saveFilePath = Path.Combine(saveFolder, saveFileName);
                            dbmp.Bitmap.Save(saveFilePath, ImageFormat.Png);
                            Console.WriteLine("成功保存到 " + saveFilePath);
                            dbmp.Dispose();
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }


        }

        static int Int32Input(object prompt, int @default = 0, int min = 0, int max = int.MaxValue)
        {
            Console.Write(prompt);
            while (true)
            {
                try
                {
                    string read = Console.ReadLine()?.Trim();
                    if (string.IsNullOrEmpty(read))
                    {
                        return @default;
                    }
                    if (int.TryParse(read, out int i) && min <= i && i <= max)
                    {
                        return i;
                    }
                }
                catch
                {
                    Console.WriteLine($"只接受整数({min}~{max})");
                }
            }
        }
        static bool YNInput(object prompt, string yes = "y", string no = "n")
        {
            while (true)
            {
                Console.Write(prompt);
                string read = Console.ReadLine()?.Trim();
                if (read == yes)
                {
                    return true;
                }
                if (read == no)
                {
                    return false;
                }
            }
        }

        static string OpenFileInput(object prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                try
                {
                    string read = Console.ReadLine()?.Trim().Trim('"');
                    if (string.IsNullOrEmpty(read))
                    {
                        Console.WriteLine("请输入文件路径！");
                    }
                    if (!File.Exists(read))
                    {
                        Console.WriteLine("文件不存在！");
                    }
                    File.OpenRead(read).Close(); // 测试是否拥有文件权限
                    return read;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        static string OpenDirectoryInput(object prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                try
                {
                    string read = Console.ReadLine()?.Trim().Trim('"');
                    if (string.IsNullOrEmpty(read))
                    {
                        Console.WriteLine("请输入文件夹路径！");
                    }
                    if (!Directory.Exists(read))
                    {
                        Console.WriteLine("文件夹不存在！是否创建？");
                        if (YNInput("（y=是，n=否）："))
                        {
                            Directory.CreateDirectory(read);
                            Console.WriteLine("文件夹已创建");
                        }
                        else
                        {
                            continue;
                        }
                    }
                    return read;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        static string SaveFileInput(object prompt, string @default = null)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                try
                {
                    string read = Console.ReadLine()?.Trim().Trim('"');
                    if (string.IsNullOrEmpty(read))
                    {
                        if (@default == null)
                        {
                            Console.WriteLine("请输入文件路径！");
                        }
                        else
                        {
                            return @default;
                        }
                    }
                    if (File.Exists(read))
                    {
                        Console.WriteLine("文件已存在！是否覆盖？");
                        if (!YNInput("（y=是，n=否）："))
                        {
                            continue;
                        }
                    }
                    return read;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// Knuth-Durstenfeld Shuffle打乱算法
        /// </summary>
        public static void KnuthDurstenfeld<T>(T[] targetList)
        {
            for (uint i = (uint)targetList.Length - 1; i > 0; i--)
            {
                int exchange = Cast(Xorshift(Seed ^ i + 1), i + 1);
                T temp = targetList[i];
                targetList[i] = targetList[exchange];
                targetList[exchange] = temp;
            }
        }
        public static void ReversedKnuthDurstenfeld<T>(T[] targetList)
        {
            for (uint i = 1; i < targetList.Length; i++)
            {
                int exchange = Cast(Xorshift(Seed ^ i + 1), i + 1);
                T temp = targetList[i];
                targetList[i] = targetList[exchange];
                targetList[exchange] = temp;
            }
        }

        /// <summary>
        /// XORShift
        /// </summary>
        /// <param name="seed">种子</param>
        /// <returns></returns>
        public static uint Xorshift(uint seed)
        {
            seed ^= seed << XorshiftX;
            seed ^= seed >> XorshiftY;
            seed ^= seed << XorshiftZ;
            return seed;
        }

        public static int Cast(uint x, uint max) => (int)(x / 4294967295d * max);
    }
}
