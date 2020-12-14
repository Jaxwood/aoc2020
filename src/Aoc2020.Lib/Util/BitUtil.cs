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

        public static long ConvertBitArrayToLong(BitArray bitArr, int idx = default)
        {
            var result = 0L;

            for (int bit = 0; bit < bitArr.Length; bit++)
            {
                result += bitArr[bit] ? Convert.ToInt64(Math.Pow(2, bit)) : 0L;
            }

            return result;
        }
    }
}
