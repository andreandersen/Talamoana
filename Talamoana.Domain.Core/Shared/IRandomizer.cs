namespace Talamoana.Domain.Core.Shared
{
    public interface IRandomizer
    {
        int Next(int minInclusive, int maxInclusive);
    }
}
