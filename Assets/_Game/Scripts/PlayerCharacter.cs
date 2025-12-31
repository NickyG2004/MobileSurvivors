using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System;

public class PlayerCharacter : MonoBehaviour
{
    public event Action OnLevelUp = delegate { };

    // OnEXPGained(currentEXP, EXPForNextLevelUp)
    public event Action<float, float> OnEXPGained = delegate { };

    [Header("Debug Settings")]
    public bool debugMode = true;

    [Header("Weapon Settings")]
    [SerializeField] private WeaponData _startingWeaponData;

    [Header("Sound Effect Settings")]
    [SerializeField] AudioClip _levelUpSound = null;
    [UnityEngine.Range(0, 1)]
    [SerializeField] private float _levelUpSoundVolume = 1f;

    // We are going to set Exp to zero everytime we level up
    public float EXP { get; private set; } = 0;
    public int LVL { get; private set; } = 1;

    private float _expForNextLevelUp = 40f;

    // tracking Total EXP (in case we need it later)
    public float _expTotal { get; private set; } = 0;

    private GameController _gameController;

    private void Awake()
    {
        _gameController = FindAnyObjectByType<GameController>();
    }

    private void Start()
    {
        CreateWeapon(_startingWeaponData);
    }

    public void CreateWeapon(WeaponData weaponData)
    {
        if (debugMode)
        {
            // Debug.Log("PlayerCharacter: Creating weapon with data: " + weaponData.ToString());
        }
        // Here we would instantiate the weapon based on the weaponData

        // add WeaponSystem component to this gameobject
        WeaponSystem newWeaponSystem = gameObject.AddComponent<WeaponSystem>();

        // customize weapon system with weapon data
        newWeaponSystem.SetupWeapon(weaponData);
    }

    public void IncreaseEXP(float amount)
    {
        if (debugMode)
        {
            // Debug.Log("PlayerCharacter: Increasing EXP by " + amount);
        }
        // Increase the current EXP
        EXP += amount;
        OnEXPGained?.Invoke(EXP, _expForNextLevelUp);

        // If we have enough EXP to level up, Ding
        if (EXP >= _expForNextLevelUp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        // increse level
        LVL++;

        if (debugMode)
        {
            // Debug.Log("PlayerCharacter: Leveling up to level " + LVL);
        }

        // store overflow exp into next level
        float overflowExp = EXP - _expForNextLevelUp;

        // store current EXP into total EXP, then reset current EXP
        _expTotal += _expForNextLevelUp;

        // wipe exp to zero, add overflow
        EXP = overflowExp;

        // increase exp for next level up
        // Example: Increase by 50% for next level
        _expForNextLevelUp *= 1.5f;

        OnEXPGained?.Invoke(EXP, _expForNextLevelUp);
        OnLevelUp?.Invoke();

        // play sound effect
        AudioHelper.PlayClip2D(_levelUpSound, _levelUpSoundVolume);

        GetUpgrades();

    }

    private void GetUpgrades()
    {
        // Here we would call the GameController to get possible upgrades
        // For now, we will just log that we are getting upgrades

        // Note if we wanted to add a menu this is where we would do it!

        if (debugMode)
        {
            // Debug.Log("PlayerCharacter: Getting possible upgrades for level " + LVL);
        }

        // get a few options from our Upgrade pool
        List<IUpgrade> upgradeChoices = _gameController.GetUpgradeChoices(4);
        foreach (IUpgrade upgrade in upgradeChoices)
        {
            if (debugMode)
            {
                // Debug.Log("PlayerCharacter: Upgrade available: " + upgrade.ToString());
            }
        }

        // for simplicity choose one of the options and upgrade automatically
        // in an actual, larger game, we would present these options to the player
        int randomIndex = UnityEngine.Random.Range(0, upgradeChoices.Count);
        IUpgrade newUpgrade = upgradeChoices[randomIndex];

        if (debugMode)
        {
            Debug.Log("PlayerCharacter: Applying upgrade: " + newUpgrade.ToString());
        }

        // Apply the upgrade to the player character
        newUpgrade.Upgrade(this);
    }
}
