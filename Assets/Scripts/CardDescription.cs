using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Card;

public class CardDescription : MonoBehaviour
{
    #region Initialisation
    public GameObject cardInGame;
    public GameObject playerVisual;
    public GameObject deadCards;

    public Card card ;
    public Card target;
    public Step step;
    private bool choose;
    private GameObject ciblerButton;

    void Start(){
        // Cache la description en entréé de jeu
        GameObject.Find("CardDescription").SetActive(false);
        ciblerButton = GameObject.Find("CiblerButton");
        ciblerButton.SetActive(false);

        // Initialise step
        step = GameObject
            .Find("Rappel")
            .transform
            .GetComponent < Step > ();
    }
    #endregion

    #region Play a Card
    // Lorsqu'une caret est joué, vérifie de quel type il est
    public void ChooseCard(){
        GameObject.Find("CardDescription").SetActive(false);
        if(step.isPlayer){  
            GodClass god = card as GodClass;
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
            GameObject.Find("PlayerHand").SetActive(false);
            ciblerButton.SetActive(true);
            /* PLUS QUE PORVISOIRE !! */
        }
        else{
            GodClass god = card as GodClass;
            step.isPlayer = !step.isPlayer;
        }

            /*
        switch (card.Type){
            case TypeEnum.God:
                PlayGodEffect();
                break;
            case TypeEnum.Fox:
                PlayFoxEffect();
                break;
        }*/
    }
    #endregion

    public void SetValues(Card newCard, int id) {
        // A partir de l'id de la carte, on retrouver et associe son emplacement à cardInGame
        // En fonction de si c'est son village, associe la carte en tant que carte joué ou carte ciblé
        if(step.isPlayer){
            card = newCard;
        }
        else{
            target = newCard;
            cardInGame = playerVisual.transform.GetComponent<UIManagerField>().listCardsField[id];
        }

        // Sélectionne toutes les zones à éditer
        RawImage photo = GameObject
            .Find("Image")
            .transform
            .GetComponent < RawImage > ();
        Text name = GameObject
            .Find("Name")
            .transform
            .GetComponent < Text > ();
        TextMeshProUGUI desc = GameObject
            .Find("Description")
            .transform
            .GetComponent < TextMeshProUGUI > ();
        TextMeshProUGUI effect = GameObject
            .Find("Effect")
            .transform
            .GetComponent < TextMeshProUGUI > ();
        TextMeshProUGUI disciple = GameObject
            .Find("Disciple")
            .transform
            .GetComponent < TextMeshProUGUI > ();

        // Edite la description 
        photo.texture = Resources.Load("Images/" + card.Name)as Texture2D;
        name.text = card.Name;
        desc.text = card.Description;

        // Seulement si c'est une carte Dieu
        if(card.Type == TypeEnum.God){
                GodClass god = card as GodClass;
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
