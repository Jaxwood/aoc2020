using System;

namespace Aoc2020.Lib.Day20
{
    [Flags]
    public enum Alignment
    {
        Nineteen = 1,
        OneHundredEighty = 2,
        TwoHundredSeventy = 4,
        Zero = 8,
        Front = 16,
        Back = 32,
    }
}
