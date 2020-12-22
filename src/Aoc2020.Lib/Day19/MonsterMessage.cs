using System.Collections.Generic;

namespace Aoc2020.Lib.Day19
{
    public record MonsterMessage(IDictionary<int, Validatable[]> Rules, IEnumerable<string> Messages);
}
