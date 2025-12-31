using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool debugMode = true;

    [SerializeField] private Rigidbody2D _rb = null;

    private int _damage = 5;
    private float _moveSpeed = 2;
    private Vector2 _direction = Vector2.zero;

    //[Header("Sound Effect Settings")]
    //[SerializeField] AudioClip _enemyHitSound = null;
    //[Range(0, 1)]
    //[SerializeField] private float _enemyHitSoundVolume = 1f;

    public void Spawn(Vector2 direction, int damage, float moveSpeed)
    {
        // set varibles for weapon
        _direction = direction.normalized;
        _damage = damage;
        _moveSpeed = moveSpeed;
        // test nearby enemies
        // point to the nearest enemy
        // play spawn sound
    }

    public void FixedUpdate()
    {
        // move projectile in forward direction

        // calculate target position offset
        Vector2 offsetPos = _direction * _moveSpeed * Time.deltaTime;

        // add offset to current (local)
        _rb.MovePosition(_rb.position + offsetPos);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // search for Health component on collision object
        if (debugMode)
        { 
            Debug.Log("Projectile: " + collision.name + " hit!");
        }

        if (collision.TryGetComponent(out Health health))
        {
            // apply damage
            health.TakeDamage(_damage);

            // hit sound effect
            //AudioHelper.PlayClip2D(_enemyHitSound, _enemyHitSoundVolume);

            if (debugMode)
            {
                Debug.Log("Projectile: " + collision.name + " took " + _damage + " damage!");
            }
            // destroy projectile
            Destroy(this.gameObject);
        }
    }
}
