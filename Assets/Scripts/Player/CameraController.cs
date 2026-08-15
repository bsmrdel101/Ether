using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
  [Header("Controls")]
  [SerializeField] private float sensitivity = 0.1f;
  private float xRotation = 0f;

  [Header("References")]
  [SerializeField] private Transform player;


  private void Update()
  {
    Vector2 mouseDelta = Mouse.current.delta.ReadValue();
    float mouseX = mouseDelta.x * sensitivity;
    float mouseY = mouseDelta.y * sensitivity;

    xRotation -= mouseY;
    xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    player.Rotate(Vector3.up * mouseX);
  }
}
