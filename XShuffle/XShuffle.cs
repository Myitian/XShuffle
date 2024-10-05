using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Myitian.Shuffling;

public class XShuffle
{
    public const int DeafultXorshiftX = 12;
    public const int DeafultXorshiftY = 25;
    public const int DeafultXorshiftZ = 27;
    public const ulong DeafultXorshiftStarMultiplier = 0x2545F4914F6CDD1D;
    public const uint DeafultSeed = 233333;

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
    public unsafe void Shuffle<T>(T* target, int length)
        => Shuffle(target, length, 0, null, null);
    public void Shuffle(IUnsignedShuffleable target)
        => Shuffle(target, 0, null, null);
    public void Shuffle(ILongShuffleable target)
        => Shuffle(target, 0, null, null);
    public void Shuffle(string target)
        => Shuffle(target, 0, null, null);
    public string ShuffleStringElements(string target)
        => ShuffleStringElements(target, 0, null, null);

    public unsafe void ShuffleL<T>(T* target, long length)
        => ShuffleL(target, length, 0, null, null);
    public void ShuffleL(ILongShuffleable target)
        => ShuffleL(target, 0, null, null);


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
    public void Shuffle(IUnsignedShuffleable target, uint start = 0, uint? end = null, uint? seed = null)
    {
        uint l = target.Length;
        uint i = (end ?? l) - 1;
        uint u = i;
        uint s = seed ?? Seed;
        while (i > start)
        {
            uint exchange = CastU(XorshiftStar(s ^ u + 1), u + 1);
            target.Swap(i--, exchange);
            u--;
        }
    }
    public void Shuffle(ILongShuffleable target, int start = 0, int? end = null, uint? seed = null)
    {
        if (target.Length > int.MaxValue)
            throw new ArgumentException("target too long");
        int l = (int)target.Length;
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
    public unsafe void Shuffle<T>(T* target, int length, int start = 0, int? end = null, uint? seed = null)
    {
        int l = length;
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
    public void ShuffleL(ILongShuffleable target, long start = 0, long? end = null, uint? seed = null)
    {
        long l = target.Length;
        long i = EndIndexConvert(end, l) - 1;
        start = ExpandIndexConvert(start, l);
        uint u = (uint)i;
        uint s = seed ?? Seed;
        while (i > start)
        {
            long exchange = Cast(XorshiftStarL(s ^ u + 1), u + 1);
            target.Swap(i--, exchange);
            u--;
        }
    }
    public unsafe void ShuffleL<T>(T* target, long length, long start = 0, long? end = null, uint? seed = null)
    {
        long l = length;
        long i = EndIndexConvert(end, l) - 1;
        start = ExpandIndexConvert(start, l);
        uint u = (uint)i;
        uint s = seed ?? Seed;
        while (i > start)
        {
            long exchange = Cast(XorshiftStarL(s ^ u + 1), u + 1);
            Swap(target, i--, exchange);
            u--;
        }
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
    public void ReversedShuffle(IUnsignedShuffleable target, uint start = 0, uint? end = null, uint? seed = null)
    {
        uint l = target.Length;
        uint e = end ?? l;
        uint i = start + 1;
        uint u = i;
        uint s = seed ?? Seed;
        while (i < e)
        {
            uint exchange = CastU(XorshiftStar(s ^ u + 1), ++u);
            target.Swap(i++, exchange);
        }
    }
    public void ReversedShuffle(ILongShuffleable target, int start = 0, int? end = null, uint? seed = null)
    {
        if (target.Length > int.MaxValue)
            throw new ArgumentException("target too long");
        int l = (int)target.Length;
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
    public unsafe void ReversedShuffle<T>(T* target, int length, int start = 0, int? end = null, uint? seed = null)
    {
        int l = length;
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
    public void ReversedShuffleL(ILongShuffleable target, long start = 0, long? end = null, uint? seed = null)
    {
        long l = target.Length;
        long e = EndIndexConvert(end, l);
        long i = ExpandIndexConvert(start, l) + 1;
        uint u = (uint)i;
        uint s = seed ?? Seed;
        while (i < e)
        {
            long exchange = Cast(XorshiftStarL(s ^ u + 1), ++u);
            target.Swap(i++, exchange);
        }
    }
    public unsafe void ReversedShuffleL<T>(T* target, long length, long start = 0, long? end = null, uint? seed = null)
    {
        long l = length;
        long e = EndIndexConvert(end, l);
        long i = ExpandIndexConvert(start, l) + 1;
        uint u = (uint)i;
        uint s = seed ?? Seed;
        while (i < e)
        {
            long exchange = Cast(XorshiftStarL(s ^ u + 1), ++u);
            Swap(target, i++, exchange);
        }
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
    public void Shuffle(ILongShuffleable target, Index? start = null, Index? end = null, uint? seed = null)
    {
        if (target.Length > int.MaxValue)
            throw new ArgumentException("target too long");
        int l = (int)target.Length;
        int istart = start?.GetOffset(l) ?? 0;
        int? iend = end?.GetOffset(l);
        Shuffle(target, istart, iend, seed);
    }
    public unsafe void Shuffle<T>(T* target, int length, Index? start = null, Index? end = null, uint? seed = null)
    {
        int l = length;
        int istart = start?.GetOffset(l) ?? 0;
        int? iend = end?.GetOffset(l);
        Shuffle(target, length, istart, iend, seed);
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
    public void ReversedShuffle(ILongShuffleable target, Index? start = null, Index? end = null, uint? seed = null)
    {
        if (target.Length > int.MaxValue)
            throw new ArgumentException("target too long");
        int l = (int)target.Length;
        int istart = start?.GetOffset(l) ?? 0;
        int? iend = end?.GetOffset(l);
        ReversedShuffle(target, istart, iend, seed);
    }
    public unsafe void ReversedShuffle<T>(T* target, int length, Index? start = null, Index? end = null, uint? seed = null)
    {
        int l = length;
        int istart = start?.GetOffset(l) ?? 0;
        int? iend = end?.GetOffset(l);
        ReversedShuffle(target, length, istart, iend, seed);
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
    public void Shuffle(ILongShuffleable target, Range? range = null, uint? seed = null)
        => Shuffle(target, range?.Start, range?.End, seed);
    public unsafe void Shuffle<T>(T* target, int length, Range? range = null, uint? seed = null)
        => Shuffle(target, length, range?.Start, range?.End, seed);
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
    public void ReversedShuffle(ILongShuffleable target, Range? range = null, uint? seed = null)
        => ReversedShuffle(target, range?.Start, range?.End, seed);
    public unsafe void ReversedShuffle<T>(T* target, int length, Range? range = null, uint? seed = null)
        => ReversedShuffle(target, length, range?.Start, range?.End, seed);
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
    public ulong XorshiftStarL(uint seed)
    {
        ulong x = seed;
        x ^= x << XorshiftX;
        x ^= x >> XorshiftY;
        x ^= x << XorshiftZ;
        return x * XorshiftStarMultiplier;
    }

    static string CreateString(int[] ints, string s)
    {
        StringBuilder sb = new(s.Length);
        for (int i = 0; i < ints.Length; i++)
        {
            sb.Append(StringInfo.GetNextTextElement(s, ints[i]));
        }
        return sb.ToString();
    }

    public static int EndIndexConvert(int? end, int len)
        => end is null ? len : ExpandIndexConvert(end.Value, len);
    public static long EndIndexConvert(long? end, long len)
        => end is null ? len : ExpandIndexConvert(end.Value, len);

    public static int ExpandIndexConvert(int index, int len)
        => index >= 0 ? index : len + index;
    public static long ExpandIndexConvert(long index, long len)
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
    public static unsafe void Swap<T>(T* seq, int index0, int index1)
    {
        T temp = seq[index0];
        seq[index0] = seq[index1];
        seq[index1] = temp;
    }
    public static unsafe void Swap<T>(T* seq, long index0, long index1)
    {
        T temp = seq[index0];
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
    public static uint CastU(uint x, uint max)
        => (uint)(x / 4294967296d * max);
    public static long Cast(ulong x, ulong max)
        => (long)(x / 9223372036854775808d * max);

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
