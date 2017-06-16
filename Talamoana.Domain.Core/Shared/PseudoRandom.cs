using System;
using System.Diagnostics;

namespace Talamoana.Domain.Core.Shared
{
    public class PseudoRandom : IRandomizer
    {
        private readonly Random _rnd = new Random();

        /// <inheritdoc />
        public int Next(int minInclusive, int maxInclusive)
        {
            var r = _rnd.Next(minInclusive, maxInclusive + 1);
            //Debug.WriteLine($"{minInclusive,5}-{maxInclusive,-5} - {r}");

            return r;
        }
            
    }
}