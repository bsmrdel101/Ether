using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
  [Header("Movement")]
  [SerializeField] private float _playerSpeed = 5.0f;
  [SerializeField] private float _jumpHeight = 1.5f;
  [SerializeField] private float _gravityValue = -9.81f;

  [Header("Input Actions")]
  [SerializeField] private InputActionReference _moveAction;
  [SerializeField] private InputActionReference _jumpAction;

  [Header("References")]
  [SerializeField] private CharacterController _controller;
  private Vector3 _playerVelocity;


  public override void OnNetworkSpawn()
  {
    if (!IsOwner)
    {
      enabled = false;
      return;
    }

    _moveAction.action.Enable();
    _jumpAction.action.Enable();
  }

  public override void OnNetworkDespawn()
  {
    // if (!IsOwner) return;
    // _moveAction.action.Disable();
    // _jumpAction.action.Disable();
  }

  private void Update()
  {
    HandleJump();
    Vector3 movement = GetMovement();
    ApplyMovement(movement);
  }

  private Vector3 GetMovement()
  {
    Vector2 input = _moveAction.action.ReadValue<Vector2>();
    Vector3 movement = (transform.right * input.x) + (transform.forward * input.y);
    return Vector3.ClampMagnitude(movement, 1f);
  }

  private void ApplyMovement(Vector3 movement)
  {
    Vector3 finalMove = (movement * _playerSpeed) + (_playerVelocity.y * Vector3.up);
    _controller.Move(finalMove * Time.deltaTime);
  }

  private void HandleJump()
  {
    if (_controller.isGrounded && _playerVelocity.y < 0)
    {
      _playerVelocity.y = -2f;
    }

    if (_jumpAction.action.triggered && _controller.isGrounded)
    {
      _playerVelocity.y = Mathf.Sqrt(_jumpHeight * -2.0f * _gravityValue);
    }

    _playerVelocity.y += _gravityValue * Time.deltaTime;
  }
}
