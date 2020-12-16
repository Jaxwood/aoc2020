using System;
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
            return this.book.Others.SelectMany(id => id)
                            .Where(id => !IsValid(id))
                            .Sum();
        }

        private bool IsValid(int candidate)
        {
            foreach (var rule in this.book.Rules.Values.SelectMany(rule => rule))
            {
                if (candidate >= rule.Start.Value && candidate <= rule.End.Value)
                {
                    return true;
                }
            }

            return false;
        }
    }
}