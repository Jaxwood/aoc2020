using Aoc2020.Lib.Day08;
using Aoc2020.Lib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace Aoc2020.Tests.Day08
{
    public class Day08Tests
    {
        [Theory]
        [InlineData("Day08/Example1.txt", 5)]
        [InlineData("Day08/Input.txt", 2014)]
        public void Part1(string filepath, int expected)
        {
            var parser = new Parser(filepath);
            var instructions = parser.Parse(new InstructionFactory());
            var sut = new Program(instructions.ToArray());
            var actual = sut.Run();
            Assert.Equal(expected, actual);
        }
    }

    internal class InstructionFactory : IParseFactory<Instruction>
    {
        public Instruction Create(Line line)
        {
            foreach (Match m in Regex.Matches(line.Raw, @"(jmp|nop|acc)\s([+|-])(\d+)"))
            {
                var instruction = m.Groups[1];
                var sign = m.Groups[2];
                var num = m.Groups[3];

                return new Instruction()
                {
                    Type = ParseInstructionType(instruction.Value),
                    Sign = ParseSign(sign.Value),
                    Number = Convert.ToInt32(num.Value),
                };
            }

            return null;
        }

        private InstructionType ParseInstructionType(string value)
        {
            switch (value)
            {
                case "jmp":
                    return InstructionType.Jump;
                case "nop":
                    return InstructionType.NoOperation;
                case "acc":
                    return InstructionType.Accumulate;
                default:
                    throw new Exception($"Unknown instruction: {value}");
            }
        }

        private Sign ParseSign(string value)
        {
            switch (value)
            {
                case "+":
                    return Sign.Positive;
                case "-":
                    return Sign.Negative;
                default:
                    throw new Exception($"Unknown sign: {value}");
            }
        }
    }
}
