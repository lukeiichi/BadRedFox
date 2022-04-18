using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mirror;

public class MultiplayerManager : NetworkManager
{
    public GameObject playerManager;
    public NetworkConnection connexion;
    public override void OnStartServer()
    {
        Debug.Log("kek");
    }

    public override void OnClientConnect(NetworkConnection conn){
        connexion = conn;
        Debug.Log("commence connect"); 
    }
}