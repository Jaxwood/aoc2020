using Aoc2020.Lib.Day19.Rules;
using Aoc2020.Lib.Util;
using System.Collections.Generic;

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

        public long Validate(int ruleNumber)
        {
            var validRules = 0L;

            foreach (var message in this.messages)
            {
                var rule = new NumberRule(ruleNumber);
                var valid = rule.Validate(new ValidationContext
                {
                    Position = 0,
                    Candidate = message,
                    Rules = this.rules,
                });

                validRules += valid.Valid && valid.Position >= message.Length ? 1 : 0;
            }

            return validRules;
        }
    }
}
