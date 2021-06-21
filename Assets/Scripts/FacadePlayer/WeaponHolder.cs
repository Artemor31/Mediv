public abstract class WeaponHolder
{
    public abstract IMeleeWeapon MeleeWeapon { get; set; }
    public abstract IRangeWeapon RangeWeapon { get; set; }
}