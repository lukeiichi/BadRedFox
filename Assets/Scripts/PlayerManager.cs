using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkManager
{    
    public GameObject playerField;

    public static List<Card> listCard = new List<Card>(); 

    void Awake(){

        GameObject playerVisual = Instantiate(playerField, new Vector3(0, 0, 0), Quaternion.identity);
        NetworkServer.Spawn(playerVisual);
        
        playerVisual.GetComponent<UIManagerField>().DrawCards();
    }
}
