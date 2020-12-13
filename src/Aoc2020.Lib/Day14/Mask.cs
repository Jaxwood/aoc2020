using System.Collections;
using System.Collections.Generic;

namespace Aoc2020.Lib.Day14
{
    public record Mask(MaskValues[] MaskValues, IEnumerable<KeyValuePair<int, BitArray>> Memory);

    public enum MaskValues
    {
        Zero = 0,
        One = 1,
        Ignore = 2,
    }
}
