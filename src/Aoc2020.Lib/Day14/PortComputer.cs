using Aoc2020.Lib.Util;
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
        private readonly Dictionary<long, BitArray> memory;

        public PortComputer(IEnumerable<Mask> masks)
        {
            this.masks = masks;
            this.memory = new Dictionary<long, BitArray>();
            for (var i = 0; i < SIZE; i++)
            {
                this.memory.Add(i, new BitArray(SIZE));
            }
        }

        public long Decode()
        {
            foreach (var m in this.masks)
            {
                foreach (var n in m.Memory)
                {
                    this.memory[n.Key] = this.VersionOne(m.MaskValues, n.Value);
                }
            }

            return this.memory.Values.Select(ConvertToLong).Sum();
        }

        public long MemoryDecode()
        {
            foreach (var m in this.masks)
            {
                foreach (var n in m.Memory)
                {
                    var floatingBits = this.VersionTwo(m.MaskValues, BitUtil.ConvertLongToBitArray(n.Key));
                    foreach (var d in floatingBits)
                    {
                        this.memory[ConvertToLong(d)] = n.Value;
                    }
                }
            }

            return this.memory.Values.Select(ConvertToLong).Sum();
        }

        private long ConvertToLong(BitArray bitArr, int idx = default)
        {
            var result = 0L;

            for (int bit = 0; bit < bitArr.Length; bit++)
            {
                result += bitArr[bit] ? Convert.ToInt64(Math.Pow(2, bit)) : 0L;
            }

            return result;
        }

        private BitArray VersionOne(MaskValues[] mask, BitArray value)
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

            return value;
        }

        private IEnumerable<BitArray> VersionTwo(MaskValues[] mask, BitArray value)
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
                var pattern = Convert.ToString(i, 2);
                var padded = pattern.PadLeft(SIZE, '0');
                var ln = padded.Length;
                for (var j = padded.Length - 1; j >= 0; j--)
                {
                    if (floatingBits.Count() > j)
                    {
                        cp.Set(floatingBits[j], padded[SIZE - 1 - j] == '1');
                    }
                }
                yield return cp;
            }
        }
    }
}
