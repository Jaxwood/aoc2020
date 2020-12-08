using Aoc2020.Lib.Util;
using System;
using System.Collections.Generic;

namespace Aoc2020.Lib.Day08
{
    public class Program
    {
        private Instruction[] instructions;

        public Program(Instruction[] instructions)
        {
            this.instructions = instructions;
        }

        public Result<int> Run(int overrideIndex, bool haltOnRepeat = true)
        {
            var index = 0;
            var accumulator = 0;
            var previousIndexes = new HashSet<int>();
            while (true)
            {
                if (index >= this.instructions.Length)
                {
                    return new Success<int>(accumulator);
                }

                var instruction = this.instructions[index];
                if (previousIndexes.Contains(index))
                {
                    if (!haltOnRepeat) return new Failure<int>();
                    return new Success<int>(accumulator);
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
