using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

using static CardInGame;
using static MyExtensions;

public class Card : ScriptableObject {
    #region Initialisation
    public enum TypeEnum {
        God,
        Fidele,
        Fox
    }

    public int id;
    public string name;
    [TextArea]
    public string description;
    public TypeEnum type;
    public Color color = Color.white;

    /* Initialisation de la carte
    Fonction qui donne toutes les informations à la carte
    */
    public Card(){}
    public Card(int Id, string Name, string Description, TypeEnum Type) {
        id = Id;
        name = Name;
        description = Description;
        type = Type;
    }
    #endregion

    #region Getters
    public int Id {
        get {return id;}
    }
    public string Name {
        get {return name;}
    }
    public string Description {
        get {return description;}
    }
    public TypeEnum Type {
        get {return type;}
    }
    public Color Color {
        get {return color;}
    }
    #endregion

    // Edite la couleur de la carte 
    public void SetColor(Color coul) {
        color = coul;
    }

    // Supprime de la main et du terrain, la carte donné en paramètre et l'ajoute à la liste des morts
    public void Die(GameObject cardObject, Card card, CardInGame cardPlace){
        // Vérifie si la gardienne protège la carte
        if(cardPlace.Protected == Protection.Vulnerable){
            UIManagerField playerVisual = GetUIManager(GetGameObject("PlayerVisual(Clone)"));
            if (card.Type == TypeEnum.Fidele){
                FideleClass fidele = ConvertFidele(card);
                GameObject godMinus = Array.Find(playerVisual.listCardsField, x => GetCardInGame(x).Name == cardPlace.God.Name);
                
                // Retire un niveau au dieu du croyant
                GetCardInGame(godMinus).LevelMinus();
                playerVisual.UpdateHand(GetCardInGame(godMinus).card, "levelMinus", null);
            }
            // Retire l'image de la carte
            Destroy(GetImage(cardObject));
            Destroy(GetEventTrigger(cardObject));
            playerVisual.UpdateHand(card, "dead", null);

            // Ajoute la carte à la liste des morts
            GetDeadCardManager(GetGameObject("DeadCardManager")).listDeadCards.Add(card);
        }
        else{
            Debug.Log("La carte sélectionnée n'a pas pu être attaqué");
            // Retire le statut de protection s'il ne marchait qu'une seule fois
            if(cardPlace.Protected == Protection.OneTime){
                cardPlace.SetProtection(Protection.Vulnerable);
            }
        }
    }
}
