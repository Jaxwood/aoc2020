using Aoc2020.Lib.Day18;
using Aoc2020.Lib.Util;
using System;
using System.Collections.Generic;
using Xunit;

namespace Aoc2020.Tests.Day18
{
    public class Day18Tests
    {
        [Theory]
        [InlineData("Day18/Example1.txt", 26406)]
        [InlineData("Day18/Input.txt", 11076907812171)]
        public void Part1(string filename, long expected)
        {
            var parser = new Parser(filename);
            var expressions = parser.Parse(new ExpressionFactory());
            var sut = new ExpressionEvaluator(expressions, new LeftPrecedenceEvaluator());
            var actual = sut.Evaluate();
            Assert.Equal(expected, actual);
        }
    }

    internal class ExpressionFactory : IParseFactory<IEnumerable<Expression>>
    {
        public IEnumerable<Expression> Create(Line line)
        {
            var result = new List<Expression>();
            var segments = line.Raw.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (var segment in segments)
            {
                foreach (var c in segment)
                {
                    switch (c)
                    {
                        case '(':
                            result.Add(new Expression(Token.Open));
                            break;
                        case ')':
                            result.Add(new Expression(Token.Close));
                            break;
                        case '+':
                            result.Add(new Expression(Token.Add));
                            break;
                        case '*':
                            result.Add(new Expression(Token.Multiply));
                            break;
                        default:
                            result.Add(new Expression(Token.Number, Convert.ToInt32(c.ToString())));
                            break;
                    }
                }
            }
            return result;
        }
    }
}
