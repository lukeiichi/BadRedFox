using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

using static MyExtensions;
using static Card;

public class CardInGame : MonoBehaviour
{
    public enum Protection{
        Vulnerable,
        Protected,
        OneTime
    }

    #region Initialisation
    // Card and position
    [SerializeField]
    public Card card;
    public GameObject cardPlace;

    // Values
    public bool effect;
    public Color color;
    public int level;
    public string name;
    public CardInGame god;

    // effets d'autres cartes
    public Protection isProtected = Protection.Vulnerable;
    public bool isInfected = false;

    // Others
    private GameObject descriptionCard;
    private GameObject cardFromEffect;
    #endregion

    #region Getters
    public Protection Protected{
        get {return isProtected;}
    }
    public CardInGame God{
        get{return god;}
    }
    public Card Card{
        get{return card;}
    }
    public GameObject CardPlace{
        get{return cardPlace;}
    }
    public bool Effect{
        get{return effect;}
    }
    public string Name{
        get{return name;}
    }
    public Color Color {
        get {return color;}
    }
    public int Level {
        get {return level;}
    }
    #endregion

    // Trouve et initialise descriptionCard au lancement du script
    void Start(){
        descriptionCard =  GameObject.Find("CardDescription");
        cardFromEffect =  GameObject.Find("CardFromEffect");
    }

    #region EditCard
    public void SetInfection(){
        
    }

    // Edite la protection de la carte
    public void SetProtection(Protection protection){
        isProtected = protection;
    }
    // Ajouter le minus
    // Modifie la couleur de la carte
    public void SetColor(Color coul, UIManagerField managerField){
        color = coul;
        
        // Associe le fidèle au bon Dieu de sa croyance
        if(coul == Color.white){
            god = null;
        }else if(card.Type == TypeEnum.Fidele){
            god = GetCardInGame(Array.Find(managerField.listCardsHand, x => GetCardInGame(x).Color == coul));
        }
    }

    // Modifie la couleur de la carte reçu en gris foncé et annule son effet
    public void SetEffect(bool newEffect){  
        if(newEffect == false){
            RawImage image = GetImage(cardPlace);
            image.color = new Color32(90,90,90,255);
            effect = newEffect;
        }
    }

    // Retire 1 point au niveau de la carte
    public void LevelMinus() {
        level --;
    }

    // Modifie les données de la carte (statut, type et niveau)
    public void SetValues(Card newCard, GameObject newCardPlace) {
        cardPlace = newCardPlace;
        card = newCard;

        name = card.Name;
        effect = true;
        if(card.Type == TypeEnum.God){
            GodClass god = card as GodClass;
            level = god.Level;
        }
    }
    #endregion

    #region DescriptionOnSelect
    // Fait apparaître la section Description et lui ajoute envoie les informations de la cartes
    public void AddDescription()
    {
        descriptionCard.SetActive(true);
        GetDescription(descriptionCard).SetValues(card, cardPlace);
    }

    // Fait disparaître la section Description
    public void RemoveDescription(){
        GameObject descriptionField = GameObject.Find("CardDescription");
        if(descriptionField){
            descriptionField.SetActive(false);
        }   
    }
    #endregion
}
