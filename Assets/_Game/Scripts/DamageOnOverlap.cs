using UnityEngine;

public class DamageOnOverlap : MonoBehaviour
{
    public bool debugMode = true;

    [SerializeField] private int _damage = 10;
    [SerializeField] private float _damageFrequency = .2f;

    private Collider2D _triggerCollider;

    private void Awake()
    {
        // ensure we have a Collider2D and it's a trigger
        _triggerCollider = GetComponent<Collider2D>();
        _triggerCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // More preforment than GetComponent
        if (collision.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
            if (debugMode)
            {
                Debug.Log("DamageOnOverlap: Damaged: " + collision.gameObject.name + " by " + _damage);
            }

            // disable trigger to stop next attack
            _triggerCollider.enabled = false;
            // call method after delay
            Invoke(nameof(ReadyAttack), _damageFrequency);
        }
    }

    private void ReadyAttack()
    {
        _triggerCollider.enabled = true;
    }
}
