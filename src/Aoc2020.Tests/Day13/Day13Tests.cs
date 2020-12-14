using Aoc2020.Lib.Day13;
using Xunit;

namespace Aoc2020.Tests.Day13
{
    public class Day13Tests
    {
        [Theory]
        [InlineData("7,13,x,x,59,x,31,19", 939, 295)]
        [InlineData("23,x,x,x,x,x,x,x,x,x,x,x,x,41,x,x,x,37,x,x,x,x,x,479,x,x,x,x,x,x,x,x,x,x,x,x,13,x,x,x,17,x,x,x,x,x,x,x,x,x,x,x,29,x,373,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,19", 1000417, 171)]
        public void Part1(string schedule, int departureTime, int expected)
        {
            var sut = new BusScheduler(schedule);
            Assert.Equal(expected, sut.Schedule(departureTime));
        }

        [Theory]
        [InlineData("17,x,13,19", 3417)]
        [InlineData("67,7,59,61", 754018)]
        [InlineData("67,x,7,59,61", 779210)]
        [InlineData("67,7,x,59,61", 1261476)]
        [InlineData("1789,37,47,1889", 1202161486)]
        [InlineData("23,x,x,x,x,x,x,x,x,x,x,x,x,41,x,x,x,37,x,x,x,x,x,479,x,x,x,x,x,x,x,x,x,x,x,x,13,x,x,x,17,x,x,x,x,x,x,x,x,x,x,x,29,x,373,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,19", 539746751134958)]
        public void Part2(string schedule, long expected)
        {
            var sut = new BusScheduler(schedule);
            Assert.Equal(expected, sut.ScheduleByOffset());
        }
    }
}
