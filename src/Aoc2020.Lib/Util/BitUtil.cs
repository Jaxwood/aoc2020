using System;
using System.Collections;

namespace Aoc2020.Lib.Util
{
    public static class BitUtil
    {
        public static BitArray ConvertLongToBitArray(long candidate, int size = 36)
        {
            var val = Convert.ToString(candidate, 2);
            var result = new BitArray(size, false);
            for (var i = 0; i < val.Length; i++)
            {
                result.Set(val.Length - 1 - i, val[i] == '1');
            }

            return result;
        }

    }
}
