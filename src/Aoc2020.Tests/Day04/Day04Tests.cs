using Aoc2020.Lib.Day04;
using Aoc2020.Lib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aoc2020.Tests.Day04
{
    public class Day04Tests
    {
        [Theory]
        [InlineData("Day04/Example1.txt", 2)]
        [InlineData("Day04/Input.txt", 264)]
        public void Part1(string filepath, int expected)
        {
            var parser = new Parser(filepath);
            var factory = new PassportFactory();
            parser.Parse(factory);
            var sut = new PassportValidator(factory.Passports);
            var actual = sut.Validate();
            Assert.Equal(expected, actual);
        }
    }

    internal class PassportFactory : IParseFactory<Passport>
    {
        public List<Passport> Passports { get; private set; }
        private Passport passport;

        public PassportFactory()
        {
            this.Passports = new List<Passport>();
        }
        public Passport Create(Line line)
        {
            if (this.passport == null)
            {
                this.passport = new Passport();
            }

            if (string.IsNullOrEmpty(line.Raw))
            {
                this.Passports.Add(this.passport);
                this.passport = new Passport();
                return null;
            }

            var segments = line.Raw.Split(' ');
            foreach(var segment in segments)
            {
                UpdatePassport(segment.Split(':'));
            }

            return null;
        }

        private void UpdatePassport(string[] segments)
        {
            var identifier = segments[0];
            var value = segments[1];

            switch (segments[0])
            {
                case "byr":
                    this.passport.BirthYear = Convert.ToInt32(value);
                    break;
                case "iyr":
                    this.passport.IssueYear = Convert.ToInt32(value);
                    break;
                case "eyr":
                    this.passport.ExperationYear = Convert.ToInt32(value);
                    break;
                case "hgt":
                    this.passport.Height = value;
                    break;
                case "hcl":
                    this.passport.HairColor = value;
                    break;
                case "ecl":
                    this.passport.EyeColor = value;
                    break;
                case "pid":
                    this.passport.PassportId = value;
                    break;
                case "cid":
                    this.passport.CountryId = Convert.ToInt32(value);
                    break;
                default:
                    throw new Exception($"unknown identifier {identifier}");
                    break;
            }
        }
    }
}
