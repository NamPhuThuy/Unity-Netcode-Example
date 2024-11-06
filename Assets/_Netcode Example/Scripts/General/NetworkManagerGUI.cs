using System;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class NetworkManagerGUI : MonoBehaviour
    {
        static private NetworkManager m_NetworkManager;

        void Awake()
        {
            m_NetworkManager = GetComponent<NetworkManager>();
        }

        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            if (!m_NetworkManager.IsClient && !m_NetworkManager.IsServer)
            {
                StartButtons();
            }
            else
            {
                StatusLabels();

                SubmitNewPosition();
            }

            GUILayout.EndArea();
        }

        static void StartButtons()
        {
            if (GUILayout.Button("Host")) m_NetworkManager.StartHost();
            if (GUILayout.Button("Client")) m_NetworkManager.StartClient();
            if (GUILayout.Button("Server")) m_NetworkManager.StartServer();
            if (GUILayout.Button("Quit")) QuitApp();
        }

        static void QuitApp()
        {
            Application.Quit();
        }

        static void StatusLabels()
        {
            var mode = m_NetworkManager.IsHost ?
                "Host" : m_NetworkManager.IsServer ? "Server" : "Client";

            GUILayout.Label("Transport: " +
                m_NetworkManager.NetworkConfig.NetworkTransport.GetType().Name);
            GUILayout.Label("Mode: " + mode);
        }

        private void Update()
        {
            if (m_NetworkManager.IsServer)
            {
                foreach (ulong uid in m_NetworkManager.ConnectedClientsIds)
                    m_NetworkManager.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<PlayerMovementController>().MovementHandle();
            }
            else
            {
                Debug.Log("check for clients");
                var playerObject = m_NetworkManager.SpawnManager.GetLocalPlayerObject();
                var player = playerObject.GetComponent<PlayerMovementController>();
                player.MovementHandle();
            }
        }

        static void SubmitNewPosition()
        {
            /*if (m_NetworkManager.IsServer)
                if (GUILayout.Button("Move"))
            {
                //Check if this instance is a host (a client and a server) or not
                if (m_NetworkManager.IsClient) return;
                
                foreach (ulong uid in m_NetworkManager.ConnectedClientsIds)
                    m_NetworkManager.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<PlayerMovementController>().Move();
            }
            else if (m_NetworkManager.IsClient)
            {
                if (GUILayout.Button("Request Position Change"))
                {
                    var playerObject = m_NetworkManager.SpawnManager.GetLocalPlayerObject();
                    var player = playerObject.GetComponent<PlayerMovementController>();
                    player.Move();
                }
            }*/
            
            
            if (GUILayout.Button(m_NetworkManager.IsServer ? "Move" : "Request Position Change"))
            {
                //This condition checks if the instance is a dedicated server (i.e., it’s only the server, without any client functions). Ensures this is a server-only instance and not a host
                if (m_NetworkManager.IsServer && !m_NetworkManager.IsClient )
                {
                    /*
                     - m_NetworkManager.ConnectedClientsIds: return a list of unique client IDs (ulong type) for all connected clients.
                     
                     - m_NetworkManager.SpawnManager.GetPlayerNetworkObject(uid): retrieves the NetworkObject for a specific player using their uid. The NetworkObject represents the networked player object associated with each client.
                     */
                    foreach (ulong uid in m_NetworkManager.ConnectedClientsIds)
                        m_NetworkManager.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<PlayerMovementController>().Move();
                }
                //If the instance is not a dedicated server (meaning it’s either a client or a host), the code in the else block executes.
                else
                {
                    var playerObject = m_NetworkManager.SpawnManager.GetLocalPlayerObject();
                    var player = playerObject.GetComponent<PlayerMovementController>();
                    player.Move();
                }
            }
        }
    }