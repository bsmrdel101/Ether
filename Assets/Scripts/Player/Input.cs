using UnityEngine;
using UnityEngine.InputSystem;

public static class Input
{
  public static InputAction Move { get; private set; }
  public static InputAction Jump { get; private set; }

  static Input()
  {
    Move = InputSystem.actions["Move"];
    Jump = InputSystem.actions["Jump"];
  }
}
