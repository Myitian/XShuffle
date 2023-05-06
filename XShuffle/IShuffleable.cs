using System;
using System.Collections.Generic;
using System.Text;

namespace Myitian.XShuffle
{
    public interface IShuffleable
    {
        int Length { get; }
        void Swap(int index0, int index1);
    }
}
