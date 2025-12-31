using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool debugMode = false;

    // [SerializeField] private PlayerCharacter _player;
    [SerializeField] private float _moveSpeed = 1f;


    private PlayerCharacter _player;
    private Rigidbody2D _rigidbody2D;

    public void Initialize(PlayerCharacter player)
    {
        _player = player;
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (_player == null)
            return;
        // get direction from player
        Vector2 direction = _player.transform.position - transform.position;
        direction.Normalize();
        // calculate target position offset
        Vector2 offestPos = direction * _moveSpeed * Time.deltaTime;
        // add offset to current (local)
        _rigidbody2D.MovePosition(_rigidbody2D.position + offestPos);
    }
}
