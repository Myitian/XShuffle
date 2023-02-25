using System;
using System.Collections;
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

        public void Shuffle<T>(T[] target, int start = 0, int? end = null)
        {
            int l = target.Length;
            int i = EndIndexConvert(end, l);
            start = ExpandIndexConvert(start, l);
            uint u = (uint)i;
            for (; i > start; i--)
            {
                int exchange = Cast(XorshiftStar(Seed ^ u + 1), u + 1);
                Swap(target, i, exchange);
                u--;
            }
        }
        public void Shuffle<T>(IList<T> target, int start = 0, int? end = null)
        {
            int l = target.Count;
            int i = EndIndexConvert(end, l);
            start = ExpandIndexConvert(start, l);
            uint u = (uint)i;
            for (; i > start; i--)
            {
                int exchange = Cast(XorshiftStar(Seed ^ u + 1), u + 1);
                Swap(target, i, exchange);
                u--;
            }
        }
        public void Shuffle(BitArray target, int start = 0, int? end = null)
        {
            int l = target.Length;
            int i = EndIndexConvert(end, l);
            start = ExpandIndexConvert(start, l);
            uint u = (uint)i;
            for (; i > start; i--)
            {
                int exchange = Cast(XorshiftStar(Seed ^ u + 1), u + 1);
                Swap(target, i, exchange);
                u--;
            }
        }
        public string Shuffle(string target, int start = 0, int? end = null)
        {
            char[] chars = target.ToCharArray();
            Shuffle(chars, start, end);
            return new string(chars);
        }

        public void ReversedShuffle<T>(T[] target, int start = 0, int? end = null)
        {
            int l = target.Length;
            int e = EndIndexConvert(end, l);
            int i = ExpandIndexConvert(start, l) + 1;
            uint u = (uint)i;
            for (; i <= e; i++)
            {
                int exchange = Cast(XorshiftStar(Seed ^ u + 1), ++u);
                Swap(target, i, exchange);
            }
        }
        public void ReversedShuffle<T>(IList<T> target, int start = 0, int? end = null)
        {
            int l = target.Count;
            int e = EndIndexConvert(end, l);
            int i = ExpandIndexConvert(start, l) + 1;
            uint u = (uint)i;
            for (; i <= e; i++)
            {
                int exchange = Cast(XorshiftStar(Seed ^ u + 1), ++u);
                Swap(target, i, exchange);
            }
        }
        public void ReversedShuffle(BitArray target, int start = 0, int? end = null)
        {
            int l = target.Length;
            int e = EndIndexConvert(end, l);
            int i = ExpandIndexConvert(start, l) + 1;
            uint u = (uint)i;
            for (; i <= e; i++)
            {
                int exchange = Cast(XorshiftStar(Seed ^ u + 1), ++u);
                Swap(target, i, exchange);
            }
        }
        public string ReversedShuffle(string target, int start = 0, int? end = null)
        {
            char[] chars = target.ToCharArray();
            ReversedShuffle(chars, start, end);
            return new string(chars);
        }
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        public void Shuffle<T>(T[] target, Index? start = null, Index? end = null)
        {
            int l = target.Length;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            Shuffle(target, istart, iend);
        }
        public void Shuffle<T>(IList<T> target, Index? start = null, Index? end = null)
        {
            int l = target.Count;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            Shuffle(target, istart, iend);
        }
        public void Shuffle(BitArray target, Index? start = null, Index? end = null)
        {
            int l = target.Length;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            Shuffle(target, istart, iend);
        }
        public string Shuffle(string target, Index? start = null, Index? end = null)
        {
            int l = target.Length;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            return Shuffle(target, istart, iend);
        }

        public void ReversedShuffle<T>(T[] target, Index? start = null, Index? end = null)
        {
            int l = target.Length;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            ReversedShuffle(target, istart, iend);
        }
        public void ReversedShuffle<T>(IList<T> target, Index? start = null, Index? end = null)
        {
            int l = target.Count;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            ReversedShuffle(target, istart, iend);
        }
        public void ReversedShuffle(BitArray target, Index? start = null, Index? end = null)
        {
            int l = target.Length;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            ReversedShuffle(target, istart, iend);
        }
        public string ReversedShuffle(string target, Index? start = null, Index? end = null)
        {
            int l = target.Length;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            return ReversedShuffle(target, istart, iend);
        }

        public void Shuffle<T>(T[] target, Range? range = null)
        {
            Shuffle(target, range?.Start, range?.End);
        }
        public void Shuffle<T>(IList<T> target, Range? range = null)
        {
            Shuffle(target, range?.Start, range?.End);
        }
        public void Shuffle(BitArray target, Range? range = null)
        {
            Shuffle(target, range?.Start, range?.End);
        }
        public string Shuffle(string target, Range? range = null)
        {
            return Shuffle(target, range?.Start, range?.End);
        }

        public void ReversedShuffle<T>(T[] target, Range? range = null)
        {
            ReversedShuffle(target, range?.Start, range?.End);
        }
        public void ReversedShuffle<T>(IList<T> target, Range? range = null)
        {
            ReversedShuffle(target, range?.Start, range?.End);
        }
        public void ReversedShuffle(BitArray target, Range? range = null)
        {
            ReversedShuffle(target, range?.Start, range?.End);
        }
        public string ReversedShuffle(string target, Range? range = null)
        {
            return ReversedShuffle(target, range?.Start, range?.End);
        }
#endif

        public uint XorshiftStar(uint seed)
        {
            ulong x = seed;
            x ^= x << XorshiftX;
            x ^= x >> XorshiftY;
            x ^= x << XorshiftZ;
            return (uint)((x * XorshiftStarMultiplier) >> 32);
        }

        public static int EndIndexConvert(int? end, int len)
        {
            return end is null ? len : ExpandIndexConvert(end.Value, len);
        }

        public static int ExpandIndexConvert(int index, int len)
        {
            return index >= 0 ? index : len + index;
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
        public static void Swap(BitArray seq, int index0, int index1)
        {
            bool temp = seq[index0];
            seq[index0] = seq[index1];
            seq[index1] = temp;
        }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
        public void Shuffle<T>(Span<T> target, int start = 0, int? end = null)
        {
            int l = target.Length;
            int i = EndIndexConvert(end, l);
            start = ExpandIndexConvert(start, l);
            uint u = (uint)i;
            for (; i > start; i--)
            {
                int exchange = Cast(XorshiftStar(Seed ^ u + 1), u + 1);
                Swap(target, i, exchange);
                u--;
            }
        }
        public void ReversedShuffle<T>(Span<T> target, int start = 0, int? end = null)
        {
            int l = target.Length;
            int e = EndIndexConvert(end, l);
            int i = ExpandIndexConvert(start, l) + 1;
            uint u = (uint)i;
            for (; i <= e; i++)
            {
                int exchange = Cast(XorshiftStar(Seed ^ u + 1), ++u);
                Swap(target, i, exchange);
            }
        }
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        public void Shuffle<T>(Span<T> target, Index? start = null, Index? end = null)
        {
            int l = target.Length;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            Shuffle(target, istart, iend);
        }
        public void ReversedShuffle<T>(Span<T> target, Index? start = null, Index? end = null)
        {
            int l = target.Length;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            ReversedShuffle(target, istart, iend);
        }
        public void Shuffle<T>(Span<T> target, Range? range = null)
        {
            Shuffle(target, range?.Start, range?.End);
        }
        public void ReversedShuffle<T>(Span<T> target, Range? range = null)
        {
            ReversedShuffle(target, range?.Start, range?.End);
        }
#endif
        public static void Swap<T>(Span<T> seq, int index0, int index1)
        {
            T temp = seq[index0];
            seq[index0] = seq[index1];
            seq[index1] = temp;
        }
#endif

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
