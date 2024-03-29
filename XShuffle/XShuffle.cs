﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Myitian.Shuffling
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

        public void Shuffle<T>(T[] target)
            => Shuffle(target, 0, null, null);
        public void Shuffle<T>(IList<T> target)
            => Shuffle(target, 0, null, null);
        public void Shuffle(IList target)
            => Shuffle(target, 0, null, null);
        public void Shuffle(BitArray target)
            => Shuffle(target, 0, null, null);
        public void Shuffle(IShuffleable target)
            => Shuffle(target, 0, null, null);
        public void Shuffle(string target)
            => Shuffle(target, 0, null, null);
        public string ShuffleStringElements(string target)
            => ShuffleStringElements(target, 0, null, null);


        public void Shuffle<T>(T[] target, int start = 0, int? end = null, uint? seed = null)
        {
            int l = target.Length;
            int i = EndIndexConvert(end, l) - 1;
            start = ExpandIndexConvert(start, l);
            uint u = (uint)i;
            uint s = seed ?? Seed;
            while (i > start)
            {
                int exchange = Cast(XorshiftStar(s ^ u + 1), u + 1);
                Swap(target, i--, exchange);
                u--;
            }
        }
        public void Shuffle<T>(IList<T> target, int start = 0, int? end = null, uint? seed = null)
        {
            int l = target.Count;
            int i = EndIndexConvert(end, l) - 1;
            start = ExpandIndexConvert(start, l);
            uint u = (uint)i;
            uint s = seed ?? Seed;
            while (i > start)
            {
                int exchange = Cast(XorshiftStar(s ^ u + 1), u + 1);
                Swap(target, i--, exchange);
                u--;
            }
        }
        public void Shuffle(IList target, int start = 0, int? end = null, uint? seed = null)
        {
            int l = target.Count;
            int i = EndIndexConvert(end, l) - 1;
            start = ExpandIndexConvert(start, l);
            uint u = (uint)i;
            uint s = seed ?? Seed;
            while (i > start)
            {
                int exchange = Cast(XorshiftStar(s ^ u + 1), u + 1);
                Swap(target, i--, exchange);
                u--;
            }
        }
        public void Shuffle(BitArray target, int start = 0, int? end = null, uint? seed = null)
        {
            int l = target.Length;
            int i = EndIndexConvert(end, l) - 1;
            start = ExpandIndexConvert(start, l);
            uint u = (uint)i;
            uint s = seed ?? Seed;
            while (i > start)
            {
                int exchange = Cast(XorshiftStar(s ^ u + 1), u + 1);
                Swap(target, i--, exchange);
                u--;
            }
        }
        public void Shuffle(IShuffleable target, int start = 0, int? end = null, uint? seed = null)
        {
            int l = target.Length;
            int i = EndIndexConvert(end, l) - 1;
            start = ExpandIndexConvert(start, l);
            uint u = (uint)i;
            uint s = seed ?? Seed;
            while (i > start)
            {
                int exchange = Cast(XorshiftStar(s ^ u + 1), u + 1);
                target.Swap(i--, exchange);
                u--;
            }
        }
        public string Shuffle(string target, int start = 0, int? end = null, uint? seed = null)
        {
            char[] chars = target.ToCharArray();
            Shuffle(chars, start, end, seed);
            return new string(chars);
        }
        public string ShuffleStringElements(string target, int start = 0, int? end = null, uint? seed = null)
        {
            int[] indexes = StringInfo.ParseCombiningCharacters(target);
            Shuffle(indexes, start, end, seed);
            return CreateString(indexes, target);
        }

        public void ReversedShuffle<T>(T[] target, int start = 0, int? end = null, uint? seed = null)
        {
            int l = target.Length;
            int e = EndIndexConvert(end, l);
            int i = ExpandIndexConvert(start, l) + 1;
            uint u = (uint)i;
            uint s = seed ?? Seed;
            while (i < e)
            {
                int exchange = Cast(XorshiftStar(s ^ u + 1), ++u);
                Swap(target, i++, exchange);
            }
        }
        public void ReversedShuffle<T>(IList<T> target, int start = 0, int? end = null, uint? seed = null)
        {
            int l = target.Count;
            int e = EndIndexConvert(end, l);
            int i = ExpandIndexConvert(start, l) + 1;
            uint u = (uint)i;
            uint s = seed ?? Seed;
            while (i < e)
            {
                int exchange = Cast(XorshiftStar(s ^ u + 1), ++u);
                Swap(target, i++, exchange);
            }
        }
        public void ReversedShuffle(IList target, int start = 0, int? end = null, uint? seed = null)
        {
            int l = target.Count;
            int e = EndIndexConvert(end, l);
            int i = ExpandIndexConvert(start, l) + 1;
            uint u = (uint)i;
            uint s = seed ?? Seed;
            while (i < e)
            {
                int exchange = Cast(XorshiftStar(s ^ u + 1), ++u);
                Swap(target, i++, exchange);
            }
        }
        public void ReversedShuffle(BitArray target, int start = 0, int? end = null, uint? seed = null)
        {
            int l = target.Length;
            int e = EndIndexConvert(end, l);
            int i = ExpandIndexConvert(start, l) + 1;
            uint u = (uint)i;
            uint s = seed ?? Seed;
            while (i < e)
            {
                int exchange = Cast(XorshiftStar(s ^ u + 1), ++u);
                Swap(target, i++, exchange);
            }
        }
        public void ReversedShuffle(IShuffleable target, int start = 0, int? end = null, uint? seed = null)
        {
            int l = target.Length;
            int e = EndIndexConvert(end, l);
            int i = ExpandIndexConvert(start, l) + 1;
            uint u = (uint)i;
            uint s = seed ?? Seed;
            while (i < e)
            {
                int exchange = Cast(XorshiftStar(s ^ u + 1), ++u);
                target.Swap(i++, exchange);
            }
        }
        public string ReversedShuffle(string target, int start = 0, int? end = null, uint? seed = null)
        {
            char[] chars = target.ToCharArray();
            ReversedShuffle(chars, start, end, seed);
            return new string(chars);
        }
        public string ReversedShuffleStringElements(string target, int start = 0, int? end = null, uint? seed = null)
        {
            int[] indexes = StringInfo.ParseCombiningCharacters(target);
            ReversedShuffle(indexes, start, end, seed);
            return CreateString(indexes, target);
        }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        public void Shuffle<T>(T[] target, Index? start = null, Index? end = null, uint? seed = null)
        {
            int l = target.Length;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            Shuffle(target, istart, iend, seed);
        }
        public void Shuffle<T>(IList<T> target, Index? start = null, Index? end = null, uint? seed = null)
        {
            int l = target.Count;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            Shuffle(target, istart, iend, seed);
        }
        public void Shuffle(IList target, Index? start = null, Index? end = null, uint? seed = null)
        {
            int l = target.Count;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            Shuffle(target, istart, iend, seed);
        }
        public void Shuffle(BitArray target, Index? start = null, Index? end = null, uint? seed = null)
        {
            int l = target.Length;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            Shuffle(target, istart, iend, seed);
        }
        public void Shuffle(IShuffleable target, Index? start = null, Index? end = null, uint? seed = null)
        {
            int l = target.Length;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            Shuffle(target, istart, iend, seed);
        }
        public string Shuffle(string target, Index? start = null, Index? end = null, uint? seed = null)
        {
            int l = target.Length;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            return Shuffle(target, istart, iend, seed);
        }
        public string ShuffleStringElements(string target, Index? start = null, Index? end = null, uint? seed = null)
        {
            int[] indexes = StringInfo.ParseCombiningCharacters(target);
            int l = indexes.Length;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            Shuffle(indexes, istart, iend, seed);
            return CreateString(indexes, target);
        }

        public void ReversedShuffle<T>(T[] target, Index? start = null, Index? end = null, uint? seed = null)
        {
            int l = target.Length;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            ReversedShuffle(target, istart, iend, seed);
        }
        public void ReversedShuffle<T>(IList<T> target, Index? start = null, Index? end = null, uint? seed = null)
        {
            int l = target.Count;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            ReversedShuffle(target, istart, iend, seed);
        }
        public void ReversedShuffle(IList target, Index? start = null, Index? end = null, uint? seed = null)
        {
            int l = target.Count;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            ReversedShuffle(target, istart, iend, seed);
        }
        public void ReversedShuffle(BitArray target, Index? start = null, Index? end = null, uint? seed = null)
        {
            int l = target.Length;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            ReversedShuffle(target, istart, iend, seed);
        }
        public void ReversedShuffle(IShuffleable target, Index? start = null, Index? end = null, uint? seed = null)
        {
            int l = target.Length;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            ReversedShuffle(target, istart, iend, seed);
        }
        public string ReversedShuffle(string target, Index? start = null, Index? end = null, uint? seed = null)
        {
            int l = target.Length;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            return ReversedShuffle(target, istart, iend, seed);
        }
        public string ReversedShuffleStringElements(string target, Index? start = null, Index? end = null, uint? seed = null)
        {
            int[] indexes = StringInfo.ParseCombiningCharacters(target);
            int l = indexes.Length;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            ReversedShuffle(indexes, istart, iend, seed);
            return CreateString(indexes, target);
        }

        public void Shuffle<T>(T[] target, Range? range = null, uint? seed = null)
            => Shuffle(target, range?.Start, range?.End, seed);
        public void Shuffle<T>(IList<T> target, Range? range = null, uint? seed = null)
            => Shuffle(target, range?.Start, range?.End, seed);
        public void Shuffle(IList target, Range? range = null, uint? seed = null)
            => Shuffle(target, range?.Start, range?.End, seed);
        public void Shuffle(BitArray target, Range? range = null, uint? seed = null)
            => Shuffle(target, range?.Start, range?.End, seed);
        public void Shuffle(IShuffleable target, Range? range = null, uint? seed = null)
            => Shuffle(target, range?.Start, range?.End, seed);
        public string Shuffle(string target, Range? range = null, uint? seed = null)
            => Shuffle(target, range?.Start, range?.End, seed);
        public string ShuffleStringElements(string target, Range? range = null, uint? seed = null)
            => ShuffleStringElements(target, range?.Start, range?.End, seed);

        public void ReversedShuffle<T>(T[] target, Range? range = null, uint? seed = null)
            => ReversedShuffle(target, range?.Start, range?.End, seed);
        public void ReversedShuffle<T>(IList<T> target, Range? range = null, uint? seed = null)
            => ReversedShuffle(target, range?.Start, range?.End, seed);
        public void ReversedShuffle(IList target, Range? range = null, uint? seed = null)
            => ReversedShuffle(target, range?.Start, range?.End, seed);
        public void ReversedShuffle(BitArray target, Range? range = null, uint? seed = null)
            => ReversedShuffle(target, range?.Start, range?.End, seed);
        public void ReversedShuffle(IShuffleable target, Range? range = null, uint? seed = null)
            => ReversedShuffle(target, range?.Start, range?.End, seed);
        public string ReversedShuffle(string target, Range? range = null, uint? seed = null)
            => ReversedShuffle(target, range?.Start, range?.End, seed);
        public string ReversedShuffleStringElements(string target, Range? range = null, uint? seed = null)
            => ReversedShuffleStringElements(target, range?.Start, range?.End, seed);
#endif

        public uint XorshiftStar(uint seed)
        {
            ulong x = seed;
            x ^= x << XorshiftX;
            x ^= x >> XorshiftY;
            x ^= x << XorshiftZ;
            return (uint)((x * XorshiftStarMultiplier) >> 32);
        }

        static string CreateString(int[] ints, string s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ints.Length; i++)
            {
                sb.Append(StringInfo.GetNextTextElement(s, ints[i]));
            }
            return sb.ToString();
        }
        public static int EndIndexConvert(int? end, int len)
            => end is null ? len : ExpandIndexConvert(end.Value, len);

        public static int ExpandIndexConvert(int index, int len)
            => index >= 0 ? index : len + index;

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
        public static void Swap(IList seq, int index0, int index1)
        {
            object temp = seq[index0];
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
        public static void Swap<T>(Span<T> seq, int index0, int index1)
        {
            T temp = seq[index0];
            seq[index0] = seq[index1];
            seq[index1] = temp;
        }

        public void Shuffle<T>(Span<T> target) => Shuffle(target, 0, null, null);
        public void Shuffle<T>(Span<T> target, int start = 0, int? end = null, uint? seed = null)
        {
            int l = target.Length;
            int i = EndIndexConvert(end, l) - 1;
            start = ExpandIndexConvert(start, l);
            uint u = (uint)i;
            uint s = seed ?? Seed;
            while (i > start)
            {
                int exchange = Cast(XorshiftStar(s ^ u + 1), u + 1);
                Swap(target, i--, exchange);
                u--;
            }
        }
        public void ReversedShuffle<T>(Span<T> target, int start = 0, int? end = null, uint? seed = null)
        {
            int l = target.Length;
            int e = EndIndexConvert(end, l);
            int i = ExpandIndexConvert(start, l) + 1;
            uint u = (uint)i;
            uint s = seed ?? Seed;
            while (i < e)
            {
                int exchange = Cast(XorshiftStar(s ^ u + 1), ++u);
                Swap(target, i++, exchange);
            }
        }
#endif
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        public void Shuffle<T>(Span<T> target, Index? start = null, Index? end = null, uint? seed = null)
        {
            int l = target.Length;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            Shuffle(target, istart, iend, seed);
        }
        public void ReversedShuffle<T>(Span<T> target, Index? start = null, Index? end = null, uint? seed = null)
        {
            int l = target.Length;
            int istart = start?.GetOffset(l) ?? 0;
            int? iend = end?.GetOffset(l);
            ReversedShuffle(target, istart, iend, seed);
        }
        public void Shuffle<T>(Span<T> target, Range? range = null, uint? seed = null)
            => Shuffle(target, range?.Start, range?.End, seed);
        public void ReversedShuffle<T>(Span<T> target, Range? range = null, uint? seed = null)
            => ReversedShuffle(target, range?.Start, range?.End, seed);
#endif

        public static int Cast(uint x, uint max)
            => (int)(x / 4294967296d * max);

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
                throw new ArgumentOutOfRangeException(nameof(x));
            }
            if (y < 0 || y > 63)
            {
                throw new ArgumentOutOfRangeException(nameof(y));
            }
            if (z < 0 || z > 63)
            {
                throw new ArgumentOutOfRangeException(nameof(z));
            }
            if (m == 0)
            {
                throw new ArgumentException("Argument m cannot be zero");
            }

            XorshiftX = x;
            XorshiftY = y;
            XorshiftZ = z;
            XorshiftStarMultiplier = m;
            Seed = seed;
        }

        public override bool Equals(object obj)
            => obj is XShuffle xs
                && Seed == xs.Seed
                && XorshiftX == xs.XorshiftX
                && XorshiftY == xs.XorshiftY
                && XorshiftZ == xs.XorshiftZ
                && XorshiftStarMultiplier == xs.XorshiftStarMultiplier;
        public override int GetHashCode()
            => (int)Seed ^ XorshiftX ^ XorshiftY ^ XorshiftZ ^ XorshiftStarMultiplier.GetHashCode();
        public override string ToString()
            => $"[{GetType()}@seed:{Seed}+args:x{XorshiftX}y{XorshiftY}z{XorshiftZ}m{XorshiftStarMultiplier}]";
    }
}
