namespace Aoc2020.Lib.Day08
{
    public record Instruction
    {
        public InstructionType Type { get; set; }

        public Sign Sign { get; set;  }

        public int Number { get; set; }
    }
}
