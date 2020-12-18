using System;
using System.Linq;

namespace Aoc2020.Lib.Day18
{
    public class LeftPrecedenceEvaluator : Evaluable
    {
        public long EvaluateExpression(Expression[] expressions)
        {
            var result = 0L;

            // number only
            if (expressions.Length == 1)
            {
                return expressions.First().Value;
            }

            for (int i = 1; i < expressions.Count(); i += 2)
            {
                switch (expressions[i].Token)
                {
                    case Token.Add:
                        result = (i == 1 ? expressions[i - 1].Value : result) + expressions[i + 1].Value;
                        break;
                    case Token.Multiply:
                        result = (i == 1 ? expressions[i - 1].Value : result) * expressions[i + 1].Value;
                        break;
                    case Token.Number:
                    case Token.Open:
                    case Token.Close:
                    default:
                        throw new Exception($"Unexpected token {expressions[i].Token}");
                }
            }

            return result;
        }
    }
}
