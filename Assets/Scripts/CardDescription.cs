using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = System.Random;

using static MyExtensions;
using static Card;

public class CardDescription : MonoBehaviour
{
    #region Initialisation
    public GameObject playerVisual;
    public GameObject deadCards;
    public GameObject chooseButton;
    public GameObject playButton;
    public Step step;

    public GameObject cardInGame;
    public GameObject cardPlayedInGame;
    public Card card ;
    public Card target;
    private bool choose;

    void Start(){
        // Cache la description en entréé de jeu
        HideDescription();
        playButton.SetActive(false);

        // Initialise step
        step = GetStep(GameObject.Find("Rappel"));
    }
    #endregion

    // Cache la section description
    public void HideDescription(){
        GameObject.Find("CardDescription").SetActive(false);
    }

    #region Play a Card
    public void PlayCard(){
        // Repasser à l'étape de sélection du jeu
        step.isPlayer = !step.isPlayer;
        step.ChangeText("Choisis une carte à jouer");
        chooseButton.SetActive(true);
        playButton.SetActive(false);
        HideDescription();

        GodClass god = card as GodClass;
        switch(god.Name){
            case "La Divinatrice" : 
                god.DivinatriceEffect(target, cardInGame);
                break;
            case "L'Enchanteresse" :
                god.EnchanteresseEffect(target, cardInGame);
                break;
            case "La Gardienne" : 
                god.GardienneEffect(target);
                break;
            case "L'Imitatrice" :
                god.ImitatriceEffect();
                break;
            case "Le Métamorphe" :
                god.MetamorpheEffect(target, cardPlayedInGame, cardInGame);
                break;
            default :
                card.Die(cardInGame, target);
                break;
        }
        /*
                    GodClass god = card as GodClass;
            step.isPlayer = !step.isPlayer;*/
    }

    // Lorsqu'une caret est joué, vérifie de quel type il est
    public void ChooseCard(){
        HideDescription();
        if(card.Name == "La Divinatrice"){
            GodClass god = card as GodClass;
            if(god.Level == 1){
                Random rand = new Random();
            }
        }
            /*
            switch(god.Name){
                case "Divinatrice":
                    if(god.Level == 1){
                        choose = false;
                        step.ChangeText("Choisis un village à cibler");
                    }
                    break;
                case "Enchanteresse" :
                    Debug.Log(deadCards.transform.GetComponent<DeadCardManager>().GetRandomCard());
                    break;
                default:
                    break;
            }*/
            
            // Debug.Log(deadCards.transform.GetComponent<DeadCardManager>().GetRandomCard());

            // Change le texte du rappel de l'étape
            step.ChangeText("Choisis une carte à cibler");
                
            /* PLUS QUE PROVISOIRE !! */
            step.isPlayer = !step.isPlayer;
            chooseButton.SetActive(false);
            playButton.SetActive(true);
            /* PLUS QUE PORVISOIRE !! */
    }
    #endregion

    public void SetValues(Card newCard, int id) {
        // A partir de l'id de la carte, on retrouver et associe son emplacement à cardInGame
        // En fonction de si c'est son village, associe la carte en tant que carte joué ou carte ciblé
        GameObject newCardInGame = GetUIManager(playerVisual).listCardsField[id];
        if(step.isPlayer){
            card = newCard;
            cardPlayedInGame = newCardInGame;
            GodClass god = card as GodClass;
            Debug.Log(god.Level);
        }
        else{
            target = newCard;
            cardInGame = newCardInGame;
        }

        // Sélectionne toutes les zones à éditer
        RawImage photo = GetImage(GameObject.Find("Image"));
        TextMeshProUGUI name = GetText(GameObject.Find("Name"));
        TextMeshProUGUI desc = GetText(GameObject.Find("Description"));
        TextMeshProUGUI effect = GetText(GameObject.Find("Effect"));
        TextMeshProUGUI disciple = GetText(GameObject.Find("Disciple"));

        // Edite la description 
        photo.texture = Resources.Load("Images/" + newCard.Name)as Texture2D;
        name.text = newCard.Name;
        desc.text = newCard.Description;

        // Seulement si c'est une carte Dieu
        if(newCard.Type == TypeEnum.God){
                GodClass god = newCard as GodClass;
                // Vérifie le niveau actuel de la carte
                switch (god.Level){
                        case 1:
                        effect.text = god.Level1;
                        break;
                        case 2:
                        effect.text = god.Level2;
                        break;
                        case 3:
                        effect.text = god.Level3;
                        break;
                        case 4:
                        effect.text = god.Level4;
                        break;
                        case 5:
                        effect.text = god.Level5;
                        break;
                }
                disciple.text = god.Disciple;
        
        }
        // Si ce n'est pas un dieu, effacer le texte en trop
        else{
            disciple.text = "";
            effect.text = "";
        }
    }
}
