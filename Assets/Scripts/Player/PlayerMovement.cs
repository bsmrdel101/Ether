using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
  [Header("Movement")]
  [SerializeField] private float _playerSpeed = 14f;
  [SerializeField] private float _jumpHeight = 1.5f;
  [SerializeField] private float _gravityValue = -24f;

  [Header("References")]
  [SerializeField] private CharacterController _controller;
  [SerializeField] private Transform _camPos;
  private Vector3 _playerVelocity;


  public override void OnNetworkSpawn()
  {
    if (!IsOwner) enabled = false;
  }

  private void Update()
  {
    HandleJump();
    Vector3 movement = GetMovement();
    ApplyMovement(movement);
  }

  private Vector3 GetMovement()
  {
    Vector2 input = Input.Move.ReadValue<Vector2>();
    Vector3 movement = (_camPos.right * input.x) + (_camPos.forward * input.y);
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

    if (Input.Jump.IsPressed() && _controller.isGrounded)
    {
      _playerVelocity.y = Mathf.Sqrt(_jumpHeight * -2.0f * _gravityValue);
    }

    _playerVelocity.y += _gravityValue * Time.deltaTime;
  }
}
