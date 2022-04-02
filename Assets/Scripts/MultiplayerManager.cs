using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mirror;

public class MultiplayerManager : NetworkManager
{

    public NetworkConnection connexion;
    public override void OnStartServer()
    {
        Debug.Log("kek");
    }

    public virtual void OnClientConnect(NetworkConnection conn){
        Debug.Log("commence connect");
        connexion = conn;

    }


}