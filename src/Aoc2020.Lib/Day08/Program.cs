using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2020.Lib.Day08
{
    public class Program
    {
        private Instruction[] instructions;
        private int index;
        private int accumulator;

        public Program(Instruction[] instructions)
        {
            this.instructions = instructions;
            this.index = 0;
            this.accumulator = 0;
        }

        public int Run()
        {
            var previousIndexes = new HashSet<int>();
            while (true)
            {
                var instruction = this.instructions[this.index];
                if (previousIndexes.Contains(this.index))
                {
                    return this.accumulator;
                }

                previousIndexes.Add(this.index);
                switch (instruction.Type)
                {
                    case InstructionType.Jump:
                        this.index += GetNumber(instruction);
                        break;
                    case InstructionType.Accumulate:
                        this.accumulator += GetNumber(instruction);
                        this.index++;
                        break;
                    case InstructionType.NoOperation:
                        this.index++;
                        break;
                    default:
                        throw new Exception("Unknown instruction");
                }
            }
        }

        private static int GetNumber(Instruction instruction)
        {
            return instruction.Sign == Sign.Positive
                                        ? instruction.Number
                                        : -1 * instruction.Number;
        }
    }
}
