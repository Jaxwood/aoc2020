using Aoc2020.Lib.Day25;
using Xunit;

namespace Aoc2020.Tests.Day25
{
    public class Day25Tests
    {
        [Theory]
        [InlineData(5764801, 17807724, 14897079)]
        [InlineData(17807724, 5764801, 14897079)]
        [InlineData(10441485, 1004920, 17032383)]
        [InlineData(1004920, 10441485, 17032383)]
        public void Part1(long cardkey, long doorkey, long expected)
        {
            var sut = new RoomKey();
            var actual = sut.EncryptionKey(cardkey, doorkey);
            Assert.Equal(expected, actual);
        }
    }
}
