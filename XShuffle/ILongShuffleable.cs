namespace Myitian.Shuffling;

public interface ILongShuffleable
{
    long Length { get; }
    void Swap(long index0, long index1);
}
