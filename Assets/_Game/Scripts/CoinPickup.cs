using UnityEngine;

public class CoinPickup : Pickup
{
    [Header("General Settings")]
    [SerializeField] private int _coinAmount = 1;

    [Header("Sound Effect Settings")]
    [SerializeField] AudioClip _soundEffectClip = null;
    [Range(0,1)]
    [SerializeField] private float _soundEffectVolume = 1f;

    protected override void Collect(PlayerCharacter playercharacter)
    {
        // Add coins!
        if (debugMode)
        {
            Debug.Log("CoinPickup: Coins collected by Player!");
        }
        // Note: could be optimized by saving coin reference in PlayerCharacter class.
        // This is fine for now.

        if (playercharacter.TryGetComponent(out Coins coins))
        {
            coins.IncreaseCoins(_coinAmount);
        }

        // Play sound effect
        AudioHelper.PlayClip2D(_soundEffectClip, _soundEffectVolume);

        // TODO Integrate with object pool.
        //AudioSource audioSource = AudioHelper.PlayClip2D(_soundEffectClip, _soundEffectVolume);
        //audioSource.pitch = UnityEngine.Random.Range(0.8f, 1f);

        // play parical effect

    }
}
