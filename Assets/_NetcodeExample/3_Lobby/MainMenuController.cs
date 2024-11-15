using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button _hostButton;
    [SerializeField] private Button _joinButton;

    private void Start()
    {
        _hostButton.onClick.AddListener(OnHostClicked);
        _joinButton.onClick.AddListener(OnJoinClicked);
    }

    private async void OnHostClicked()
    {
        Debug.Log("Hos clicked");
        bool succeeded = await GameLobbyManager.Instance.CreateLobby();
        
        //If lobby is created successfully => send the player to the lobby-scene
        if (succeeded)
        {
            SceneManager.LoadSceneAsync("Lobby");
        }
    }

    private void OnJoinClicked()
    {
        Debug.Log("Join clicked");
        GameLobbyManager.Instance.JoinLobby();
    }
}
