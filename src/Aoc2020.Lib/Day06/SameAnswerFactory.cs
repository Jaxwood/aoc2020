using Aoc2020.Lib.Util;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day06
{
    public class SameAnswerFactory : IParseFactory<HashSet<char>>
    {
        private List<string> answers;

        public HashSet<char> Create(Line line)
        {
            if (answers == null)
            {
                answers = new List<string>();
            }

            if (string.IsNullOrEmpty(line.Raw))
            {
                var same = GroupAnswers();
                this.answers = null;
                return same;
            }

            answers.Add(line.Raw);
            
            if (line.LastLine)
            {
                return GroupAnswers();
            }

            return null;
        }

        private HashSet<char> GroupAnswers()
        {
            return this.answers
                .Select(c => new HashSet<char>(c))
                .Aggregate((acc, next) =>
                   {
                       acc.IntersectWith(new HashSet<char>(next));
                       return acc;
                   });
        }
    }
}
