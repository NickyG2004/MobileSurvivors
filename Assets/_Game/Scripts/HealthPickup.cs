using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class HealthPickup : Pickup
{
    [Header("General Settings")]
    [SerializeField] private int _healthAmount = 20;

    [Header("Sound Effect Settings")]
    [SerializeField] AudioClip _soundEffectClip = null;
    [Range(0, 1)]
    [SerializeField] private float _soundEffectVolume = 1f;

    protected override void Collect(PlayerCharacter playercharacter)
    {
        // Add health!
        if (debugMode)
        {
            Debug.Log("HealthPickup: Health collected by Player!");
        }

        // Note: could be optimized by saving health reference in PlayerCharacter class.
        // This is fine for now.

        if (playercharacter.TryGetComponent(out Health health))
        {
            health.IncreaseHealth(_healthAmount);
        }

        // Play sound effect
        AudioHelper.PlayClip2D(_soundEffectClip, _soundEffectVolume);
    }
}
