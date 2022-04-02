using System.Collections;
using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

using static MyExtensions;

public class PlayerManager : NetworkBehaviour
{    
    public GameObject playerField;
    public MultiplayerManager networkManager;
    public List<GameObject> listCamera = new List<GameObject>();

    void Awake(){
        networkManager = GetGameObject("NetworkingManager").transform.GetComponent<MultiplayerManager>();
        listCamera.Add(GetGameObject("Main Camera"));
        listCamera.Add(GetGameObject("Main Camera2"));
        listCamera.Add(GetGameObject("Main Camera3"));
        listCamera.Add(GetGameObject("Main Camera4"));
        CreateField(); 
    }

    public void CreateField(){
        Transform pos = listCamera[networkManager.numPlayers].transform.GetComponent<Transform>();
        GameObject playerVisual = Instantiate(playerField,new Vector3(pos.position.x, pos.position.y,0), Quaternion.identity);
        NetworkServer.Spawn(playerVisual);
        //playerVisual.transform.GetComponent<NetworkIdentity>().AssignClientAuthority(networkManager.connexion);
        playerVisual.GetComponent<UIManagerField>().CmdDrawCards();
        playerVisual.transform.GetComponent<Canvas>().worldCamera = listCamera[networkManager.numPlayers].transform.GetComponent<Camera>();
    }
}
