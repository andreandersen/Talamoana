namespace Talamoana.Domain.Core.Items.Base
{
    public interface IWeaponStats
    {
        decimal AttacksPerSecond { get; }
        decimal CritChance { get; }
        int DamageMax { get; }
        int DamageMin { get; }
        int Range { get; }
    }
}