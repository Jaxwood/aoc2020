using Aoc2020.Lib.Day19.Contracts;
using Aoc2020.Lib.Day19.Rules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day19
{
    public class MessageValidator
    {
        private readonly IEnumerable<string> messages;
        private readonly IDictionary<int, Validatable[]> rules;

        public MessageValidator(Envelope envelope)
        {
            this.messages = envelope.Messages;
            this.rules = envelope.Rules;
        }

        public long Validate(int ruleNumber, int endPattern)
        {
            var validRules = 0L;
            var patterns = this.RulePatterns(endPattern);

            foreach (var message in this.messages)
            {
                var endsWith = patterns.Any(p => message.EndsWith(p));
                var rule = new NumberRule(ruleNumber);
                var valid = rule.Validate(new ValidationContext
                {
                    Position = 0,
                    Candidate = message,
                    Rules = this.rules,
                });

                validRules += endsWith && valid.Valid && valid.Position == message.Length ? 1 : 0;
            }

            return validRules;
        }

        private string[] RulePatterns(int ruleNumber)
        {
            var result = new List<string>();
            var stack = new Stack<(IEnumerable<Validatable>, string)>();
            stack.Push((this.rules[ruleNumber], ""));

            while (stack.Count > 0)
            {
                var (rules, acc) = stack.Pop();
                if (!rules.Any())
                {
                    result.Add(acc);
                    continue;
                }
                var current = rules.First();
                var others = rules.Skip(1);
                switch (current)
                {
                    case CharacterRule cr:
                        acc += cr.Character;
                        stack.Push((others, acc));
                        break;
                    case OrRule or:
                        stack.Push((or.Left.Concat(others), acc));
                        stack.Push((or.Right.Concat(others), acc));
                        break;
                    case NumberRule nr:
                        stack.Push((this.rules[nr.RuleNumber].Concat(others), acc));
                        break;
                    default:
                        throw new Exception("Unknown rule type");
                }
            }

            return result.ToArray();
        }
    }
}
