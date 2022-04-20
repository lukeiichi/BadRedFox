using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using Random = System.Random;
using System.Threading.Tasks;
using System.Linq;
using Mirror;

using static Card;
using static MyExtensions;

public class UIManagerField : NetworkBehaviour
{
    //Initialisation
    public GameObject[] listCardsField;
    public List<GameObject> listCardsHand = new List<GameObject>();
    public GameObject playerVisual;
    public List<Card> playerCards = new List<Card>();

    private static readonly Random random = new Random();
    private List < Color > listColors = new List < Color > () {new Color(1f,0.5f,0.5f,1f), new Color(0.5f,1f,0.5f,1f), new Color(0.5f,0.5f,1f,1f), new Color(0.9f,1f,0.5f,1f)};

    // Modifie l'état des cartes situé dans la main ou le terrain du joueur
    public void UpdateHand(Card updatedCard, string reason, Card target){
        GameObject cardFoundField = Array.Find(listCardsField, x => GetCardInGame(x).Name == updatedCard.Name);
        GameObject cardFoundHand = listCardsHand.Find(x => GetCardInGame(x).Name == updatedCard.Name);
        switch (reason){
            case "dead":
                if (updatedCard.Type == TypeEnum.Fidele){
                    GetNumber(cardFoundHand).Kill();
                }else{
                    Destroy(GetImage(cardFoundHand));
                    Destroy(GetEventTrigger(cardFoundHand));
                }
                break;
            case "levelMinus":
                GetCardInGame(cardFoundField).LevelMinus();
                break;
            case "effect":
                GetCardInGame(cardFoundHand).SetEffect(false);
                break;
            default:
                GetCardInGame(cardFoundField).card = target;
                GetImage(cardFoundField).texture = Resources.Load("Images/" + target.Name) as Texture2D;
                break;
        }
    }
    
    // Fonction au lancement qui distribue toute les cartes dont un village
    public void CmdDrawCards(bool isPlayer)
    {
        // Définit la liste de cartes dans la main du joueur
        Transform playerHand = GetGameObject("PlayerHand").transform;
        for (var i = 0; i < playerHand.transform.childCount; i++){
            listCardsHand.Add(playerHand.GetChild(i).gameObject);
        }

        #region CardManager
        // Récupère toutes les cartes du CardManager
        CardManager cardManager = GetCardManager(GetGameObject("CardManager"));

        // Récupère la carte villageois, renard et les cartes dieux
        Card villageois = cardManager
            .cards
            .Find(x => x.Name == "Le Fidèle");
        Card renard = cardManager
            .cards
            .Find(x => x.Name == "L'Apprenti");
        List < Card > gods = cardManager
            .cards
            .FindAll(x => x.Type.ToString() == TypeEnum.God.ToString());
        #endregion

        #region DeckBuilding
        // Ajoute les dieux choisis au pif
        for (var i = 0; i < 4; i++) {
            playerCards.Add(GetGods(gods, i));
        }
        renard.SetColor(Color.white);

        // Ajoute la carte renard
        playerCards.Add(renard);

        // Ajoute des images aux cartes de la main autre que fidèle
        if(isPlayer == true){
            for (var i = 0; i < playerCards.Count; i++) {
                
                    GetTextureCoroutine(playerCards[i], listCardsHand[i], playerCards[i].Color, true); 
            }
            // Ajoute l'image et la texte du villageois dans la main
            GetTextureCoroutine(villageois, listCardsHand[5], Color.white, true);
        }

        //Ajoute 20 cartes villageois
        for (var i = 0; i < 20; i++) {
            playerCards.Add(villageois);
        }

        playerCards.ShuffleCard();
        // Ajoute des images aux cartes du terrain
        List < Color > colorGod = new List < Color > ();
        for (var i = 0; i < 5; i++) {
            colorGod.Add(new Color(1f,0.5f,0.5f,1f));
            colorGod.Add(new Color(0.5f,0.5f,1f,1f));
            colorGod.Add(new Color(0.5f,1f,0.5f,1f));
            colorGod.Add(new Color(0.9f,1f,0.5f,1f));
        }
        colorGod.ShuffleCard();
        for (var i = 0; i < 25; i++) {

            Color colorCard = Color.white;
            if (playerCards[i].Type == Card.TypeEnum.Fidele) {
                colorCard = colorGod.First();
                colorGod.Remove(colorGod.First());
            } else if (playerCards[i].Type == Card.TypeEnum.God) {
                colorCard = playerCards[i].Color;
            }
            GetTextureCoroutine(playerCards[i], listCardsField[i], colorCard, isPlayer);
        }
        #endregion
    }
    // Récupère 1 carte dieux et le supprime de la liste pour pas être pioché 2 fois
    private Card GetGods(List < Card > cards, int i) {
        
        int number = (random.Next(cards.Count));

        Card card = cards[number];
        card.SetColor(listColors[i]);
        cards.Remove(card);
        GetCardManager(GetGameObject("CardManager")).cards.Remove(card);
        return card;
    }

    // Ajoute une image aux cartes créées et les associe à un emplacement
    private void GetTextureCoroutine(Card card, GameObject cardPlace, Color colorCard, bool isPlayer) {
        // Définit les données et la couleur de cardInGame
        CardInGame cardInGame = GetCardInGame(cardPlace);
        cardInGame.SetValues(card, cardPlace);
        cardInGame.SetColor(colorCard, listCardsHand);

        if (isPlayer == true){
            RawImage rawImage = GetImage(cardPlace);
            // Modifie la couleur et l'image du GameObject
            rawImage.color = cardInGame.Color;
            rawImage.texture = Resources.Load("Images/" + cardInGame.card.Name) as Texture2D;
        }
    }
}
