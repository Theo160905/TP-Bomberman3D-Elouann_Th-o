using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    private float _speed = 5;

    [SerializeField] 
    private Animator _animator;

    public Vector2 CurrentMovement { get; set; }
    public bool IsMoving { get; set; } = true;



    private Rigidbody _rb;
    private PlayerInput _input;
    private InputAction _moveInput;

    private float WalkTopSpeed = 7f;
    private float WalkAcceleration = 4.0f;
    private float WalkDeceleration = 1f;
    private float WalkVelPower = 0.87f;
    private float FrictionAmount = 0.25f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _input = GetComponent<PlayerInput>();
        _moveInput = _input.actions.FindAction("Move");
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (!IsMoving) return;

        CurrentMovement = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Vector2 moveInput = _moveInput.ReadValue<Vector2>();

        float targetSpeedX = moveInput.x * WalkTopSpeed;
        float targetSpeedY = moveInput.y * WalkTopSpeed;

        float speedDifX = targetSpeedX - _rb.velocity.x;
        float speedDifY = targetSpeedY - _rb.velocity.y;

        float accelRateX = (Mathf.Abs(targetSpeedX) > 0.01f) ? WalkAcceleration : WalkDeceleration;
        float accelRateY = (Mathf.Abs(targetSpeedY) > 0.01f) ? WalkAcceleration : WalkDeceleration;

        float movementX = Mathf.Pow(Mathf.Abs(speedDifX) * accelRateX, WalkVelPower) * Mathf.Sign(speedDifX);
        float movementY = Mathf.Pow(Mathf.Abs(speedDifY) * accelRateY, WalkVelPower) * Mathf.Sign(speedDifY);

        _rb.AddForce(movementX * Vector2.right);
        _rb.AddForce(movementY * Vector3.forward);

        if (Mathf.Abs(moveInput.x) < 0.01f && Mathf.Abs(moveInput.y) < 0.01f)
        {
            float frictionX = Mathf.Min(Mathf.Abs(_rb.velocity.x), Mathf.Abs(FrictionAmount));
            frictionX *= Mathf.Sign(_rb.velocity.x);
            _rb.AddForce(Vector2.right * -frictionX);

            float frictionY = Mathf.Min(Mathf.Abs(_rb.velocity.y), Mathf.Abs(FrictionAmount));
            frictionY *= Mathf.Sign(_rb.velocity.y);
            _rb.AddForce(Vector2.up * -frictionY);
        }

        Vector3 mouvement = new Vector3(CurrentMovement.x, 0, CurrentMovement.y);
        mouvement.Normalize();
        transform.Translate(_speed * mouvement * Time.fixedDeltaTime, Space.World);

        /* Animation (si nécessaire) */
        //if (mouvement != Vector3.zero)
        //{
        //    _animator.SetFloat("Velocity", 7);
        //    Quaternion toRotation = Quaternion.LookRotation(mouvement, Vector2.up);
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.fixedDeltaTime);
        //}
        //else
        //{
        //    _animator.SetFloat("Velocity", 0);
        //}
    }


    /* TEST*/
    /*
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
        Vector2 moveInput = _moveInput.ReadValue<Vector2>();

        float targetSpeedX = moveInput.x * WalkTopSpeed;
        float targetSpeedY = moveInput.y * WalkTopSpeed;

        float speedDifX = targetSpeedX - _rb.velocity.x;
        float speedDifY = targetSpeedY - _rb.velocity.y;

        float accelRateX = (Mathf.Abs(targetSpeedX) > 0.01f) ? WalkAcceleration : WalkDeceleration;
        float accelRateY = (Mathf.Abs(targetSpeedY) > 0.01f) ? WalkAcceleration : WalkDeceleration;

        float movementX = Mathf.Pow(Mathf.Abs(speedDifX) * accelRateX, WalkVelPower) * Mathf.Sign(speedDifX);
        float movementY = Mathf.Pow(Mathf.Abs(speedDifY) * accelRateY, WalkVelPower) * Mathf.Sign(speedDifY);

        _rb.AddForce(movementX * Vector2.right);
        _rb.AddForce(movementY * Vector3.forward);
        #endregion

        #region Friction
        if (Mathf.Abs(moveInput.x) < 0.01f && Mathf.Abs(moveInput.y) < 0.01f)
        {
            float frictionX = Mathf.Min(Mathf.Abs(_rb.velocity.x), Mathf.Abs(FrictionAmount));
            frictionX *= Mathf.Sign(_rb.velocity.x);
            _rb.AddForce(Vector2.right * -frictionX);

            float frictionY = Mathf.Min(Mathf.Abs(_rb.velocity.y), Mathf.Abs(FrictionAmount));
            frictionY *= Mathf.Sign(_rb.velocity.y);
            _rb.AddForce(Vector2.up * -frictionY);
        }
        #endregion


        [SerializeField] private float _speed = 5;
        [SerializeField] public Vector2 CurrentMovement { get; set; }
        [SerializeField] public bool IsMoving { get; set; } = true;
        [SerializeField] private Animator _animator;

        public void OnMovement(InputAction.CallbackContext context)
        {
            if (!IsMoving) return;

            CurrentMovement = context.ReadValue<Vector2>();
        }

    */
    //void FixedUpdate()
    //{
    //Vector3 mouvement = new Vector3(CurrentMovement.x, 0, CurrentMovement.y);
    //mouvement.Normalize();
    //transform.Translate(_speed * mouvement * Time.fixedDeltaTime, Space.World);

    /* Si besoin de faire animation type Idle etc*/
    //if (mouvement != Vector3.zero)
    //{
    //    _animator.SetFloat("Velocity", 7);
    //    Quaternion toRotation = Quaternion.LookRotation(mouvement, Vector2.up);
    //    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.fixedDeltaTime);
    //}
    //else
    //{
    //    _animator.SetFloat("Velocity", 0);
    //}
    //}

}