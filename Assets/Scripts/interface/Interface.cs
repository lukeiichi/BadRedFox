using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static MyExtensions;

public class Interface : MonoBehaviour
{
    public Transform interfaceTransform;
    public Canvas interfaceCamera;
    public Camera camera;
    public Transform cameraTransform;


    // Change de terrain
    public void ChangeField(int numCamera)
    {
        var x = 0;
        var y = 0;
        GameObject player = GetGameObject("PlayerField");
        GameObject ennemy = GetGameObject("Ennemy");
        // Trouve la position adéquat
        switch(numCamera){
            case 1:
                x = 2100;
                break;
            case 2:
                x = 2100;
                y = 1200;
                break;
            case 3:
                y = 1200;
                break;
            default:
                break;
        }

        // Définit une nouvelle position 
        Vector3 newPosition = new Vector3(0,0,-10);
        // Définit les valeurs adéquat au joueur
        newPosition.x = x;
        newPosition.y = y;
        cameraTransform.position = newPosition;
        //remet l'inferface au premier plan
        newPosition.z = 0;
        interfaceTransform.position = newPosition;
    }
}
