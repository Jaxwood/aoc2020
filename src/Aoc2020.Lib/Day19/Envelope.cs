using System.Collections.Generic;

namespace Aoc2020.Lib.Day19
{
    public record Envelope(IDictionary<int, Validatable[]> Rules, IEnumerable<string> Messages);
}
