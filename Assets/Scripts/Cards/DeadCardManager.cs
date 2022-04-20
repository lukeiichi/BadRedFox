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
    public string GetRandomGod(){
        
        Debug.Log(listDeadCards +" count dead");
        List<Card> gods = listDeadCards.FindAll(delegate(Card x){return x.Type == TypeEnum.God;});
        Debug.Log(gods.Count);
        if (gods.Count > 0){
            Random random = new Random();
            Card randomCard = gods[random.Next(gods.Count)];
            return randomCard.Name;
        }else{
            return "La Spiritiste";
        } 
    }
}
