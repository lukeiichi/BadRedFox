using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

using static MyExtensions;
using static Card;

public class CardInGame : MonoBehaviour
{
    #region Initialisation
    [SerializeField]
    public Card card;
    public GameObject cardPlace;

    public Color color;
    public int level;
    public string name;
    public CardInGame god;

    private GameObject descriptionCard;
    private GameObject cardFromEffect;
    #endregion

    #region Getters
    public CardInGame God{
        get{return god;}
    }
    public GameObject CardPlace{
        get{return cardPlace;}
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
        descriptionCard =  GetGameObject("CardDescription");
        cardFromEffect =  GetGameObject("CardFromEffect");
    }

    #region EditCard
    // Modifie la couleur de la carte
    public void SetColor(Color coul){
        color = coul;

        // Associe le fidèle au bon Dieu de sa croyance
        if(coul == Color.white){
            god = null;
        }else if(card.Type == TypeEnum.Fidele){
            god = GetCardInGame(Array.Find(GetUIManager(GetGameObject("PlayerVisual(Clone)")).listCardsHand, x => GetCardInGame(x).Color == coul));
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
        if(card.Type == TypeEnum.God){
            GodClass god = ConvertGod(card);
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
        GameObject descriptionField = GetGameObject("CardDescription");
        if(descriptionField){
            descriptionField.SetActive(false);
        }   
    }
    #endregion
}
