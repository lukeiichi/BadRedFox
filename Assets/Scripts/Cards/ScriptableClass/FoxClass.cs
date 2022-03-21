using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName="FoxCard", menuName="Assets/Fox")]
public class FoxClass : Card
{
    public FoxClass(){}
    public FoxClass(int Id, string Name, string Description) {
        id = Id;
        name = Name;
        description = Description;

        type = TypeEnum.Fox;
    }

    // Effet du renard
    // Supprime les composant image et event de la carte tué
    // Appel aussi la fonction update de la main du joueur visé
    //
    //Sait pas encore quel joueur on vise
    public void Kill(GameObject cardObject, Card card){
        // Vérifie si la gardienne protège la carte
        if(!card.Protected){
            Destroy(cardObject.transform.GetComponent<RawImage>());
            Destroy(cardObject.transform.GetComponent<EventTrigger>());

            UIManagerField playerVisual = GameObject.Find("PlayerVisual(Clone)").transform.GetComponent<UIManagerField>();
            playerVisual.UpdateHand(card, "dead");

            GameObject.Find("DeadCardManager").transform.GetComponent<DeadCardManager>().listDeadCards.Add(card);
        }
        else{
            Return("La carte sélectionnée n'a pas pu être attaqué");
        }
    }

    private void Return(string message){
        Debug.Log(message);
    }
}
