using UnityEngine;

public class Melee : MonoBehaviour
{
    public bool debugMode = true;

    // [SerializeField] private Rigidbody2D _rb = null;

    private int _damage = 5;

    //[Header("Sound Effect Settings")]
    //[SerializeField] AudioClip _enemyHitSound = null;
    //[Range(0, 1)]
    //[SerializeField] private float _enemyHitSoundVolume = 1f;

    public void Spawn(int damage)
    {
        // set varibles for weapon
        _damage = damage;
    }

    public void FixedUpdate()
    {
        // Follow the player charcter
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // search for Health component on collision object
        if (debugMode)
        {
            Debug.Log("Melee: " + collision.name + " hit!");
        }

        if (collision.TryGetComponent(out Health health))
        {
            // apply damage
            health.TakeDamage(_damage);

            // hit sound effect
            //AudioHelper.PlayClip2D(_enemyHitSound, _enemyHitSoundVolume);

            if (debugMode)
            {
                Debug.Log("Melee: " + collision.name + " took " + _damage + " damage!");
            }
            // destroy projectile
            Destroy(this.gameObject);
        }
    }
}
