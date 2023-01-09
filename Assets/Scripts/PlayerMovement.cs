using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _Speed = 30f;

    private PlayerInput _Input;
    private Vector2 _Movement;
    private Rigidbody2D _RigidBody;

    private bool _canDash = true;
    private float _dashingPower = 20f;
    private float _dashingTime = 0.1f;
    private float _dashingCooldown = 0.5f;

    [SerializeField] private TrailRenderer tr;

    private void Awake()
    {
        _Input = new PlayerInput();
        _RigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _Input.Gameplay.Movement.performed += OnMovement;
        _Input.Gameplay.Movement.canceled += OnMovement;

        _Input.Gameplay.Dash.performed += OnDashing;
        _Input.Gameplay.Dash.canceled += OnDashing;

        _Input.Enable();
    }

    private void OnDisable()
    {
        _Input.Gameplay.Movement.performed -= OnMovement;
        _Input.Gameplay.Movement.canceled -= OnMovement;

        _Input.Gameplay.Dash.performed -= OnDashing;
        _Input.Gameplay.Dash.canceled -= OnDashing;

        _Input.Disable();
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        _Movement = context.ReadValue<Vector2>();
    }

    private void OnDashing(InputAction.CallbackContext context)
    {
        if (_canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        _RigidBody.AddForce(_Movement * _Speed);
    }

    private IEnumerator Dash()
    {
        _canDash = false;
        _RigidBody.velocity = _Movement * _dashingPower;
        tr.emitting = true;
        yield return new WaitForSeconds(_dashingTime);
        tr.emitting = false;
        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;
    }
}