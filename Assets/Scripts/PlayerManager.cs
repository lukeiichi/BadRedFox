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
    public GameObject descriptionObject;
    public MultiplayerManager networkManager;
    public Camera camera;

    [SyncVar] private int numberPlayers;

    void Awake(){
        networkManager = GetGameObject("NetworkingManager").transform.GetComponent<MultiplayerManager>();
    }

    void Start(){
        if(isLocalPlayer){
            CreateField();
            CreateOpponent();

            Transform transformInterface = GetGameObject("Interface").transform.GetComponent<Transform>();
            Vector3 positionInterface = GetGameObject("Interface").transform.GetComponent<Transform>().position;
            positionInterface.x = 0;
            transformInterface.position = positionInterface;

            //GameObject description = Instantiate(descriptionObject, new Vector3(0, 0, -9), Quaternion.identity);
            //description.name = "CardDescriptio";
        }
    } 

    void Update(){
        numberPlayers = networkManager.numPlayers;
    }

    public void CreateField(){
        //RpcGetNumberUser();
        GameObject playerVisual = Instantiate(playerField,new Vector3(0, 0, 0), Quaternion.identity);
        playerVisual.name = "PlayerField";
        NetworkServer.Spawn(playerVisual);        
        
        playerVisual.GetComponent<UIManagerField>().CmdDrawCards();

        //playerVisual.transform.GetComponent<Canvas>().worldCamera = camera;
    }

    public void CreateOpponent(){
        //RpcGetNumberUser();
        GameObject playerVisual = Instantiate(playerField,new Vector3(2100, 0, 0), Quaternion.identity);
        playerVisual.name = "Ennemy";
        NetworkServer.Spawn(playerVisual);
        
        playerVisual.GetComponent<UIManagerField>().CmdDrawCards();

        //playerVisual.transform.GetComponent<Canvas>().worldCamera = camera;
    }

    /*[ClientRpc] public void RpcGetNumberUser(){
        Debug.Log("under the nember get");
        numberPlayers = networkManager.numPlayers;
    } */
}
