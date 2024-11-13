using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boostrap : MonoBehaviour
{
     
    async void Start()
    {
        //Used await to make sure the Unity Services is initialized before anyth
        await UnityServices.InitializeAsync();

        if (UnityServices.State == ServicesInitializationState.Initialized)
        {
            Debug.Log("Initial successfully");
            
            //Can be sign in with Facebook, Apple, Google, Oculus, Steam,.. or Anonymous             
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            if (AuthenticationService.Instance.IsSignedIn)
            {
                string username = PlayerPrefs.GetString("Username", "Player");
                if (username == "")
                {
                    username = "Player";
                    PlayerPrefs.SetString("Username", "Player");
                }

                SceneManager.LoadSceneAsync("MainMenu");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
