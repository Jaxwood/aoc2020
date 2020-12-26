using System;
using System.Collections.Generic;

namespace Aoc2020.Lib.Day19.Contracts
{
    public record ValidationContext
    {
        public string Candidate { get; init; }

        public int Position { get; init; }

        public IDictionary<int, Validatable[]> Rules { get; init; }

        public bool OutOfBounds()
        {
            return this.Position >= this.Candidate.Length;
        }
    }
}
