using System.Collections;
using System.Collections.Generic;

namespace Aoc2020.Lib.Day14
{
    public interface Decodable
    {
        IEnumerable<BitArray> Decode(MaskValues[] mask, BitArray value);
    }
}
