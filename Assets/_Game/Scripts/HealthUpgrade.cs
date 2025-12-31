using UnityEngine;

public class HealthUpgrade : IUpgrade
{
    [Header("Debug Settings")]
    public bool isDebug = false;

    // [Header("General Settings")]
    private int _maxHealthIncreaseAmount = 100;

    public void Upgrade(PlayerCharacter playerCharacter)
    {
        // Upgrade max health!
        if (playerCharacter.TryGetComponent(out Health health))
        {
            health.IncreaseMaxHealth(_maxHealthIncreaseAmount);
        }
    }
}
