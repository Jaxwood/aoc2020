using System.IO;
using System.Collections.Generic;

namespace Aoc2020.Lib.Util
{
    public class Parser
    {
        private readonly string path;

        public Parser(string path)
        {
            this.path = path;
        }

        public IEnumerable<T> Parse<T>(IParseFactory<T> factory)
        {
            var result = new List<T>();
            var file = new StreamReader(this.path);
            string line;
            int lineNum = 0;
            while ((line = file.ReadLine()) != null)
            {
                result.Add(factory.Create(new Line(line, lineNum++)));
            }
            file.Close();

            return result;
        }
    }
}
