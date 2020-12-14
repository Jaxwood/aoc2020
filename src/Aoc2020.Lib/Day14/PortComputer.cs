using Aoc2020.Lib.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day14
{
    public class PortComputer
    {
        private const int SIZE = 36;
        private readonly IEnumerable<Mask> masks;
        private readonly Decodable decoder;
        private readonly Dictionary<long, BitArray> memory;

        public PortComputer(IEnumerable<Mask> masks, Decodable decoder)
        {
            this.masks = masks;
            this.decoder = decoder;
            this.memory = new Dictionary<long, BitArray>();
            for (var i = 0; i < SIZE; i++)
            {
                this.memory.Add(i, new BitArray(SIZE));
            }
        }

        public long Decode(bool updateMemory = false)
        {
            foreach (var m in this.masks)
            {
                foreach (var n in m.Memory)
                {
                    if (updateMemory)
                    {
                        foreach (var code in this.decoder.Decode(m.MaskValues, BitUtil.ConvertLongToBitArray(n.Key)))
                        {
                            this.memory[BitUtil.ConvertBitArrayToLong(code)] = n.Value;
                        }
                    }
                    else
                    {
                        foreach (var code in this.decoder.Decode(m.MaskValues, n.Value))
                        {
                            this.memory[n.Key] = code;
                        }
                    }
                }
            }

            return this.memory.Values.Select(BitUtil.ConvertBitArrayToLong).Sum();
        }
    }
}
