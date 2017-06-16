namespace Talamoana.Domain.Core.Modifiers
{
    /// <summary>
    ///     Used to represent spawn weight for calculating spawn probability
    /// </summary>
    public class TagWeight
    {
        public TagWeight(string tag, int weight)
        {
            TagId = tag;
            Weight = weight;
        }

        public string TagId { get; }
        public int Weight { get; }
    }
}