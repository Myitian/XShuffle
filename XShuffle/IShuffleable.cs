namespace Myitian.Shuffling;

public interface IShuffleable
{
    int Length { get; }
    void Swap(int index0, int index1);
}
