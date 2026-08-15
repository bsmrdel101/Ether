using UnityEngine;
using Unity.Services.Core;
using System;
using Unity.Services.Authentication;
using Unity.Services.Multiplayer;

public class SessionManager : MonoBehaviour
{
  private async void Start()
  {
    try
    {
      await UnityServices.InitializeAsync();
      await AuthenticationService.Instance.SignInAnonymouslyAsync();

      SessionOptions options = new SessionOptions
      {
        MaxPlayers = 2
      }.WithRelayNetwork();

      await MultiplayerService.Instance.CreateOrJoinSessionAsync("code", options);
    }
    catch (Exception e)
    {
      Debug.LogException(e);
    }
  }
}
