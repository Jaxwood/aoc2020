using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day18
{
    public class ExpressionEvaluator : Evaluable
    {
        private readonly Evaluable evaluator;

        public ExpressionEvaluator(Evaluable evaluator)
        {
            this.evaluator = evaluator;
        }

        public long Evaluate(Expression[] expressions)
        {
            var queue = new Queue<Expression[]>();
            queue.Enqueue(expressions);
            var exp = Array.Empty<Expression>();

            while (queue.Count > 0)
            {
                exp = queue.Dequeue();
                if (this.HasSubExpression(exp))
                {
                    var (from, to) = this.ExtractSubExpression(exp);
                    var subExpression = exp.Skip(from + 1).Take(to - from - 1).ToArray();
                    var num = this.evaluator.Evaluate(subExpression);

                    queue.Enqueue(exp.Take(from)
                                     .Concat(new[] { new Expression(Token.Number, num) })
                                     .Concat(exp.Skip(to + 1))
                                     .ToArray());
                }
            }

            return this.evaluator.Evaluate(exp.ToArray());
        }

        private (int, int) ExtractSubExpression(Expression[] exp)
        {
            int from = 0; int to = 0;
            for (int i = 0; i < exp.Length; i++)
            {
                var token = exp[i].Token;
                if (token == Token.Open)
                {
                    from = i;
                }
                if (token == Token.Close)
                {
                    to = i;
                    break;
                }
            }
            return (from, to);
        }

        private bool HasSubExpression(IEnumerable<Expression> candidate)
        {
            return candidate.Any(exp => exp.Token == Token.Open || exp.Token == Token.Close);
        }
    }
}
