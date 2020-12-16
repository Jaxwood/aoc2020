using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day16
{
    public class TicketProcessor
    {
        private TicketBook book;

        public TicketProcessor(TicketBook book)
        {
            this.book = book;
        }

        public long InvalidTickets()
        {
            return this.book.Others
                        .Where(id => !IsValid(id))
                        .SelectMany(t => t)
                        .Where(t => !MatchRule(t))
                        .Sum();
        }

        private bool IsValid(IEnumerable<int> tickets)
        {
            return tickets.All(t => MatchRule(t));
        }

        private bool MatchRule(int ticket)
        {
            return this.book.Rules.Values.SelectMany(id => id)
                .Any(rule => this.WithinRange(ticket, rule));
        }

        private bool WithinRange(int ticket, Range range)
        {
            return ticket >= range.Start.Value && ticket <= range.End.Value;
        }
    }
}