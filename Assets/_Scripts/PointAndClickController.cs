using UnityEngine;
using UnityEngine.InputSystem;

public class PointAndClickController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public LayerMask groundLayer;

    private Vector3 _targetPosition;
    private bool _isMoving = false;
    private Camera _mainCamera;
    private PlayerInputActions _inputActions; // Change this to match your generated class name

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
        _targetPosition = transform.position;
        _mainCamera = Camera.main;
    }

    private void OnEnable() => _inputActions.Enable();
    private void OnDisable() => _inputActions.Disable();

    private void Update()
    {
        // 1. Check for Input
        if (_inputActions.Player.Click.triggered)
        {
            SetTargetPosition();
        }

        // 2. Move towards target
        if (_isMoving)
        {
            MovePlayer();
        }
    }

    private void SetTargetPosition()
    {
        // Create a ray from the camera through the mouse position
        var ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, groundLayer))
        {
            _targetPosition = hit.point;
            // Keep the target at the player's current height to avoid tilting
            _targetPosition.y = transform.position.y; 
            _isMoving = true;
        }
    }

    private void MovePlayer()
    {
        // Calculate distance to target
        var distance = Vector3.Distance(transform.position, _targetPosition);

        if (distance > 0.1f)
        {
            // Rotate towards target
            var direction = (_targetPosition - transform.position).normalized;
            var lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            // Move forward
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            _isMoving = false;
        }
    }

    public bool PlayerIsMoving()
    {
        return _isMoving;
    }
}