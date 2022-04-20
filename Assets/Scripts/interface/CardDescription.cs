using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = System.Random;

using static Step;
using static CardInGame;
using static MyExtensions;
using static Card;

public class CardDescription : MonoBehaviour
{
    #region Initialisation
    public GameObject playerVisual;
    public Interface interfaceScript;
    public DeadCardManager deadCards;
    public GameObject chooseButton;
    public GameObject playButton;
    public Step step;

    public GameObject targetPlace;
    public GameObject usedPlace;
    public Card card ;
    public Card target;

    private string spiritisteEffect;

    private enum WantedEnum
    {
        All,
        God,
        Fidele,
        SimpleFidele,
    }
    private WantedEnum targetWanted = WantedEnum.All;

    void Start(){
        ChangeButton(false, true);
        //HideDescription(); 
        
        // Initialise step
        step = GetStep(GetGameObject("Rappel"));
    }
    #endregion

    // Cache la section description
    public void HideDescription(){
        GetGameObject("CardDescription").SetActive(false);
    }

    #region Play a Card
    public void PlayCard(){
        // Repasser à l'étape de sélection du jeu
        step.NextStep();
        ChangeButton(false, true);
        HideDescription();

        // Vérifie si la carte est un renard ou un dieu
        if(card.Type == TypeEnum.God){ 
            GodClass god = ConvertGod(card);
            switch(god.Name == "La Spiritiste"?spiritisteEffect:god.Name){
                case "La Divinatrice" : 
                    god.DivinatriceEffect(target, targetPlace, usedPlace);
                    break;
                case "L'Enchanteresse" :
                    god.EnchanteresseEffect(target, targetPlace, usedPlace);
                    break;
                case "La Gardienne" : 
                    god.GardienneEffect(GetCardInGame(usedPlace).Level, GetCardInGame(targetPlace), interfaceScript.actualField);
                    break;
                case "L'Imitatrice" :
                    god.ImitatriceEffect();
                    break;
                case "Le Métamorphe" :
                    god.MetamorpheEffect(target, usedPlace, targetPlace, interfaceScript.actualField);
                    break;
                case "La Contagion" :
                    god.ContagionEffect();
                    break;
                case "La Spiritiste" :
                    Debug.Log("La Spiritiste n'a pas réussi à utiliser le pouvoir d'un défunt");
                    break;
                default :
                    god.MetamorpheEffect(target, usedPlace, targetPlace, interfaceScript.actualField);
                    break;
            }
        }
        else{
            card.Die(targetPlace, target, GetCardInGame(targetPlace), interfaceScript.actualField);
        }
    }

    // Lorsqu'une caret est joué, vérifie de quel type il est
    public void ChooseCard(){
        interfaceScript.ChangeField(1);
        HideDescription();
        GodClass god = ConvertGod(card);
        if(card.Name == "La Spiritiste"){
            spiritisteEffect = deadCards.GetRandomGod();
        }
        if(card.Name == "La Divinatrice" || spiritisteEffect == "La Divinatrice"){
            if(GetCardInGame(usedPlace).Level == 1){
                Random rand = new Random();
            }
        }else if(card.Name == "La Gardienne"){
            switch(GetCardInGame(usedPlace).Level){
                case 5:
                    targetWanted = WantedEnum.All;
                    break;
                case 2:
                    targetWanted = WantedEnum.Fidele;
                    break;
                case 1:
                    targetWanted = WantedEnum.SimpleFidele;
                    break;
                default :
                    targetWanted = WantedEnum.God;
                    break;
            }
        }
            // Change le texte du rappel de l'étape
            step.NextStep();
            ChangeButton(true, false);
    }
    #endregion

    private void ChangeButton(bool playBool, bool chooseBool){
        playButton.SetActive(playBool);
        chooseButton.SetActive(chooseBool);
    }
    public void SetValues(Card newCard, GameObject newCardPlace) {
        Debug.Log(GetCardInGame(newCardPlace).Protected);

        // A partir de l'id de la carte, on retrouver et associe son emplacement à cardInGame
        // En fonction de si c'est son village, associe la carte en tant que carte joué ou carte ciblé
        if(step.Etape != EtapeEnum.Target){
            card = newCard;
            usedPlace = newCardPlace;
            
            // Retire les boutons si c'est un vilageois lors de la phase de séléection de carte à jouer
            if(newCard.Type == TypeEnum.Fidele){
                ChangeButton(false, false);
            }else if(step.Etape == EtapeEnum.Target){
                ChangeButton(true, false);
            }
            else{
                ChangeButton(false, true);
            }
        }
        else{
            if(card.Name == "La Gardienne"){
                var canI = true;
                Debug.Log(targetWanted);
                //Vérifie que la carte est apte à être sélectionné (Gardienne)
                if(targetWanted != WantedEnum.All){
                    if (newCard.Type == TypeEnum.Fidele)
                    {
                        if (newCard.Name != "Le Fidèle")
                        {
                            if (targetWanted != WantedEnum.Fidele)
                            {
                                canI = false;
                            }
                        } 
                    }
                    else if (newCard.Type == TypeEnum.God)
                    {
                        if (targetWanted != WantedEnum.God)
                        {
                            canI = false;
                        }
                    }
                }

                if(canI == true){
                    ChangeButton(true, false);
                }
                else{
                    ChangeButton(false, false);
                }
            }

            target = newCard;
            targetPlace = newCardPlace;
        }

        // Sélectionne toutes les zones à éditer
        RawImage photo = GetImage(GetGameObject("Image"));
        RawImage protection = GetImage(GetGameObject("Protection"));
        TextMeshProUGUI name = GetText(GetGameObject("Name"));
        TextMeshProUGUI desc = GetText(GetGameObject("Description"));
        TextMeshProUGUI effect = GetText(GetGameObject("Effect"));
        TextMeshProUGUI disciple = GetText(GetGameObject("Disciple"));

        if(GetCardInGame(newCardPlace).Protected != Protection.Vulnerable){
            protection.color = new Color(protection.color.r, protection.color.g, protection.color.b, 1f);
        }else{
            protection.color = new Color(protection.color.r, protection.color.g, protection.color.b, 0f);
        }

        // Edite la description 
        photo.texture = Resources.Load("Images/" + newCard.Name)as Texture2D;
        name.text = newCard.Name;
        desc.text = newCard.Description;

        // Vérifie la carte peut utiliser son effet
        if(GetCardInGame(newCardPlace).Effect){
            // Seulement si c'est une carte Dieu
            if (newCard.Type == TypeEnum.God)
            {
                GodClass god = ConvertGod(newCard);
                // Vérifie le niveau actuel de la carte
                switch (GetCardInGame(newCardPlace).Level)
                {
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
                    default:
                        effect.text = card.Name + " ne peut pas utiliser son pouvoir";
                        ChangeButton(false, false);
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
        else
        {
            effect.text = card.Name + " ne peut pas utiliser son pouvoir";
            ChangeButton(false, false);
        }
    }
}
