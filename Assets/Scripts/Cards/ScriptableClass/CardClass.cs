using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

using static MyExtensions;

public class Card : ScriptableObject {
    #region Initialisation
    public enum TypeEnum {
        God,
        Fidele,
        Fox
    }
    public enum Protection{
        Vulnerable,
        Protected,
        OneTime
    }

    public int id;
    public string name;
    [TextArea]
    public string description;
    public TypeEnum type;

    public bool effect = true;
    public Color color = Color.white;

    public Protection isProtected = Protection.Vulnerable;

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
    public Protection Protected{
        get {return isProtected;}
    }
    public int Id {
        get {return id;}
    }
    public string Name {
        get {return name;}
    }
    public string Description {
        get {return description;}
    }
    public bool Effect {
        get {return effect;}
    }
    public TypeEnum Type {
        get {return type;}
    }
    public Color Color {
        get {return color;}
    }
    #endregion

    // Edite la protection de la carte
    public void SetProtection(Protection protection){
        isProtected = protection;
    }
    // Edite la couleur de la carte 
    public void SetColor(Color coul) {
        color = coul;
    }
    
    // Modifie la couleur de la carte reçu en gris foncé
    public void SetEffect(bool newEffect, GameObject cardInGame){  
        if(newEffect == false){
            RawImage image = GetImage(cardInGame);
            image.color = new Color32(90,90,90,255);
        }
    }
        // Supprime de la main et du terrain, la carte donné en paramètre et l'ajoute à la liste des morts
        public void Die(GameObject cardObject, Card card){
        // Vérifie si la gardienne protège la carte
        if(card.Protected == Protection.Vulnerable){
            UIManagerField playerVisual = GetUIManager(GameObject.Find("PlayerVisual(Clone)"));
            if (card.Type == TypeEnum.Fidele){
                FideleClass fidele = card as FideleClass;
                GameObject godMinus = Array.Find(playerVisual.listCardsField, x => GetCardInGame(x).card.Name == fidele.God.Name);
                GetCardInGame(godMinus).LevelMinus();
            }
            Destroy(GetImage(cardObject));
            Destroy(GetEventTrigger(cardObject));
            playerVisual.UpdateHand(card, "dead");

            GetDeadCardManager(GameObject.Find("DeadCardManager")).listDeadCards.Add(card);
        }
        else{
            Debug.Log("La carte sélectionnée n'a pas pu être attaqué");
            if(card.Protected == Protection.OneTime){
                card.SetProtection(Protection.Vulnerable);
            }
        }
    }
}
