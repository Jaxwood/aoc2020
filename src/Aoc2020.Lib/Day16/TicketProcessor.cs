using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day16
{
    public class TicketProcessor
    {
        private readonly TicketBook book;

        public TicketProcessor(TicketBook book)
        {
            this.book = book;
        }

        public long Departure()
        {
            var validTickets = this.book.Others.Where(ticket => IsValid(ticket));

            var rows = this.Transpose(validTickets).ToList();
            var possibleFields = new Dictionary<int, HashSet<string>>();
            for (int i = 0; i < rows.Count; i++)
            {
                possibleFields[i] = new HashSet<string>();
                foreach (var (k, v) in this.book.Rules)
                {
                    if (rows[i].All(r => MatchRules(r, v)))
                    {
                        var set = possibleFields[i];
                        set.Add(k);
                        possibleFields[i] = set;
                    }
                }
            }

            var result = new Dictionary<int, string>();
            var confirmedFields = new HashSet<string>();
            foreach (var (k,v) in possibleFields.OrderBy(c => c.Value.Count()))
            {
                v.ExceptWith(confirmedFields);
                confirmedFields.Add(v.First());
                result[k] = v.First();
            }

            return result
                .Where(kv => kv.Value.StartsWith("departure"))
                .Aggregate(1L, (acc, n) => acc * this.book.My[n.Key]);
        }

        public long InvalidTickets()
        {
            return this.book.Others
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
            return this.book.Rules.Any(rule => this.MatchRules(ticket, rule.Value));
        }

        private bool MatchRules(int ticket, Range[] rules)
        {
            return rules.Any(rule => this.WithinRange(ticket, rule));
        }

        private bool WithinRange(int ticket, Range range)
        {
            return ticket >= range.Start.Value && ticket <= range.End.Value;
        }

        private IEnumerable<IList<int>> Transpose(IEnumerable<IList<int>> candidate)
        {
            var len = candidate.First().Count;
            for (int i = 0; i < len; i++)
            {
                var row = new List<int>();
                foreach (var inner in candidate)
                {
                    row.Add(inner[i]);
                }
                yield return row;
            }
        }
    }
}