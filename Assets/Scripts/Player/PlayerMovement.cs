using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerDatas _playerStats;

    public bool IsMoving { get; set; } = true;

    private Rigidbody _rb;
    private PlayerInput _input;
    private InputAction _moveInput;

    private float _walkTopSpeed;
    private float _walkAcceleration;
    private float _walkDeceleration;
    private float _walkVelPower;
    private float _frictionAmount;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _input = GetComponent<PlayerInput>();
        _moveInput = _input.actions.FindAction("Move");

        _walkTopSpeed = _playerStats.WalkTopSpeed;
        _walkAcceleration = _playerStats.WalkAcceleration;
        _walkDeceleration = _playerStats.WalkDeceleration;
        _walkVelPower = _playerStats.WalkVelPower;
        _frictionAmount = _playerStats.FrictionAmount;
    }

    void FixedUpdate()
    {
        Vector2 moveInput = _moveInput.ReadValue<Vector2>();

        if (moveInput.x == moveInput.y && moveInput != Vector2.zero) return;

        float targetSpeedX = moveInput.x * _walkTopSpeed;
        float targetSpeedZ = moveInput.y * _walkTopSpeed;

        float speedDifX = targetSpeedX - _rb.velocity.x;
        float speedDifZ = targetSpeedZ - _rb.velocity.z;

        float accelRateX = (Mathf.Abs(targetSpeedX) > 0.01f) ? _walkAcceleration : _walkDeceleration;
        float accelRateY = (Mathf.Abs(targetSpeedZ) > 0.01f) ? _walkAcceleration : _walkDeceleration;

        float movementX = Mathf.Pow(Mathf.Abs(speedDifX) * accelRateX, _walkVelPower) * Mathf.Sign(speedDifX);
        float movementZ = Mathf.Pow(Mathf.Abs(speedDifZ) * accelRateY, _walkVelPower) * Mathf.Sign(speedDifZ);

        Vector3 movementForce = Vector3.right * movementX + Vector3.forward * movementZ;

        _rb.AddForce(movementForce);

        if (Mathf.Abs(moveInput.x) < 0.01f)
        {
            float frictionX = Mathf.Min(Mathf.Abs(_rb.velocity.x), Mathf.Abs(_frictionAmount));
            frictionX *= Mathf.Sign(_rb.velocity.x);
            _rb.AddForce(Vector3.right * -frictionX);
        }

        if (Mathf.Abs(moveInput.y) < 0.01f)
        {
            float frictionZ = Mathf.Min(Mathf.Abs(_rb.velocity.y), Mathf.Abs(_frictionAmount));
            frictionZ *= Mathf.Sign(_rb.velocity.y);
            _rb.AddForce(Vector3.forward * -frictionZ);
        }

        if (moveInput == Vector2.zero) return;
        this.transform.rotation = Quaternion.LookRotation(new Vector3(-moveInput.y, 0, moveInput.x));

    }
}