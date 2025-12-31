using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    // OnHealthChanged(currentHealth, maxHealth)
    public event Action<int, int> OnHealthChanged = delegate { };

    public bool debugMode = true;

    [SerializeField] private int _currentHealth = 100;
    [SerializeField] private int _healthMax = 100;

    [Header("Sound Effect Settings")]
    [SerializeField] AudioClip _hitSound = null;
    [Range(0, 1)]
    [SerializeField] private float _hitSoundVolume = 1f;

    [SerializeField] AudioClip _deathSound = null;
    [Range(0, 1)]
    [SerializeField] private float _deathSoundVolume = 1f;

    public UnityEvent OnKilled;

    public void IncreaseHealth(int amount)
    {
        if (debugMode)
        {
            Debug.Log("Health: Increasing health -> " + amount);
        }
        _currentHealth += amount;

        // Make sure to stay within max health range (min, max)
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _healthMax);

        if (debugMode)
        {
            Debug.Log("Health: Current health after increase -> " + _currentHealth);
        }

        // Notify listeners of health change
        OnHealthChanged?.Invoke(_currentHealth, _healthMax);

        // Optional: You could add a check here to see if health is above max and adjust accordingly
        //if (_currentHealth > _healthMax)
        //{
        //    _currentHealth = _healthMax;
        //}
    }

    public void TakeDamage(int amount)
    {
        if (debugMode)
        {
            Debug.Log("Health: Taking damage -> " + amount);
        }

        _currentHealth -= amount;

        // play hit sound effect
        AudioHelper.PlayClip2D(_hitSound, _hitSoundVolume);

        // check if dead
        if (_currentHealth <= 0)
        {
            if (debugMode)
            {
                Debug.Log("Health: Died");
            }
            Kill();
        }

        OnHealthChanged?.Invoke(_currentHealth, _healthMax);
    }

    public void IncreaseMaxHealth(int amount)
    {
        _healthMax += amount;
        _currentHealth += amount;

        if (debugMode)
        {
            Debug.Log("Health: Increasing max health -> " + amount);
            Debug.Log("Health: Current Max Health" + _healthMax);
        }

        // Optional: You could add a check here to see if current health is above new max and adjust accordingly
        //if (_currentHealth > _healthMax)
        //{
        //_currentHealth = _healthMax;
        //}

        // Notify listeners of health change
        OnHealthChanged?.Invoke(_currentHealth, _healthMax);
    }

    public void Kill()
    {
        // play death sound effect
        AudioHelper.PlayClip2D(_deathSound, _deathSoundVolume);

        OnKilled?.Invoke();
        Destroy(gameObject);
    }

}
