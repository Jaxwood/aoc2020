using System;
using System.Collections.Generic;

namespace Aoc2020.Lib.Day16
{
    public record TicketBook(IDictionary<string, Range[]> Rules, IEnumerable<int> My, IEnumerable<IEnumerable<int>> Others);
}
