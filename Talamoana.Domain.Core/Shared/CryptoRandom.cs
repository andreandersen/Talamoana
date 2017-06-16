using System;
using System.Security.Cryptography;
using System.Threading;

namespace Talamoana.Domain.Core.Shared
{
    /// <summary>
    ///     Provides a cryptographically 'safe' randomization
    /// </summary>
    /// <remarks>
    ///     This instance is thread safe, and can be shared between threads.
    /// </remarks>
    public class CryptoRandom : IRandomizer, IDisposable
    {       
        private readonly ThreadLocal<RandomNumberGenerator> _rng;

        public CryptoRandom()
        {
            _rng = new ThreadLocal<RandomNumberGenerator>(RandomNumberGenerator.Create);
        }

        public int Next(int minInclusive, int maxInclusive) => 
            (int) (minInclusive + (maxInclusive + 1 - minInclusive) *
                  (RandomUint() / (double) uint.MaxValue));

        public double Next(double minInclusive, double maxInclusive) =>
            minInclusive + ((maxInclusive - minInclusive) * RandomDouble());

        private uint RandomUint()
        {
            var scale = uint.MaxValue;
            while (scale == uint.MaxValue)
            {
                var bytes = new byte[4];
                _rng.Value.GetBytes(bytes);
                scale = BitConverter.ToUInt32(bytes, 0);
            }
            return scale;
        }            

        private double RandomDouble()
        {
            byte[] result = new byte[8];
            _rng.Value.GetBytes(result);
            return (double) BitConverter.ToUInt64(result, 0) / ulong.MaxValue;

            //var bytes = new byte[8];
            //_rng.Value.GetBytes(bytes);

            //var v = BitConverter.ToUInt64(bytes, 0);
            //// We only use the 53-bits of integer precision available in a IEEE 754 64-bit double.
            //// The result is a fraction, 
            //// r = (0, 9007199254740991) / 9007199254740992 where 0 <= r && r < 1.
            //v &= ((1UL << 53) - 1);
            //var r = (double) v / (double) (1UL << 53);
            //return r;
        }

        /// <inheritdoc />
        public void Dispose() => _rng?.Dispose();
    }
}