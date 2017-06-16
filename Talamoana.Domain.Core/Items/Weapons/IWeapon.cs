namespace Talamoana.Domain.Core.Items.Weapons
{
    public interface IWeapon
    {
        string FriendlyName { get; }
        WeaponType WeaponType { get; }
        bool IsTwoHanded { get; }
        bool IsRanged { get; }
        WeaponDamage BaseDamage { get; }
        WeaponDamage CalculatedDamage { get; }
    }
}
