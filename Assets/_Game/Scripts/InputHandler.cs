using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    // Debug Mode switch
    public bool debugMode = true;

    private InputSystem_Actions _inputSystemActions;
    public event Action<Vector2> TouchStarted;
    public event Action<Vector2> TouchEnded;
    public Vector2 TouchStartPosition { get; private set; }
    public Vector2 TouchCurrentPosition { get; private set; }
    public bool TouchHeld { get; private set; }


    public void Awake()
    {
       _inputSystemActions = new InputSystem_Actions();
    }

    private void Update()
    {
        // Check if the player is touching the screen
        if (TouchHeld)
        {
            // read the input from our inout action
            Vector2 TouchPosition = _inputSystemActions.Player.TouchPoint.ReadValue<Vector2>();

            // Update current position
            TouchCurrentPosition = TouchPosition;
        }
    }

    private void OnEnable()
    {
        _inputSystemActions.Enable();
        _inputSystemActions.Player.TouchPoint.performed += OnTouchPreformed;
        _inputSystemActions.Player.TouchPoint.canceled += OnTouchCancelled;
    }

    private void OnDisable()
    {
        _inputSystemActions.Player.TouchPoint.performed -= OnTouchPreformed;
        _inputSystemActions.Player.TouchPoint.canceled -= OnTouchCancelled;
        _inputSystemActions.Disable();
    }

    private void OnTouchPreformed(InputAction.CallbackContext context)
    {
        if (debugMode)
        {
            Debug.Log("InputHandler: Touch Preformed");
        }
        // Change bool to reflect player touching screen
        TouchHeld = true;

        // read the input from our inout action
        Vector2 TouchPosition = context.ReadValue<Vector2>();

        // Save start position
        TouchStartPosition = TouchPosition;

        // Update current position - here its our start position
        TouchCurrentPosition = TouchPosition;

        // Send event notification for listeners
        TouchStarted?.Invoke(TouchPosition);

        if (debugMode)
        {
            Debug.Log("InputHandler: Touch Start Position: " + TouchStartPosition);
        }
    }

    private void OnTouchCancelled(InputAction.CallbackContext context)
    {
        if (debugMode)
        {
            Debug.Log("InputHandler: Touch Cancelled");
        }

        // Change bool to reflect player not touching screen
        TouchHeld = false;

        // Send event notification for listeners
        TouchEnded?.Invoke(TouchCurrentPosition);

        if (debugMode)
        {
            Debug.Log("InputHandler: Touch End Position: " + TouchCurrentPosition);
        }

        // Reset touch positions
        TouchStartPosition = Vector2.zero;
        TouchCurrentPosition = Vector2.zero;
    }
}

