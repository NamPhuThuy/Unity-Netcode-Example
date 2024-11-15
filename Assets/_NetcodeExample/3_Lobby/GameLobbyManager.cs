using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Lobbies;
using UnityEngine;

public class GameLobbyManager : Singleton<GameLobbyManager>
{
    public async Task<bool> CreateLobby()
    {
        Dictionary<string, string> playerData = new Dictionary<string, string>()
            {
                {"GamerTag", "HostPlayer"}
            };
        bool succeeded = await LobbyManager.Instance.CreateLobby(4, true, playerData);
        return succeeded;
    }

    public void JoinLobby()
    {
         
    }

    public string GetLobbyCode()
    {
        return LobbyManager.Instance.GetLobbyCode();
    }
}
