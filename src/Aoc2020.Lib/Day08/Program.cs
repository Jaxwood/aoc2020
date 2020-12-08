using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Aoc2020.Lib.Day08
{
    public class Program
    {
        private Instruction[] instructions;

        public Program(Instruction[] instructions)
        {
            this.instructions = instructions;
        }

        public int Run(int overrideIndex, bool haltOnRepeat = true)
        {
            var index = 0;
            var accumulator = 0;
            var previousIndexes = new HashSet<int>();
            while (true)
            {
                if (index >= this.instructions.Length)
                {
                    return accumulator;
                }

                var instruction = this.instructions[index];
                if (haltOnRepeat && previousIndexes.Contains(index))
                {
                    return accumulator;
                }

                previousIndexes.Add(index);
                switch (instruction.Type)
                {
                    case InstructionType t when t == InstructionType.Jump && index == overrideIndex:
                        index++;
                        break;
                    case InstructionType t when t == InstructionType.NoOperation && index == overrideIndex:
                        index += GetNumber(instruction);
                        break;
                    case InstructionType.Jump:
                        index += GetNumber(instruction);
                        break;
                    case InstructionType.Accumulate:
                        accumulator += GetNumber(instruction);
                        index++;
                        break;
                    case InstructionType.NoOperation:
                        index++;
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
