using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))] 
public class PlayerMoveController : MonoBehaviour
{
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;

    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private float _jumpHeight = 1.0f;
    [SerializeField] private float _gravityValue = -9.81f;
    [SerializeField] private float _rotationSpeed = 5f;

    private AnimatorController _animatorController;
    private ShootingBulletController _shootingBulletController;

    private PlayerInput _playerInput;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _shootAction;

    private Transform _cameraTransform;
    private Vector3 _move = Vector3.zero;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _animatorController = GetComponent<AnimatorController>();
        _shootingBulletController = GetComponent<ShootingBulletController>();

        _cameraTransform = Camera.main.transform;

        _moveAction = _playerInput.actions["Move"];
        _jumpAction = _playerInput.actions["Jump"];
        _shootAction = _playerInput.actions["Shoot"];

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        SetOnTheGround();
        Move();
        Jump();
        Rotate();
    }

    public Transform GetCameraTransform()
    {
        return _cameraTransform;
    }

    private void SetOnTheGround()
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }
    }

    private void Move()
    {
        float temporaryPlayerSpeed = _playerSpeed;
        AccelerateWhenShiftKeyIsPressed(ref temporaryPlayerSpeed);

        Vector2 input = _moveAction.ReadValue<Vector2>();
        _move.x = _animatorController.GetCurrentAnimationBlendVector().x;
        _move.z = _animatorController.GetCurrentAnimationBlendVector().y;
        // _move.x = input.x;
        // _move.z = input.y;
        _animatorController.SmoothBlendAnimation(input);
        _move = _move.x * _cameraTransform.right.normalized + _move.z * _cameraTransform.forward.normalized;
        _move.y = 0;

        _controller.Move(_move * Time.deltaTime * temporaryPlayerSpeed);

        _animatorController.AnimateMoving();
    }

    private void AccelerateWhenShiftKeyIsPressed(ref float temporaryPlayerSpeed)
    {
        if  (Input.GetKey(KeyCode.LeftShift))
        {
            temporaryPlayerSpeed *= 3; // 1.5f;
        }
    }

    private void Jump()
    {
        // Changes the height position of the player..
        if (_jumpAction.triggered && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
            _animatorController.PlayJumpAnimation();
        }

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    private void Rotate()
    {
        float targetAngle = _cameraTransform.eulerAngles.y;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        _shootAction.performed += _ => _shootingBulletController.Shoot();
    }

    private void OnDisable()
    {
        _shootAction.performed -= _ => _shootingBulletController.Shoot();
    }
}