using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    private bool _debugMode = false;

    public string Name { get; private set; }

    //[Header("General Settings")]
    int _damage = 1;
    float _cooldown = 1;

    //[Header("Sound Effect Settings")]
    private AudioClip _weaponFireSoundEffect = null;
    private float _weaponFireSoundEffectVolume = 1f;

    //[Header("Detection Settings")]
    private bool _onlyFireIfNearbyTargets = false;
    private float _detectionRadius = 10;
    private ContactFilter2D _targetFilter;

    //[Header("Attack Type")]
    private bool _isProjectile = false;
    private bool _isMelee = false;
    

    //[Header("Projectile Settings")]
    private Projectile _projectile = null;
    float _moveSpeed = 2;
    float _projectileLifetime = 2;

    //[Header("Melee Settings")]
    private Melee _melee = null;
    float _meleeLifetime = 2;

    // Used in Projectile Calculations
    private List<Collider2D> _targetsDetected = new List<Collider2D>();
    private Vector2 _direction = Vector2.zero;

    // debug gizmos
    private void OnDrawGizmos()
    {
        if (_debugMode)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _detectionRadius);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }

    private void Awake()
    {
        // If projectile and melee are both false, throw an error
        if (!_isProjectile && !_isMelee)
        {
            //Debug.LogError("WeaponSystem: At least one attack type must be selected (Projectile or Melee).");
        }

        // Prevent both projectile and melee from being true
        if (_isProjectile && _isMelee)
        {
            //Debug.LogError("WeaponSystem: Cannot have both Projectile and Melee attack types selected.");
        }
    }

    private void Start()
    {
        // (MethodName, delay, repeatRate)
        InvokeRepeating(nameof(Attack), _cooldown, _cooldown);
    }

    private Collider2D DetectClosestTarget()
    {
        // detect 
        Physics2D.OverlapCircle(transform.position, _detectionRadius, _targetFilter, _targetsDetected);

        // if no ememies detected, return null
        if (_targetsDetected == null)
        {
            return null;
        }

        // test for colsest ememy
        // use these varibles tp store the closest enemy as we test
        Collider2D closestTarget = null;
        float closestDistance = Mathf.Infinity;

        // test!
        foreach (Collider2D target in _targetsDetected)
        {
            // draw the line between the weapon and the target
            if (_debugMode)
            {
                Debug.DrawLine(transform.position, target.transform.position, Color.red);
            }
            Vector3 distanceVector = target.transform.position - transform.position;

            // get length of line, as a number value
            float currentDistance = distanceVector.sqrMagnitude;

            // if it's closer than the previous closest, store it
            if (currentDistance <= closestDistance)
            {
                closestTarget = target;
                closestDistance = currentDistance;
            }
        }

        // return our closest target
        return closestTarget;
    }

    public void Attack()
    {
        if (_debugMode)
        {
            Debug.Log("WeaponSystem: Attack");
        }

        // only check for targets if we're suppose to hold fire until nearby targets are detected
        if (_onlyFireIfNearbyTargets)
        {
            Collider2D target = DetectClosestTarget();

            // if there's no target, don't fire
            if (target == null)
            {
                return;
            }

            _direction = target.transform.position - transform.position;

            // get direction scaled to 1 value
            _direction.Normalize();

            if (_debugMode)
            {
                Debug.Log("WeaponSystem: Target detected: " + target.name);
            }
        }


        if (_isProjectile == true)
        {
            // Instantiate(_projectile, transform.position, Quaternion.identity);
            Projectile newProjectile = Instantiate(_projectile, transform.position, Quaternion.identity);
            newProjectile.Spawn(_direction, _damage, _moveSpeed);

            // play sound effect
            AudioHelper.PlayClip2D(_weaponFireSoundEffect, _weaponFireSoundEffectVolume);

            // destroy projectile after lifetime
            Destroy(newProjectile.gameObject, _projectileLifetime);
        }

        if (_isMelee == true)
        {
            // Instantiate(_melee, transform.position, Quaternion.identity);
            Melee newMelee = Instantiate(_melee, transform.position, Quaternion.identity);
            newMelee.Spawn(_damage);

            // play sound effect
            AudioHelper.PlayClip2D(_weaponFireSoundEffect, _weaponFireSoundEffectVolume);

            // destroy projectile after lifetime
            Destroy(newMelee.gameObject, _meleeLifetime);
        }
    }

    public void DecreaseCooldown(float amount)
    {
        // don't allow cooldown to go below 0
        _cooldown -= amount;
        if (_cooldown < 0.1f)
        {
            _cooldown = 0.1f;
        }

        if (_debugMode)
        {
            Debug.Log("WeaponSystem: Weapon Cooldown Decreased");
        }
    }

    public void IncreaseDamage(int increaseAmount)
    {
        _damage += increaseAmount;

        if (_debugMode)
        {
            Debug.Log("WeaponSystem: Weapon Damage Increased");
        }
    }

    public void SetupWeapon(WeaponData data)
    {
        // assign the data into local varibles
        Name = data.Name;
        _debugMode = data.debugMode;
        _damage = data.Damage;
        _cooldown = data.Cooldown;
        _weaponFireSoundEffect = data.WeaponFireSoundEffect;
        _weaponFireSoundEffectVolume = data.WeaponFireSoundEffectVolume;
        _onlyFireIfNearbyTargets = data.OnlyFireIfNearbyTargets;
        _detectionRadius = data.DetectionRadius;
        _targetFilter = data.TargetFilter;
        _isProjectile = data.IsProjectile;
        _isMelee = data.IsMelee;
        _projectile = data.Projectile;
        _moveSpeed = data.MoveSpeed;
        _projectileLifetime = data.ProjectileLifetime;
        _melee = data.Melee;
        _meleeLifetime = data.MeleeLifetime;
    }
}
