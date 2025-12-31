using UnityEngine;

public class CooldownUpgrade : IUpgrade
{
    [Header("Debug Settings")]
    public bool isDebug = false;

    // [Header("General Settings")]
    private float _cooldownReductionAmount = 0.2f;

    public void Upgrade(PlayerCharacter playerCharacter)
    {
        WeaponSystem[] weaponSystems = playerCharacter.GetComponents<WeaponSystem>();

        if (weaponSystems != null && weaponSystems.Length > 0)
        {
            foreach (WeaponSystem weapon in weaponSystems)
            {
                weapon.DecreaseCooldown(_cooldownReductionAmount);

                if (isDebug)
                {
                    Debug.Log("CooldownUpgrade: Cooldown reduced by " + _cooldownReductionAmount);
                }
            }
        }
    }
}
