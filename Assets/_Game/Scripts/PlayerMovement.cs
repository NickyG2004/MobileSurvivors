using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private float _moveSpeed = 2f;
    private float _maxMoveSpeed = 7f;

    private Vector2 _moveDirection;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_inputHandler == null)
        {
            return;
        }

        if (_inputHandler.TouchHeld)
        {
            // calculate move direction
            _moveDirection = _inputHandler.TouchCurrentPosition 
                - _inputHandler.TouchStartPosition;
            _moveDirection.Normalize();
        }
    }

    private void FixedUpdate()
    {
        // move in current move direction
        Vector2 offsetPos = _moveDirection * _moveSpeed * Time.fixedDeltaTime;
        _rigidbody2D.MovePosition(_rigidbody2D.position + offsetPos);
    }

    public void IncreaseMoveSpeed(float amount)
    {
        _moveSpeed = Mathf.Min(_moveSpeed + amount, _maxMoveSpeed);

        if (_moveSpeed > _maxMoveSpeed)
        {
            _moveSpeed = _maxMoveSpeed;
        }
    }
}
