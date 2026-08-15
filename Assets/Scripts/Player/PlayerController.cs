using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
  [Header("References")]
  [SerializeField] private GameObject _playerCamObject;


  private void Start()
  {
    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;
  }

  public override void OnNetworkSpawn()
  {
    if (!IsOwner) return;
    _playerCamObject.SetActive(true);
  }
}
