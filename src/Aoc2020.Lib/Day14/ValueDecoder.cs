using System.Collections;
using System.Collections.Generic;

namespace Aoc2020.Lib.Day14
{
    public class ValueDecoder : Decodable
    {
        public IEnumerable<BitArray> Decode(MaskValues[] mask, BitArray value)
        {
            for (var i = 0; i < mask.Length; i++)
            {
                switch (mask[i])
                {
                    case MaskValues.Zero:
                        value.Set(i, false);
                        break;
                    case MaskValues.One:
                        value.Set(i, true);
                        break;
                    case MaskValues.Floating:
                        break;
                }
            }

            yield return value;
        }
    }
}
