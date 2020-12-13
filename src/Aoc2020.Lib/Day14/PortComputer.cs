using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day14
{
    public class PortComputer
    {
        private const int SIZE = 36;
        private readonly IEnumerable<Mask> masks;
        private readonly Dictionary<int, BitArray> memory;

        public PortComputer(IEnumerable<Mask> masks)
        {
            this.masks = masks;
            this.memory = new Dictionary<int, BitArray>();
            for (var i = 0; i < SIZE; i++)
            {
                this.memory.Add(i, new BitArray(SIZE));
            }
        }

        public long Compute()
        {
            foreach (var m in this.masks)
            {
                foreach (var n in m.Memory)
                {
                    this.memory[n.Key] = this.ApplyMask(m.MaskValues, n.Value);
                }
            }

            return this.memory.Values.Select(ConvertToLong).Sum();
        }

        private long ConvertToLong(BitArray bitArr, int idx)
        {
            var result = 0L;

            for (int bit = 0; bit < bitArr.Length; bit++)
            {
                result += bitArr[bit] ? Convert.ToInt64(Math.Pow(2, bit)) : 0L;
            }

            return result;
        }

        private BitArray ApplyMask(MaskValues[] mask, BitArray value)
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
                    case MaskValues.Ignore:
                        break;
                }
            }

            return value;
        }
    }
}
