using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mirror;

public class MultiplayerManager : NetworkManager
{
    public override void OnStartServer(){
        Debug.Log("kek");
    }
    public override void OnClientConnect(){

/*
           NetworkClient.RegisterPrefab(playerField);

         
        GameObject playerVisual = Instantiate(playerField, new Vector3(0, 0, 0), Quaternion.identity);
        NetworkServer.Spawn(playerVisual);*/
    }


}