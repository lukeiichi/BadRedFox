using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Random = System.Random;

static class MyExtensions
{
    // Mélange une liste
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

    // Retourne un scrript bien précis à partir d'un GameObject
    public static CardInGame GetCardInGame(GameObject fromObject){
        return fromObject.transform.GetComponent<CardInGame>();
    }
    public static Card GetCard(CardInGame fromCard){
        return fromCard.transform.GetComponent<Card>();
    }
    public static CardDescription GetDescription(GameObject fromObject){
        return fromObject.transform.GetComponent<CardDescription>();
    }
    public static UIManagerField GetUIManager(GameObject fromObject){
        return fromObject.transform.GetComponent<UIManagerField>();
    }
    public static TextMeshProUGUI GetText(GameObject fromObject){
        return fromObject.transform.GetComponent<TextMeshProUGUI>();
    }
    public static RawImage GetImage(GameObject fromObject){
        return fromObject.transform.GetComponent<RawImage>();
    }
    public static Number GetNumber(GameObject fromObject){
        return fromObject.transform.GetComponent<Number>();
    }
    public static EventTrigger GetEventTrigger(GameObject fromObject){
        return fromObject.transform.GetComponent<EventTrigger>();
    }
    public static CardManager GetCardManager(GameObject fromObject){
        return fromObject.transform.GetComponent<CardManager>();
    }
    public static DeadCardManager GetDeadCardManager(GameObject fromObject){
        return fromObject.transform.GetComponent<DeadCardManager>();
    }
    public static Step GetStep(GameObject fromObject){
        return fromObject.transform.GetComponent<Step>();
    }

    // Convertisseur d'une carte en Type précis
    public static GodClass ConvertGod(Card card){
        return card as GodClass;
    }
    public static FideleClass ConvertFidele(Card card){
        return card as FideleClass;
    }
    public static FoxClass ConvertFox(Card card){
        return card as FoxClass;
    }

    //Ajouter les gameobject.Find + god as god
}
