using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;

    private float WalkTopSpeed = 7f;
    private float WalkAcceleration = 4.0f;
    private float WalkDeceleration = 1f;
    private float WalkVelPower = 0.87f;

    private float FrictionAmount = 0.25f;

    private PlayerInput _input;
    private InputAction _moveInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _input = GetComponent<PlayerInput>();
        _moveInput = _input.actions.FindAction("Move");
    }

    private void Update()
    {
        Movement();
    }

    public void Movement()
    {
        #region Run
        float targetSpeed = _moveInput.ReadValue<Vector2>().x * WalkTopSpeed;

        float speedDif = targetSpeed - _rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? WalkAcceleration : WalkDeceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, WalkVelPower) * Mathf.Sign(speedDif);

        _rb.AddForce(movement * Vector2.right);
        #endregion

        #region Friction
        // check if we're grounded and that we are trying to stop (not pressing forwards nor backwards)
        if (Mathf.Abs(_moveInput.ReadValue<Vector2>().x) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(_rb.velocity.x), Mathf.Abs(FrictionAmount));

            // sets to movement direction
            amount *= Mathf.Sign(_rb.velocity.x);

            // applies force against movement direction
            _rb.AddForce(Vector2.right * -amount);
        }
        #endregion


    }

}
