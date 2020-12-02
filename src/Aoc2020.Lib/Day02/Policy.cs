namespace Aoc2020.Lib.Day02
{
    public class Policy
    {
        public Policy() { }
        public Policy(int min, int max, char letter, string password)
        {
            Min = min;
            Max = max;
            Letter = letter;
            Password = password;
        }

        protected int Min { get; }
        protected int Max { get; }
        protected char Letter { get; }
        protected string Password { get; }

        public virtual bool IsValid()
        {
            return false;
        }
    }
}
