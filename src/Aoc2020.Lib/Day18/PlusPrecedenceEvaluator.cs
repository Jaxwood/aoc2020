using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day18
{
    public class PlusPrecedenceEvaluator : Evaluable
    {
        private readonly Evaluable evaluator;

        public PlusPrecedenceEvaluator(Evaluable evaluator)
        {
            this.evaluator = evaluator;
        }

        public long EvaluateExpression(Expression[] expressions)
        {
            var queue = new Queue<Expression[]>();
            queue.Enqueue(expressions);
            var exp = new Expression[0];

            while (queue.Count > 0)
            {
                exp = queue.Dequeue();
                if (this.HasAddToken(exp))
                {
                    var idx = Array.FindIndex(exp, c => c.Token == Token.Add) - 1;
                    var toEval = exp.Skip(idx).Take(3).ToArray();
                    var sum = this.evaluator.EvaluateExpression(toEval);
                    queue.Enqueue(
                        exp.Take(idx)
                           .Concat(new[] { new Expression(Token.Number, sum) })
                           .Concat(exp.Skip(idx + 3))
                           .ToArray());
                }
            }

            return this.evaluator.EvaluateExpression(exp);
        }

        private bool HasAddToken(Expression[] exp)
        {
            return exp.Any(c => c.Token == Token.Add);
        }
    }
}
