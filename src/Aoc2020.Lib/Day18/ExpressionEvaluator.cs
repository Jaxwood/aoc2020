using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day18
{
    public class ExpressionEvaluator
    {
        private readonly IEnumerable<IEnumerable<Expression>> expressions;
        private readonly Evaluable evaluator;

        public ExpressionEvaluator(
            IEnumerable<IEnumerable<Expression>> expressions,
            Evaluable evaluator)
        {
            this.expressions = expressions;
            this.evaluator = evaluator;
        }

        public long Evaluate()
        {
            var sum = 0L;
            foreach (var expression in this.expressions)
            {
                var queue = new Queue<Expression[]>();
                queue.Enqueue(expression.ToArray());

                while (queue.Count > 0)
                {
                    var exp = queue.Dequeue();
                    if (this.HasSubExpression(exp))
                    {
                        var (from, to) = this.ExtractSubExpression(exp);
                        var subExpression = exp.Skip(from + 1).Take(to - from - 1).ToArray();
                        var num = this.evaluator.EvaluateExpression(subExpression);

                        queue.Enqueue(exp.Take(from)
                                         .Concat(new[] { new Expression(Token.Number, num) })
                                         .Concat(exp.Skip(to + 1))
                                         .ToArray());
                    }
                    else
                    {
                        sum += this.evaluator.EvaluateExpression(exp.ToArray());
                    }
                }
            }

            return sum;
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
