using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
  [Header("References")]
  [SerializeField] private GameObject _playerCamObject;


  private void Start()
  {
    if (!IsOwner) return;
    _playerCamObject.SetActive(true);
  }
}
