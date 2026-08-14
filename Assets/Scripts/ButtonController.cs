using UnityEngine;
using Unity.Netcode;

public class ButtonController : MonoBehaviour
{
  public void OnClickStartClient()
  {
    NetworkManager.Singleton.StartClient();
  }
}
