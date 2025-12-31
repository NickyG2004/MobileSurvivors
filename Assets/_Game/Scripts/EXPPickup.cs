using UnityEngine;

public class EXPPickup : Pickup
{
    [Header("General Settings")]
    [SerializeField] private float _expValue = 20;

    [Header("Sound Effect Settings")]
    [SerializeField] AudioClip _soundEffectClip = null;
    [Range(0, 1)]
    [SerializeField] private float _soundEffectVolume = 1f;

    protected override void Collect(PlayerCharacter playercharacter)
    {
        // Implement the logic for what happens when the player collects the item
        if (debugMode)
        {
            Debug.Log("EXPPickup: EXPPickup collected by Player!");
        }

        // Increase player's EXP
        playercharacter.IncreaseEXP(_expValue);

        // Play sound effect
        AudioHelper.PlayClip2D(_soundEffectClip, _soundEffectVolume);
    }
}
