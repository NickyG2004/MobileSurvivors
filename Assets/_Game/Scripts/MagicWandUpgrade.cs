using UnityEngine;

public class MagicWandUpgrade : IUpgrade
{
    private string _magicWandName = "MagicWand";
    private int _damageIncreaseValue = 5;
    private WeaponData _weaponData;

    public MagicWandUpgrade(WeaponData weaponData)
    {
        _weaponData = weaponData;
    }

    public void Upgrade(PlayerCharacter playerCharacter)
    {
        // Search for WeaponSystem component (MagicWand)
        WeaponSystem[] weaponSystems = playerCharacter.GetComponents<WeaponSystem>();
        foreach (WeaponSystem weaponSystem in weaponSystems)
        {
            // Check if the weapon is a MagicWand
            if (weaponSystem.Name == _magicWandName)
            {
                // Upgrade the weapon's damage
                weaponSystem.IncreaseDamage(_damageIncreaseValue);
                return;

                // If so, upgrade it
            }
        }

        // If not found, add one
        playerCharacter.CreateWeapon(_weaponData);
    }
}
