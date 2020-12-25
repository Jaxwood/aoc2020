using System;

namespace Aoc2020.Lib.Day25
{
    public class RoomKey
    {
        public KeyMetadata Handshake(long subjectNumber, Predicate<KeyMetadata> condition)
        {
            var loopSize = 1L;
            var value = 1L;
            while (true)
            {
                value *= subjectNumber;
                value %= 20201227;
                var key = new KeyMetadata(value, loopSize);
                if (condition(key))
                {
                    return key;
                }
                loopSize++;
            }
        }

        public long EncryptionKey(long cardkey, long doorkey)
        {
            var secret = this.Handshake(7L, (key) => key.Key == cardkey);
            var encryptionKey = this.Handshake(doorkey, (key) => key.Secret == secret.Secret);
            return encryptionKey.Key;
        }
    }

    public record KeyMetadata(long Key, long Secret);
}
