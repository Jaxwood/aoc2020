using System;
using System.Collections.Generic;

namespace Aoc2020.Lib.Day16
{
    public record TicketBook(IDictionary<string, Range[]> Rules, IList<int> My, IEnumerable<IList<int>> Others);
}
