using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Random = System.Random;
using System.Threading.Tasks;
using System.Linq;
using Mirror;

public class UIManagerField : NetworkBehaviour
{
    public enum TypeEnum {
        God,
        Fidele,
        Fox
    }

    //Initialisation
    public GameObject[] listCardsField;
    public GameObject[] listCardsHand;

    private static readonly Random random = new Random();

    // Fonction au lancement qui distribue toute les cartes dont un village a besoin
    public void DrawCards()
    {
        #region Initialisation
        List < Card > playerCards = new List < Card > ();
        #endregion

        #region CardManager
        // Récupère toutes les cartes du CardManager
        CardManager cardManager = GameObject
            .Find("CardManager")
            .transform
            .GetComponent < CardManager > ();

        // Récupère la carte villageois, renard et les cartes dieux
        Card villageois = cardManager
            .cards
            .Find(x => x.Name == "Fidèle");
        Card renard = cardManager
            .cards
            .Find(x => x.Name == "Fox");
        List < Card > gods = cardManager
            .cards
            .FindAll(x => x.Type.ToString() == TypeEnum.God.ToString());
        #endregion

        #region DeckBuilding
        // Ajoute les dieux choisis au pif
        for (var i = 0; i < 4; i++) {
            PlayerManager.listCard.Add(GetGods(gods, i));
        }
        renard.SetColor(Color.white);

        // Ajoute la carte renard
        PlayerManager.listCard.Add(renard);

        // Ajoute des images aux cartes de la main autre que fidèle
        for (var i = 0; i < PlayerManager.listCard.Count; i++) {
            GetTextureCoroutine(PlayerManager.listCard[i], listCardsHand[i], PlayerManager.listCard[i].Color);
        }
        // Ajoute l'image et la texte du villageois dans la main
        GetTextureCoroutine(villageois, listCardsHand[5], Color.white);

        //Ajoute 20 cartes villageois
        for (var i = 0; i < 20; i++) {
            PlayerManager.listCard.Add(villageois);
        }

        PlayerManager.listCard.ShuffleCard();
        // Ajoute des images aux cartes du terrain
        List < Color > colorGod = new List < Color > ();
        for (var i = 0; i < 5; i++) {
            colorGod.Add(Color.red);
            colorGod.Add(Color.yellow);
            colorGod.Add(Color.green);
            colorGod.Add(Color.blue);
        }
        colorGod.ShuffleCard();
        for (var i = 0; i < 25; i++) {

            Color colorCard = Color.white;
            if (PlayerManager.listCard[i].Type == Card.TypeEnum.Fidele) {
                colorCard = colorGod.First();
                colorGod.Remove(colorGod.First());
            } else if (PlayerManager.listCard[i].Type == Card.TypeEnum.God) {
                colorCard = PlayerManager.listCard[i].Color;
            }
            GetTextureCoroutine(PlayerManager.listCard[i], listCardsField[i], colorCard);
        }
        #endregion
    }
    // Récupère 1 carte dieux et le supprime de la liste pour pas être pioché 2 fois
    private Card GetGods(List < Card > cards, int i) {
        List < Color > colors = new List < Color > () {Color.red, Color.green, Color.blue, Color.yellow};
        int number = (random.Next(cards.Count));

        Card card = cards[number];
        card.SetColor(colors[i]);
        cards.Remove(card);
        GameObject
            .Find("CardManager")
            .transform
            .GetComponent < CardManager > ()
            .cards
            .Remove(card);
        return card;
    }

    // Ajoute une image aux cartes créées url : L'url de l'image carte : La carte a
    // éditer
    private void GetTextureCoroutine(Card card, GameObject cardPlace, Color colorCard) {
        CardInGame cardInGame = cardPlace.transform.GetComponent < CardInGame > ();
        cardInGame.SetValues(card);
        cardInGame.SetColor(colorCard);

        RawImage rawImage = cardPlace.transform.GetComponent < RawImage > ();
        rawImage.color = cardInGame.Color;

        rawImage.texture = Resources.Load("Images/" + cardInGame.card.Name)as Texture2D;
    }
    
}

#region Extensions
// Mélange une liste
static class MyExtensions
{
    private static readonly Random random = new Random();
    public static void ShuffleCard < T > (this IList < T > list)
    {
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
#endregion