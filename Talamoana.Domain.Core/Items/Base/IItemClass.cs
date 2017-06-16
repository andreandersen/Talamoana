namespace Talamoana.Domain.Core.Items.Base
{
    public interface IItemClass
    {
        string Category { get; }
        string Id { get; }
        string Name { get; }
    }
}