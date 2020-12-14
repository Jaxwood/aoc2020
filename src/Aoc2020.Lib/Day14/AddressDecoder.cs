using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day14
{
    public class AddressDecoder : Decodable
    {
        private const int SIZE = 36;

        public IEnumerable<BitArray> Decode(MaskValues[] mask, BitArray value)
        {
            var floatingBits = new List<int>();
            for (var i = 0; i < mask.Length; i++)
            {
                switch (mask[i])
                {
                    case MaskValues.Zero:
                        break;
                    case MaskValues.One:
                        value.Set(i, true);
                        break;
                    case MaskValues.Floating:
                        floatingBits.Add(i);
                        break;
                }
            }

            var len = floatingBits.Count();
            for (int i = 0; i < Math.Pow(2, len); i++)
            {
                var cp = new BitArray(value);
                var pattern = Convert.ToString(i, 2).PadLeft(SIZE, '0');
                for (var j = pattern.Length - 1; j >= 0; j--)
                {
                    if (floatingBits.Count() > j)
                    {
                        cp.Set(floatingBits[j], pattern[SIZE - 1 - j] == '1');
                    }
                }
                yield return cp;
            }
        }
    }
}
