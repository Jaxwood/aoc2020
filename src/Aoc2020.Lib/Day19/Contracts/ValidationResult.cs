﻿namespace Aoc2020.Lib.Day19.Contracts
{
    public record ValidationResult
    {
        public bool Valid { get; init; }

        public int Position { get; init; }
    }
}
