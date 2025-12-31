using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData_", menuName = "ScriptableObjects/WeaponData")]

public class WeaponData : ScriptableObject
{
    [Header("Debug Settings")]
    public bool debugMode = true;

    [Header("General Settings")]
    [SerializeField] string _name = "Default";
    [SerializeField] int _damage = 1;
    [SerializeField] float _cooldown = 1;

    [Header("Sound Effect Settings")]
    [SerializeField] AudioClip _weaponFireSoundEffect = null;
    [Range(0, 1)]
    [SerializeField] private float _weaponFireSoundEffectVolume = 1f;

    [Header("Detection Settings")]
    [SerializeField] private bool _onlyFireIfNearbyTargets = false;
    [UnityEngine.Range(1, 20)]
    [SerializeField] private float _detectionRadius = 10;
    [SerializeField] private ContactFilter2D _targetFilter;

    [Header("Attack Type")]
    [SerializeField] private bool _isProjectile = false;
    [SerializeField] private bool _isMelee = false;


    [Header("Projectile Settings")]
    [SerializeField] Projectile _projectile = null;
    [UnityEngine.Range(1, 5)]
    [SerializeField] float _moveSpeed = 2;
    [SerializeField] float _projectileLifetime = 2;

    [Header("Melee Settings")]
    [SerializeField] Melee _melee = null;
    [SerializeField] float _meleeLifetime = 2;

    public string Name => _name;
    public int Damage => _damage;
    public float Cooldown => _cooldown;
    public AudioClip WeaponFireSoundEffect => _weaponFireSoundEffect;
    public float WeaponFireSoundEffectVolume => _weaponFireSoundEffectVolume;
    public bool OnlyFireIfNearbyTargets => _onlyFireIfNearbyTargets;
    public float DetectionRadius => _detectionRadius;
    public ContactFilter2D TargetFilter => _targetFilter;
    public bool IsProjectile => _isProjectile;
    public bool IsMelee => _isMelee;
    public Projectile Projectile => _projectile;
    public float MoveSpeed => _moveSpeed;
    public float ProjectileLifetime => _projectileLifetime;
    public Melee Melee => _melee;
    public float MeleeLifetime => _meleeLifetime;
}
