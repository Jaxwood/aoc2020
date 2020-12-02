using System.Linq;

namespace Aoc2020.Lib.Day02
{
    public class PartOnePolicy : Policy
    {
        public PartOnePolicy() { }
        public PartOnePolicy(int min, int max, char letter, string password)
            :base(min, max, letter, password) { }
    
        public override bool IsValid()
        {
            var accepted = this.Password
                               .ToCharArray()
                               .Where(c => c == this.Letter)
                               .Count();
            return accepted >= this.Min && accepted <= this.Max;
        }
    }
}
