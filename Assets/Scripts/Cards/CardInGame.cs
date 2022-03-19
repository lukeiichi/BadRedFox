using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Card;

/*
Pensez à enlever tout les trucs qui servent a rien
les donnes
statut
color
etc
..
..
..
..
*/

public class CardInGame : MonoBehaviour
{
    #region Initialisation
    public int id;
    [SerializeField]
    public Card card;

    public bool effect;
    public TypeEnum type;
    public Color color;
    public int level;

    private GameObject descriptionCard;
    #endregion

    #region Getters
    public int Id{
        get{return id;}
    }
    public bool Effect {
        get {return effect;}
    }
    public Card.TypeEnum Type {
        get {return type;}
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
    }

    #region EditCard
    // Modifie la couleur de la carte
    public void SetColor(Color coul){
        
        color = coul;
    }

    // Modifie les données de la carte (statut, type et niveau)
    public void SetValues(Card newCard, int i) {
        id = i;
        card = newCard;

        effect = card.Effect;
        type = card.Type;
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
        descriptionCard.transform.GetComponent<CardDescription>().SetValues(card, id);
    }

    // Fait disparaître la section Description
    public void RemoveDescription(){
        //descriptionCard.SetActive(false);
    }
    #endregion
}
