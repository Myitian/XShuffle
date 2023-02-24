using System;
using System.Collections.Generic;

namespace Myitian.XShuffle
{
    public class XShuffle
    {
        const int DeafultXorshiftX = 12;
        const int DeafultXorshiftY = 25;
        const int DeafultXorshiftZ = 27;
        const ulong DeafultXorshiftStarMultiplier = 0x2545F4914F6CDD1D;
        const uint DeafultSeed = 233333;

        public int XorshiftX;
        public int XorshiftY;
        public int XorshiftZ;
        public ulong XorshiftStarMultiplier;
        public uint Seed;

        public void Shuffle<T>(T[] targetList, int start = 0, int? end = null)
        {
            int i = EndValueConvert(end, targetList.Length);
            uint u = (uint)i;
            for (; i > start; i--)
            {
                int exchange = Cast(XorshiftStar(Seed ^ u + 1), u + 1);
                Swap(targetList, i, exchange);
                u--;
            }
        }
        public void Shuffle<T>(IList<T> targetList, int start = 0, int? end = null)
        {
            int i = EndValueConvert(end, targetList.Count);
            uint u = (uint)i;
            for (; i > start; i--)
            {
                int exchange = Cast(XorshiftStar(Seed ^ u + 1), u + 1);
                Swap(targetList, i, exchange);
                u--;
            }
        }
        public string Shuffle(string str, int start = 0, int? end = null)
        {
            char[] chars = str.ToCharArray();
            Shuffle(chars, start, end);
            return new string(chars);
        }
        public void ReversedShuffle<T>(T[] targetList, int start = 0, int? end = null)
        {
            int e = EndValueConvert(end, targetList.Length);
            int i = start + 1;
            uint u = (uint)i;
            for (; i <= e; i++)
            {
                int exchange = Cast(XorshiftStar(Seed ^ u + 1), ++u);
                Swap(targetList, i, exchange);
            }
        }
        public void ReversedShuffle<T>(IList<T> targetList, int start = 0, int? end = null)
        {
            int e = EndValueConvert(end, targetList.Count);
            int i = start + 1;
            uint u = (uint)i;
            for (; i <= e; i++)
            {
                int exchange = Cast(XorshiftStar(Seed ^ u + 1), ++u);
                Swap(targetList, i, exchange);
            }
        }
        public string ReversedShuffle(string str, int start = 0, int? end = null)
        {
            char[] chars = str.ToCharArray();
            ReversedShuffle(chars);
            return new string(chars);
        }

        public uint XorshiftStar(uint seed)
        {
            ulong x = seed;
            x ^= x << XorshiftX;
            x ^= x >> XorshiftY;
            x ^= x << XorshiftZ;
            return (uint)((x * XorshiftStarMultiplier) >> 32);
        }

        public static int EndValueConvert(int? end, int len)
        {
            if (end is null)
            {
                return len - 1;
            }
            else
            {
                return end.Value - 1;
            }
        }
        public static void Swap<T>(T[] seq, int index0, int index1)
        {
            T temp = seq[index0];
            seq[index0] = seq[index1];
            seq[index1] = temp;
        }
        public static void Swap<T>(IList<T> seq, int index0, int index1)
        {
            T temp = seq[index0];
            seq[index0] = seq[index1];
            seq[index1] = temp;
        }

        public static int Cast(uint x, uint max) => (int)(x / 4294967296d * max);

        public XShuffle()
            : this(DeafultSeed) { }
        public XShuffle(uint seed)
            : this(seed, DeafultXorshiftX, DeafultXorshiftY, DeafultXorshiftZ) { }
        public XShuffle(uint seed, int x, int y, int z)
            : this(seed, x, y, z, DeafultXorshiftStarMultiplier) { }
        public XShuffle(uint seed, int x, int y, int z, ulong m)
        {
            if (x < 0 || x > 63)
            {
                throw new ArgumentOutOfRangeException("x");
            }
            if (y < 0 || y > 63)
            {
                throw new ArgumentOutOfRangeException("y");
            }
            if (z < 0 || z > 63)
            {
                throw new ArgumentOutOfRangeException("z");
            }
            if (m == 0)
            {
                throw new ArgumentException("m");
            }

            XorshiftX = x;
            XorshiftY = y;
            XorshiftZ = z;
            XorshiftStarMultiplier = m;
            Seed = seed;
        }
    }
}
