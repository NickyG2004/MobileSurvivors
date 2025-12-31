using UnityEngine;

public class ElectroBallUpgrade : IUpgrade
{
    private string _electroBallName = "ElectroBall";
    private float _cooldownReductionValue = .2f;
    private WeaponData _weaponData;

    public ElectroBallUpgrade(WeaponData weaponData)
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
            if (weaponSystem.Name == _electroBallName)
            {
                // Upgrade the weapon's damage
                weaponSystem.DecreaseCooldown(_cooldownReductionValue);
                return;

                // If so, upgrade it
            }
        }

        // If not found, add one
        playerCharacter.CreateWeapon(_weaponData);
    }
}
