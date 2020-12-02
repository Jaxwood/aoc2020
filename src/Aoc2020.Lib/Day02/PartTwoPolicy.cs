namespace Aoc2020.Lib.Day02
{
    public class PartTwoPolicy : Policy
    {
        public PartTwoPolicy() { }
        public PartTwoPolicy(int min, int max, char letter, string password)
            :base(min, max, letter, password) { }
        
        public override bool IsValid()
        {
            return this.Password[this.Min - 1] == this.Letter ^ this.Password[this.Max - 1] == this.Letter;
        }
    }
}
