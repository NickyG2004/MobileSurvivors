using UnityEngine;

public class Coins : MonoBehaviour
{
    public bool debugMode = true;

    [SerializeField] private int _currentCoins = 0;
    [SerializeField] private int _maxCoins = 20;

    public void IncreaseCoins(int amount)
    {
        if (debugMode)
        {
            Debug.Log("Coins: Increasing coins -> " + amount);
        }
        _currentCoins += amount;

        // Make sure to stay within max health range (min, max)
        _currentCoins = Mathf.Clamp(_currentCoins, 0, _maxCoins);

        if (debugMode)
        {
            Debug.Log("Coins: Current coins after increase -> " + _currentCoins);
        }

        // Optional: You could add a check here to see if health is above max and adjust accordingly
        //if (_currentHealth > _healthMax)
        //{
        //    _currentHealth = _healthMax;
        //}
    }

    public void DecreaseCoins(int amount)
    {
        if (debugMode)
        {
            Debug.Log("Coins: Decreasing coins -> " + amount);
        }
        _currentCoins -= amount;
        // Make sure to stay within min range
        _currentCoins = Mathf.Max(_currentCoins, 0);
        if (debugMode)
        {
            Debug.Log("Coins: Current coins after decrease -> " + _currentCoins);
        }
    }

    public int GetCurrentCoins()
    {
        return _currentCoins;
    }
}
