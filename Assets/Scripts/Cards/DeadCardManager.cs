using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using static Card;
using System;

public class DeadCardManager : MonoBehaviour
{
    public List<Card> listDeadCards = new List<Card>();

    // Choisit un Dieu au hasard dans la liste des défunt pour l'effet du spiritiste
    // Nla liste est toute le temps vide donc caca
    public string GetRandomCard(){
        List<Card> gods = listDeadCards.FindAll(delegate(Card x){return x.Type == TypeEnum.God;});
        if (listDeadCards.Count > 0 && gods.Count > 0){
            Random random = new Random();
            Card randomCard = listDeadCards[random.Next(listDeadCards.Count)];
            return randomCard.Name;
        }else{
            return "Aucune carte n'a été trouvé";
        }
           
    }
}
