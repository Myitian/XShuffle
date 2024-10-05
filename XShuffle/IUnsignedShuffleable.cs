namespace Myitian.Shuffling;

public interface IUnsignedShuffleable
{
    uint Length { get; }
    void Swap(uint index0, uint index1);
}
